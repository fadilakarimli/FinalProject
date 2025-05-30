using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;

namespace FinalProjectConsume.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IInstagramService _instagramService;
        private readonly IBlogService _blogService;
        private readonly IDestinationFeatureService _destinationFeatureService;
        private readonly ITeamMemberService _teamMemberService;
        private readonly ITrandingDestinationService _trandingDestinationService;
        private readonly ITourService _tourService;
        private readonly ISliderService _sliderService;
        private readonly INewsLetterService _newsLetterService;
        private readonly ISliderInfoService _sliderInfoService;
        private readonly ISpecialOfferService _specialOfferService;

        public HomeController(IBrandService brandService,
                             IInstagramService instagramService, IBlogService blogService,
                             IDestinationFeatureService destinationFeatureService,
                             ITeamMemberService teamMemberService,
                             ITrandingDestinationService trandingDestinationService, ITourService tourService
                           , ISliderService sliderService, INewsLetterService newsLetterService, ISliderInfoService sliderInfoService, 
                            ISpecialOfferService specialOfferService)
        {
            _brandService = brandService;
            _instagramService = instagramService;
            _blogService = blogService;
            _destinationFeatureService = destinationFeatureService;
            _teamMemberService = teamMemberService;
            _trandingDestinationService = trandingDestinationService;
            _tourService = tourService;
            _sliderService = sliderService;
            _newsLetterService = newsLetterService;
            _sliderInfoService = sliderInfoService;
            _specialOfferService = specialOfferService;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var destinationFeatures = await _destinationFeatureService.GetAllAsync();
            var teamMembers = await _teamMemberService.GetAllAsync();
            var trandingDestinatons = await _trandingDestinationService.GetAllAsync();
            var tours = await _tourService.GetAllAsync();
            var sliders = await _sliderService.GetAllAsync();
            var sliderInfos = await _sliderInfoService.GetAllAsync();
            var specialOffer = await _specialOfferService.GetAllAsync();

            var model = new HomePageVM
            {
                Brands = brands.ToList(),
                Instagrams = instagrams.ToList(),
                Blogs = blogs.ToList(),
                DestinationFeatures = destinationFeatures.ToList(),
                TeamMembers = teamMembers.ToList(),
                TrandingDestinations = trandingDestinatons.ToList(),
                Tours = tours.ToList(),
                Sliders = sliders.ToList(),
                SliderInfos = sliderInfos.ToList(),
                SpecialOffers = specialOffer.ToList(),
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(SubscribePostVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Email düzgün deyil.";
                return RedirectToAction("Index");
            }

            var response = await _newsLetterService.CreateAsync(new NewsLetterCreate { Email = model.Email });

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Abun?lik uðurla ?lav? edildi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "X?ta baþ verdi.";
                return RedirectToAction("Index");
            }
        }



    }
}
