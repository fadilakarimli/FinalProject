using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;   
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync();
            return View(settings);
        }
    }
}
