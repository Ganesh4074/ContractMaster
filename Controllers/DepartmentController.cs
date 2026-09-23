using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> GetDepartments()
    {
        var departments = await _service.GetDepartments();

        return Ok(departments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(
        NewDepartmentDTO newDepartment)
    {
        await _service.CreateDepartment(newDepartment);

        return Ok();
    }
}