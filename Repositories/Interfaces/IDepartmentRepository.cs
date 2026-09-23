using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
    Task AddAsync(Department department);
}