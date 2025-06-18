using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ProfileVm
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Zodiac { get; set; }
        public string ZodiacSymbol { get; set; }
        public string ZodiacTrait { get; set; }
        public string SuggestedDesign { get; set; }

        public List<MenuItemVm> MenuItems { get; set; } = new();
        public List<OrderVm> Orders { get; set; } = new();
    }

    public class MenuItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "Şəkil seçin")]
        public IFormFile? FormFile { get; set; }
        public bool IsSelected { get; set; }
    }

    public class OrderVm
    {
        public DateTime Date { get; set; }
        public string MasterName { get; set; }
        public string DesignName { get; set; }
        public decimal Price { get; set; }
    }


}
