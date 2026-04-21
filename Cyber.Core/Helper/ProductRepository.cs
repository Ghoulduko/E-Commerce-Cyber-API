using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using Cyber.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cyber.Core.Helper;

public class ProductRepository : GenericService<Product>, IProductRepository
{
    public ProductRepository(CyberDbContext context) : base(context) {}

    public async Task<object> PaginateProducts(int page, int brandId, decimal priceFrom, decimal priceTo)
    {
        if (page < 1) page = 1;

        var products = await _context.Products.Where(x => x.ContentType == ContentType.Products)
            .Skip((page - 1) * 9)
            .Take(9)
            .ToListAsync();

        var total = await _context.Products.CountAsync();
        
        if (brandId > 0 && priceFrom >= 0 && priceTo > 0)
        {
            total = await _context.Products.Where(x => x.BrandId == brandId && x.Price >= priceFrom && x.Price <= priceTo && x.ContentType == ContentType.Products).CountAsync();
            products = await _context.Products.Where(x => x.BrandId == brandId && x.Price >= priceFrom && x.Price <= priceTo && x.ContentType == ContentType.Products)
                .Skip((page - 1) * 9)
                .Take(9)
                .ToListAsync();
        }

        if (page > (int)Math.Ceiling((double)total / 9))
        {
            page = (int)Math.Ceiling((double)total / 9);
        }

        return new
        {
            paginatedProducts = products,
            totalPages = (int)Math.Ceiling((double)total / 9) - 1,
            currentPage = page,
        };
    }

    public async Task<List<Product>> GetFavoriteProducts(int userId)
    {
        var products = await _context.FavoriteProducts
            .Where(x => x.UserId == userId)
            .Include(x => x.Product)
            .Select(x => x.Product)
            .ToListAsync();
        return products;
    }
    
}
