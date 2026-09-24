using System.Security.Claims;
using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly CommentService _service;

    public CommentsController(CommentService service)
    {
        _service = service;
    }

    [HttpGet("contract/{contractId:int}/{version:int}")]
    public async Task<ActionResult<List<CommentDTO>>> GetComments(
        int contractId, int version)
    {
        var comments = await _service.GetComments(contractId,version);

        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(
        NewCommentDTO newComment)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _service.CreateComment(newComment, userId);

        return Ok();
    }
}