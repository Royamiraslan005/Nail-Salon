using Microsoft.AspNetCore.Mvc;

namespace NailSalon.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NailSalon.BL.Services.Abstractions;
    using System.Threading.Tasks;

    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        // İstifadəçinin səbətini göstərir
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            if (userId == null)
            {
                // İstifadəçi daxil olmayıbsa, yönləndir və ya boş səbət göstər
                return RedirectToAction("Login", "Account");
            }

            var basketItems = await _basketService.GetBasketAsync(userId);
            return View(basketItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId, int quantity = 1)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = User.Identity.Name;

            await _basketService.AddToBasketAsync(userId, productId, quantity);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(int itemId)
        {
            await _basketService.RemoveFromBasketAsync(itemId);
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
