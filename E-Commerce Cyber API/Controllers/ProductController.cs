using AutoMapper;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Services;
using Cyber.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(ProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet("AllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpPost("PaginatedProducts")]
    public async Task<IActionResult> GetPaginatedProducts([FromQuery] int page, ProductFilterDto productFilter)
    {
        var products = await _productService.PaginatedProducts(page, productFilter);
        return Ok(products);
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

    [HttpGet("ProductsByContentType/{type}")]
    public async Task<IActionResult> GetProducts(ContentType type)
    {
        var products = await _productService.GetProductsByContentType(type);
        return Ok(products);
    }

    [HttpGet("ProductsById/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        return Ok(product);
    }
}
