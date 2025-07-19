using AutoMapper;
using USplitAPI.Domain;
using USplitAPI.Dtos;

namespace USplitAPI.Mappings;

public class CurrentBalanceProfile : Profile
{
    public CurrentBalanceProfile()
    {
        CreateMap<CurrentBalanceEntity, CurrentBalanceDto>();
        CreateMap<CurrentBalanceDto, CurrentBalanceEntity>();
    }
}