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
        INailTypeService _nailTypeService;

        public HomeController(IMasterService masterService, IReviewService reviewService, IServicesService servicesService, INailTypeService nailTypeService)
        {
            _masterService = masterService;
            _reviewService = reviewService;
            _servicesService = servicesService;
            _nailTypeService = nailTypeService;
        }

        public async Task<IActionResult> Index()
        {
            List<MasterVm> masterVms =await _masterService.GetAllAsync();
            var nailTypes = await _nailTypeService.GetAllAsync();

            var nailTypeVms = nailTypes.Select(x => new NailTypeVm
            {
                Id = x.Id,
                Title = x.Title,
                Price = (decimal)x.Price,
                ImageUrl = x.ImageUrl
            }).ToList();
            ViewBag.NailTypesVms = nailTypeVms;
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
                new() { ImageUrl="burc.jpg", Title = "12 Bürc Dizaynı", Description = "Hər bürcə uyğun xüsusi dırnaq dizaynları." },
                new() { ImageUrl="unikal.jpg", Title = "Unikal Stil", Description = "Minimalistdən ekstravaqant dizaynlara qədər seçimlər." },
                new() { ImageUrl="adgunu.jpg", Title = "Ad günü Endirimi", Description = "Ad gününüzə özəl 30% endirim sizi gözləyir!" },
                new() { ImageUrl="qonaq.png", Title = "Qonaqpərvərlik", Description = "Seçiminizə uyğun içkilər və şirniyyatlar təqdim olunur." }
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
