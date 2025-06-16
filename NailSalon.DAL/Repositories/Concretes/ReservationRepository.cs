using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Concretes
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetUserReservationsAsync(string userId)
        {
            return await _context.Reservations
                .Include(r => r.Master)
                .Include(r => r.NailType)
                .Where(r => r.AppUserId == userId)
                .ToListAsync();
        }
    }



}
