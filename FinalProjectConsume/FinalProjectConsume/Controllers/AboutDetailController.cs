using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class AboutDetailController : Controller
    {
        private readonly IAboutBlogService _aboutBlogService;
        public AboutDetailController(IAboutBlogService aboutBlogService)
        {
            _aboutBlogService = aboutBlogService;   
        }
        public async Task<IActionResult> Index(int id)
        {
            var aboutBlog = await _aboutBlogService.GetByIdAsync(id);
            if (aboutBlog == null) return NotFound();

            var allBlogs = await _aboutBlogService.GetAllAsync();

            var relatedBlogs = allBlogs
                .Where(b => b.Id != aboutBlog.Id)
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToList();

            var vm = new AboutDetailVM
            {
                AboutBlog = aboutBlog, // əsas blog
            };

            return View(vm);
        }
    }
}
