using AutoMapper;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Interfaces;
using Cyber.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
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
