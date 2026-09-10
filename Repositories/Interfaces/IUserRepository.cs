using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<List<User>> GetAllAsync();

    Task<User?> GetByEmailAsync(string email);

    Task<List<Approvals>> GetPendingApprovalsAsync(int id);
}