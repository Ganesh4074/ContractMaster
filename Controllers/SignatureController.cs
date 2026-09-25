using System.Security.Claims;
using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class SignaturesController : ControllerBase
{
    private readonly ISignatureService _signatureService;

    public SignaturesController(ISignatureService signatureService)
    {
        _signatureService = signatureService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _signatureService.GetSignatures());
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Update( int id, UpdateSignatureDTO update)
    {
        //To get current user Id and role
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRole = User.FindFirstValue(ClaimTypes.Role);

        if (!int.TryParse(userIdClaim, out var userId) || string.IsNullOrWhiteSpace(userRole))
        {
            throw new UnauthorizedAccessException("User identity information is missing.");
        }

        var signature = await _signatureService.UpdateSignature(id, update, userId, userRole);
        return signature is null? NotFound() : Ok(signature);
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(NewSignatureDTO newSignature)
    {
        var signature = await _signatureService.CreateSignature(newSignature);

        return Ok(signature);
    }
}