using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.User;

public class UpdateUserPasswordDto
{
    public int Id { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
