using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IPvaMakeService
    {
        Task<IEnumerable<PvaMakeDto>> GetMakeDetailsAsync();
        Task<PvaMakeDto> AddMakeAsync(PvaMakeDto newStateDto);
        Task<IActionResult> DeleteMakeAsync(int id);
        Task<PvaMakeDto> UpdateMakeAsync(int id, PvaMakeDto updatedStateDto);
        Task<PvaMakeDto> GetMakeByIdAsync(int id);
    }
}
