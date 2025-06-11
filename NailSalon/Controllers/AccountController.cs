using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.BL.Services.Abstractions; // ← zodiac servis üçün əlavə et

namespace NailSalon.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IZodiacService _zodiacService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IZodiacService zodiacService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _zodiacService = zodiacService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (!ModelState.IsValid) return View(model);

            var zodiac = _zodiacService.Calculate(model.BirthDate);

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                BirthDate = model.BirthDate,
                Zodiac = zodiac
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Email və ya şifrə yalnışdır.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
