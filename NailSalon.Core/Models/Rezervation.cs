using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int MasterId { get; set; } 
        public Master Master { get; set; }

        public int ServiceId { get; set; }
        public Services Service { get; set; }

        public string UserId { get; set; }  
        public AppUser User { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public ICollection<ReservationMenu> ReservationMenu { get; set; }
    }

}
