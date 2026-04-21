using Cyber.Application.Dtos.Role;

namespace Cyber.Application.Interfaces;

public interface IRoleService
{
    Task AddRole(AddRoleDto request);
    Task<RoleDto> GetRoleByName(string name);
    Task<List<RoleDto>> GetAllRoles();
    Task DeleteRole(string name);
}