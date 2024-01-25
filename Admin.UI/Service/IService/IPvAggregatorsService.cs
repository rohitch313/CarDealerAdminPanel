
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IAggregatorsService
    {
        Task<IEnumerable<PvAggregatorsDto>> GetAggregatorDetailsAsync();
        Task<PvAggregatorsDto> DeleteAggregatorAsync(int id);
        Task<PvAggregatorsDto> GetAggregatorByIdAsync(int id);
    }
}
