using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IPvaVariantService
    {
        Task<IEnumerable<PvaVariantDto>> GetVariantDetailsAsync();
        Task<PvaVariantDto> AddVariantAsync(PvaVariantDto newStateDto);
        Task<IActionResult> DeleteVariantAsync(int id);
        Task<PvaVariantDto> UpdateVariantAsync(int id, PvaVariantDto updatedStateDto);
        Task<PvaVariantDto> GetVariantByIdAsync(int id);
    }
}
