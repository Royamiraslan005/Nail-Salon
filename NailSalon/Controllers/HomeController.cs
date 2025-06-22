using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.BL.Services.Concretes;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;
using System.Threading.Tasks;

namespace NailSalon.Controllers
{
    public class HomeController : Controller
    {
        IMasterService _masterService;
        IReviewService _reviewService;
        IServicesService _servicesService;

        public HomeController(IMasterService masterService, IReviewService reviewService, IServicesService servicesService)
        {
            _masterService = masterService;
            _reviewService = reviewService;
            _servicesService = servicesService;
        }

        public async Task<IActionResult> Index()
        {
            List<MasterVm> masterVms =await _masterService.GetAllAsync();
            ViewBag.MasterVms = masterVms;
            ViewBag.Services = await _servicesService.GetAllAsync();
            return View();

        }
        public IActionResult About()
        {
            var vm = new AboutVm
            {
                Title = "✨ Haqqımızda",
                Description = "ZodiNails – yalnız dırnaq salonu deyil, həm də bürcünüzə uyğun gözəllik təcrübəsi! Estetik və enerjiyə uyğun dizaynlarla sizi fərqli hiss etdiririk.",
                Features = new List<FeatureVm>
            {
                new() { Icon = "💫", Title = "12 Bürc Dizaynı", Description = "Hər bürcə uyğun xüsusi dırnaq dizaynları." },
                new() { Icon = "🎨", Title = "Unikal Stil", Description = "Minimalistdən ekstravaqant dizaynlara qədər seçimlər." },
                new() { Icon = "🌟", Title = "Ad günü Endirimi", Description = "Ad gününüzə özəl 30% endirim sizi gözləyir!" },
                new() { Icon = "🧁", Title = "Qonaqpərvərlik", Description = "Seçiminizə uyğun içkilər və şirniyyatlar təqdim olunur." }
            }
            };

            return View(vm);
        }

 

        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _reviewService.CreateAsync(vm);
            return RedirectToAction("Index");
        }
    }
}
