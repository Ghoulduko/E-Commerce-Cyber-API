using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cyber.Application.Interfaces;
using Cyber.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cyber.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("SignInTime", DateTime.Now.ToString()),
            new Claim(ClaimTypes.Role, user.Role!.RoleName.ToString())
        };

        var jwtSecretKey = _configuration["JwtSecretKey"] ?? throw new ArgumentNullException("No jwt secret key found");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://ltdluka.ge/",
            audience: "https://ltdluka.ge/",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token).ToString();
    }
}