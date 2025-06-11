using FinalProjectConsume.Models.AboutAgency;
using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Contact;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FinalProjectConsume.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient = new();
        private readonly IHttpClientFactory _httpClientFactory;
        public ContactController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(string search)
        {
            var model = new ContactVM
            {
                SearchTerm = search ?? string.Empty
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(ContactVM postContactVM)
        {
            using (var multipartContent = new MultipartFormDataContent())
            {
                multipartContent.Add(new StringContent(postContactVM.email), "Email");
                multipartContent.Add(new StringContent(postContactVM.phoneNumber), "PhoneNumber");
                multipartContent.Add(new StringContent(postContactVM.fullName), "FullName");
                multipartContent.Add(new StringContent(postContactVM.message), "Message");
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(postContactVM);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responseMessage = await _httpClient.PostAsync("https://localhost:7145/api/Contact/CreateMessage", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorContent = await responseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the message.");
                    return View(postContactVM);
                }

            }

        }
    }
}
