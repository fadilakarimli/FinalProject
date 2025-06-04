using FinalProjectConsume.Models.Account;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new Login());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginSuccess = await _accountService.LoginAsync(model);
            var content = await loginSuccess.Content.ReadAsStringAsync();
            if (!loginSuccess.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                return View(model);
            }
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (loginResponse != null && loginResponse.Success)
            {
                // SESSION 
                //HttpContext.Session.SetString("AuthToken", loginResponse.Token);
                //HttpContext.Session.SetString("UserName", loginResponse.UserName ?? "");
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, loginResponse.UserName ?? "")
                //};

                //if (loginResponse.Roles != null && loginResponse.Roles.Any())
                //{
                //    foreach (var role in loginResponse.Roles)
                //    {
                //        claims.Add(new Claim(ClaimTypes.Role, role));
                //    }
                //}

                var identity = new ClaimsIdentity("CookieAuth");
                identity.AddClaim(new Claim(ClaimTypes.Name, model.UserNameOrEmail));  

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, loginResponse?.Error ?? "Login failed.");
                return View(model);
            }




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
