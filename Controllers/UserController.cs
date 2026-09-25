using ContractMaster.Constants;
using ContractMaster.DTOs;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> Create(NewUserDto newUser)
    {
        await _userService.CreateUser(newUser);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userService.GetUsers());
    }

}