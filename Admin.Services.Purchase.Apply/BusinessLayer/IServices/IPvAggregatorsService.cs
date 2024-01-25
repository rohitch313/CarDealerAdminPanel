using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IAggregatorsService
    {
        Task<IEnumerable<PvAggregatorsDto>> GetAggregatorDetailsAsync();
        Task<IActionResult> DeleteAggregatorAsync(int id);
        Task<PvAggregatorsDto> GetAggregatorByIdAsync(int id);
    }
}
