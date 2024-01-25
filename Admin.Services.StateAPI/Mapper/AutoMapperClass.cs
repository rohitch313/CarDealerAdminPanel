using Admin.Services.StateAPI.Models.DTO;
using AutoMapper;

namespace Admin.Services.StateAPI.Mapper
{
    public class AutoMapperClass: Profile
    {
        public AutoMapperClass() 
        {
            CreateMap<Statetbl, StatetDto>().ReverseMap();

        }
    }
}
