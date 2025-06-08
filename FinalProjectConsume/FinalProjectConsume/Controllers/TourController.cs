using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IAmenityService _amenityService;
        private readonly IActivityService _activityService;
        private readonly ICityService _cityService;
        public TourController(ITourService tourService
                           , IAmenityService amenityService,
                             IActivityService activityService,
                             ICityService cityService)
        {
            _tourService = tourService;
            _amenityService = amenityService;
            _activityService = activityService;
            _cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            var amenities= await _amenityService.GetAllAsync();
            var activity = await _activityService.GetAllAsync();    
            var city = await _cityService.GetAllAsync();

            var model = new TourPageVM
            {
                Tours = tours.ToList(),
                Amenities = amenities.ToList(),
                Activities = activity.ToList(), 
                Cities = city.ToList(),
            };

            return View(model);
        }
    }
}
