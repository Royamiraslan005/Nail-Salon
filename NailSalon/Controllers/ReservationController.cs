
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;
using NailSalon.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NailSalon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly INailTypeService _nailTypeService;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(IReservationService reservationService, IUserService userService, IMasterService masterService, INailTypeService nailTypeService, UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _userService = userService;
            _masterService = masterService;
            _nailTypeService = nailTypeService;
            _userManager = userManager;
        }


        public async Task<IActionResult> Create()
        {

            var masters = await _masterService.GetAllAsync();
            var designs = await _nailTypeService.GetAllAsync();
            ViewBag.Masters = masters;
            ViewBag.Designs = designs;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ReservationVm vm, string username)
        {
            
            if (vm.Date < DateTime.Today)
            {
                ModelState.AddModelError("Date", "Keçmiş tarix seçilə bilməz.");

                var masters = await _masterService.GetAllAsync();
                var designs = await _nailTypeService.GetAllAsync();
                ViewBag.Masters = masters;
                ViewBag.Designs = designs;

                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
                return RedirectToAction("Login", "Account");

            vm.UserId = user.Id;

            var success = await _reservationService.MakeReservationAsync(vm);

            if (success)
            {
                if (vm.WantsFoodDrink)
                    return RedirectToAction("Index", "Menu");
                else
                    return RedirectToAction("Profile", "Account");
            }

            ModelState.AddModelError("", "Seçilmiş vaxt artıq doludur.");

            var allMasters = await _masterService.GetAllAsync();
            var allDesigns = await _nailTypeService.GetAllAsync();
            ViewBag.Masters = allMasters;
            ViewBag.Designs = allDesigns;

            return View(vm);
        }
    }
    }