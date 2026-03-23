using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.User;

public class GetUserDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
