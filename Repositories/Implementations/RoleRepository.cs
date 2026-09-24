using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ContractMasterContext _db;

    public RoleRepository(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _db.Roles.FirstOrDefaultAsync(role => role.Id == id);
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _db.Roles.ToListAsync();
    }

    public async Task AddAsync(Role role)
    {
        await _db.Roles.AddAsync(role);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role role)
    {
        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> HasUsersAsync(int roleId)
    {
        return await _db.Users.AnyAsync(user => user.RoleId == roleId);
    }
}