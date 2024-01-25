using Admin.UI.Models;
using Admin.UI.Service.IService;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;


namespace Admin.UI.Service
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CarService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<CarDto> AddCarAsync(CarDto newCarDto)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }

                // Set up HttpClient with the token in the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7017/api/Car/AddCar", newCarDto);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CarDto>(responseBody);
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

        public async Task<CarDto> DeleteCarAsync(int id)
        {
            try
            {

                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }

                // Set up HttpClient with the token in the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7017/api/Car/Delete/{id}");

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

                // Assuming CarDto is the expected type, deserialize the response content to CarDto
                CarDto carDto = JsonConvert.DeserializeObject<CarDto>(await response.Content.ReadAsStringAsync());

                return carDto;
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


        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7017/api/Car/GetCarById{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CarDto>(responseBody);
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

        public async Task<IEnumerable<CarDto>> GetCarDetailsAsync()
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

                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7017/api/Car/GetAllCar");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the response body to CarDto objects
                    return JsonConvert.DeserializeObject<List<CarDto>>(responseBody) ?? new List<CarDto>();
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
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


        public async Task<CarDto> UpdateCarAsync(int id, CarDto carDto)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where token is missing or not retrieved properly
                    throw new Exception("Token not found or invalid.");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7017/api/Car/Update{id}", carDto);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CarDto>(responseBody);
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
