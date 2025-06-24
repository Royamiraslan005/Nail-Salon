using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System.Text.Json;

namespace NailSalon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly INailTypeService _nailTypeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public ReservationController(
            IReservationService reservationService,
            IUserService userService,
            IMasterService masterService,
            INailTypeService nailTypeService,
            UserManager<AppUser> userManager,
            IEmailService emailService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _masterService = masterService;
            _nailTypeService = nailTypeService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            ViewBag.Masters = await _masterService.GetAllAsync();

            var allDesigns = await _nailTypeService.GetAllAsync();
            var filteredDesigns = allDesigns
                .Where(x => x.Zodiac == user.Zodiac || x.Zodiac == "All" || string.IsNullOrEmpty(x.Zodiac))
                .ToList();

            ViewBag.Designs = filteredDesigns;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(ReservationVm vm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            vm.UserId = user.Id;

            var success = await _reservationService.MakeReservationAsync(vm);

            if (!success)
            {
                ModelState.AddModelError("Date", "Seçilmiş vaxtda bu usta artıq bron edilib.");
                ViewBag.Masters = await _masterService.GetAllAsync();

                var allDesigns = await _nailTypeService.GetAllAsync();
                var filteredDesigns = allDesigns
                    .Where(x => x.Zodiac == user.Zodiac || x.Zodiac == "All" || string.IsNullOrEmpty(x.Zodiac))
                    .ToList();
                ViewBag.Designs = filteredDesigns;

                return View(vm);
            }

            if (vm.WantsFoodDrink)
            {
                TempData["ReservationVm"] = JsonSerializer.Serialize(vm);
                return RedirectToAction("Index", "Menu");
            }

            
            return RedirectToAction("Profile", "Account");
        }



        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmReservation(ConfirmReservationVm model)
        {
            try
            {
                await _emailService.SendEmailAsync(model.Email, "Təsdiq", "Rezervasiyanız qeydə alındı");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Burada loglama və ya istifadəçiyə xəbərdarlıq verə bilərsən
                ModelState.AddModelError("", "Email göndərmə zamanı xəta baş verdi: " + ex.Message);
                return View("Confirm", model);
            }
        }

    }
}
