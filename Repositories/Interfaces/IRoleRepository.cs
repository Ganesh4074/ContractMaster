using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
    Task<List<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task DeleteAsync(Role role);
    Task<bool> HasUsersAsync(int roleId);

}