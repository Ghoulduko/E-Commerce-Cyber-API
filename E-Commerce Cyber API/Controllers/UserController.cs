using Cyber.Application.Dtos.User;
using Cyber.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("SignUp")]
    public IActionResult Add([FromBody] AddUserDto user)
    {
        var token = _userService.AddUser(user);
        return Ok(new { token });

    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("GetProfile")]
    [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");
        var user = _userService.GetUserById(int.Parse(userId));
        return Ok(user);
    }

    [HttpGet("GetById/{id}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null) return NotFound("User not found");
        return Ok(user);
    }

    [HttpGet("GetByEmail/{email}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public IActionResult GetByEmail(string email)
    {
        var user = _userService.GetUserByEmail(email);
        if (user == null) return NotFound("User not found");
        return Ok(user);
    }

    [HttpPatch("UpdatePassword")]
    [Authorize]
    public IActionResult Update([FromBody] UpdateUserPasswordDto req)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");

        _userService.UpdatePassword(int.Parse(userId), req);
        return Ok("Password updated successfully");
    }

    [HttpDelete("DeleteAccount")]
    [Authorize]
    public IActionResult DeleteAccount([FromBody] DeleteUserDto req)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");

        _userService.DeleteAccount(int.Parse(userId),req);
        return Ok("Account deleted successfully");
    }
}
