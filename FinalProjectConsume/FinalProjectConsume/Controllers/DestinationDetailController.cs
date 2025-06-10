using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class DestinationDetailController : Controller
    {
        private readonly ITrandingDestinationService _trandingDestinationService;
        public DestinationDetailController(ITrandingDestinationService trandingDestinationService)
        {
            _trandingDestinationService = trandingDestinationService;   
        }
        public async Task<IActionResult> Index(int id, string search)
        {
            var destination = await _trandingDestinationService.GetByIdAsync(id);
            if (destination == null) return NotFound();

            var vm = new DestinationDetailVM
            {
                TrandingDestination = destination,
                SearchTerm = search ?? string.Empty
            };

            return View(vm);
        }
    }
}
