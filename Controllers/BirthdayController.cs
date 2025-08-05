using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Birthday_tracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace Birthday_tracker.Controllers
{
    public class BirthdayController : Controller
    {
        private readonly IBirthdayService _service;

        public BirthdayController(IBirthdayService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string sortField = "id")
        {
            var sorted = await _service.GetSortedAsync(sortField.ToLower());
            return View(sorted);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteBirthdayAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
