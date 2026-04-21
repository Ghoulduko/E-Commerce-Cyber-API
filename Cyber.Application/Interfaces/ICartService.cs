using Cyber.Application.Dtos.Cart;

namespace Cyber.Application.Interfaces;

public interface ICartService
{
    Task<List<CartDto>> GetAllCarts();

    Task<CartDto> GetCart(int userId);

    Task AddToCart(int userId, AddItemToCartDto item);

    Task RemoveFromCart(int userId, int cartItemId);

    Task UpdateQuantity(int userId, UpdateCartItemQuantityDto req);

    Task ClearCart(int userId);
}