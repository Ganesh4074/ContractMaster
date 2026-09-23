using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContractMaster.DTOs;
using ContractMaster.Services;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class SignaturesController : ControllerBase
{
    private readonly SignatureService _signatureService;

    public SignaturesController(
        SignatureService signatureService)
    {
        _signatureService = signatureService;
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
    public async Task<IActionResult> Update(
        int id,
        UpdateSignatureDTO update)
    {
        var signature =
            await _signatureService.UpdateSignature(
                id,
                update);

        return signature is null
            ? NotFound()
            : Ok(signature);
    }
}