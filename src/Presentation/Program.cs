using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Security;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

var connection = new SqliteConnection("Data Source=football-finder.db");
connection.Open();

using (var comman = connection.CreateCommand())
{
    comman.CommandText = "PRAGMA jorunal_mode = DELETE;";
    comman.ExecuteNonQuery();
}
builder.Services.AddDbContext<ApplicationDbContext>(DbContextOptions =>
{
    DbContextOptions.UseSqlite(connection);
});

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.Configure<AutenticacionServiceOptions>(
    builder.Configuration.GetSection("AuthenticationService")
);
builder.Services.AddScoped<IAuthSecurity, AuthSecurity>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
