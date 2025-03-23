using AutoMapper;
using Banking_system.DAL.Enums.Loan;
using Banking_system.DAL.Model;
using Banking_system.DTO_s.LoanDto_s;

namespace Banking_system.PL.AutoMapperProfiles
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
