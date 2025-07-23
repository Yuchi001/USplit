using USplitAPI.Helpers;

namespace USplitAPI.Services.Interfaces;

public interface IFamilyService
{
    Task<ResultTuple> AddFamilyAsync(int ownerUserId, string name);

    Task<ResultTuple> RemoveFamilyAsync(int ownerUserId, int familyId);

    Task<ResultTuple> GetUserDebtsAsync(int familyId, int userId);

    Task<ResultTuple> GetFamilyAsync(int familyId);
}