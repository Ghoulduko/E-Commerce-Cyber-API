using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Entities;

[Table("Brands")]
public class Brand
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<Product>? Products { get; set; }
}
