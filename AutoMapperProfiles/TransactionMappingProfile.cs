using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DTO_s.TransactionDto_s;
using Banking_system.Enums.Transactions;

namespace Banking_system.AutoMapperProfiles
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile() 
        {
            CreateMap<TransactionCreateDto, Transaction>()
                .ForMember(dest => dest.TrxType,
                opt => opt.MapFrom(src => Enum.Parse<TransactionType>(src.TrxType, true)))
                .ReverseMap();

            CreateMap<TransactionReadDto, Transaction>()
                .ForMember(dest => dest.TrxType,
                opt => opt.MapFrom(src => Enum.Parse<TransactionType>(src.TrxType, true)))
                .ReverseMap();
        }
    }
}
