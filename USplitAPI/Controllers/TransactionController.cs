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
    [Authorize]
    [Route("get-debts")]
    public async Task<IActionResult> GetUserDebtsAsync(int userId, int familyId) =>
        this.ControllerResponse(await _service.GetUserDebtsAsync(familyId: familyId, userId: userId));
    
    [HttpPost]
    [Authorize]
    [Route("add")]
    public async Task<IActionResult> AddTransaction(int userId, TransactionOptionsDto options) =>
        this.ControllerResponse(await _service.AddTransaction(userId, options));

    [HttpPost]
    [Authorize]
    [Route("resolve-debt")]
    public async Task<IActionResult> ResolveDebt(int userId, int debtId) =>
        this.ControllerResponse(await _service.ResolveDebt(userId: userId, debtId: debtId));
}