using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OLMS.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OLMS.Application.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;
    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(UserBase user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName.Value),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var now = DateTime.UtcNow.AddHours(7);
        var expires = now.AddMinutes(_jwtOptions.ExpirationMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now,
            Expires = expires,
            Issuer = _jwtOptions.Issuer,    
            Audience = _jwtOptions.Audience,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
