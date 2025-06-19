using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using FinalProjectConsume.Models.Booking;

namespace FinalProjectConsume.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")  
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingCreateVM vm)
        {
            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7145/api/Booking/CreateBooking", content);

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                var bookingResult = JsonSerializer.Deserialize<Booking>(resultJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }

}
