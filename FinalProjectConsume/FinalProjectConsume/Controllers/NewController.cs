using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class NewController : Controller
    {
        private readonly IBlogService _blogService;
        public NewController(IBlogService blogService)
        {
            _blogService = blogService; 
        }
        public async Task<IActionResult> Index(string search)
        {
            var blogs = await _blogService.GetAllAsync();

            var vm = new NewVM
            {
                Blogs = blogs.ToList(),
                SearchTerm = search ?? string.Empty

            };
            return View(vm);
        }
    }
}
