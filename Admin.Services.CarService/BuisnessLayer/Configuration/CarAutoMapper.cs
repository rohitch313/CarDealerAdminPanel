using AutoMapper;
using Admin.Services.CarService.DataLayer.Models;

namespace Admin.Services.CarService.BuisnessLayer.Configuration
{
    public class CarAutoMapper: Profile
    {
        public CarAutoMapper()
        {
            CreateMap<Car,CarDto>().ReverseMap();
        }
    }
}
