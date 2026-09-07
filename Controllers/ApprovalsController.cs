using Microsoft.AspNetCore.Mvc;
using ContractMaster.Data;
using ContractMaster.DTOs;
using Microsoft.AspNetCore.Authorization;
using ContractMaster.Services;
namespace ContractMaster.Controller;

[ApiController]
[Route("/Approvals/[controller]")]
public class ApprovalsController : ControllerBase
{
    private readonly ApprovalService _approvalService;
    public ApprovalsController(ApprovalService approvalService)
    {
        _approvalService=approvalService;
    }

    [HttpGet("id")]
    [Authorize]
    public async Task<IActionResult> GetApprovalById(int id)
    {
        var approval=await _approvalService.GetApprovalById(id);
        if(approval==null)
        {
            return NotFound();
        }
        return Ok(approval);
    }
}
