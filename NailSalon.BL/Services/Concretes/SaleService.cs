using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IShopRepository _shopRepo;

        public SaleService(ISaleRepository saleRepo, IShopRepository shopRepo)
        {
            _saleRepo = saleRepo;
            _shopRepo = shopRepo;
        }

        public async Task<string> CreateStripeSessionAsync(SaleVm vm, string userId)
        {
            // Məhsulu verilən Id ilə DB-dən götürürük
            var product = await _shopRepo.GetByIdAsync(vm.ProductId);
            if (product == null)
                throw new Exception("Məhsul tapılmadı.");

            // Miqdar yoxdursa minimum 1 təyin edirik
            if (vm.Quantity <= 0)
                vm.Quantity = 1;

            // Qiyməti sentlərə çeviririk (Stripe sentlərlə işləyir)
            var amountInCents = (long)(product.Price * 100);
            if (amountInCents < 1)
                amountInCents = 100; // Minimum 1 USD sent (1 sent minimal Stripe-də)

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
    {
        new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "usd",
                UnitAmount = amountInCents,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = product.Name,
                    Description = product.Description
                }
            },
            Quantity = vm.Quantity
        }
    },
                Mode = "payment",
                SuccessUrl = "https://localhost:7284/Sale/Success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7284/Sale/Cancel",
                Metadata = new Dictionary<string, string>
    {
        { "productId", product.Id.ToString() },
        { "userId", userId },
        { "quantity", vm.Quantity.ToString() }
    }
            };




            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Url;
        }


        public async Task ProcessStripePaymentAsync(string sessionId)
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);

            var productId = int.Parse(session.Metadata["productId"]);
            var userId = session.Metadata["userId"];
            var quantity = int.Parse(session.Metadata["quantity"]);

            var product = await _shopRepo.GetByIdAsync(productId);

            var sale = new Sale
            {
                ProductId = product.Id,
                Quantity = quantity,
                UserId = userId,
                TotalPrice = product.Price * quantity
            };

            await _saleRepo.CreateAsync(sale);
        }

        public async Task<List<SaleVm>> GetSalesByUserAsync(string userId)
        {
            var sales = await _saleRepo.GetByUserIdAsync(userId);
            return sales.Select(x => new SaleVm
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice
            }).ToList();
        }
    }

}
