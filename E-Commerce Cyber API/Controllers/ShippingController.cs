using Cyber.Application.Interfaces;
using Cyber.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingController : Controller
{
    private readonly IShippingService _shippingService;

    public ShippingController(IShippingService shippingService)
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

    [HttpPost("UpdateShipping")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> UpdateStatus(int shippingId, ShippingStatus shippingStatus)
    {
        await _shippingService.UpdateStatus(shippingId, shippingStatus);
        return Ok(new {message = "Shipping Status updated successfully" });
    }

    [HttpDelete("DeleteShipping/${shippingId}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN")]
    public async Task<IActionResult> DeleteShipping(int shippingId)
    {
        await _shippingService.DeleteShipping(shippingId);
        return Ok();
    }
}