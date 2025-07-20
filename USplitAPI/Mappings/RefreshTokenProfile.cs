using AutoMapper;
using USplitAPI.Domain;
using USplitAPI.Dtos;

namespace USplitAPI.Mappings;

public class RefreshTokenProfile : Profile
{
    public RefreshTokenProfile()
    {
        CreateMap<RefreshTokenDto, RefreshTokenEntity>();
        CreateMap<RefreshTokenEntity, RefreshTokenDto>();
    }
}