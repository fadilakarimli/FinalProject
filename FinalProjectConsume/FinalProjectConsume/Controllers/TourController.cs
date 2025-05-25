using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class TourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
