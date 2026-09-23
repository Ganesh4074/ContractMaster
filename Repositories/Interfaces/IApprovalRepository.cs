using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface IApprovalRepository
{
    Task<Approvals?> GetByIdAsync(int id);

    Task AddAsync(Approvals approval);

    Task<List<Approvals>> GetAllAsync();

    Task UpdateAsync(Approvals approval);
    Task<List<Approvals>> GetByContractAndVersionAsync(int contractId, int version);
    Task AddRangeAsync(List<Approvals> approvals);
}