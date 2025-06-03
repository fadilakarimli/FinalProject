using FinalProjectConsume.Models.Account;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool loginSuccess = await _accountService.LoginAsync(model);
            if (loginSuccess)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.UserNameOrEmail),
        };

                var identity = new ClaimsIdentity(claims, "Login");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
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

            var registerResponse = await _accountService.RegisterAsync(request);

            if (!registerResponse)
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
                return View(request);
            }
            return View("VerifyEmail"); 
        }
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string verifyEmail, string token)
        {
            if (verifyEmail == null || token == null)
                return BadRequest("Invalid confirmation request.");

            var response = await _accountService.VerifyEmailAsync(verifyEmail, token);

            ViewBag.Message = response;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {   
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }


    }
}
