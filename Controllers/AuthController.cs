using ContractMaster.DTOs;
using ContractMaster.Services;
using ContractMaster.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO request, IAuthService auth)
    {
        var response = await auth.Login(request);
        return response is null ? Unauthorized() : Ok(response);
    }
}