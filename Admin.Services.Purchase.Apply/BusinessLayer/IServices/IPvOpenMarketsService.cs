using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IPvOpenMarketsService
    {
        Task<IEnumerable<PvOpenMarketsDto>> GetOpenMarketDetailsAsync();
        Task<IActionResult> DeleteOpenMarketAsync(int id);
        Task<PvOpenMarketsDto> GetOpenMarketByIdAsync(int id);
    }
}
