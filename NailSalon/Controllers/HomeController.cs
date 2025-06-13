using Microsoft.AspNetCore.Mvc;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
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
    }
}
