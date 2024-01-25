using Admin.UI.Models;

namespace Admin.UI.Service.IService
{
    public interface ICarService
    {
        Task<CarDto> AddCarAsync(CarDto newCarDto);
        Task<IEnumerable<CarDto>> GetCarDetailsAsync();
        Task<CarDto> DeleteCarAsync(int id);
        Task<CarDto> UpdateCarAsync(int id, CarDto carDto);
        Task<CarDto> GetCarByIdAsync(int id);
    }
}
