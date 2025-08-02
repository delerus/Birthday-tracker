using Microsoft.AspNetCore.Mvc;
using Birthday_tracker.Data;
using Birthday_tracker.Models;

namespace Birthday_tracker.Controllers
{
    public class BirthdayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BirthdayController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var birthdays = _context.Birthdays.ToList();
            return View(birthdays);
        }
    }
}
