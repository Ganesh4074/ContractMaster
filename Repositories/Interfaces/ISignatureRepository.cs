using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface ISignatureRepository
{
    Task AddRangeAsync(List<Signature> signatures);

    Task<bool> ExistsAsync(int contractId, int version);

    Task<List<Signature>> GetAllAsync();

    Task<Signature?> GetByIdAsync(int id);

    Task UpdateAsync(Signature signature);
    public Task<List<Signature>> GetByContractAndVersionAsync(int contractId, int version);
}