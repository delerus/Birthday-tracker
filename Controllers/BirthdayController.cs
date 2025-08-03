using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Birthday_tracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace Birthday_tracker.Controllers
{
    public class BirthdayController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBirthdayService _service;

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
