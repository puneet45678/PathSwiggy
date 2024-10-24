using AutoMapper;
using CustomerApi.Models.Dto;
using CustomerApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Customer, CustomerReadDTO>();
            CreateMap<CustomerCreateDTO, Customer>();
        }
    }
}
