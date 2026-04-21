using Cyber.Application.Dtos.User;

namespace Cyber.Application.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginUserDto request);
}