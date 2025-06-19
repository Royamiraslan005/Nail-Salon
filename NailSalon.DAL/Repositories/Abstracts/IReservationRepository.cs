using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IReservationRepository
    {
        bool IsSlotAvailable(int masterId, DateTime date);
        void CreateReservation(Reservation reservation);

        Task<List<Reservation>> GetAll(string id);
    }

}
