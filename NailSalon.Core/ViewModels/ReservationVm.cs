using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ReservationVm
    {
        public int MasterId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public List<Master>? Masters { get; set; }
        public List<Service>? Services { get; set; }
    }
}
