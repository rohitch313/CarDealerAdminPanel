using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IPvaModelService
    {
        Task<IEnumerable<PvaModelDto>> GetModelDetailsAsync();
        Task<PvaModelDto> AddModelAsync(PvaModelDto newStateDto);
        Task<IActionResult> DeleteModelAsync(int id);
        Task<PvaModelDto> UpdateModelAsync(int id, PvaModelDto updatedStateDto);
        Task<PvaModelDto> GetModelByIdAsync(int id);
    }
}
