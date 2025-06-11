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
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int UstaId { get; set; }
        public Master Master { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public ICollection<ReservationMenuItem> ReservationMenuItems { get; set; }
    }

}
