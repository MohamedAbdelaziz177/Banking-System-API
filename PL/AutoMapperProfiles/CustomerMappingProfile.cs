using AutoMapper;
using Banking_system.DAL.Model;
using Banking_system.DTO_s.CustomerDto_s;

namespace Banking_system.PL.AutoMapperProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {

            CreateMap<Customer, CustomerCreateDto>().ReverseMap();

            CreateMap<CustomerReadDto, Customer>().ReverseMap();

            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
        }
    }
}
