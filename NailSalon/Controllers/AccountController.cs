using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Enums;

namespace NailSalon.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IZodiacService _zodiacService;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IZodiacService zodiacService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _zodiacService = zodiacService;
            _roleManager = roleManager;
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
                await _userManager.AddToRoleAsync(user,"Member");
          
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View();
            }

            

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() => View();



        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }






        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model,string? returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email və ya şifrə yalnışdır.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); 
            }


            ModelState.AddModelError("", "Email və ya şifrə yalnışdır.");
            return View(model);
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString()
                });
            }

            return RedirectToAction("Index", "Home");
        }
            [HttpGet]
            public async Task<IActionResult> Profile()
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login");

                var zodiacInfo = _zodiacService.GetZodiacInfo(user.BirthDate);

                var vm = new ProfileVm
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Zodiac = zodiacInfo.Name,
                    ZodiacSymbol = zodiacInfo.Symbol,
                    ZodiacTrait = zodiacInfo.Trait,
                    SuggestedDesign = zodiacInfo.SuggestedDesign,


                };

                return View(vm);
            }
    

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
