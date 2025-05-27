using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Services
{
    public class NewsLetterService : INewsLetterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/NewsLetter/";

        public NewsLetterService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(NewsLetterCreate model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"{_baseUrl}AddEmail", content);
        }
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete/{id}");
        }
        public async Task<IEnumerable<NewsLetter>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NewsLetter>>($"{_baseUrl}GetAll");
        }
    }
}
