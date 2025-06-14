using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.BL.Services.Concretes;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;
using System.Threading.Tasks;

namespace NailSalon.Controllers
{
    public class HomeController : Controller
    {
        IMasterService _masterService;

        public HomeController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public async Task<IActionResult> Index()
        {
            List<MasterVm> masterVms =await _masterService.GetAllAsync();
            return View(masterVms);

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
    }
}
