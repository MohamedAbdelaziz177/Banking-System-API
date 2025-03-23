using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DTO_s;
using Banking_system.DTO_s.AuthDto;
using Banking_system.DTO_s.RoleDto_s;

namespace Banking_system.AutoMapperProfiles
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile() 
        {
            CreateMap<RegisterDto, AppUser>().ReverseMap();
            CreateMap<LoginDto, AppUser>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();  
        }
    }
}
