using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ReservationInfoVm
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string MasterName { get; set; }
        public NailType NailType { get; set; }
        public decimal Price { get; set; }
        public bool WantsMenu { get; set; }
    }
}
