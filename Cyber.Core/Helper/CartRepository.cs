using AutoMapper;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Cyber.Core.Interfaces;

namespace Cyber.Core.Helper;

public class CartRepository : GenericService<Cart>, ICartRepository
{
    
    public CartRepository(CyberDbContext context) : base(context) {}

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
    
    public async Task ClearCart(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.UserId == userId);
    
        if (cart == null) return;
    
        _context.CartItems.RemoveRange(cart.CartItems);
        await _context.SaveChangesAsync();
    }
}
