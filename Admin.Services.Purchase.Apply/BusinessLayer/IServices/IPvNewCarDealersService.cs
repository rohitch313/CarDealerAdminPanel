using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IPvNewCarDealersService
    {
        Task<IEnumerable<PvNewCarDealersDto>> GetNewCarDetailsAsync();
        Task<IActionResult> DeleteNewCarAsync(int id);
        Task<PvNewCarDealersDto> GetNewCarByIdAsync(int id);
    }
}
