using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber.Core.Entities;

[Table("ShippingHistories")]
public class ShippingHistory
{
    [Key]
    public int Id { get; init; }
    
    public int ShippingId { get; init; }
    
    public string Status { get; set; }
    
    public DateTime ChangedStatusAt { get; set; }
    
    public Shipping Shipping { get; init; }
}