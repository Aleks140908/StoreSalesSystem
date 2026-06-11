using StoreSalesSystem.Application.Services;
using StoreSalesSystem.Domain.Enums;

namespace StoreSalesSystem.ConsoleUI
{
    public class SaleUI
    {
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        private readonly PromoService promoService;
        private readonly SaleService saleService;

        public SaleUI(ProductService productService, CategoryService categoryService, PromoService promoService, SaleService saleService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.promoService = promoService;
            this.saleService = saleService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== STORE SALES SYSTEM ===");
                Console.WriteLine("1. Добави Продукт");
                Console.WriteLine("2. Създай Продажба");
                Console.WriteLine("3. Добави Продукт към Продажба");
                Console.WriteLine("4. Приложи Промо");
                Console.WriteLine("5. Завърши Продажба");
                Console.WriteLine("6. Преглед на Историята на Продажбите");
                Console.WriteLine("7. Редактирай Продукт");
                Console.WriteLine("8. Деактивирай Продукт");
                Console.WriteLine("9. Търсене на Продукти");
                Console.WriteLine("10. Филтрирай Продукти по Категория");
                Console.WriteLine("11. Провери Наличност");
                Console.WriteLine("12. Добави Промо Код");
                Console.WriteLine("13. Редактирай Промо Код");
                Console.WriteLine("14. Деактивирай Промо Код");
                Console.WriteLine("15. Провери Валидността на Промо Кода");
                Console.WriteLine("0. Изход");
                Console.Write("Избери: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProductUI(); break;
                    case "2": CreateSaleUI(); break;
                    case "3": AddProductToSaleUI(saleService); break;
                    case "4": ApplyPromoUI(); break;
                    //case "5": CompleteSaleUI(); break;
                    case "6": ViewSalesHistoryUI(); break;
                    case "7": EditProductUI(); break;
                    case "8": DeactivateProductUI(); break;
                    case "9": SearchProductsUI(); break;
                    case "10": FilterByCategoryUI(); break;
                    case "11": CheckStockUI(); break;
                    case "12": AddPromoUI(); break;
                    case "13": EditPromoUI(); break;
                    case "14": DeactivatePromoUI(); break;
                    case "15": CheckPromoValidityUI(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Избери отново");
                        Console.ReadKey();
                        break;
                }
            }

        }
        private void AddProductUI()
        {
            Console.Write("Код: ");
            string code = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(code))
            {
                Console.WriteLine("Кодът е задължителен.");
                return;
            }

            Console.Write("Име: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Името е задължително.");
                return;
            }

            Console.Write("Цена: ");
            string priceInput = Console.ReadLine()!;
            if (!decimal.TryParse(priceInput, out decimal price) || price <= 0)
            {
                Console.WriteLine("Невалидна цена.");
                return;
            }

            Console.Write("Категори ID: ");
            string catInput = Console.ReadLine()!;
            if (!int.TryParse(catInput, out int categoryId) || categoryId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Наличност: ");
            string stockInput = Console.ReadLine()!;
            if (!int.TryParse(stockInput, out int stock) || stock < 0)
            {
                Console.WriteLine("Невалидна наличност.");
                return;
            }

            try
            {
                productService.AddProduct(code, name, price, categoryId, stock);
                Console.WriteLine("Продуктът е добавен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ApplyPromoUI()
        {
            Console.Write("Продажба ID: ");
            string saleInput = Console.ReadLine()!;
            if (!int.TryParse(saleInput, out int saleId) || saleId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Промо Код: ");
            string code = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(code))
            {
                Console.WriteLine("Кодът е задължителен.");
                return;
            }

            try
            {
                saleService.ApplyPromo(saleId, code);
                Console.WriteLine("Промото е приложено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ViewSalesHistoryUI()
        {
            var sales = saleService.GetSalesHistory();

            if (sales == null || sales.Count() == 0)
            {
                Console.WriteLine("Няма продажби.");
                return;
            }

            foreach (var sale in sales)
            {
                Console.WriteLine($"{sale.Id} | Total: {sale.Total} | Date: {sale.Date}");
            }
        }

        private void AddProductToSaleUI(SaleService saleService)
        {
            Console.Write("Продажба ID: ");
            string saleInput = Console.ReadLine()!;
            if (!int.TryParse(saleInput, out int saleId) || saleId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Продукт ID: ");
            string prodInput = Console.ReadLine()!;
            if (!int.TryParse(prodInput, out int productId) || productId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Количество: ");
            string qtyInput = Console.ReadLine()!;
            if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
            {
                Console.WriteLine("Невалидно количество.");
                return;
            }

            try
            {
                saleService.AddProductToSale(saleId, productId, qty);
                Console.WriteLine("Продуктът е добавен към продажбата.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateSaleUI()
        {
            var sale = saleService.CreateSale();
            Console.WriteLine($"Създадена е продажба с ID: {sale.Id}");
            Console.ReadKey();
        }
        private void EditProductUI()
        {
            Console.Write("Продукт ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Ново име: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Името е задължително.");
                return;
            }

            Console.Write("Нова цена: ");
            string priceInput = Console.ReadLine()!;
            if (!decimal.TryParse(priceInput, out decimal price) || price <= 0)
            {
                Console.WriteLine("Невалидна цена.");
                return;
            }

            Console.Write("Нова категория ID: ");
            string catInput = Console.ReadLine()!;
            if (!int.TryParse(catInput, out int categoryId) || categoryId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            try
            {
                productService.EditProduct(id, name, price, categoryId);
                Console.WriteLine("Продуктът е обновен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void DeactivateProductUI()
        {
            Console.Write("Продукт ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            try
            {
                productService.DeactivateProduct(id);
                Console.WriteLine("Продуктът е деактивиран.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SearchProductsUI()
        {
            Console.Write("Търсене: ");
            string text = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Текстът е задължителен.");
                return;
            }

            var results = productService.SearchProducts(text);

            if (results == null || results.Count() == 0)
            {
                Console.WriteLine("Няма намерени продукти.");
                return;
            }

            foreach (var p in results)
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Code} | {p.Price}");
        }


        private void FilterByCategoryUI()
        {
            Console.Write("Категория ID: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int categoryId) || categoryId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            var products = productService.GetProductsByCategory(categoryId);

            if (products == null || products.Count() == 0)
            {
                Console.WriteLine("Няма продукти.");
                return;
            }

            foreach (var p in products)
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price}");
        }

        private void CheckStockUI()
        {
            Console.Write("Product ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int productId) || productId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Quantity: ");
            string qtyInput = Console.ReadLine()!;
            if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
            {
                Console.WriteLine("Невалидно количество.");
                return;
            }

            bool hasStock = productService.HasStock(productId, qty);

            Console.WriteLine(hasStock ? "Има наличност." : "Няма достатъчно наличност.");
        }
        private void AddPromoUI()
        {
            Console.Write("Промо код: ");
            string code = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(code))
            {
                Console.WriteLine("Кодът е задължителен.");
                return;
            }

            Console.Write("Тип (0=Процент, 1=Фиксирана сума): ");
            string typeInput = Console.ReadLine()!;
            if (!int.TryParse(typeInput, out int typeValue) || (typeValue != 0 && typeValue != 1))
            {
                Console.WriteLine("Невалиден тип.");
                return;
            }

            Console.Write("Стойност: ");
            string valInput = Console.ReadLine()!;
            if (!decimal.TryParse(valInput, out decimal value) || value <= 0)
            {
                Console.WriteLine("Невалидна стойност.");
                return;
            }

            Console.Write("Валидност от: ");
            string fromInput = Console.ReadLine()!;
            if (!DateTime.TryParse(fromInput, out DateTime from))
            {
                Console.WriteLine("Невалидна дата.");
                return;
            }

            Console.Write("Валидност до: ");
            string untilInput = Console.ReadLine()!;
            if (!DateTime.TryParse(untilInput, out DateTime until) || until < from)
            {
                Console.WriteLine("Невалидна дата.");
                return;
            }

            try
            {
                promoService.AddPromo(code, (PromoType)typeValue, value, from, until);
                Console.WriteLine("Промо кодът е добавен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void EditPromoUI()
        {
            Console.Write("Промо ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Нов код: ");
            string code = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(code))
            {
                Console.WriteLine("Кодът е задължителен.");
                return;
            }

            Console.Write("Нов тип (0=Процент, 1=Фиксирана сума): ");
            string typeInput = Console.ReadLine()!;
            if (!int.TryParse(typeInput, out int typeValue) || (typeValue != 0 && typeValue != 1))
            {
                Console.WriteLine("Невалиден тип.");
                return;
            }

            Console.Write("Нова стойност: ");
            string valInput = Console.ReadLine()!;
            if (!decimal.TryParse(valInput, out decimal value) || value <= 0)
            {
                Console.WriteLine("Невалидна стойност.");
                return;
            }

            Console.Write("Нова валидност от: ");
            string fromInput = Console.ReadLine()!;
            if (!DateTime.TryParse(fromInput, out DateTime from))
            {
                Console.WriteLine("Невалидна дата.");
                return;
            }

            Console.Write("Нова валидност до: ");
            string untilInput = Console.ReadLine()!;
            if (!DateTime.TryParse(untilInput, out DateTime until) || until < from)
            {
                Console.WriteLine("Невалидна дата.");
                return;
            }

            try
            {
                promoService.EditPromo(id, code, (PromoType)typeValue, value, from, until);
                Console.WriteLine("Промо кодът е обновен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void DeactivatePromoUI()
        {
            Console.Write("Промо ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            try
            {
                promoService.DeactivatePromo(id);
                Console.WriteLine("Промо кодът е деактивиран.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckPromoValidityUI()
        {
            Console.Write("Промо код: ");
            string code = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(code))
            {
                Console.WriteLine("Кодът е задължителен.");
                return;
            }

            bool valid = promoService.IsPromoValid(code);

            Console.WriteLine(valid ? "Промо кодът е валиден." : "Промо кодът не е валиден.");
        }

    }
}