using AutoMapper;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cyber.Core.Helper;

public class ProductRepository : GenericService<Product>
{
    public ProductRepository(CyberDbContext context, IMapper mapper) : base(context, mapper) {}

    public async Task<object> PaginateProducts(int page)
    {
        if (page < 1) page = 1;

        var total = await _context.Products.CountAsync();
        var products = await _context.Products.Skip((page - 1) * 9).Take(9).ToListAsync();

        if (page > (int)Math.Ceiling((double)total / 9))
        {
            page = (int)Math.Ceiling((double)total / 9);
        }

        return new
        {
            paginatedProducts = products,
            totalPages = (int)Math.Ceiling((double)total / 9),
            currentPage = page,
            //totalProducts = total
        };
    }
}
