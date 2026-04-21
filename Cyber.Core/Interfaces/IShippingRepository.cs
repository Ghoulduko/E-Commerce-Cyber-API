using Cyber.Core.Entities;

namespace Cyber.Core.Interfaces;

public interface IShippingRepository
{
    Task<List<Shipping>> GetAllShippings();
    Task<Shipping> GetShippingById(int shippingId);
    Task<List<Shipping>> GetUserShippings(int userId);
    Task Add(Shipping shipping);
    Task<Shipping> GetById(int shippingId);
    Task Save();
    Task Delete(int id);
}