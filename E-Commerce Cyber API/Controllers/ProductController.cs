using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Services;
using Cyber.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("AllProducts")]
    public IActionResult GetAllProducts()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("ProductsByContentType/{type}")]
    public IActionResult GetProducts(ContentType type)
    {
        var products = _productService.GetProductsByContentType(type);
        return Ok(products);
    }

    [HttpGet("ProductsById/{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productService.GetProductById(id);
        return Ok(product);
    }

    [HttpPost("FilteredProducts")]
    public IActionResult GetFilteredProducts([FromBody] GetFilteredProductDto req)
    {
        var products = _productService.GetFilteredProducts(req);
        return Ok(products);
    }
}
