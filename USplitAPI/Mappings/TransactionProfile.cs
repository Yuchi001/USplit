using AutoMapper;
using USplitAPI.Domain;
using USplitAPI.Dtos;

namespace USplitAPI.Mappings;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionEntity, TransactionDto>();
        CreateMap<TransactionDto, TransactionEntity>();
    }
}