using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Address;

public class AddAddressDto
{
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string AddressLine { get; set; }
    public required string PhoneNumber { get; set; }
}
