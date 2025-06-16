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
        Task CreateAsync(ReservationVm vm, string userId);
        Task<List<ReservationVm>> GetUserReservationsAsync(string userId);
    }
}
