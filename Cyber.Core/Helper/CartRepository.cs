using AutoMapper;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Helper;

public class CartRepository : GenericService<Cart>
{
    public CartRepository(CyberDbContext context, IMapper mapper) : base(context, mapper)
    {

    }


    public async Task<Cart?> GetCartWithItems(int userId)
    {
        return await _context.Carts.Include(c => c.CartItems).ThenInclude(i => i.Product).SingleOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<List<Cart>?> GetCartsForAdmin()
    {
        return await _context.Carts.Include(c => c.CartItems).ThenInclude(i => i.Product).ToListAsync();
    }

    public async Task<Cart?> GetCartsForAdding(int userId)
    {
        return await _context.Carts.Include(c => c.CartItems).SingleOrDefaultAsync(c => c.UserId == userId);
    }
}
