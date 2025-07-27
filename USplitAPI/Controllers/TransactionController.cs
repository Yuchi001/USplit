using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USplitAPI.Dtos;
using USplitAPI.Extensions;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }
    
    [HttpGet]
    //[Authorize]
    [Route("get-debts")]
    public async Task<IActionResult> GetUserDebtsAsync(int familyId, int userId) =>
        this.ControllerResponse(await _service.GetUserDebtsAsync(familyId: familyId, userId: userId));
    
    [HttpPost]
    //[Authorize]
    [Route("add")]
    public async Task<IActionResult> AddTransaction(TransactionOptionsDto options) =>
        this.ControllerResponse(await _service.AddTransaction(options));
}