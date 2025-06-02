using FinalProjectConsume.Services.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FinalProjectConsume.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Account/";
        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> LoginAsync(Login model)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "Login", model);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<bool> RegisterAsync(Register model)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "Register", model);
            return response.IsSuccessStatusCode;
        }
       
    }
}
