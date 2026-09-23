using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ContractMasterContext _db;

    public DepartmentRepository(ContractMasterContext db)
    {
        _db = db;
    }
    public async Task<List<Department>> GetAllAsync()
    {
        return await _db.Departments.ToListAsync();
    }
    public async Task AddAsync(Department department)
    {
        await _db.Departments.AddAsync(department);
        await _db.SaveChangesAsync();
    }
}