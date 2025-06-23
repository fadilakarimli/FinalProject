using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogDetailController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null) return NotFound();

            var allBlogs = await _blogService.GetAllAsync();

            var relatedBlogs = allBlogs
                .Where(b => b.Id != blog.Id)
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToList();

            var vm = new BlogDetailVM
            {
                Blog = blog, // əsas blog
                RelatedBlogs = relatedBlogs // əlaqəli olanlar
            };

            return View(vm);
        }
    }
}
