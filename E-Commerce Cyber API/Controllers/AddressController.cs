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
    public IActionResult GetAll()
    {
        var allAddresses = _addressService.GetAll();
        return Ok(allAddresses);
    }

    [HttpGet("GetAddressById")]
    public IActionResult Get(int id)
    {
        var address = _addressService.GetById(id);
        return Ok(address);
    }
    
    [HttpPost("AddAddress")]
    public IActionResult AddAddress(AddAddressDto address)
    {
        //var userId = User.FindFirst("UserId")?.Value;
        //if (userId == null) throw new ArgumentException("You are not logged in");
        _addressService.AddAddress(address);
        return Ok("Successfully added new address");
    }

    [HttpDelete("DeleteAddress")]
    public IActionResult Delete(int id)
    {
        _addressService.Delete(id);
        return Ok("Successfully deleted address");
    }
}
