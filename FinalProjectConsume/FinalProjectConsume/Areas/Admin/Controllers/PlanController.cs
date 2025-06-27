using FinalProjectConsume.Models.Plan;
using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]

    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        private readonly ITourService _tourService;

        public PlanController(IPlanService planService, ITourService tourService)
        {
            _planService = planService;
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var plans = (await _planService.GetAllAsync()).ToList();
            var tours = await _tourService.GetAllAsync();

            foreach (var plan in plans)
            {
                var tour = tours.FirstOrDefault(t => t.Id == plan.TourId);
                if (tour != null)
                    plan.TourName = tour.Name;
            }

            return View(plans);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Burada turları al
            var tours = await _tourService.GetAllAsync();

            var model = new PlanCreate
            {
                Tours = tours.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList()
            };

            return View(model);
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