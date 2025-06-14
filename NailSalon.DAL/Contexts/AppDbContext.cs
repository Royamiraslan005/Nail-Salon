﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Contexts
{
   public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationMenu> ReservationMenus { get; set; }
    }
}
