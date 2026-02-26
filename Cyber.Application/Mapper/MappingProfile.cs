using AutoMapper;
using Cyber.Application.Dtos.Address;
using Cyber.Application.Dtos.Role;
using Cyber.Application.Dtos.User;
using Cyber.Core.Entities;

namespace Cyber.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, Product>();

        // Address mapping
        CreateMap<Address, AddressDto>();
        CreateMap<AddAddressDto, Address>();

        // User mapping
        CreateMap<User, UserDto>();
        CreateMap<AddUserDto, User>();

        // Role mapping
        CreateMap<Role, RoleDto>();
        CreateMap<AddRoleDto, Role>();
    }
}
