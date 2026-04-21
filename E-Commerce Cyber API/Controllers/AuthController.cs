using Cyber.Application.Dtos.User;
using Cyber.Application.Interfaces;
using Cyber.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        var token = await _authService.Login(request);
        return Ok(new { token });
    }
}
