using Microsoft.AspNetCore.Mvc;
using ContractMaster.DTOs;
using Microsoft.AspNetCore.Authorization;
using ContractMaster.Services;
namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class ApprovalsController : ControllerBase
{
    private readonly ApprovalService _approvalService;
    public ApprovalsController(ApprovalService approvalService)
    {
        _approvalService=approvalService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewApprovalDTO newApproval)
    {
        await _approvalService.CreateApproval(newApproval);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _approvalService.GetApprovals());
    }

    [HttpGet("{id}")]
    [Authorize(Roles ="Administrator,Approver")]
    public async Task<IActionResult> GetApprovalById(int id)
    {
        var approval=await _approvalService.GetApprovalById(id);
        if(approval==null)
        {
            return NotFound();
        }
        return Ok(approval);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Approver,Administrator")]
    public async Task<IActionResult> Update(int id, UpdateApprovalDTO update)
    {
        var approval = await _approvalService.UpdateApproval(id, update);
        return approval is null ? NotFound() : Ok(approval);
    }

    [HttpGet("Get-Error")]
    public async Task<IActionResult> Error()
    {
        var k=100;
        var z=0;
        return Ok(k/z);
    }
}
