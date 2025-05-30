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
        private readonly ITeamMemberService _teamMemberService;
        public TeamController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }
        public async Task<IActionResult> Index()
        {
            var teamMembers = await _teamMemberService.GetAllAsync();
            var model = new TeamPageVM
            {
                TeamMembers = teamMembers.ToList(),
            };

            return View(model);
        }
    }
}
