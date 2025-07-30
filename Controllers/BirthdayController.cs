using Microsoft.AspNetCore.Mvc;

namespace Birthday_tracker.Controllers
{
    public class BirthdayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
