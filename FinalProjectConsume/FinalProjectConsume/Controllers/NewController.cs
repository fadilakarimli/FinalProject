using Microsoft.AspNetCore.Mvc;

namespace FinalProjectConsume.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
