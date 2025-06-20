using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class SettingService : ISettingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7145/api";

        public SettingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SettingVM>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/admin/Setting/GetAll");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<SettingVM>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
