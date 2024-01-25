using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface ICustomerSupportService
    {
        Task<IEnumerable<CustomerSupportDto>> GetCrustSptDetailsAsync();
        Task<CustomerSupportDto> AddCrustSptAsync(CustomerSupportDto newStateDto);
        Task<CustomerSupportDto> DeleteCrustSptAsync(int id);
        Task<CustomerSupportDto> UpdateCrustSptAsync(int id, CustomerSupportDto updatedStateDto);
        Task<CustomerSupportDto> GetCrustSptByIdAsync(int id);


    }
}
