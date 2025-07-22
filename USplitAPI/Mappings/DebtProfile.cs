using AutoMapper;
using USplitAPI.Domain;
using USplitAPI.Dtos;

namespace USplitAPI.Mappings;

public class DebtProfile : Profile
{
    public DebtProfile()
    {
        CreateMap<DebtEntity, DebtDto>();
        CreateMap<DebtDto, DebtEntity>();
    }
}