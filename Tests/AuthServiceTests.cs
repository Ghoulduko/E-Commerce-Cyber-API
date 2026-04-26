
using Cyber.Application.Dtos.User;
using Cyber.Application.Interfaces;
using Cyber.Application.Services;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly CyberDbContext _context;

    public AuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<CyberDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
       _context = new CyberDbContext(options);
       
       // Fake dependencies with Moq
       _mockTokenService = new Mock<ITokenService>();
       _mockEmailService = new Mock<IEmailService>();

       // Create the real AuthService with fake dependencies
       _authService = new AuthService(
           _context,
           _mockTokenService.Object,
           _mockEmailService.Object
       );
       
       var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword("Password123");
       _context.Users.Add(new User
       {
           Id = 1,
           Name = "TestUser",
           Email = "test@cyber.ge",
           Password = hashedPassword,
           Role = new Role { RoleName = "User" }
       });
       _context.SaveChanges();
    }
    
    [Fact]
    public async Task Login_ShouldReturnToken_WhenCredentialsAreCorrect()
    {
        _mockTokenService
            .Setup(t => t.CreateToken(It.IsAny<User>()))
            .Returns("fake-jwt-token");
        
        var result = await _authService.Login(new LoginUserDto
        {
            Email = "test@cyber.ge",
            Password = "Password123"
        });

        Assert.Equal("fake-jwt-token", result);
    }

    [Fact]
    public async Task Login_ShouldThrowException_WhenEmailDoesntExist()
    {
        var request = new LoginUserDto
        {
            Email = "test@cyberTengo.ge",
            Password = "Password123"
        };
        await Assert.ThrowsAsync<ArgumentException>(() => _authService.Login(request));
    }
    
    [Fact]
    public async Task Login_ShouldThrowException_WhenPasswordIsWrong()
    {
        var request = new LoginUserDto
        {
            Email = "test@cyber.ge",
            Password = "paswadaswordewa123524"
        };
        await Assert.ThrowsAsync<ArgumentException>(() => _authService.Login(request));
    }

    [Fact]
    public async Task Login_ShouldCallCreateToken_WhenCredentialsAreCorrect()
    {
        _mockTokenService
            .Setup(t => t.CreateToken(It.IsAny<User>()))
            .Returns("fake-jwt-token");
        
        var request = new LoginUserDto
        {
            Email = "test@cyber.ge",
            Password = "Password123"
        };
        
        await _authService.Login(request);
        
        _mockTokenService.Verify(t => t.CreateToken(It.IsAny<User>()), Times.Once);
    }
}