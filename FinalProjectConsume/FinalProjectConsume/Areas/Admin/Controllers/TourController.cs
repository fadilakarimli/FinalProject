using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TourController : Controller
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }

        public IActionResult Create()
        {
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
                Id = tour.Id,
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
