using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.SliderInfo;
using FinalProjectConsume.Models.SpecialOffer;
using FinalProjectConsume.Models.TrandingDestination;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TeamController : Controller
    {
        //private readonly ITeamMemberService _teamMemberService;
        private readonly IAboutTeamMemberService _aboutTeamMemberService;
        public TeamController(ITeamMemberService teamMemberService, IAboutTeamMemberService aboutTeamMemberService)
        {
            //_teamMemberService = teamMemberService;
            _aboutTeamMemberService = aboutTeamMemberService;
        }
        public async Task<IActionResult> Index()
        {
            //var teamMembers = await _teamMemberService.GetAllAsync();
            var aboutTeamMember = await _aboutTeamMemberService.GetAllAsync();
            var model = new TeamPageVM
            {
                AboutTeamMembers = aboutTeamMember.ToList(),

                //TeamMembers = teamMembers.ToList(),
            };

            return View(model);
        }
    }
}
