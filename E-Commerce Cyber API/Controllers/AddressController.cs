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
    public IActionResult GetAll()
    {
        var allAddresses = _addressService.GetAll();
        return Ok(allAddresses);
    }

    [HttpGet("GetAddressById/{id}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public IActionResult Get(int id)
    {
        var address = _addressService.GetById(id);
        return Ok(address);
    }

    [HttpGet("GetUserAddresses")]
    [Authorize]
    public IActionResult GetUserAddresses()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if(userId == null) return Unauthorized("You are not logged in");
        var addresses = _addressService.GetUserAddresses(int.Parse(userId));
        return Ok(addresses);
    }

    [HttpPost("AddAddress")]
    [Authorize]
    public IActionResult AddAddress([FromBody] AddAddressDto address)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");
        _addressService.AddAddress(address, int.Parse(userId));
        return Ok("Successfully added new address");
    }

    [HttpDelete("DeleteAddress/{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) return Unauthorized("You are not logged in");
        _addressService.Delete(int.Parse(userId), id);
        return Ok("Successfully deleted address");
    }
}
