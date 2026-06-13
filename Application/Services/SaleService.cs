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
                if(sale == null){
                    throw new Exception("Продажбата не е намерена");
                }

            if (sale.IsCompleted)
                throw new Exception("Не може да се приложи промоция към завършена продажба");

            if (!saleItemRepo.GetBySaleId(saleId).Any())
                throw new Exception("Не може да се приложи промоция към празна продажба");

            var promo = promoRepo.GetByCode(promoCode);
                if(promo == null){
                    throw new Exception("Промоцията не е намерена");
                }

            if (!promo.IsActive)
                throw new Exception("Промоцията е неактивна");

            if (promo.ValidFrom > DateTime.Now || promo.ValidUntil < DateTime.Now)
                throw new Exception("Промоцията е изтекла или все още не е активна");

            sale.PromoCodeId = promo.Id;
            saleRepo.Update(sale);

            RecalculateSale(saleId);
        }
        public void RemovePromo(int saleId)
        {
            var sale = saleRepo.GetById(saleId);
                if(sale == null){
                    throw new Exception("Продажбата не е намерена");
                }

            if (sale.IsCompleted)
                throw new Exception("Не може да се модифицира завършена продажба");

            sale.PromoCodeId = null;
            saleRepo.Update(sale);

            RecalculateSale(saleId);
        }
        public void RecalculateSale(int saleId)
        {
            var sale = saleRepo.GetById(saleId);
                if(sale == null){
                    throw new Exception("Продажбата не е намерена");
                }

            var items = saleItemRepo.GetBySaleId(saleId).ToList();

            sale.Subtotal = items.Sum(i => i.LineTotal);

            sale.DiscountAmount = 0;

            if (sale.PromoCodeId.HasValue)
            {
                var promo = promoRepo.GetById(sale.PromoCodeId.Value);

                if (promo != null && promo.IsActive)
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
        public IEnumerable<Sale> GetSalesByPeriod(DateTime from, DateTime to)
        {
            return saleRepo.GetAll()
                .Where(s => s.Date >= from && s.Date <= to)
                .OrderBy(s => s.Date);
        }
        public SaleItem ChangeQuantityInSale(int saleId, int productId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new Exception("Количество трябва да бъде по-голямо от 0");

            var sale = saleRepo.GetById(saleId);
                if(sale == null){
                    throw new Exception("Продажбата не е намерена");
                }

            if (sale.IsCompleted)
                throw new Exception("Не може да се модифицира завършена продажба");

            var item = saleItemRepo.GetBySaleId(saleId)
                .FirstOrDefault(i => i.ProductId == productId);
                if(item == null){
                    throw new Exception("Продуктът не е намерен в продажбата");
                }
            var product = productRepo.GetById(productId);
                if(product == null){
                    throw new Exception("Продуктът не е намерен");
                }

            if (!product.IsActive)
                throw new Exception("Не може да се модифицира неактивен продукт");

            int diff = newQuantity - item.Quantity;

            if (diff > 0)
            {
                if (product.StockQuantity < diff)
                    throw new Exception("Няма достатъчно наличност");

                product.DecreaseStock(diff);
            }
            else if (diff < 0)
            {
                product.IncreaseStock(-diff);
            }

            productRepo.Update(product);

            item.Quantity = newQuantity;
            item.LineTotal = item.UnitPrice * newQuantity;
            saleItemRepo.Update(item);

            RecalculateSale(saleId);

            return item;
        }
        public SaleItem AddProductToSale(int saleId, int productId, int quantity)

        {

            var sale = saleRepo.GetById(saleId);

            if (sale is not null)

                throw new Exception("Продажбата не е намерена");

            if (sale!.IsCompleted)
                throw new Exception("Не може да се модифицира завършена продажба");

            if (quantity <= 0)
                throw new Exception("Количество трябва да бъде по-голямо от 0");
            var product = productRepo.GetById(productId);

            if (product == null)

                throw new Exception("Продуктът не е намерен");

           if (!product.IsActive)
                throw new Exception("Не може да се добавя неактивен продукт към продажбата");

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

            product.DecreaseStock(quantity);
            productRepo.Update(product);

            RecalculateSale(saleId);

            return item;

        }
        public void CompleteSale(int saleId, PaymentType paymentType)
        {
            var sale = saleRepo.GetById(saleId);
                if(sale == null!){
                    throw new Exception("Продажбата не е намерена");
                }

            if (sale.IsCompleted)
                throw new Exception("Продажбата вече е завършена");

            if (!saleItemRepo.GetBySaleId(saleId).Any())
                throw new Exception("Не може да се завърши празна продажба");

            RecalculateSale(saleId);

            sale.PaymentType = paymentType;
            sale.IsCompleted = true;

            saleRepo.Update(sale);
        }

        public string GenerateReceipt(int saleId)
        {
            var sale = saleRepo.GetById(saleId);
                if(sale == null){
                    throw new Exception("Продажбата не е намерена");
                }

            var items = saleItemRepo.GetBySaleId(saleId).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("--- БЕЛЕЖКА ---");
            sb.AppendLine($"ID на продажба: {sale.Id}");
            sb.AppendLine($"Дата: {sale.Date}");
            sb.AppendLine();
            sb.AppendLine("Количество | Име | Единична цена | Обща цена");

            foreach (var item in items)
            {
                var product = productRepo.GetById((int)item.ProductId!);
                sb.AppendLine($"{item.Quantity} | {product?.Name} | {item.UnitPrice:C} | {item.LineTotal:C}");
            }

            sb.AppendLine();
            sb.AppendLine($"Междинна сума: {sale.Subtotal:C}");
            sb.AppendLine($"Отстъпка: {sale.DiscountAmount:C}");
            sb.AppendLine($"Общо: {sale.Total:C}");
            sb.AppendLine($"Начин на плащане: {sale.PaymentType}");
            sb.AppendLine($"Завършена: {sale.IsCompleted}");
            sb.AppendLine("--- КРАЙ НА БЕЛЕЖКАТА ---");

            return sb.ToString();
        }

        public void RemoveProductFromSale(int saleId, int productId)
        {
            var sale = saleRepo.GetById(saleId);
                if(sale is not null){
                    throw new Exception("Продажбата не е намерена");
                }

            if (sale!.IsCompleted)
                throw new Exception("Не може да се променя завършена продажба");

            var item = saleItemRepo.GetBySaleId(saleId)
                .FirstOrDefault(i => i.ProductId == productId);
                if(item is not null){
                    throw new Exception("Продуктът не е намерен в продажбата");
                }
            var product = productRepo.GetById(productId);
                if(product is not null){
                    throw new Exception("Продуктът не е намерен");
                }

            product!.IncreaseStock(item!.Quantity);
            productRepo.Update(product);

            saleItemRepo.Delete(item.Id);

            RecalculateSale(saleId);
        }

        public IEnumerable<(Product Product, int Quantity)> GetMostPurchasedProducts(int top = 10)
        {
            var items = saleItemRepo.GetAll();

            var completedSaleIds = saleRepo.GetAll()
                .Where(s => s.IsCompleted)
                .Select(s => s.Id)
                .ToHashSet();

            var grouped = items
                .Where(i => completedSaleIds.Contains((int)i.SaleId!))
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(i => i.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .Take(top);

            var result = new List<(Product, int)>();

            foreach (var g in grouped)
            {
                var product = productRepo.GetById((int)g.ProductId!);
                if (product != null)
                    result.Add((product, g.Quantity));
            }

            return result;
        }

        public Sale GetSaleById(int saleId)
        {
            var sale = saleRepo.GetById(saleId);
            if (sale == null)
                throw new Exception("Продажбата не е намерена.");

            return sale;
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
