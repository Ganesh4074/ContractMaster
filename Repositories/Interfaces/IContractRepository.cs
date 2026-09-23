using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface IContractRepository
{
    Task AddAsync(Contract contract);

    Task<List<Contract>> GetAllAsync();

    Task<Contract?> GetByIdAsync(int id);

    Task DeleteAsync(Contract contract);
    Task<bool> ExistsAsync(int contractId, int version);
    Task UpdateAsync(Contract contract);
    Task<Contract?> GetByContractAndVersionAsync(int contractId, int version);

}