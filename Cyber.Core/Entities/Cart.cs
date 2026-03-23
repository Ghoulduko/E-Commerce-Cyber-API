using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber.Core.Entities;

[Table("Carts")]
public class Cart
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();

    public User? User { get; set; }
}
