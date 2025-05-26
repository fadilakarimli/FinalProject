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
        private readonly IBlogService _blogService;
        private readonly IDestinationFeatureService _destinationFeatureService;
        private readonly ITeamMemberService _teamMemberService;

        public HomeController(IBrandService brandService,
                             IInstagramService instagramService, IBlogService blogService,
                             IDestinationFeatureService destinationFeatureService,
                             ITeamMemberService teamMemberService)
        {
            _brandService = brandService;
            _instagramService = instagramService;
            _blogService = blogService;
            _destinationFeatureService = destinationFeatureService;
            _teamMemberService = teamMemberService;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var destinationFeatures = await _destinationFeatureService.GetAllAsync();
            var teamMembers = await _teamMemberService.GetAllAsync();

            var model = new HomePageVM
            {
                Brands = brands.ToList(),
                Instagrams = instagrams.ToList(),
                Blogs = blogs.ToList(),
                DestinationFeatures = destinationFeatures.ToList(),
                TeamMembersB = teamMembers.ToList()
            };

            return View(model);
        }

    }
}
