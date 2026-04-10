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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Shipping>()
            .HasOne(s => s.ShippingAddress)
            .WithMany()
            .HasForeignKey(s => s.ShippingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Shipping>()
            .HasOne(s => s.User)
            .WithMany(u => u.Shippings)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Shipping>()
            .Property(s => s.Cost)
            .HasPrecision(18, 2);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<ShippingItem> ShippingItems { get; set; }
}
