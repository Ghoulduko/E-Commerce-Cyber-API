using Cyber.Application.Dtos.Role;
using Cyber.Application.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "SUPERADMIN")]
    public async Task<IActionResult> Add(AddRoleDto request)
    {
        await _roleService.AddRole(request);
        return Ok("Role added successfully");
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "SUPERADMIN")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _roleService.GetAllRoles());
    }

    [HttpGet("GetRoleByName")]
    [Authorize(Roles = "SUPERADMIN")]
    public async Task<IActionResult> Get(string name)
    {
        var role = await _roleService.GetRoleByName(name);
        return Ok(role);
    }

    [HttpDelete]
    [Authorize(Roles = "SUPERADMIN")]
    public async Task<IActionResult> Delete(string name)
    {
        await _roleService.DeleteRole(name);
        return Ok("Deleted role successfully!");
    }
}
