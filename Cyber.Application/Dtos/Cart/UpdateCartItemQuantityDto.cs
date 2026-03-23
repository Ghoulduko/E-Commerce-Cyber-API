using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Cart;

public class UpdateCartItemQuantityDto
{
    public int CartItemId { get; set; }
    public required string QuantityAction { get; set; }
}
