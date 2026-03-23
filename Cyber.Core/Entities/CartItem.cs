using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Entities;

[Table("CartItems")]
public class CartItem
{
    [Key]
    public int Id { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("Cart")]
    public int CartId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }


    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
}
