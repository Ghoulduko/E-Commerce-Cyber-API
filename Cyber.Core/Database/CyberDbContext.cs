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
    private readonly IConfiguration _configuration;
    public CyberDbContext(IConfiguration configuration, DbContextOptions<CyberDbContext> ops) : base(ops)
    {
        _configuration = configuration;
    }
    public DbSet<Product> Products { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Product>()
    //        .Property(p => p.Price)
    //        .HasPrecision(18, 4);
    //}
}
