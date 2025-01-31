using AutoMapper;
using Banking_system.DTO_s;
using Banking_system.DTO_s.RoleDto_s;
using Banking_system.Model;

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
