using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Dtos.Role;

public class RoleDto
{
    public int Id { get; set; }
    public required string RoleName { get; set; }
}
