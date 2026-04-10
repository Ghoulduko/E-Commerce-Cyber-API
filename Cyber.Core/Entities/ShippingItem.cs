using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber.Core.Entities;

[Table("ShippingItems")]
public class ShippingItem
{
    [Key]
    public int Id { get; set; }
    public int Quantity { get; set; }
    [ForeignKey("Shipping")]
    public int ShippingId { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    
    
    public Shipping  Shipping { get; set; }
    public Product Product { get; set; }
}