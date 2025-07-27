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
    [Route("get")]
    public async Task<IActionResult> GetFamilyAsync(int familyId) => 
        this.ControllerResponse(await _service.GetFamilyAsync(familyId));
    
    [HttpGet]
    [Authorize]
    [Route("get-members")]
    public async Task<IActionResult> GetMembersAsync(int familyId) => 
        this.ControllerResponse(await _service.GetMembers(memberId: this.UserIdFromToken(), familyId: familyId));

    [HttpPost]
    [Authorize]
    [Route("add")]
    public async Task<IActionResult> AddFamilyASync(string name) => 
        this.ControllerResponse(await _service.AddFamilyAsync(this.UserIdFromToken(), name));
    
    [HttpPost]
    [Authorize]
    [Route("add-member")]
    public async Task<IActionResult> AddMemberAsync(int familyId, int userId) => 
        this.ControllerResponse(await _service.AddMemberAsync(ownerId: this.UserIdFromToken(), familyId: familyId, addUserId: userId));
    
    [HttpDelete]
    [Authorize]
    [Route("remove")]
    public async Task<IActionResult> RemoveFamilyAsync(int familyId) => 
        this.ControllerResponse(await _service.RemoveFamilyAsync(ownerUserId: this.UserIdFromToken(), familyId: familyId));
}