using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.BusinessLayer.IServices
{
    public interface IPvaYearOfRegService
    {
        Task<IEnumerable<PvaYearOfRegDto>> GetYearDetailsAsync();
        Task<PvaYearOfRegDto> AddYearAsync(PvaYearOfRegDto newStateDto);
        Task<IActionResult> DeleteYearAsync(int id);
        Task<PvaYearOfRegDto> UpdateYearAsync(int id, PvaYearOfRegDto updatedStateDto);
        Task<PvaYearOfRegDto> GetYearByIdAsync(int id);
    }
}
