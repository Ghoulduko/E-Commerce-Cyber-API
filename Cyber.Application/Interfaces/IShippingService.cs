using System.Linq.Expressions;
using Cyber.Application.Dtos.Shipping;
using Cyber.Core.Entities;
using Cyber.Core.Enums;

namespace Cyber.Application.Interfaces;

public interface IShippingService
{
    Task<List<ShippingDto>> GetAllShippings();
    Task<ShippingDto> GetShippingById(int shippingId);
    Task<List<ShippingDto>> GetUserShippings(int userId);
    Task AddShipping(int addressId, int userId);
    Task UpdateStatus(int shippingId, ShippingStatus shippingStatus);
    Task DeleteShipping(int shippingId);
}