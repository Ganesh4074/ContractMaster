using Microsoft.AspNetCore.Mvc;
using ContractMaster.DTOs;
using Microsoft.AspNetCore.Authorization;
using ContractMaster.Services;
using System.Security.Claims;
namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _approvalService.GetApprovals());
    }

    [HttpGet("{id}")]
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
    public async Task<IActionResult> Update(int id, UpdateApprovalDTO update)
    {
        string? userRole=User.FindFirstValue(ClaimTypes.Role);
        int userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var approval = await _approvalService.UpdateApproval(id, update,userId, userRole!);
        return approval is null ? NotFound() : Ok(approval);
    }

}
