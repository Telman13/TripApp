using Business.Abstract;
using Entities.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AuthController(IAuthService auth) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await auth.LoginAsync(dto);

            if (result.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessageFromLogin = "Invalid username or password.";
            return View(dto);
        }
    }
}