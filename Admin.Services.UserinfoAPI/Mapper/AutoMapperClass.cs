using Admin.Services.UserinfoAPI.Models;
using Admin.Services.UserinfoAPI.Models.DTO;
using AutoMapper;

namespace Admin.Services.UserinfoAPI.Mapper
{
    public class AutoMapperClass: Profile
    {
        public AutoMapperClass() 
        {
            CreateMap<Userstbl, UsersDto>().ReverseMap();

        }
    }
}
