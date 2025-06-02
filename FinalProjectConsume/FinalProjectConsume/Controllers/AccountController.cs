using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FinalProjectConsume.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var token = await _accountService.LoginAsync(request);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var resgister = await _accountService.RegisterAsync(request);
            if (!resgister)
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
                return View(request);
            }
            return RedirectToAction("Login");
        }
    }
}
