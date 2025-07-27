using USplitAPI.Helpers;

namespace USplitAPI.Services.Interfaces;

public interface IFamilyService
{
    Task<ResultTuple> AddFamilyAsync(int ownerUserId, string name);

    Task<ResultTuple> RemoveFamilyAsync(int ownerUserId, int familyId);

    Task<ResultTuple> GetFamilyAsync(int familyId);

    Task<ResultTuple> AddMemberAsync(int ownerId, int familyId, int addUserId);

    Task<ResultTuple> GetMembers(int memberId, int familyId);
}