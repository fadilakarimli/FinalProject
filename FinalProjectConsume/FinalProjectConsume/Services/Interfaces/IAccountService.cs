using FinalProjectConsume.Models.Account;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(Login model);
        Task<bool> RegisterAsync(Register model);
        Task<string> VerifyEmailAsync(string email, string token);
    }
}
