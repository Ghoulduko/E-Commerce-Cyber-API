using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Entities;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public List<Address> Addresses { get; set; } = new();
    public Cart? Cart { get; set; }
    public List<Shipping> Shippings { get; set; } = new();
}
