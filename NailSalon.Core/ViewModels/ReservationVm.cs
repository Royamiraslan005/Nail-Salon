﻿using NailSalon.Core.Models;
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
        public int MasterId { get; set; }
        public int? DesignId { get; set; }
        
        public string? UserId { get; set; }
        public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        public bool WantsFoodDrink { get; set; }

        public List<SelectListItem> MasterList { get; set; }
        public List<SelectListItem> DesignList { get; set; }
        public List<int>? SelectedMenuIds { get; set; } // checkbox-lardan gələn ID-lər
        public List<MenuItemVm>? MenuItems { get; set; }
    }



}
