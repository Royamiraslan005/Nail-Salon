using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NailSalon.BL.Services.Abstractions;
using NailSalon.BL.Services.Concretes;
using NailSalon.Core.Models;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstractions;
using NailSalon.DAL.Repositories.Abstracts;
using NailSalon.DAL.Repositories.Concretes;
using System;

namespace NailSalon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IZodiacService, ZodiacService>();
            builder.Services.AddScoped<IMasterService, MasterService>();
            builder.Services.AddScoped<IMasterRepository, MasterRepository>();
            builder.Services.AddScoped<IServicesService, ServicesService>();
            builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
            builder.Services.AddScoped<INailTypeService, NailTypeService>();
            builder.Services.AddScoped<INailTypeRepository, NailTypeRepository>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Home}/{action=Index}"
                );
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}
