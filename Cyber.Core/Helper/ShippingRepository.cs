using Cyber.Core.Database;
using Cyber.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cyber.Core.Helper;

public class ShippingRepository : GenericService<Shipping>
{
    public ShippingRepository(CyberDbContext context) : base(context) {}

    public async Task<List<Shipping>> GetAllShippings()
    {
        var shippings = await _context.Shippings.Include(i => i.ShippingItems).ToListAsync();
        return shippings;
    }
    
    public async Task<Shipping> GetShippingById(int shippingId)
    {
        var shipping = await _context.Shippings.Include(i => i.ShippingItems).FirstOrDefaultAsync(i => i.Id == shippingId);
        if (shipping == null)
            throw new InvalidOperationException("Shipping not found");
        return shipping;
    }
    
    public async Task<List<Shipping>> GetUserShippings(int userId)
    {
        var shippings = await _context.Shippings.Include(i => i.ShippingItems).Where(s => s.UserId == userId).ToListAsync();
        return shippings;
    }
}