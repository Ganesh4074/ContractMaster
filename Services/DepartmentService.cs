using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class DepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DepartmentDTO>> GetDepartments()
    {
        var departments = await _repository.GetAllAsync();

        return departments
            .Select(department => new DepartmentDTO(
                department.Id,
                department.Name))
            .ToList();
    }

    public async Task CreateDepartment(NewDepartmentDTO newDepartment)
    {
        var department = new Department
        {
            Name = newDepartment.Name
        };

        await _repository.AddAsync(department);
    }
}