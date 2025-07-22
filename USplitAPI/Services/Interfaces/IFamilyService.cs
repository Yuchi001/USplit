namespace USplitAPI.Services.Interfaces;

public interface IFamilyService
{
    Task<ResultTuple> AddFamilyAsync();

    Task<ResultTuple> RemoveFamilyAsync();
}