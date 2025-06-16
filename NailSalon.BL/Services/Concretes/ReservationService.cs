using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task CreateAsync(ReservationVm vm, string userId)
        {
            var reservation = new Reservation
            {
                MasterId = vm.MasterId,
                NailTypeId = vm.NailTypeId,
                Date = vm.Date,
                Time = vm.Time,
                Price = vm.Price,
                WantsMenu = vm.WantsMenu,
                AppUserId = userId
            };

            await _reservationRepository.CreateAsync(reservation);
        }

        public async Task<List<ReservationVm>> GetUserReservationsAsync(string userId)
        {
            var data = await _reservationRepository.GetUserReservationsAsync(userId);

            return data.Select(r => new ReservationVm
            {
                Id = r.Id,
                Date = r.Date,
                Time = r.Time,
                Price = r.Price,
                WantsMenu = r.WantsMenu,
                MasterId = r.MasterId,
                NailTypeId = r.NailTypeId
            }).ToList();
        }
    }
}
