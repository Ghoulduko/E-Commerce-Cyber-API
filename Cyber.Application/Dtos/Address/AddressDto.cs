using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Address;

public class AddressDto
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string AddressLine { get; set; }
    public required string PhoneNumber { get; set; }
}
