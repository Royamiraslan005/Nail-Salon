//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using NailSalon.BL.Services.Abstractions;
//using NailSalon.Core.Models;
//using NailSalon.Core.ViewModels;
//using NailSalon.DAL.Contexts;

//namespace NailSalon.Controllers
//{
//    public class ReservationController : Controller
//    {
//        private readonly IReservationService _reservationService;
//        private readonly UserManager<AppUser> _userManager;

//        public ReservationController(IReservationService reservationService, UserManager<AppUser> userManager)
//        {
//            _reservationService = reservationService;
//            _userManager = userManager;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Create()
//        {
//            var vm = new ReservationVm
//            {
//                Date = DateTime.Today,
//                Time = TimeSpan.FromHours(12),
//                Masters = await _reservationService.GetMastersAsync(),
//                NailTypes = await GetDesignsAsync()
//            };
//            return View(vm);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(ReservationVm vm)
//        {
//            if (!ModelState.IsValid)
//            {
//                //vm.Masters = await GetMastersAsync();     
//                //vm.NailTypes = await GetDesignsAsync();   
//                return View(vm);
//            }


//            var user = await _userManager.GetUserAsync(User);
//            if (user == null) return RedirectToAction("Login", "Account");

//            await _reservationService.CreateAsync(vm, user.Id);

//            if (vm.WantsMenu)
//                return RedirectToAction("Menu", "Order");
//            else
//                return RedirectToAction("Index", "Home");
//        }

//        private async Task<List<SelectListItem>> GetMastersAsync()
//        {
//            return new List<SelectListItem> {
//            new("Aygün", "1"), new("Nigar", "2")
//        };
//        }

//        private async Task<List<SelectListItem>> GetDesignsAsync()
//        {
//            return new List<SelectListItem> {
//            new("French Qızılı", "1"), new("Glitter Pastel", "2")
//        };
//        }
//    }
//}


