using Cyber.Application.Cache;
using Cyber.Application.Dtos.User;
using Cyber.Core.Database;
using Cyber.Core.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cyber.Application.Interfaces;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class AuthService : IAuthService
{
    private readonly CyberDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public AuthService(CyberDbContext context, ITokenService tokenService, IEmailService emailService)
    {
        _context = context;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<string> Login(LoginUserDto request)
    {
        var user = await _context.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) throw new ArgumentException("No account found with the provided email");

        if (!BC.EnhancedVerify(request.Password, user.Password)) throw new ArgumentException("The password is wrong, try again");
        
        var token = _tokenService.CreateToken(user) ?? throw new ArgumentException("The token is wrong");
        
        _emailService.SendEmail(request.Email);
        
        return token;
    }
}
