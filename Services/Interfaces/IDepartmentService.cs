using ContractMaster.DTOs;

namespace ContractMaster.Services;

public interface IDepartmentService
{
    Task<List<DepartmentDTO>> GetDepartments();

    Task CreateDepartment(NewDepartmentDTO newDepartment);
}