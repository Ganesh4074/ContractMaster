using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class SignatureRepository : ISignatureRepository
{
    private readonly ContractMasterContext _db;

    public SignatureRepository(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task<bool> ExistsAsync(
        int contractId,
        int version)
    {
        return await _db.Signatures.AnyAsync(s =>
            s.ContractId == contractId &&
            s.Version == version);
    }

    public async Task AddRangeAsync(
        List<Signature> signatures)
    {
        await _db.Signatures.AddRangeAsync(signatures);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Signature>> GetAllAsync()
    {
        return await _db.Signatures.ToListAsync();
    }

    public async Task<Signature?> GetByIdAsync(int id)
    {
        return await _db.Signatures.FindAsync(id);
    }

    public async Task UpdateAsync(Signature signature)
    {
        _db.Signatures.Update(signature);
        await _db.SaveChangesAsync();
    }
}