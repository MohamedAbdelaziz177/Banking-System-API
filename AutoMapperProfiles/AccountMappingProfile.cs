using AutoMapper;
using Banking_system.DTO_s.AccountDto_s;
using Banking_system.Enums.Account;
using Banking_system.Model;

namespace Banking_system.AutoMapperProfiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            // Kosom Git
            CreateMap<AccountCreateDto, Account>()
                .ForMember(dest => dest.accountType,
                option => option.MapFrom(src => Enum.Parse<AccountType>(src.AccountType, true)))

                .ReverseMap();

            CreateMap<AccountReadDto, Account>()
                .ForMember(dest => dest.accountType,
                option => option.MapFrom(src => Enum.Parse<AccountType>(src.AccountType, true)))
                .ForMember(dest => dest.accountStatus,
                option => option.MapFrom(src => Enum.Parse<AccountStatus>(src.AccountStatus, true)))
                .ReverseMap();

            CreateMap<AccountUpdateDto, Account>()
                .ForMember(dest => dest.accountType,
                opt => opt.MapFrom(src => Enum.Parse<AccountType>(src.AccountType, true)))
                .ForMember(dest => dest.accountStatus,
                option => option.MapFrom(src => Enum.Parse<AccountStatus>(src.AccountStatus, true)))
                .ReverseMap();


        }

    }
}
