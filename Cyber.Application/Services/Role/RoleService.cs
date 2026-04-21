using AutoMapper;
using Cyber.Application.Dtos.Role;
using Cyber.Core.Entities;
using Cyber.Application.Interfaces;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class RoleService : IRoleService
{
    private readonly IGenericService<Role> _service;
    private readonly IMapper _mapper;

    public RoleService(IGenericService<Role> userService, IMapper mapper)
    {
        _service = userService;
        _mapper = mapper;
    }

    public async Task AddRole(AddRoleDto request)
    {
        request.RoleName = request.RoleName.Trim().ToUpper();

        var role = await _service.Get(r => r.RoleName == request.RoleName);
        if (role != null) throw new Exception("The role already exists");
        var roleToAdd = _mapper.Map<Role>(request);
        await _service.Add(roleToAdd);
    }

    public async Task<RoleDto> GetRoleByName(string name)
    {
        var role = await _service.Get(r => r.RoleName == name);
        if (role == null) throw new ArgumentNullException("No role found with the provided name");
        var roleToReturn = _mapper.Map<RoleDto>(role);
        return roleToReturn;
    }

    public async Task<List<RoleDto>> GetAllRoles()
    {
        var roles = await _service.GetAll();
        var rolesToReturn = _mapper.Map<List<RoleDto>>(roles);
        return rolesToReturn;
    }

    public async Task DeleteRole(string name)
    {
        name = name.Trim().ToUpper();

        var role = await _service.Get(r => r.RoleName == name);
        if (role == null) throw new ArgumentNullException("No role found with the provided name");
        await _service.Delete(role.Id);
    }
}
