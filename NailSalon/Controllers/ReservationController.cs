using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ReservationController(UserManager<AppUser> userManager, ApplicationDbContext context)
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
                Services = _context.Services.ToList()
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
            await _context.SaveChangesAsync();

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
