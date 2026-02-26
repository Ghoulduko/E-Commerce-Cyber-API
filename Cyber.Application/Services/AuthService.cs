using Cyber.Application.Cache;
using Cyber.Application.Dtos.User;
using Cyber.Core.Database;
using Cyber.Core.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Services;

public class AuthService
{
    private readonly CyberDbContext _context;
    private readonly CacheService _cacheService;
    private readonly EmailService _emailService;
    private readonly IConfiguration _configuration;

    public AuthService(CyberDbContext context, CacheService cacheService, EmailService emailService, IConfiguration configuration)
    {
        _context = context;
        _cacheService = cacheService;
        _emailService = emailService;
        _configuration = configuration;
    }

    public string Login(LoginUserDto request)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);
        if (user == null) throw new ArgumentException("No account found with the provided email");

        if (BC.EnhancedVerify(request.Password, user.Password)) throw new ArgumentException("The password is wrong, try again");

        var claims = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("SignInTime", DateTime.Now.ToString()),
            new Claim("RoleId", user.RoleId.ToString())
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
