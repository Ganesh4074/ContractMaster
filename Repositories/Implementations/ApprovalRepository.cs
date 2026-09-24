using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class ApprovalRepository : IApprovalRepository
{
    private readonly ContractMasterContext _db;

    public ApprovalRepository(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task<Approvals?> GetByIdAsync(int id)
    {
        return await _db.Approvals.FindAsync(id);
    }

    public async Task AddAsync(Approvals approval)
    {
        await _db.Approvals.AddAsync(approval);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Approvals>> GetAllAsync()
    {
        return await _db.Approvals.ToListAsync();
    }

    public async Task UpdateAsync(Approvals approval)
    {
        _db.Approvals.Update(approval);
        await _db.SaveChangesAsync();
    }
    public async Task<List<Approvals>> GetByContractAndVersionAsync(int contractId, int version)
    {
        return await _db.Approvals.Where(a =>
                a.ContractId == contractId &&
                a.Version == version).ToListAsync();
    }
    public async Task AddRangeAsync(List<Approvals> approvals)
    {
        await _db.Approvals.AddRangeAsync(approvals);
        await _db.SaveChangesAsync();
    }
}