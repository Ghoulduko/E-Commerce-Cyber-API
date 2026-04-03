using AutoMapper;
using Cyber.Application.Dtos.Address;
using Cyber.Core.Entities;
using Cyber.Core.Helper;

namespace Cyber.Application.Services;

public class AddressService
{
    private readonly GenericService<Address> _genericAddressService;
    private readonly IMapper _mapper;

    public AddressService(GenericService<Address> addressService, IMapper mapper)
    {
        _genericAddressService = addressService;
        _mapper = mapper;
    }
    
    public async Task AddAddress(AddAddressDto address, int userId)
    {
        var addressToAdd = _mapper.Map<Address>(address);
        addressToAdd.UserId = userId;
        await _genericAddressService.Add(addressToAdd);
    }

    public async Task<List<AddressDto>> GetAll()
    {
        var allAddresses = await _genericAddressService.GetAll();
        var allAddressesToDto = _mapper.Map<List<AddressDto>>(allAddresses);
        return allAddressesToDto;
    }

    public async Task<AddressDto> GetById(int id)
    {
        var address = await _genericAddressService.GetById(id);
        var addressToDto = _mapper.Map<AddressDto>(address);
        return addressToDto;
    }

    public async Task<List<AddressDto>> GetUserAddresses(int userId)
    {
        var addresses = await _genericAddressService.Filter(a => a.UserId == userId);
        if (addresses == null) throw new ArgumentException("No addresses found");
        var addressToDto = _mapper.Map<List<AddressDto>>(addresses);
        return addressToDto;
    } 

    public async Task Delete(int userId, int id)
    {
        var address = await _genericAddressService.GetById(id);
        if (address == null) throw new ArgumentException("Address not found");
        if (address.UserId != userId) throw new UnauthorizedAccessException("You are not authorized to delete this address");
        await _genericAddressService.Delete(id);
    }
}
