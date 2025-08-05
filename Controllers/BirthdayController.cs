using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Birthday_tracker.Service;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        [HttpPost]
        public async Task<IActionResult> Create(BirthdayDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _service.AddBirthdayAsync(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", ex.ToString());
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var birthday = _service.FindAsync(id);
            if (birthday == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}