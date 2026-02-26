using Cyber.Application.Dtos.User;
using Cyber.Application.Services;
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

    [HttpPost("AddUser")]
    public IActionResult Add(AddUserDto user)
    {
        _userService.AddUser(user);
        return Ok("User added successfully!");
    }

    [HttpGet("GetAll")]
    public List<UserDto> GetUsers()
    {
        return _userService.GetAll();
    }
}
