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
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Brand Brand { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public bool IsInStock { get; set; }
    public required string ImageUrl { get; set; }
}
