using AutoMapper;
using Cyber.Application.Dtos.Address;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public void AddAddress(AddAddressDto address)
    {
        var addressToAdd = _mapper.Map<Address>(address);
        _genericAddressService.Add(addressToAdd);
    }

    public List<AddressDto> GetAll()
    {
        var allAddresses = _genericAddressService.GetAll();
        var allAddressesToDto = _mapper.Map<List<AddressDto>>(allAddresses);
        return allAddressesToDto;
    }

    public AddressDto GetById(int id)
    {
        var address = _genericAddressService.GetById(id);
        var addressToDto = _mapper.Map<AddressDto>(address);
        return addressToDto;
    }

    public void Delete(int id)
    {
        _genericAddressService.Delete(id);
    }
}
