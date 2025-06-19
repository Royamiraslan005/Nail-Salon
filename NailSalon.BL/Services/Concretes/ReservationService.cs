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
        private readonly IReservationRepository _repository;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;

        public ReservationService(IReservationRepository repository, IUserService userService, IMasterService masterService)
        {
            _repository = repository;
            _userService = userService;
            _masterService = masterService;
        }

        public async Task<bool> MakeReservationAsync(ReservationVm reservationvm)
        {
            var reservation = new Reservation()
            {
                Date = reservationvm.Date,
                WantsFoodDrink = reservationvm.WantsFoodDrink,
                MasterId = reservationvm.MasterId,
                NailTypeId = reservationvm.DesignId,
                UserId = reservationvm.UserId,
            };

            if (reservation.MasterId.HasValue &&
                    _repository.IsSlotAvailable(reservation.MasterId.Value, reservation.Date))
            {
                _repository.CreateReservation(reservation);
                return true;
            }
            return false;
        }

        public List<Design> GetDesignsByZodiac(string zodiacSign)
        {
            
            return _userService.GetAllDesigns().Where(d => d.Zodiac == zodiacSign || d.Zodiac == "All").ToList();
        }

        public Task<bool> MakeReservationAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReservationInfoVm>> GetAll(string id)
        {

            var reservations = await _repository.GetAll(id);

            var masters = _masterService.GetAllAsync();



            var vms = reservations.Select(x => new ReservationInfoVm()
            {
                
                MasterName =x.Master.FullName,
                DesignName=x.NailType.Title,
                WantsMenu=x.WantsFoodDrink,
                Date=x.Date,
               
            }).ToList();

            return  vms;
        }
    }
}
