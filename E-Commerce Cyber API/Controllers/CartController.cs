using Cyber.Application.Dtos.Cart;
using Cyber.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("GetAllCarts")]
    [Authorize(Roles = "SUPERADMIN")]
    public async Task<IActionResult> GetAllCarts()
    {
        var carts = await _cartService.GetAllCarts();
        return Ok(carts);
    }

    [HttpGet("GetUserCart")]
    [Authorize]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
            return BadRequest("You need to log in to use Cart.");

        var cartItems = await _cartService.GetCart(int.Parse(userId));
        return Ok(cartItems);
    }

    [HttpPost("AddToCart")]
    [Authorize]
    public async Task<IActionResult> AddToCart([FromBody] AddItemToCartDto req)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
            return BadRequest("You need to log in to add items to cart.");

        await _cartService.AddToCart(int.Parse(userId), req);
        return Ok(new {message = "Item added to cart successfully." });
    }

    [HttpPut("QuantityUpdate")]
    [Authorize]
    public async Task<IActionResult> UpdateCartItemQuantity([FromBody] UpdateCartItemQuantityDto req)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
            return BadRequest("You need to log in to update cart items.");

        await _cartService.UpdateQuantity(int.Parse(userId), req);
        return Ok(new {message = "Cart item quantity updated successfully." });
    }

    [HttpDelete("RemoveFromCart/{id}")]
    [Authorize]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
            return BadRequest("error occured. no user found, try logging in");

        await _cartService.RemoveFromCart(int.Parse(userId), id);
        return Ok(new {message = "Item removed from cart successfully." });
    }
}
