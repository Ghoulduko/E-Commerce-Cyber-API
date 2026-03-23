using AutoMapper;
using Cyber.Application.Dtos.User;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Services;

public class UserService
{
    private readonly GenericService<User> _service;
    private readonly GenericService<Cart> _cartService;
    private readonly AuthService _authService;
    private readonly IMapper _mapper;

    public UserService(GenericService<User> userService, GenericService<Cart> cartService, AuthService authService, IMapper mapper)
    {
        _service = userService;
        _authService = authService;
        _cartService = cartService;
        _mapper = mapper;
    }

    public async Task<string> AddUser(AddUserDto user)
    {
        var existingUser = _service.Get(u => u.Email == user.Email);
        if (existingUser != null) throw new ArgumentException("An account with that email already exists");
        if (user.Password.Length < 8) throw new ArgumentException("Password must be atleast 8 characters long");
        if (user.Name.Length < 4) throw new ArgumentException("Name must be atleast 4 characters long");
        var userToAdd = new User
        {
            Email = user.Email,
            Name = user.Name,
            Password = BC.EnhancedHashPassword(user.Password, 13),
            RoleId = 1,
        };
        await _service.Add(userToAdd);

        var cart = new Cart
        {
            UserId = userToAdd.Id,
        };
        await _cartService.Add(cart);

        var tokenToReturn = await _authService.Login(new LoginUserDto { Email = user.Email, Password = user.Password });
        return tokenToReturn;
    }

    public async Task<List<GetUserDto>> GetAll()
    {
        var users = await _service.GetAll();
        var usersDto = _mapper.Map<List<GetUserDto>>(users);
        return usersDto;
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var user = await _service.GetById(id);
        if (user == null) throw new ArgumentNullException("No account found with the provided id");
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _service.Get(u => u.Email == email);
        if (user == null) throw new ArgumentNullException("No account found with the provided email");
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task UpdatePassword(int id, UpdateUserPasswordDto req)
    {
        var user = await _service.GetById(id);
        if (!BC.EnhancedVerify(req.CurrentPassword, user.Password)) throw new ArgumentException("The password is wrong, try again!");
        if (req.CurrentPassword.Length < 8) throw new ArgumentException("Password must be atleast 8 characters long");
        var newPassword = BC.EnhancedHashPassword(req.NewPassword, 13);
        user.Password = newPassword;
        await _service.Save();
    }

    public async Task DeleteAccount(int id, DeleteUserDto req)
    {
        var user = await _service.GetById(id);
        if (!BC.EnhancedVerify(req.Password, user.Password)) throw new ArgumentException("The password is wrong, try again!");
        await _service.Delete(id);
    }
}
