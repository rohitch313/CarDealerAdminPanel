using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfoDto>> GetUserDetailsAsync();
        Task<UserInfoDto> DeleteUserAsync(int id);
        Task<UserInfoDto> UpdateUserAsync(int id, UserInfoDto updatedStateDto);
        Task<UserInfoDto> GetUserByIdAsync(int id);
        
    }
}
