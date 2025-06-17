using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IReservationRepository
    {
        Task CreateAsync(Reservation reservation);
        Task<List<Reservation>> GetUserReservationsAsync(string userId);

        void SaveAllChanges();
    }

}
