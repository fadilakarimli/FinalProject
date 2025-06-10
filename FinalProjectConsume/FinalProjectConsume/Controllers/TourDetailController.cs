using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TourDetailController : Controller
    {
        private readonly ITourService _tourService;

        public TourDetailController(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IActionResult> Index(int id, string search)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            var vm = new TourDetailVM
            {
                Tour = tour,
                SearchTerm = search ?? string.Empty

            };

            return View(vm);
        }
    }
}
