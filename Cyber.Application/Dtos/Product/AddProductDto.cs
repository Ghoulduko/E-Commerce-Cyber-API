using Cyber.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Product;

public class AddProductDto
{
    public required string Name { get; set; }
    public required int BrandId { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public bool IsInStock { get; set; }
}
