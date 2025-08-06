using Birthday_tracker.Models;
using Birthday_tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Text.Encodings.Web;

namespace Birthday_tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBirthdayService _service;

        public HomeController(ILogger<HomeController> logger, IBirthdayService service)
        {
            _logger = logger;
            _service = service;
        }

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        public async Task<IActionResult> Index()
        {

            Console.WriteLine(DateTime.Now.ToString());
            var model = new NearBirthdaysViewModel
            {
                TodayBirthdays = await _service.GetUpcomingBirthdaysAsync(1),
                UpcomingBirthdays = await _service.GetUpcomingBirthdaysAsync(7)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
