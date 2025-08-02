using Microsoft.AspNetCore.Mvc;
using USplitAPI.Extensions;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(string email, string password) =>
        this.ControllerResponse(await _service.LoginUserAsync(email: email, password: password));
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync(string email, string displayName, string password) =>
        this.ControllerResponse(await _service.RegisterUserAsync(email: email, displayName: displayName, password: password));

    [HttpGet]
    [Route("check-email")]
    public async Task<IActionResult> IsEmailTaken(string email) =>
        this.ControllerResponse(await _service.IsEmailTakenAsync(email));

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshSessionAsync(string refreshToken) =>
        this.ControllerResponse(await _service.RefreshSessionAsync(refreshToken));
}