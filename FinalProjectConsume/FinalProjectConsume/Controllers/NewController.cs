using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;

namespace FinalProjectConsume.Controllers
{
    public class NewController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly INewService _newService;
        public NewController(IBlogService blogService, INewService newService)
        {
            _blogService = blogService; 
            _newService = newService;
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

        [HttpGet]
        public async Task<IActionResult> SearchApi(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Axtarış boş ola bilməz.");

            var blogs = await _newService.SearchBlogsAsync(query);
            return Json(blogs);
        }


    }
}
