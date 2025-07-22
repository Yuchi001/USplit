using AutoMapper;
using USplitAPI.Domain;
using USplitAPI.Dtos;

namespace USplitAPI.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserEntity, DebtDto.UserShortDto>();
        CreateMap<UserDto, UserEntity>();
    }
}