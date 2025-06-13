using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ReservationMenu> ReservationMenus { get; set; }
    }
}
