using Admin.UI.Models;
using Admin.UI.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Admin.UI.Service
{
    public class AggregatorsService : IAggregatorsService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AggregatorsService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {

            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
         public async Task<PvAggregatorsDto> DeleteAggregatorAsync(int id)
        {
            try
            {
                // Get the token from the session
                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }

                // Set up HttpClient with the token in the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7024/api/PvAggregatorsAPI/Delete/{id}");

                // Check if the response indicates a failure (non-success status code)
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Handle 404 Not Found
                        // You can return null or throw a custom exception, or handle it according to your application's logic
                        return null;
                    }

                    // Handle other non-success status codes if needed
                    // For example, you might throw a custom exception with details from the response
                    throw new HttpRequestException($"HTTP request error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvAggregatorsDto>(responseBody);
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

        public async Task<PvAggregatorsDto> GetAggregatorByIdAsync(int id)
        {
            try
            {
                // Get the token from the session
                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }

                // Set up HttpClient with the token in the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7024/api/PvAggregatorsAPI/GetAggregatorById/{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvAggregatorsDto>(responseBody);
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

        public async Task<IEnumerable<PvAggregatorsDto>> GetAggregatorDetailsAsync()
        {
            try
            {
                // Get the token from the session
                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }

                // Set up HttpClient with the token in the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7024/api/PvAggregatorsAPI/GetAllAggregators");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PvAggregatorsDto>>(responseBody);
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
