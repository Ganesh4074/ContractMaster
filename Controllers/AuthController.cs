using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO request, AuthService auth)
    {
        var response = await auth.Login(request);
        return response is null ? Unauthorized() : Ok(response);
    }
}