using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.User;

public class LoginUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
