using ContractMaster.DTOs;

namespace ContractMaster.Services;

public interface IRoleService
{
    Task<List<RoleDTO>> GetRoles();

    Task CreateRole(NewRoleDTO newRole);

    Task DeleteRole(int id);
}