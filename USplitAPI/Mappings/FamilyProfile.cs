using AutoMapper;
using USplitAPI.Domain;
using USplitAPI.Dtos;

namespace USplitAPI.Mappings;

public class FamilyProfile : Profile
{
    public FamilyProfile()
    {
        CreateMap<FamilyEntity, FamilyDto>();
        CreateMap<FamilyDto, FamilyEntity>();
    }
}