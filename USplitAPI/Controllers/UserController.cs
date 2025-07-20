using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USplitAPI.Dtos;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(UserDto userDto) =>
        this.ControllerResponse(await _service.RegisterUserAsync(userDto));

    [HttpGet("check-email")]
    public async Task<IActionResult> IsEmailTaken(string email) =>
        this.ControllerResponse(await _service.IsEmailTakenAsync(email));
}