using Microsoft.AspNetCore.Mvc;

namespace NailSalon.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NailSalon.BL.Services.Abstractions;
    using NailSalon.Core.Models;
    using System.Threading.Tasks;

    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(IBasketService basketService, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
        }

        // İstifadəçinin səbətini göstərir
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (userId == null)
            {
                // İstifadəçi daxil olmayıbsa, yönləndir və ya boş səbət göstər
                return RedirectToAction("Login", "Account");
            }

            var basketItems = await _basketService.GetBasketAsync(user.Id);
            return View(basketItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId, int quantity = 1)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var user =await _userManager.FindByEmailAsync(User.Identity.Name);

            await _basketService.AddToBasketAsync(user.Id, productId, quantity);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(int id)
        {
            await _basketService.RemoveFromBasketAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Səbəti təmizlə
        [HttpPost]
        public async Task<IActionResult> ClearBasket()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            if (userId == null)
                return Unauthorized();

            await _basketService.ClearBasketAsync(userId);
            return RedirectToAction(nameof(Index));
        }
    }

}
