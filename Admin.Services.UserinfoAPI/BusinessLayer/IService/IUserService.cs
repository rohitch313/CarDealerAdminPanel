using Admin.Services.UserinfoAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.UserinfoAPI.BusinessLayer.IService
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<UsersDto>>> GetUserDetailsAsync();
        Task<IActionResult> DeleteUserAsync(int id);
        Task<UsersDto> UpdateUserAsync(int id, UsersDto updatedStateDto);
        Task<UsersDto> GetUserByIdAsync(int id);
    }
}
