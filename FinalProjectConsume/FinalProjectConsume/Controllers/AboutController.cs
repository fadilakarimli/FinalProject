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
        private readonly IChooseUsAboutService _chooseUsAboutService;
        private readonly IAboutTeamMemberService _aboutTeamMemberService;  
        private readonly IAboutAppService _aboutAppService;

        public AboutController(IBrandService brandService, IChooseUsAboutService chooseUsAboutService
                             , IAboutTeamMemberService aboutTeamMemberService, IAboutAppService aboutAppService)
        {
            _brandService = brandService;
            _chooseUsAboutService = chooseUsAboutService;
            _aboutTeamMemberService = aboutTeamMemberService;
            _aboutAppService = aboutAppService;
        }
        public async Task <IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var chooseUsAbout = await _chooseUsAboutService.GetAllAsync();
            var aboutTeamMember = await _aboutTeamMemberService.GetAllAsync();
            var aboutApp = await _aboutAppService.GetAllAsync();
            var model = new AboutPageVM
            {
                Brands = brands.ToList(),
                ChooseUsAbouts = chooseUsAbout.ToList(),
                AboutTeamMembers = aboutTeamMember.ToList(),    
                AboutApps = aboutApp.ToList(),
            };

            return View(model);
        }
    }
}
