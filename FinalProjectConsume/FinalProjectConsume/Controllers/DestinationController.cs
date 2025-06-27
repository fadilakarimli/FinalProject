using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class DestinationController : Controller
    {
        private readonly ITrandingDestinationService _trandingDestinationService;
        private readonly ISettingService _settingService;
        public DestinationController(ITrandingDestinationService trandingDestinationService, ISettingService settingService)
        {
            _trandingDestinationService = trandingDestinationService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            int take = 6; // Hər səhifədə 6 element göstərilsin

            // Bütün destinationları alırıq
            IEnumerable<TrandingDestination> destinations = await _trandingDestinationService.GetAllAsync();

            // Search varsa filtr tətbiq edirik
            if (!string.IsNullOrWhiteSpace(search))
            {
                destinations = destinations.Where(d => d.Title != null && d.Title
                                                            .ToLower()
                                                            .Contains(search.Trim().ToLower()));
            }

            // Ümumi element sayı
            int totalDestinations = destinations.Count();

            // Səhifələnmiş hissəni seçirik
            var pagedDestinations = destinations
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            // Ümumi səhifə sayı
            int totalPages = (int)Math.Ceiling(totalDestinations / (double)take);
            var settings = (await _settingService.GetAllAsync())?.ToList() ?? new List<SettingVM>();

            // ViewModel
            var model = new DestinationPageVM
            {
                TrandingDestinations = pagedDestinations,
                SearchTerm = search ?? string.Empty,
                CurrentPage = page,
                TotalPages = totalPages,
                Settings = settings

            };

            return View(model);
        }

    }
}
