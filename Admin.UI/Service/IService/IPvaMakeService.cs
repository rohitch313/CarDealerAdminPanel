
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IPvaMakeService
    {
        Task<IEnumerable<PvaMakeDto>> GetMakeDetailsAsync();
        Task<PvaMakeDto> AddMakeAsync(PvaMakeDto newStateDto);
        Task<PvaMakeDto> DeleteMakeAsync(int id);
        Task<PvaMakeDto> UpdateMakeAsync(int id, PvaMakeDto updatedStateDto);
        Task<PvaMakeDto> GetMakeByIdAsync(int id);
    }
}
