
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IPvOpenMarketsService
    {
        Task<IEnumerable<PvOpenMarketsDto>> GetOpenMarketDetailsAsync();
        Task<PvOpenMarketsDto> DeleteOpenMarketAsync(int id);
        Task<PvOpenMarketsDto> GetOpenMarketByIdAsync(int id);
    }
}
