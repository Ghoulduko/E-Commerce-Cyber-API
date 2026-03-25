using Cyber.Application.Dtos.Address;
using Cyber.Application.Services;
using Cyber.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> GetAll()
    {
        var allAddresses = await _addressService.GetAll();
        return Ok(allAddresses);
    }

    [HttpGet("GetAddressById/{id}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> Get(int id)
    {
        var address = await _addressService.GetById(id);
        return Ok(address);
    }

    [HttpGet("GetUserAddresses")]
    [Authorize]
    public async Task<IActionResult> GetUserAddresses()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if(userId == null) return Unauthorized("You are not logged in");
        var addresses = await _addressService.GetUserAddresses(int.Parse(userId));
        return Ok(addresses);
    }

    [HttpPost("AddAddress")]
    [Authorize]
    public async Task<IActionResult> AddAddress([FromBody] AddAddressDto address)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");
        await _addressService.AddAddress(address, int.Parse(userId));
        return Ok("Successfully added new address");
    }

    [HttpDelete("DeleteAddress/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");
        await _addressService.Delete(int.Parse(userId), id);
        return Ok("Successfully deleted address");
    }
}
