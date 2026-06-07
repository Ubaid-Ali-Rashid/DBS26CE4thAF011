using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Services;
using HotelManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class RoomServiceController : Controller
    {
        private readonly IRoomServiceService _roomServiceService;

        public RoomServiceController(IRoomServiceService roomServiceService)
        {
            _roomServiceService = roomServiceService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _roomServiceService.GetAllRoomServicesAsync();
            return View(services);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _roomServiceService.GetRoomServiceByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomService roomService)
        {
            if (!ModelState.IsValid) return View(roomService);
            await _roomServiceService.AddRoomServiceAsync(roomService);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _roomServiceService.GetRoomServiceByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomService roomService)
        {
            if (id != roomService.ServiceID) return BadRequest();
            if (!ModelState.IsValid) return View(roomService);
            await _roomServiceService.UpdateRoomServiceAsync(roomService);
            return RedirectToAction(nameof(Index));
        }
    }
}