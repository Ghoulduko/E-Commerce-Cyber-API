using Cyber.Application.Dtos.Role;
using Cyber.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("AddRole")]
    public IActionResult Add(AddRoleDto request)
    {
        _roleService.AddRole(request);
        return Ok("Role added successfully");
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_roleService.GetAllRoles());
    }

    [HttpGet("GetRoleByName")]
    public IActionResult Get(string name)
    {
        var role = _roleService.GetRoleByName(name);
        return Ok(role);
    }

    [HttpDelete]
    public IActionResult Delete(string name)
    {
        _roleService.DeleteRole(name);
        return Ok("Deleted role successfully!");
    }
}
