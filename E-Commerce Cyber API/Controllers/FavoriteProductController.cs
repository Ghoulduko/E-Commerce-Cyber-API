using AutoMapper;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoriteProductController : Controller
{
    private readonly IFavoriteProductService _productService;
    private readonly IMapper _mapper;
    
    public FavoriteProductController(IFavoriteProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    
    [HttpPost("AddToFavorites")]
    public async Task<IActionResult> AddProductToFavorites(AddFavoriteProductDto favoriteProduct)
    {
        var userId = User.FindFirst("UserId").Value ?? throw new InvalidOperationException("You need to login first");
        var productToAdd = _mapper.Map<FavoriteProductDto>(favoriteProduct);
        productToAdd.UserId = int.Parse(userId);
        await _productService.AddProductToFavorites(productToAdd);
        return Ok(new {message = "Product added successfully"});
    }

    [HttpGet("GetFavoriteProducts")]
    public async Task<IActionResult> GetFavoriteProducts()
    {
        var userId = User.FindFirst("UserId").Value ?? throw new Exception("You need to login first");
        var products = await _productService.GetAllFavoritedProducts(int.Parse(userId));
        return Ok(products);
    }

    [HttpDelete("DeleteProductFromFavorites")]
    public async Task<IActionResult> DeleteProductFromFavorites(int id)
    {
        var userId = User.FindFirst("UserId").Value ??  throw new InvalidOperationException("You need to login first");
        await _productService.DeleteProductFromFavorites(id, int.Parse(userId));
        return Ok(new {message = "Product deleted successfully"});
    }
}