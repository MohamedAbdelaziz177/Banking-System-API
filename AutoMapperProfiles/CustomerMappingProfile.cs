using AutoMapper;
using Banking_system.DTO_s.CustomerDto_s;
using Banking_system.Model;

namespace Banking_system.AutoMapperProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile() {

            CreateMap<Customer, CustomerCreateDto>().ReverseMap();

            CreateMap<CustomerReadDto, Customer>().ReverseMap();

            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
        }
    }
}
