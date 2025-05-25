using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _brandService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _brandService.CreateAsync(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error occurred");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new BrandEdit { Id = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BrandEdit model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var response = await _brandService.EditAsync(id, model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
                return NotFound();

            return View(brand);
        }




    }
}
