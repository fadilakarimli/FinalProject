using FinalProjectConsume.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145") // Sənin API URL
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7145/api/Review/GetAll");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var json = await response.Content.ReadAsStringAsync();
            var reviews = JsonSerializer.Deserialize<List<ReviewListVM>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(reviews);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/review/delete/{id}");
            return RedirectToAction(nameof(Index));
        }


    }
}
