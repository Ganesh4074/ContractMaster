using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [Authorize]
    public async Task<IActionResult> Create(NewContractDTO newContract)
    {
        await _contractService.CreateContract(newContract);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _contractService.GetContracts());
    }

    [HttpDelete]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _contractService.DeleteById(id);
        return NoContent();
    }
}