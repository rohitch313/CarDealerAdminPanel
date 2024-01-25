
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IPvNewCarDealersService
    {
        Task<IEnumerable<PvNewCarDealersDto>> GetNewCarDetailsAsync();
        Task<PvNewCarDealersDto> DeleteNewCarAsync(int id);
        Task<PvNewCarDealersDto> GetNewCarByIdAsync(int id);
    }
}
