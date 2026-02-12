using Microsoft.EntityFrameworkCore;
using Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Database;

public class CyberDbContext : DbContext
{
    public DbSet<Product>? Products;
}
