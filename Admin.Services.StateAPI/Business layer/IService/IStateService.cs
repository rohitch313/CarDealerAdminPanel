using Admin.Services.StateAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.StateAPI.Business_layer.IService
{
    public interface IStateService
    {
        Task<IEnumerable<StatetDto>> GetStateDetailsAsync();
        Task<StatetDto> AddStateAsync(StatetDto newStateDto);
        Task<IActionResult> DeleteStateAsync(int id);
        Task<StatetDto> UpdateStateAsync(int id, StatetDto updatedStateDto);
        Task<StatetDto> GetStateByIdAsync(int id);
    }
}
