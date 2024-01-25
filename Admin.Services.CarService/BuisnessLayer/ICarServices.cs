using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.CarService.BuisnessLayer
{
    public interface ICarServices
    {
      

        Task<IEnumerable<CarDto>> GetCarDetailsAsync();
        Task<CarDto> AddCarAsync(CarDto newStateDto);
        Task<IActionResult> DeleteCarAsync(int id);
        Task<CarDto> UpdateCarAsync(int id, CarDto updatedCarDto);
        Task<CarDto> GetCarByIdAsync(int id);
    }
}
