using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TeamDetailController : Controller
    {
        private readonly IAboutTeamMemberService _aboutTeamMemberService;


        public TeamDetailController(IAboutTeamMemberService aboutTeamMemberService)
        {
            _aboutTeamMemberService = aboutTeamMemberService;
        }

        public async Task<IActionResult> Index(int id , string search)
        {
            var aboutTeam = await _aboutTeamMemberService.GetByIdAsync(id);
            if (aboutTeam == null) return NotFound();

            var vm = new TeamDetailVM
            {
                AboutTeamMember = aboutTeam,
                SearchTerm = search ?? string.Empty
            };

            return View(vm);
        }
    }
}
