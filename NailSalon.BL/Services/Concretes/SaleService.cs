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

            var userId = session.Metadata["userId"];

            // Metadata-da neçə məhsul olduğunu tapmaq üçün 
            // "productId_0", "productId_1" və s. açarları yoxlamalıyıq
            var sales = new List<Sale>();

            int index = 0;
            while (true)
            {
                string productIdKey = $"productId_{index}";
                string quantityKey = $"quantity_{index}";

                if (!session.Metadata.ContainsKey(productIdKey))
                    break; // Daha məhsul yoxdur

                int productId = int.Parse(session.Metadata[productIdKey]);
                int quantity = int.Parse(session.Metadata[quantityKey]);

                var product = await _shopRepo.GetByIdAsync(productId);
                if (product == null)
                {
                    // Məhsul tapılmadı, istəyə bağlı olaraq ya davam et, ya da exception at
                    index++;
                    continue;
                }

                var sale = new Sale
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    UserId = userId,
                    TotalPrice = product.Price * quantity
                };

                sales.Add(sale);
                index++;
            }

            foreach (var sale in sales)
            {
                await _saleRepo.CreateAsync(sale);
            }
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

        public async Task<string> CreateStripeSessionAsync(List<BasketItemVm> basketItems, string userId)
        {
            if (basketItems == null || !basketItems.Any())
                throw new Exception("Səbət boşdur.");

            var lineItems = new List<SessionLineItemOptions>();
            var metadata = new Dictionary<string, string>
    {
        { "userId", userId }
    };

            // metadata üçün productId və count kimi məlumatları əlavə etmək istəyirsənsə, indexlə əlavə edə bilərsən
            int index = 0;

            foreach (var item in basketItems)
            {
                // DB-dən məhsulu yoxlamaq, qiymət almaq yaxşıdır
                var product = await _shopRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Məhsul tapılmadı: {item.Name}");

                var amountInCents = (long)(product.Price * 100);
                if (amountInCents < 1)
                    amountInCents = 100;

                lineItems.Add(new SessionLineItemOptions
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
                    Quantity = item.Count
                });

                metadata.Add($"productId_{index}", product.Id.ToString());
                metadata.Add($"quantity_{index}", item.Count.ToString());
                index++;
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7284/Sale/Success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7284/Sale/Cancel",
                Metadata = metadata
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

    }

}
