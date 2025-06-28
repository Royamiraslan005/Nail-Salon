﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class BasketItemVm
    {
        public int  Id { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }

}
