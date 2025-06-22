using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IReservationService
    {
        Task<bool> MakeReservationAsync(ReservationVm reservation);
        List<NailType> GetDesignsByZodiac(string zodiacSign);
        Task CreateAsync(Reservation reservation);


        Task<List<ReservationInfoVm>> GetAll(string id);
    }
}

