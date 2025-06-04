using FinalProjectConsume.Models.Account;
using FinalProjectConsume.Services.Interfaces;

namespace FinalProjectConsume.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/Account/";
        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> LoginAsync(Login model)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "Login", model);
            return response;
        }


        public async Task<bool> RegisterAsync(Register model)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "Register", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> VerifyEmailAsync(string email, string token)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/Account/VerifyEmail?verifyEmail={email}&token={token}");

            return await response.Content.ReadAsStringAsync(); 
        }


    }
}
