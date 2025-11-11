using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        );
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition(
        "FootballFinderApiBearerAuth",
        new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
        {
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            Description = "Acá pegar el token generado al loguearse.",
        }
    );

    setupAction.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "FootballFinderApiBearerAuth",
                    }, //Tiene que coincidir con el id seteado arriba en la definición
                },
                new List<string>()
            },
        }
    );
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder
    .Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
            ),
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(DbContextOptions =>
{
    DbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IParticipationRepository, ParticipationRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.Configure<AutenticacionServiceOptions>(
    builder.Configuration.GetSection("Authentication")
);
builder.Services.AddScoped<IAuthSecurity, AuthSecurity>();
builder.Services.AddScoped<IParticipationService, ParticipationService>();
#endregion

builder.Services.AddHttpClient(
    "ProvinceHttpCLient",
    client =>
    {
        client.BaseAddress = new Uri("https://apis.datos.gob.ar/georef/api/");
    }
);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "API corriendo");

app.Run();
