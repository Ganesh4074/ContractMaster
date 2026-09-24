using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class RoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RoleDTO>> GetRoles()
    {
        var roles = await _repository.GetAllAsync();

        return roles.Select(role => new RoleDTO(
            role.Id,
            role.Name
        )).ToList();
    }

    public async Task CreateRole(NewRoleDTO newRole)
    {
        var role = new Role
        {
            Name = newRole.Name
        };

        await _repository.AddAsync(role);
    }

    public async Task DeleteRole(int id)
    {
        var role = await _repository.GetByIdAsync(id);

        if (role is null)
        {
            throw new FileNotFoundException("Role not found.");
        }

        var hasUsers = await _repository.HasUsersAsync(id);

        if (hasUsers)
        {
            throw new BusinessRuleException(
                "Role cannot be deleted because it is assigned to users.");
        }

        await _repository.DeleteAsync(role);
    }
}