using AutoMapper;
using Cyber.Application.Dtos.Cart;
using Cyber.Core.Entities;
using Cyber.Application.Interfaces;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IGenericService<CartItem> _cartItemService;
    private readonly ICartQuantityCalculator _cartQuantityCalculator;
    private readonly IMapper _mapper;

    public CartService(ICartRepository repo, IGenericService<CartItem> cartItemService, IMapper mapper)
    {
        _cartRepository = repo;
        _cartItemService = cartItemService;
        _mapper = mapper;
    }

    public async Task<List<CartDto>> GetAllCarts()
    {
        var allCarts = await _cartRepository.GetCartsForAdmin();
        var cartsToReturn = _mapper.Map<List<CartDto>>(allCarts);
        return cartsToReturn;
    }

    public async Task<CartDto> GetCart(int userId)
    {
        var cart = await _cartRepository.GetCartWithItems(userId);
        if (cart == null)
            throw new ArgumentException("No cart was found for this user, try logging in.");

        var cartItems = _mapper.Map<CartDto>(cart);
        return cartItems;
    }

    public async Task AddToCart(int userId, AddItemToCartDto item)
    {
        var cart = await _cartRepository.GetCartsForAdding(userId);
        if (cart == null)
            throw new ArgumentException("in order to add items to cart, you need to Log In.");

        var existingItem = cart.CartItems?.FirstOrDefault(i => i.ProductId == item.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            var itemToAdd = _mapper.Map<CartItem>(item);
            itemToAdd.CartId = cart.Id;
            cart.CartItems?.Add(itemToAdd);
        }

        await _cartRepository.Save();
    }

    public async Task RemoveFromCart(int userId, int cartItemId)
    {
        var cart = _cartRepository.Get(c => c.UserId == userId);
        if (cart == null)
            throw new ArgumentException("You are not authorized to delete an item from the cart, try logging in.");

        var cartItem = _cartItemService.Get(i => i.Id == cartItemId && i.CartId == cart.Id);
        if (cartItem == null)
            throw new ArgumentException("item not found, try again");

        await _cartItemService.Delete(cartItemId);
    }

    public async Task UpdateQuantity(int userId, UpdateCartItemQuantityDto req)
    {
        var cart = await _cartRepository.Get(c => c.UserId == userId);
        if (cart == null)
            throw new ArgumentException("You are not authorized to delete an item from the cart, try logging in.");

        var cartItem = await _cartItemService.Get(i => i.Id == req.CartItemId && i.CartId == cart.Id);
        if (cartItem == null)
            throw new ArgumentException("item not found, try again");

        cartItem.Quantity = _cartQuantityCalculator.UpdateQuantity(cartItem.Quantity, req.QuantityAction);

        await _cartItemService.Save();
    }

    public async Task ClearCart(int userId)
    {
        await _cartRepository.ClearCart(userId);
    }
}
