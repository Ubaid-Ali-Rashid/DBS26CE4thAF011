using HotelManagementSystem.Models.Domain;
//using HotelManagementSystem.Models.ViewModels;
using HotelManagementSystem.Services;
using HotelManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        public async Task<IActionResult> Index()
        {
            var guests = await _guestService.GetAllGuestsAsync();
            return View(guests);
        }

        public async Task<IActionResult> Details(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null) return NotFound();
            return View(guest);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guest guest)
        {
            if (!ModelState.IsValid) return View(guest);
            await _guestService.AddGuestAsync(guest);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null) return NotFound();
            return View(guest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guest guest)
        {
            if (id != guest.GuestID) return BadRequest();
            if (!ModelState.IsValid) return View(guest);
            await _guestService.UpdateGuestAsync(guest);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null) return NotFound();
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _guestService.DeleteGuestAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}