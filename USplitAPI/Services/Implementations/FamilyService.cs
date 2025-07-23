using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using USplitAPI.Data;
using USplitAPI.Domain;
using USplitAPI.Dtos;
using USplitAPI.Helpers;
using USplitAPI.Services.Interfaces;

namespace USplitAPI.Services.Implementations;

public class FamilyService : IFamilyService
{
    private readonly USplitDBContext _context;
    private readonly IMapper _mapper;

    public FamilyService(USplitDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ResultTuple> AddFamilyAsync(int ownerUserId, string name)
    {
        var familyToAdd = new FamilyEntity()
        {
            Name = name,
            OwnerUserId = ownerUserId,
        };
        var addedFamily = await _context.Families.AddAsync(familyToAdd);

        await _context.SaveChangesAsync();

        var addedFamilyDto = _mapper.Map<FamilyDto>(addedFamily.Entity);
        
        return ResultTuple.Success(addedFamilyDto);
    }

    public async Task<ResultTuple> RemoveFamilyAsync(int ownerUserId, int familyId)
    {
        var foundFamily = await _context.Families.SingleOrDefaultAsync(e => e.Id == familyId);
        if (foundFamily == null) return ResultTuple.Exception(StatusCodes.Status404NotFound);
        
        if (foundFamily.OwnerUserId != ownerUserId) return ResultTuple.Exception(StatusCodes.Status401Unauthorized);

        var removedFamily = _context.Families.Remove(foundFamily);

        await _context.SaveChangesAsync();

        var removedFamilyDto = _mapper.Map<FamilyDto>(removedFamily.Entity);
        
        return ResultTuple.Success(removedFamilyDto);
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