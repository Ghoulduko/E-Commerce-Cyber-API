using Cyber.Application.Dtos.User;
using Cyber.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("Login")]
    public string Login(LoginUserDto request)
    {
        var jwtKey = _authService.Login(request);
        return jwtKey;
    }
}
