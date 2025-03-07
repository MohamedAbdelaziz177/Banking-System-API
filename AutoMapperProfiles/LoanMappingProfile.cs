using AutoMapper;
using Banking_system.DTO_s.CardDto_s;
using Banking_system.DTO_s.LoanDto_s;
using Banking_system.Enums.Card;
using Banking_system.Enums.Loan;
using Banking_system.Model;

namespace Banking_system.AutoMapperProfiles
{
    public class LoanMappingProfile : Profile
    {
        public LoanMappingProfile() 
        {
            
            CreateMap<LoanReadDto, Loan>()
                .ForMember(dest => dest.loanStatus,
                opt => opt.MapFrom(src => Enum.Parse<LoanStatus>(src.loanStatus, true)))
                .ReverseMap();

            CreateMap<LoanCreateDto, Loan>().ReverseMap();

            CreateMap<LoanUpdateDto, Loan>()
                 .ForMember(dest => dest.loanStatus,
                opt => opt.MapFrom(src => Enum.Parse<LoanStatus>(src.loanStatus, true)))
                .ReverseMap();
        }
    }
}
