using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Cart;

public class CartItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int CartId { get; set; }
    public int ProductId{ get; set; }
    public ProductDto? Product { get; set; }
}
