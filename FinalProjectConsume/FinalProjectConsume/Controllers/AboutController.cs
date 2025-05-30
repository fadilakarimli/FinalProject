using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
