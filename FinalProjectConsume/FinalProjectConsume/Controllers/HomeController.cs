using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalProjectConsume.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IInstagramService _instagramService;

        public HomeController(IBrandService brandService, IInstagramService instagramService)
        {
            _brandService = brandService;
            _instagramService = instagramService;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();

            var brandVMs = brands.Select(b => new Brand
            {
                Id = b.Id,
                Image = b.Image
            }).ToList();
            //instagram
            var instagramVMs = instagrams.ToList();

            var model = new HomePageVM
            {
                Brands = brandVMs,
                Instagrams = instagramVMs
            };

            return View(model);
        }
    }
}
