using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Services;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Cyber_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly CyberDbContext _context;
    private readonly ProductService _productService;
    private readonly FileService _fileService;

    public FileController(CyberDbContext context, ProductService productService, FileService fileService)
    {
        _context = context;
        _productService = productService;
        _fileService = fileService;
    }

    [HttpPost("UploadProduct")]
    [Authorize(Roles = "SUPERADMIN")]
    public async Task<IActionResult> UploadFile(IFormFile file, ContentType type, [FromForm] AddProductDto productRequest)
    {
        var identificator = Guid.NewGuid();
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

        var req = new MediaFile
        {
            Identificator = identificator,
            FileType = file.ContentType,
            FileName = file.FileName,
            Content = memoryStream.ToArray(),
            ContentType = type
        };

        await _fileService.AddImage(req);

        var product = new ProductDto
        {
            Name = productRequest.Name,
            BrandId = productRequest.BrandId,
            Description = productRequest.Description,
            ContentType = type,
            Price = productRequest.Price,
            IsInStock = productRequest.IsInStock,
            ImageUrl = $"https://localhost:7054/api/File/Download?Identificator={identificator}"
        };

        await _productService.AddProduct(product);

        return Ok(req);
    }

    //[HttpGet("Download")]
    //public IActionResult Download(Guid Identificator)
    //{
    //    var fromdb = _context.MediaFiles.FirstOrDefault(i => i.Identificator == Identificator);

    //    if (fromdb is null) return NotFound("no file store");

    //    return File(fromdb.Content, fromdb.FileType, Guid.NewGuid() + fromdb.FileName);
    //}

    //[HttpPost("GetViews")]
    //public IActionResult GetView(List<ContentType> types)
    //{
    //    var pictures = _context.MediaFiles
    //        .Where(i => types.Any(j => j == i.ContentType)).Select(i => i.Identificator);
    //    return Ok(pictures);
    //}
}
