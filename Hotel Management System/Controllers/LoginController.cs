using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Services;
using HotelManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string passwordHash)
        {
            var isValid = await _loginService.ValidateLoginAsync(username, passwordHash);
            if (!isValid)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Login login)
        {
            if (!ModelState.IsValid) return View(login);
            await _loginService.AddLoginAsync(login);
            return RedirectToAction(nameof(Index));
        }
    }
}