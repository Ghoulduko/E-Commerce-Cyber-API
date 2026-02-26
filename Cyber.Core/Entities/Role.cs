using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Entities;

[Table("Roles")]
public class Role
{
    [Key]
    public int Id { get; set; }
    public required string RoleName { get; set; }

    public List<User>? Users { get; set; }
}
