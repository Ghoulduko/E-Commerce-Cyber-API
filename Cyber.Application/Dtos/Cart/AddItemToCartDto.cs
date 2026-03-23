using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Cart;

public class AddItemToCartDto
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
}
