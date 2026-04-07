using AutoMapper;
using Cyber.Application.Dtos;
using Cyber.Application.Dtos.Address;
using Cyber.Application.Dtos.Cart;
using Cyber.Application.Dtos.Product;
using Cyber.Application.Dtos.Role;
using Cyber.Application.Dtos.User;
using Cyber.Core.Entities;

namespace Cyber.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, Product>();
        CreateMap<ProductDto, Product>().ReverseMap();
        
        // Favorite Products mapping
        CreateMap<FavoriteProduct, FavoriteProductDto>().ReverseMap();
        CreateMap<AddFavoriteProductDto, FavoriteProductDto>().ReverseMap();

        // Address mapping
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<AddAddressDto, Address>().ReverseMap();

        // User mapping
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<AddUserDto, User>().ReverseMap();
        CreateMap<User, GetUserDto>().ReverseMap();

        // Role mapping
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<AddRoleDto, Role>().ReverseMap();

        // Brand mapping
        CreateMap<BrandDto, Brand>().ReverseMap();

        // Cart mapping
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap();
        CreateMap<AddItemToCartDto, CartItem>();
    }
}
