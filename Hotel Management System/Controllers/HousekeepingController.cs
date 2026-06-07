using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Services;
using HotelManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class HousekeepingController : Controller
    {
        private readonly IHousekeepingService _housekeepingService;

        public HousekeepingController(IHousekeepingService housekeepingService)
        {
            _housekeepingService = housekeepingService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _housekeepingService.GetAllTasksAsync();
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _housekeepingService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Housekeeping housekeeping)
        {
            if (!ModelState.IsValid) return View(housekeeping);
            await _housekeepingService.AddTaskAsync(housekeeping);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _housekeepingService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Housekeeping housekeeping)
        {
            if (id != housekeeping.TaskID) return BadRequest();
            if (!ModelState.IsValid) return View(housekeeping);
            await _housekeepingService.UpdateTaskAsync(housekeeping);
            return RedirectToAction(nameof(Index));
        }
    }
}