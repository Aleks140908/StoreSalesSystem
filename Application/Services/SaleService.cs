using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;
using StoreSalesSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application.Services
{
    public class SaleService
    {
        private readonly IProductRepository productRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly ICustomerRepository customerRepo;
        private readonly ISaleRepository saleRepo;
        private readonly ISaleItemRepository saleItemRepo;
        private readonly IPromoCodeRepository promoRepo;

        public SaleService(IProductRepository productRepo, ICategoryRepository categoryRepo, ICustomerRepository customerRepo, ISaleRepository saleRepo, ISaleItemRepository saleItemRepo, IPromoCodeRepository promoRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
            this.customerRepo = customerRepo;
            this.saleRepo = saleRepo;
            this.saleItemRepo = saleItemRepo;
            this.promoRepo = promoRepo;
        }

        public void ApplyPromo(int saleId, string promoCode)
        {
            var sale = saleRepo.GetById(saleId);
            if (sale == null) throw new Exception("Продажбата не е намерена");
            if (!saleItemRepo.GetBySaleId(saleId).Any()) throw new Exception("Не може да се приложи промоция към празна продажба");
            

            var promo = promoRepo.GetByCode(promoCode);

            if (promo == null) throw new Exception("Промо кодът не е намерен");
            if (!promo.IsActive) throw new Exception("Промо кодът е неактивен");
            if (promo.ValidFrom > DateTime.Now || promo.ValidUntil < DateTime.Now) throw new Exception("Промо кодът е изтекъл или все още не е активен");

            sale.PromoCodeId = promo.Id;
            RecalculateSale(saleId);
        }
        private void RecalculateSale(int saleId)
        {
            var sale = saleRepo.GetById(saleId);
            if (sale == null)
                return;

            var items = saleItemRepo.GetBySaleId(saleId).ToList();

            sale.Subtotal = items.Sum(i => i.LineTotal);

            sale.DiscountAmount = 0;

            if (sale.PromoCodeId.HasValue)
            {
                var promo = promoRepo.GetById(sale.PromoCodeId.Value);
                if (promo != null)
                {
                    if (promo.Type == PromoType.Percentage)
                        sale.DiscountAmount = sale.Subtotal * (promo.Value / 100m);

                    if (promo.Type == PromoType.FixedAmount)
                        sale.DiscountAmount = promo.Value;

                    if (sale.DiscountAmount > sale.Subtotal)
                        sale.DiscountAmount = sale.Subtotal;
                }
            }

            sale.Total = sale.Subtotal - sale.DiscountAmount;

            saleRepo.Update(sale);
        }
        public SaleItem AddProductToSale(int saleId, int productId, int quantity)

        {

            var sale = saleRepo.GetById(saleId);

            if (sale == null)

                throw new Exception("Продажбата не е намерена");



            var product = productRepo.GetById(productId);

            if (product == null)

                throw new Exception("Продуктът не е намерен");



            if (product.StockQuantity < quantity)

                throw new Exception("Няма достатъчно наличност");



            var item = new SaleItem
            {
                SaleId = saleId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price,
                LineTotal = product.Price * quantity

            };

            saleItemRepo.Add(item);

            product.StockQuantity -= quantity;
            productRepo.Update(product);

            RecalculateSale(saleId);

            return item;

        }



        public IEnumerable<Sale> GetSalesHistory()

        {

            return saleRepo.GetAll();

        }

        public Sale CreateSale(int? customerId = null)

        {

            var sale = new Sale {

                CustomerId = customerId,

                Date = DateTime.Now,

                Items = new List<SaleItem>(),

                Subtotal = 0,

                DiscountAmount = 0,

                Total = 0

            };
            return saleRepo.Add(sale);
        }
    }
}
