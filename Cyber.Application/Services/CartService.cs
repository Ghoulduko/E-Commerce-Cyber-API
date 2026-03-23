using AutoMapper;
using Cyber.Application.Dtos.Cart;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Services;

public class CartService
{
    private readonly CartRepository _cartRepository;
    private readonly GenericService<CartItem> _cartItemService;
    private readonly IMapper _mapper;

    public CartService(CartRepository repo, GenericService<CartItem> cartItemService, IMapper mapper)
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

        switch (req.QuantityAction)
        {
            case "increment":
                if (cartItem.Quantity < 10)
                    cartItem.Quantity++;
                else
                    throw new ArgumentException("Quantity cannot be more than 10.");
                break;

            case "decrement":
                if (cartItem.Quantity > 1)
                    cartItem.Quantity--;
                else
                    throw new ArgumentException("Quantity cannot be less than 1.");
                break;

            default:
                throw new ArgumentException("Invalid quantity action. Use 'increment' or 'decrement'.");
        }

        await _cartItemService.Save();
    }
}
