using AutoMapper;
using Cyber.Application.Dtos.Shipping;
using Cyber.Application.Interfaces;
using Cyber.Core.Entities;
using Cyber.Core.Enums;
using Cyber.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cyber.Application.Services;

public class ShippingService : IShippingService
{
    private readonly IShippingRepository _service;
    private readonly IGenericService<ShippingHistory> _historyService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ILogger<ShippingService> _logger;
    
    public ShippingService(IShippingRepository service, IGenericService<ShippingHistory> historyService, ICartService cartService, IMapper mapper, ILogger<ShippingService> logger)
    {
        _service = service;
        _historyService = historyService;
        _cartService = cartService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ShippingDto>> GetAllShippings()
    {
        var allShippings = await _service.GetAllShippings();
        return _mapper.Map<List<ShippingDto>>(allShippings);
    }

    public async Task<ShippingDto> GetShippingById(int shippingId)
    {
        var shipping = await _service.GetShippingById(shippingId);
        return _mapper.Map<ShippingDto>(shipping);
    }
    
    public async Task<List<ShippingDto>> GetUserShippings(int userId)
    {
        var allShippings = await _service.GetUserShippings(userId);
        return _mapper.Map<List<ShippingDto>>(allShippings);
    }
    

    public async Task AddShipping(int addressId, int userId)
    {
        var userCart = await _cartService.GetCart(userId);
        
        if (userCart.CartItems == null || userCart.CartItems.Count == 0)
            throw new InvalidOperationException("No items found in cart to ship");
        
        var shippingItems = _mapper.Map<List<ShippingItemDto>>(userCart.CartItems);
        
        var cost = userCart.CartItems.Sum(i => i.Product.Price * i.Quantity);

        var shippingInfo = new ShippingDto()
        {
            ShippingItems = shippingItems,
            ShippingStatus = ShippingStatus.InProgress.ToString(),
            CreatedAt = DateTime.Now,
            UserId = userId,
            Cost = cost,
            ShippingAddressId = addressId
        };
        
        var shipping = _mapper.Map<Shipping>(shippingInfo);
        await _service.Add(shipping);
        await _cartService.ClearCart(userId);

        var newHistory = new ShippingHistory
        {
            ShippingId = shipping.Id,
            Status = shipping.ShippingStatus.ToString(),
            ChangedStatusAt = DateTime.Now,
        };
        
        await _historyService.Add(newHistory);
    }

    public async Task UpdateStatus(int shippingId, ShippingStatus shippingStatus)
    {
        var shipping = await _service.GetShippingById(shippingId);
        
        if (shipping.ShippingStatus == shippingStatus)
            throw new InvalidOperationException($"Shipping status is already {shippingStatus}");
        
        shipping.ShippingStatus = shippingStatus;
        
        
        var shippingHistory = await _historyService.GetFirst(s => s.ShippingId == shipping.Id);
        shippingHistory.Status = shippingStatus.ToString();
        shippingHistory.ChangedStatusAt = DateTime.Now;
        await _service.Save();
    }

    public async Task DeleteShipping(int shippingId)
    {
        var shipping = await _service.GetById(shippingId);
        if (shipping == null)
            throw new InvalidOperationException("Shipping not found");
        
        await _service.Delete(shipping.Id);
    }
}