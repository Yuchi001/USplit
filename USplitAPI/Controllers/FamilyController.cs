using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USplitAPI.Extensions;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService _service;

    public FamilyController(IFamilyService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    [Route("get-debts")]
    public async Task<IActionResult> GetUserDebtsAsync(int userId) =>
        this.ControllerResponse(await _service.GetUserDebtsAsync(userId));

    [HttpPost]
    [Authorize]
    [Route("add")]
    public async Task<IActionResult> AddFamilyASync(string name) => this.ControllerResponse(await _service.AddFamilyAsync(this.UserIdFromToken(), name));
    
    [HttpDelete]
    [Authorize]
    [Route("remove")]
    public async Task<IActionResult> RemoveFamilyAsync(int familyId) => this.ControllerResponse(await _service.RemoveFamilyAsync(this.UserIdFromToken(), familyId));
}