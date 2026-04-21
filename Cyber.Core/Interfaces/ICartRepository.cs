using System.Linq.Expressions;
using Cyber.Core.Entities;

namespace Cyber.Core.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartWithItems(int userId);

    Task<List<Cart>?> GetCartsForAdmin();

    Task<Cart?> GetCartsForAdding(int userId);

    Task ClearCart(int userId);
    
    Task Save();
    
    Task<Cart?> Get(Expression<Func<Cart, bool>> predicate);
}