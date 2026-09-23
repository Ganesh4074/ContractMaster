using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContractMaster.DTOs;
using ContractMaster.Services;
using System.Security.Claims;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class SignaturesController : ControllerBase
{
    private readonly SignatureService _signatureService;
    private readonly UserService _userService;

    public SignaturesController(SignatureService signatureService, UserService userService)
    {
        _signatureService = signatureService;
        _userService=userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _signatureService.GetSignatures()
        );
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, UpdateSignatureDTO update)
    {  
        int userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!) ;
        var signature =
            await _signatureService.UpdateSignature(
                id,
                update,userId);

        return signature is null
            ? NotFound()
            : Ok(signature);
    }
}