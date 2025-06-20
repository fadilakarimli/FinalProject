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
        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            int take = 6; // Səhifədə neçə tur göstəriləcək

            var amenities = await _amenityService.GetAllAsync();
            var activities = await _activityService.GetAllAsync();
            var cities = await _cityService.GetAllAsync();

            IEnumerable<Tour> tours = await _tourService.GetAllAsync();

            // Axtarış varsa filterləyirik
            if (!string.IsNullOrWhiteSpace(search))
            {
                tours = tours.Where(t => t.Name != null && t.Name
                                         .ToLower()
                                         .Contains(search.Trim().ToLower()));
            }

            // Səhifələmə üçün ümumi element sayı
            int totalTours = tours.Count();

            // Səhifələnmiş tur siyahısı
            var pagedTours = tours
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            // Səhifələmə üçün total səhifə sayı
            int totalPages = (int)Math.Ceiling(totalTours / (double)take);

            var model = new TourPageVM
            {
                Tours = pagedTours,
                Amenities = amenities.ToList(),
                Activities = activities.ToList(),
                Cities = cities.ToList(),
                SearchTerm = search ?? string.Empty,
                CurrentPage = page,
                TotalPages = totalPages
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

        //[HttpGet]
        //public async Task<IActionResult> Paginated(int page = 1)
        //{
        //    int take = 6;
        //    var pagedResult = await _tourService.GetPaginatedAsync(page, take);

        //    var model = new TourPageVM
        //    {
        //        Tours = pagedResult.Items.ToList(),
        //        CurrentPage = page,
        //        TotalPages = pagedResult.TotalPages
        //    };

        //    return View(model);
        //}




    }
}
