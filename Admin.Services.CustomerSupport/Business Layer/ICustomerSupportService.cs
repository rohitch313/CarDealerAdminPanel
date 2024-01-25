using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.CustomerSupport.Business_Layer
{
    public interface ICustomerSupportService
    {
        Task<IEnumerable<CustomerSupportDto>> GetCrustSptDetailsAsync();
        Task<CustomerSupportDto> AddCrustSptAsync(CustomerSupportDto newStateDto);
        Task<IActionResult> DeleteCrustSptAsync(int id);
        Task<CustomerSupportDto> UpdateCrustSptAsync(int id, CustomerSupportDto updatedStateDto);
        Task<CustomerSupportDto> GetCrustSptByIdAsync(int id);
    }
}
