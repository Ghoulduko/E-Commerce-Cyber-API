using Cyber.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Database;

public class CyberDbContext : DbContext
{
    public CyberDbContext(DbContextOptions<CyberDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
}
