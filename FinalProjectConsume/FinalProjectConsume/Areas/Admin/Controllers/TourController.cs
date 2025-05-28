using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IActivityService _activityService;
        private readonly IAmenityService _amenityService;
        private readonly ICountryService _countryService;

        public TourController(ITourService tourService , IAmenityService amenityService , IActivityService activityService, ICountryService countryService)
        {
            _tourService = tourService;
            _activityService = activityService;
            _amenityService = amenityService;
            _countryService = countryService;
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
            var amenities = await _amenityService.GetAllAsync();
            var countries = await _countryService.GetAllAsync(); // <--- BURA ƏLAVƏ ET

            ViewBag.Activities = activities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            ViewBag.Amenities = amenities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            ViewBag.Countries = countries
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList(); // <--- VƏ BU SƏTRİ

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(TourCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _tourService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Tour yaradılarkən xəta baş verdi.");
            return View(model);
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
                CityId = tour.CityId,
                ExistingImageUrl = tour.ImageUrl
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

        public async Task<IActionResult> Detail(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();
            return View(tour);
        }
    }
}
