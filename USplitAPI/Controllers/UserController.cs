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
    [Route("register")]
    public async Task<IActionResult> RegisterUserAsync(UserDto userDto) =>
        this.ControllerResponse(await _service.RegisterUserAsync(userDto));
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUserAsync(string email, string password, bool rememberMe) =>
        this.ControllerResponse(await _service.LoginUserAsync(email, password, rememberMe));
    
    [HttpDelete]
    [Route("logout")]
    public async Task<IActionResult> LogoutUserAsync(int userId) =>
        this.ControllerResponse(await _service.LogoutUserAsync(userId));

    [HttpGet]
    [Route("check-email")]
    public async Task<IActionResult> IsEmailTaken(string email) =>
        this.ControllerResponse(await _service.IsEmailTakenAsync(email));
    
    [HttpDelete]
    [Route("remove")]
    public async Task<IActionResult> RemoveUserAsync(int id) =>
        this.ControllerResponse(await _service.RemoveUserAsync(id));
}