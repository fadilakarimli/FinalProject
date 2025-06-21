using FinalProjectConsume.Models.Plan;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var plans = await _planService.GetAllAsync();
            return View(plans);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlanCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _planService.CreateAsync(model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Plan yaratmaq zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null) return NotFound();

            var model = new PlanEdit
            {
                Day = plan.Day,
                Title = plan.Title,
                Description = plan.Description,
                TourId = plan.TourId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlanEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _planService.EditAsync(id, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Plan yenilənmə zamanı xəta baş verdi.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _planService.DeleteAsync(id);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null) return NotFound();

            return View(plan);
        }
    }
}