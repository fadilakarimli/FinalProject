using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IActivityService _activityService;
        private readonly IAmenityService _amenityService;
        private readonly ICountryService _countryService;
        private readonly IExperienceService _experienceService;
        private readonly ICityService _cityService;

        public TourController(ITourService tourService , IAmenityService amenityService , IActivityService activityService, ICountryService countryService
                           , IExperienceService experienceService, ICityService cityService)
        {
            _tourService = tourService;
            _activityService = activityService;
            _amenityService = amenityService;
            _countryService = countryService;
            _experienceService = experienceService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var activities = await _activityService.GetAllAsync();
            var cities = await _cityService.GetAllAsync();
            var amenities = await _amenityService.GetAllAsync();
            var countries = await _countryService.GetAllAsync();
            //var experiences = await _experienceService.GetAllAsync(); // 🔹 YENİ

            ViewBag.Cities = cities
                 .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                 .ToList();

            ViewBag.Activities = activities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            ViewBag.Amenities = amenities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            ViewBag.Countries = countries
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            //ViewBag.Experiences = experiences
            //    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            //    .ToList(); // 🔹 ƏLAVƏ

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]TourCreate model)
        {
            if (!ModelState.IsValid)
            {
                // Eyni ViewBag-ləri POST-da da doldur:
                var activities = await _activityService.GetAllAsync();
                var amenities = await _amenityService.GetAllAsync();
                var countries = await _countryService.GetAllAsync();
                //var experiences = await _experienceService.GetAllAsync();


                ViewBag.Activities = activities
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();

                ViewBag.Amenities = amenities
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();

                ViewBag.Countries = countries
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();

                //ViewBag.Experiences = experiences
                //    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                //    .ToList();

                return View(model);
            }

            // Əgər model düzgünsə:
            await _tourService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
    
        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            var model = new TourEdit
            {
                Name = tour.Name,
                Duration = tour.Duration,
                CountryCount = tour.CountryCount,
                Price = tour.Price,
                OldPrice = tour.OldPrice,
                Capacity = tour.Capacity 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TourEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _tourService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Yeniləmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _tourService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);

            if (tour == null)
                return NotFound();

            if (!string.IsNullOrEmpty(tour.StartDate))
            {
                if (DateTime.TryParseExact(tour.StartDate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var startDate))
                {
                    Console.WriteLine($"Parsed StartDate: {startDate}");
                }
            }

            if (!string.IsNullOrEmpty(tour.EndDate))
            {
                if (DateTime.TryParseExact(tour.EndDate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var endDate))
                {
                    Console.WriteLine($"Parsed EndDate: {endDate}");
                }
            }

            return View(tour);
        }


        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var cities = await _cityService.GetAllAsync(); // Şəhər siyahısı
            var activities = await _activityService.GetAllAsync(); // Fəaliyyət siyahısı

            var vm = new TourSearchRequest
            {
                AvailableCities = cities,      // bunu TourSearchRequest-də əlavə elə
                AvailableActivities = activities // eyni şəkildə buraya da
            };

            return View(vm);
        }



        [HttpGet]
        public async Task<IActionResult> SearchResults(List<int> CityIds, List<int> ActivityIds, int? Capacity, DateTime? StartDate)
        {
            var searchRequest = new TourSearchRequest
            {
                //Name = Name,
                CityIds = CityIds ?? new List<int>(),
                ActivityIds = ActivityIds ?? new List<int>(),
                Capacity = Capacity,
                StartDate = StartDate,
            };

            var results = await _tourService.SearchAsync(searchRequest);

            return View(results);
        }



    }
}
