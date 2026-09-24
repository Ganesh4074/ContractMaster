using ContractMaster.Constants;
using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ContractController : ControllerBase
{
    private readonly ContractService _contractService;

    public ContractController(ContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    [Authorize(Roles =RoleNames.Sales)]
    public async Task<IActionResult> Create(NewContractDTO newContract)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        await _contractService.CreateContract(newContract, userId);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _contractService.GetContracts());
    }

    [HttpDelete]
    [Authorize(Roles =RoleNames.Admin)]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _contractService.DeleteById(id);
        return NoContent();
    }
}