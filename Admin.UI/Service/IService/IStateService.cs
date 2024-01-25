using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IStateService
    {
        Task<IEnumerable<StateDto>> GetStateDetailsAsync();
        Task<StateDto> AddStateAsync(StateDto newStateDto);
        Task<StateDto> DeleteStateAsync(int id);
        Task<StateDto> UpdateStateAsync(int id, StateDto updatedStateDto);
        Task<StateDto> GetStateByIdAsync(int id);
    }
}
