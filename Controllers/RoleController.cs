using ContractMaster.Constants;
using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = RoleNames.Admin)]
public class RolesController : ControllerBase
{
    private readonly RoleService _service;

    public RolesController(RoleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoleDTO>>> GetRoles()
    {
        var roles = await _service.GetRoles();

        return Ok(roles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(NewRoleDTO newRole)
    {
        await _service.CreateRole(newRole);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        await _service.DeleteRole(id);

        return NoContent();
    }
}