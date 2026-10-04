using ContractMaster.DTOs;
using ContractMaster.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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
    [HttpGet("MicrosoftLogin")]
    public async Task<IActionResult> MLogin()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri="/auth/me"
            },
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
    [HttpGet("me")]
    public IActionResult Me()
    {
        if(!User.Identity?.IsAuthenticated ?? true)
        {
            return Unauthorized();
        }
        return Ok(new
        {
            Name=User.Identity.Name,
            Claims=User.Claims.Select(c=> new
            {
                c.Type,
                c.Value
            })
        });
    }
}