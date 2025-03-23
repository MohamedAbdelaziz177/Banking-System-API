using AutoMapper;
using Banking_system.DAL.Enums.Account;
using Banking_system.DAL.Model;
using Banking_system.DTO_s.AccountDto_s;

namespace Banking_system.PL.AutoMapperProfiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            // Fuck Git (-_-)
            CreateMap<AccountCreateDto, Account>()
                .ForMember(dest => dest.accountType,
                option => option.MapFrom(src => Enum.Parse<AccountType>(src.AccountType, true)))
                .ForMember(dest => dest.accountStatus,
                option => option.MapFrom(src => Enum.Parse<AccountStatus>(src.AccountStatus, true)))
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
