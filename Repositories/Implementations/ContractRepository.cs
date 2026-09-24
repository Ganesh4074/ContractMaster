using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ContractMasterContext _db;

    public ContractRepository(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Contract contract)
    {
        await _db.Contracts.AddAsync(contract);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Contract>> GetAllAsync()
    {
        return await _db.Contracts.ToListAsync();
    }

    public async Task<Contract?> GetByIdAsync(int id)
    {
        return await _db.Contracts.FirstOrDefaultAsync(contract => contract.Id == id);
    }

    public async Task DeleteAsync(Contract contract)
    {
        _db.Contracts.Remove(contract);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> ExistsAsync(int contractId, int version)
    {
        return await _db.Contracts.AnyAsync(contract=> contract.ContractId==contractId && contract.Version==version);
    }
    public async Task UpdateAsync(Contract contract)
    {
        _db.Contracts.Update(contract);
        await _db.SaveChangesAsync();
    }
    public async Task<Contract?> GetByContractAndVersionAsync(int contractId, int version)
    {
        return await _db.Contracts.FirstOrDefaultAsync(contract =>
                contract.ContractId == contractId &&
                contract.Version == version);
    }
}