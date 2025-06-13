using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;

namespace NailSalon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public ReservationController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ReservationVm
            {
                Masters = _context.Masters.ToList(),
                Services = _context.Services.ToList(),
                Menu = _context.Menu.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationVm model)
        {
            if (!ModelState.IsValid)
            {
                model.Masters = _context.Masters.ToList();
                model.Services = _context.Services.ToList();
                model.Menu = _context.Menu.ToList(); // Menü listi yenidən yüklənir
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            var reservation = new Reservation
            {
                MasterId = model.MasterId,
                ServiceId = model.ServiceId,
                Date = model.Date,
                Time = model.Time,
                UserId = user.Id
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(); // Əvvəl rezervasiyanı save edirik ki, Id gəlsin

            // Menü seçimlərini əlavə et
            if (model.SelectedMenuIds != null && model.SelectedMenuIds.Any())
            {
                foreach (var menuId in model.SelectedMenuIds)
                {
                    var reservationMenu = new ReservationMenu
                    {
                        ReservationId = reservation.Id,
                        MenuId = menuId
                    };
                    _context.ReservationMenus.Add(reservationMenu);
                }

                await _context.SaveChangesAsync(); // Menü məlumatlarını da yadda saxlayırıq
            }

            TempData["message"] = "Rezervasiya uğurla yaradıldı!";
            return RedirectToAction("MyReservations");
        }


        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            var reservations = _context.Reservations
                .Include(r => r.Master)
                .Include(r => r.Service)
                .Where(r => r.UserId == user.Id)
                .ToList();

            return View(reservations);
        }
    }
}
