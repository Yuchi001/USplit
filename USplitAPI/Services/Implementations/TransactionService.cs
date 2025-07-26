using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly USplitDBContext _context;

    public TransactionService(IMapper mapper, USplitDBContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ResultTuple> GetUserDebtsAsync(int familyId, int userId)
    {
        var foundFamily = await _context.UserFamilies.SingleOrDefaultAsync(e => e.FamilyId == familyId && e.UserId == userId);
        if (foundFamily == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);
        
        var foundDebts = await _context.Debts
            .Where(d => d.UserFamily.UserId == userId)
            .ProjectTo<DebtDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return ResultTuple.Success(foundDebts);
    }
}