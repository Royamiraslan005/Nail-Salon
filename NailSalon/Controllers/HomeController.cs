using Microsoft.AspNetCore.Mvc;

namespace NailSalon.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
