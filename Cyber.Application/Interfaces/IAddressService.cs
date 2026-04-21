using Cyber.Application.Dtos.Address;

namespace Cyber.Application.Interfaces;

public interface IAddressService
{
    Task AddAddress(AddAddressDto address, int userId);

    Task<List<AddressDto>> GetAll();

    Task<AddressDto> GetById(int id);

    Task<List<AddressDto>> GetUserAddresses(int userId);

    Task Delete(int userId, int id);
}