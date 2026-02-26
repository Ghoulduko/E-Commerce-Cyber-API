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
    private readonly CyberDbContext _context;
    private readonly GenericService<User> _service;
    private readonly IMapper _mapper;

    public UserService(GenericService<User> userService, CyberDbContext context, IMapper mapper)
    {
        _service = userService;
        _context = context;
        _mapper = mapper;
    }

    public void AddUser(AddUserDto user)
    {
        var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email);
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

        _service.Add(userToAdd);
    }

    public List<UserDto> GetAll()
    {
        var users = _service.GetAll();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return usersDto;
    }

    public UserDto GetUserById(int id)
    {
        var user = _service.GetById(id);
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public UserDto GetUserByEmail(string email)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        if (user == null) throw new ArgumentNullException("No account found with the provided email");
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public void UpdatePassword(UpdateUserPasswordDto req)
    {
        var user = _service.GetById(req.Id);
        if (!BC.EnhancedVerify(req.CurrentPassword, user.Password)) throw new ArgumentException("The password is wrong, try again!");
        if (req.CurrentPassword.Length < 8) throw new ArgumentException("Password must be atleast 8 characters long");
        var newPassword = BC.EnhancedHashPassword(req.NewPassword, 13);
        user.Password = newPassword;
        _context.SaveChanges();
    }

    public void DeleteUser(DeleteUserDto req)
    {
        var user = GetUserById(req.Id);
        if (!BC.EnhancedVerify(req.Password, user.Password)) throw new ArgumentException("The password is wrong, try again!");
        _service.Delete(req.Id);
    }
}
