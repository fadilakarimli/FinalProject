using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class AboutController : Controller
    {
        private readonly IBrandService _brandService;


        public AboutController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task <IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var model = new AboutPageVM
            {
                Brands = brands.ToList(),
            };

            return View(model);
        }
    }
}
