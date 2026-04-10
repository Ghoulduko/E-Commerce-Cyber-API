using Cyber.Application.Dtos.Shipping;
using Cyber.Application.Services;
using Cyber.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingController : Controller
{
    private readonly ShippingService _shippingService;

    public ShippingController(ShippingService shippingService)
    {
        _shippingService = shippingService;
    }

    [HttpGet("GetAllShippings")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> GetAllShippings()
    {
        var shippings = await _shippingService.GetAllShippings();
        return Ok(shippings);
    }

    [HttpGet("GetShippingById/${shippingId}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> GetShippingById(int shippingId)
    {
        var shipping = await _shippingService.GetShippingById(shippingId);
        return Ok(shipping);
    }

    [HttpGet("GetUserShipping")]
    public async Task<IActionResult> GetUserShipping()
    {
        var userId = User.FindFirst("UserId").Value ?? throw new InvalidOperationException("You need to log in first");
        var userShippings = await _shippingService.GetUserShippings(int.Parse(userId));
        return Ok(userShippings);
    }
    
    [HttpPost("AddShipping")]
    [Authorize]
    public async Task<IActionResult> AddShipping(int addressId)
    {
        var userId = User.FindFirst("UserId").Value ?? throw new InvalidOperationException("You need to log in first");
        
        await _shippingService.AddShipping(addressId,int.Parse(userId));
        return Ok(new {message = "Successfully added shipping"});
    }

    [HttpDelete("DeleteShipping/${shippingId}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> DeleteShipping(int shippingId)
    {
        await _shippingService.DeleteShipping(shippingId);
        return Ok();
    }
}