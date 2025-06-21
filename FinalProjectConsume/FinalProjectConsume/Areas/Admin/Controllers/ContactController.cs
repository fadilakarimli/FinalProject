using FinalProjectConsume.Models.Contact;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Admin/Contact
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7145/api/admin/Contact/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "An error occurred while loading messages.");
                return View(new List<ContactVM>());  // ContactVM tipi ilə boş list göndər
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var contactList = JsonConvert.DeserializeObject<List<PostContact>>(jsonData);

            // Bunu əlavə et: PostContact -> ContactVM map
            var viewModelList = contactList.Select(x => new ContactVM
            {
                id = x.id , // Əgər Id property-ni əlavə etmisənsə (əvvəlki cavabda demişdim)
                email = x.email,
                phoneNumber = x.phoneNumber,
                fullName = x.fullName,
                message = x.message
            }).ToList();

            return View(viewModelList);
        }


        // GET: Admin/Contact/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7145/api/admin/Contact/GetById/{id}"); // endpoint sənin API-nə uyğun olmalıdır
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Mesaj tapılmadı.");
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<ContactVM>(jsonData);
            return View(contact);
        }

        // Areas/Admin/Controllers/ContactController.cs

        [HttpPost]
        public async Task<IActionResult> Reply(int id, string replyMessage)
        {
            if (string.IsNullOrWhiteSpace(replyMessage))
            {
                ModelState.AddModelError("", "Reply message cannot be empty.");
                return RedirectToAction("Details", new { id });
            }

            var client = _httpClientFactory.CreateClient();

            // Query string ilə göndəririk
            var requestUrl = $"https://localhost:7145/api/admin/Contact/Reply?contactId={id}&replyMessage={Uri.EscapeDataString(replyMessage)}";

            var response = await client.PostAsync(requestUrl, null); // Body yox, query parametrlərlə POST

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to reply to the message.");
                return RedirectToAction("Details", new { id });
            }

            TempData["Success"] = "Reply sent successfully!";
            return RedirectToAction("Details", new { id });
        }

    }
}
