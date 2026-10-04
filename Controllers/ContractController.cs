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
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(NewContractDTO newContract)
    {
        var email = User.FindFirstValue(ClaimTypes.Upn);

        
        await _contractService.CreateContract(newContract, email);
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