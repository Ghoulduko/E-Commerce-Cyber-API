using Cyber.Core.Entities;

namespace Cyber.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}