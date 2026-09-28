using System.Security.Claims;
using ContractMaster.DTOs;
using ContractMaster.Services;
using ContractMaster.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentsController(ICommentService service)
    {
        _service = service;
    }

    [HttpGet("contract/{contractId:int}/{version:int}")]
    public async Task<ActionResult<List<CommentDTO>>> GetComments(
        int contractId, int version)
    {
        var comments = await _service.GetCommentsById(contractId,version);

        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(NewCommentDTO newComment)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _service.CreateComment(newComment, userId);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments=await _service.GetCommentsAsync();
        return Ok(comments);
    }
}