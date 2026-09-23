using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize/*(Roles = "Admin")*/]
    [AllowAnonymous]
    public async Task<IActionResult> Create(NewUserDto newUser)
    {
        await _userService.CreateUser(newUser);
        return Ok();
    }

    [HttpGet]
    [Authorize/*(Roles = "Admin")*/]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userService.GetUsers());
    }

    [HttpGet("Approvals/pending")]
    [Authorize]
    public async Task<IActionResult> GetPendingApprovals()
    {
        return Ok(await _userService.GetPendingApprovals());
    }
}