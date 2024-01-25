using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDto>> GetAdminDetailsAsync();
        Task<AdminDto> RegisterAdmin(AdminDto adminDTO);
        Task<(AdminDto, string)> LoginAdmin(AdminDto adminDTO);


    }
}
