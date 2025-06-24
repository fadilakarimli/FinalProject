using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class TourDetailController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IExperienceService _experienceService;
        private readonly HttpClient _httpClient;

        public TourDetailController(ITourService tourService, IExperienceService experienceService)
        {
            _tourService = tourService;

            // HttpClient-i burada yarat
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145") // API URL-ni düzgün yaz
            };
            _experienceService = experienceService;
        }
        public async Task<IActionResult> Index(int id, string search)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            // Review-ları API-dən al
            List<ReviewCreateVM> reviews = new List<ReviewCreateVM>();
            var reviewResponse = await _httpClient.GetAsync($"https://localhost:7145/api/Review/GetByTourId/tour/{id}");
            if (reviewResponse.IsSuccessStatusCode)
            {
                var reviewJson = await reviewResponse.Content.ReadAsStringAsync();
                reviews = JsonSerializer.Deserialize<List<ReviewCreateVM>>(reviewJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            // Experience-ləri xidmət vasitəsilə al
            var experiences = await _experienceService.GetByTourIdAsync(id);

            var vm = new TourDetailVM
            {
                Tour = tour,
                SearchTerm = search ?? string.Empty,
                Reviews = reviews,
                Experiences = experiences.ToList()  // burada doldur
            };

            return View(vm);
        }

    }
}
