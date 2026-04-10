
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber.Core.Entities;

[Table("Addresses")]
public class Address
{
    [Key]
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string AddressLine { get; set; }
    public required string PhoneNumber { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}
