using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.UI.Service
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IEnumerable<AdminDto>> GetAdminDetailsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7213/api/Admin/RegisteredAdmin");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AdminDto>>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }


        public async Task<(AdminDto, string)> LoginAdmin(AdminDto adminDTO)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7213/api/Admin/login", adminDTO);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the response body to an object with a "token" property
                var tokenResponse = JsonConvert.DeserializeAnonymousType(responseBody, new { token = "" });

                // Deserialize the response body to AdminDto object
                var admin = JsonConvert.DeserializeObject<AdminDto>(responseBody);

                // Set the token in a cookie
                _httpContextAccessor.HttpContext.Response.Cookies.Append("Token", tokenResponse.token, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddHours(1), // Set expiration time if needed
                    HttpOnly = true, // Prevent JavaScript access
                    Secure = true, // Send over HTTPS
                    SameSite = SameSiteMode.Strict // Control cross-site behavior
                });

                return (admin, tokenResponse.token);
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task<AdminDto> RegisterAdmin(AdminDto adminDTO)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7213/api/Admin/register", adminDTO);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AdminDto>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}
