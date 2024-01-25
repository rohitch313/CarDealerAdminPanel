using Microsoft.AspNetCore.Mvc;

namespace AdminService.BuisnessLayer
{
    public interface IAdminServices
    {
        Task<IEnumerable<AdminDto>> GetAdminDetailsAsync();
        Task<IActionResult> RegisterAdmin(AdminDto adminDTO);
        Task<IActionResult> LoginAdmin(AdminDto adminDTO);

    }
}
