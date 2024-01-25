using AdminService.DataLayer.Models;
using AutoMapper;


namespace AdminService.BuisnessLayer.Configuration
{
    public class AdminAutoMapper: Profile
    {
        public AdminAutoMapper()
        {
            CreateMap<AdminTable,AdminDto>().ReverseMap();
        }
    }
}
