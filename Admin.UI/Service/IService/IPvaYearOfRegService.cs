
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IPvaYearOfRegService
    {
        Task<IEnumerable<PvaYearOfRegDto>> GetYearDetailsAsync();
        Task<PvaYearOfRegDto> AddYearAsync(PvaYearOfRegDto newStateDto);
        Task<PvaYearOfRegDto> DeleteYearAsync(int id);
        Task<PvaYearOfRegDto> UpdateYearAsync(int id, PvaYearOfRegDto updatedStateDto);
        Task<PvaYearOfRegDto> GetYearByIdAsync(int id);
    }
}
