using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class AuthSecurity : IAuthSecurity
{
    private readonly AutenticacionServiceOptions _options;

    public AuthSecurity(IOptions<AutenticacionServiceOptions> options)
    {
        _options = options.Value;
    }

    public string GeneraToken(User user)
    {
        var securityPassword = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_options.SecretForKey)
        );

        var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();

        claimsForToken.Add(new Claim("email", user.Email));
        claimsForToken.Add(new Claim("role", ((int)user.Role).ToString()));
        claimsForToken.Add(new Claim("id", user.Id.ToString()));
        claimsForToken.Add(new Claim("username", user.Name));

        var jwtSecurityToken = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signature
        );

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return tokenToReturn.ToString();
    }
}

public class AutenticacionServiceOptions
{
    public const string AutenticacionService = "Authentication";

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretForKey { get; set; }
}
