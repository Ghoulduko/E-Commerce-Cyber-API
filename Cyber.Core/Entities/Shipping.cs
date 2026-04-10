using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cyber.Core.Enums;

namespace Cyber.Core.Entities;

[Table("Shippings")]
public class Shipping
{
    [Key]
    public int Id { get; set; }
    
    public List<ShippingItem> ShippingItems { get; set; }
    
    public ShippingStatus ShippingStatus { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public decimal Cost { get; set; }
    
    public int UserId { get; set; }
    public int ShippingAddressId { get; set; }
    
    public User User { get; set; }
    public Address ShippingAddress { get; set; }
}