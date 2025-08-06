using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Birthday_tracker.Services;
using Humanizer;
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
            return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var birthday = await _service.FindAsync(id);

            if (birthday == null)
            {
                return RedirectToAction("Index");
            }

            var birthdayDto = new BirthdayDto
            {
                Name = birthday.Name,
                BirthdayDate = birthday.BirthdayDate,
            };

            ViewData["BirthdayId"] = birthday.Id;
            ViewData["BirthdayUserName"] = birthdayDto.Name;
            ViewData["BirthdayDate"] = birthday.BirthdayDate.ToString("d");
            ViewData["BirthdayImage"] = birthday.Image;

            return View(birthdayDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BirthdayDto birthdayDto)
        {
            var birthday = await _service.FindAsync(id);

            if (birthday == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ViewData["BirthdayId"] = birthday.Id;
                ViewData["BirthdayUserName"] = birthdayDto.Name;
                ViewData["BirthdayDate"] = birthday.BirthdayDate.ToString("d");
                ViewData["BirthdayImage"] = birthday.Image;
                return View(birthdayDto);
            }

            try
            {
                await _service.UpdateBirthdayAsync(birthday, birthdayDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", ex.ToString());
                return View(birthdayDto);
            }

            return RedirectToAction("Index");
        }
    }
}