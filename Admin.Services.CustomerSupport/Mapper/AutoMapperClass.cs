using Admin.Services.CustomerSupport.Business_Layer;
using Admin.Services.CustomerSupport.Models;
using AutoMapper;

namespace Admin.Services.CustomerSupport.Mapper
{
    public class AutoMapperClass : Profile
    {
        public AutoMapperClass()
        {
            CreateMap<CustomerSupporttbl, CustomerSupportDto>().ReverseMap();

        }
    }
}
