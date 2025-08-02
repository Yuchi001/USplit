using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USplitAPI.Dtos;
using USplitAPI.Extensions;
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
    [Route("add")]
    public async Task<IActionResult> AddUserAsync(UserDto userDto) =>
        this.ControllerResponse(await _service.AddUserAsync(userDto));
    
    [HttpGet]
    [Authorize]
    [Route("get")]
    public async Task<IActionResult> GetDataAsync() =>
        this.ControllerResponse(await _service.GetUserAsync(this.UserIdFromToken()));
    
    [HttpDelete]
    [Authorize]
    [Route("remove")]
    public async Task<IActionResult> RemoveUserAsync() =>
        this.ControllerResponse(await _service.RemoveUserAsync(this.UserIdFromToken()));
}