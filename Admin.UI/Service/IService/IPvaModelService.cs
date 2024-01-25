
using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IPvaModelService
    {
        Task<IEnumerable<PvaModelDto>> GetModelDetailsAsync();
        Task<PvaModelDto> AddModelAsync(PvaModelDto newStateDto);
        Task<PvaModelDto> DeleteModelAsync(int id);
        Task<PvaModelDto> UpdateModelAsync(int id, PvaModelDto updatedStateDto);
        Task<PvaModelDto> GetModelByIdAsync(int id);
    }
}
