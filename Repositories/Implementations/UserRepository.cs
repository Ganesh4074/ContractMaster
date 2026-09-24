using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ContractMasterContext _db;

    public UserRepository(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _db.Users.Include(user => user.Role).ToListAsync();
    }

    public async Task<List<Approvals>> GetPendingApprovalsAsync()
    {
        return await _db.Approvals.Where(approval =>
            approval.ApprovalStatus == ApprovalStatus.Pending).ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.Include(user => user.Role).FirstOrDefaultAsync(user => user.EMail == email);
    }
}