using Cyber.Application.Dtos.Product;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int BrandId { get; set; }
    public required string Description { get; set; }
    public required ContentType ContentType { get; set; }
    public required decimal Price { get; set; }
    
    public bool IsFavorite { get; set; } = false;
    public bool IsInStock { get; set; }
    public string? ImageUrl { get; set; }
}
