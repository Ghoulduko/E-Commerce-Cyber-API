using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }
}
