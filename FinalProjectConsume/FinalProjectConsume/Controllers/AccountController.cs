using FinalProjectConsume.Helpers;
using FinalProjectConsume.Models.Account;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IAccountService _accountService;
        private readonly string _apiBaseUrl = "/api";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {

            PropertyNameCaseInsensitive = true
        };
        public AccountController(IAccountService accountService, IHttpClientFactory httpClientFactory, HttpClient httpClient, IConfiguration configuration)
        {
            _accountService = accountService;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;

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
            var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(content, new JsonSerializerOptions
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

        [HttpGet]

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
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email cannot be empty.");
                return View();
            }

            var requestUri = "https://localhost:7145/api/Account/ForgetPassword";
            var response = await _httpClient.PostAsJsonAsync(requestUri, email);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObj = System.Text.Json.JsonSerializer.Deserialize<ResponseObject>(responseContent, options);
                TempData["Message"] = responseObj.ResponseMessage;
                return RedirectToAction("ForgetPasswordConfirmation");
            }   
            else
            {
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseObject>();
                ModelState.AddModelError("", responseObj.ResponseMessage);
                return View();
            }
        }

        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            // Pre-fill the form with email and token if provided
            var model = new UserPasswordVM
            {
                email = email,
                token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserPasswordVM userPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userPasswordVM);
            }

            var requestUri = "https://localhost:7145/api/Account/ResetPassword";
            var response = await _httpClient.PostAsJsonAsync(requestUri, userPasswordVM);

            if (response.IsSuccessStatusCode)
            {
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseObject>();
                TempData["Message"] = responseObj.ResponseMessage;
                return RedirectToAction("ResetPasswordConfirmation");
            }
            else
            {
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseObject>(); 
                ModelState.AddModelError("", responseObj.ResponseMessage);
                return View(userPasswordVM);
            }
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }



    }
}
