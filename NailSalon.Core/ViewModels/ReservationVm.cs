using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NailSalon.Core.ViewModels
{
    public class ReservationVm
    {
        public int Id { get; set; }

        [Required]
        public int MasterId { get; set; }

        [Required]
        public int NailTypeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public decimal Price { get; set; }

        public bool WantsMenu { get; set; }

        // Dropdownlar üçün
        public List<SelectListItem>? Masters { get; set; }
        public List<SelectListItem>? NailTypes { get; set; }
    }


}
