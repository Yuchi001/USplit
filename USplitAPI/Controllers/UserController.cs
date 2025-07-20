using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USplitAPI.Dtos;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(UserDto userDto) =>
        this.ControllerResponse(await service.RegisterUserAsync(userDto));

    [HttpGet("check-email")]
    public async Task<IActionResult> IsEmailTaken(string email) =>
        this.ControllerResponse(await service.IsEmailTaken(email));
}