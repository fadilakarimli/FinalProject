using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class DestinationController : Controller
    {
        private readonly ITrandingDestinationService _trandingDestinationService;
        public DestinationController(ITrandingDestinationService trandingDestinationService)
        {
            _trandingDestinationService = trandingDestinationService;
        }
        public async Task <ActionResult> Index()
        {
            var trandingDestinatons = await _trandingDestinationService.GetAllAsync();

            var model = new DestinationPageVM
            {
                TrandingDestinations = trandingDestinatons.ToList(),
            };

            return View(model);
        }
    }
}
