using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> LoginAsync(Login model);
        Task<bool> RegisterAsync(Register model);
    }
}
