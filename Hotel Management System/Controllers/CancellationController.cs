using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Services;
using HotelManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class CancellationController : Controller
    {
        private readonly ICancellationService _cancellationService;

        public CancellationController(ICancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        public async Task<IActionResult> Index()
        {
            var cancellations = await _cancellationService.GetAllCancellationsAsync();
            return View(cancellations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cancellation = await _cancellationService.GetCancellationByIdAsync(id);
            if (cancellation == null) return NotFound();
            return View(cancellation);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cancellation cancellation)
        {
            if (!ModelState.IsValid) return View(cancellation);
            await _cancellationService.AddCancellationAsync(cancellation);
            return RedirectToAction(nameof(Index));
        }
    }
}