using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FinalProjectConsume.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IAmenityService _amenityService;
        private readonly IActivityService _activityService;
        private readonly ICityService _cityService;
        private readonly HttpClient _httpClient;

        public TourController(ITourService tourService
                           , IAmenityService amenityService,
                             IActivityService activityService,
                             ICityService cityService)
        {
            _tourService = tourService;
            _amenityService = amenityService;
            _activityService = activityService;
            _cityService = cityService;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")
            };
        }
        public async Task<IActionResult> Index(string search)
        {
            var amenities= await _amenityService.GetAllAsync();
            var activity = await _activityService.GetAllAsync();    
            var city = await _cityService.GetAllAsync();
            var tours = await _tourService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Adına görə filter et
                tours = tours.Where(t => t.Name != null && t.Name
                                          .ToLower()
                                          .Contains(search.Trim().ToLower()));
            }
            var model = new TourPageVM
            {
                Tours = tours.ToList(),
                Amenities = amenities.ToList(),
                Activities = activity.ToList(),
                Cities = city.ToList(),
                SearchTerm = search ?? string.Empty
            };

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> SearchByName(string? search)
        {
            var searchQuery = search ?? string.Empty;
            var searchResponse = await _httpClient.GetAsync($"/api/client/Tour/SearchByName?name={searchQuery}");
            var searchJson = await searchResponse.Content.ReadAsStringAsync();
            var searchObject = JsonSerializer.Deserialize<List<TourPageVM>>(searchJson);

            return Json(searchObject);
        }

        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] TourFilter filter)

        {
            var json = JsonSerializer.Serialize(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7145/api/Tour/Filter/filter", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Filter failed");
            }
            Console.WriteLine($"Filter status: {response.StatusCode}");
            var resultJson = await response.Content.ReadAsStringAsync();
            return Content(resultJson, "application/json");
        }



    }
}
