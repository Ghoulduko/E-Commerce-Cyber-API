using Cyber.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Product;

public class ProductFilterDto
{
    // public string? Name { get; set; }
    public required int BrandId { get; set; }
    [Precision(6, 2)]
    public decimal PriceFrom { get; set; }
    [Precision(6, 2)]
    public decimal PriceTo { get; set; }
}
