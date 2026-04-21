using Cyber.Application.Dtos.User;

namespace Cyber.Application.Interfaces;

public interface IUserService
{
    Task<string> AddUser(AddUserDto user);

    Task<List<GetUserDto>> GetAll();

    Task<UserDto> GetUserById(int id);

    Task<UserDto> GetUserByEmail(string email);

    Task UpdatePassword(int id, UpdateUserPasswordDto req);

    Task DeleteAccount(int id, DeleteUserDto req);
}