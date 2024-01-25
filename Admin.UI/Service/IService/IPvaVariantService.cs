
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IPvaVariantService
    {
        Task<IEnumerable<PvaVariantDto>> GetVariantDetailsAsync();
        Task<PvaVariantDto> AddVariantAsync(PvaVariantDto newStateDto);
        Task<PvaVariantDto> DeleteVariantAsync(int id);
        Task<PvaVariantDto> UpdateVariantAsync(int id, PvaVariantDto updatedStateDto);
        Task<PvaVariantDto> GetVariantByIdAsync(int id);
    }
}
