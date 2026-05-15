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
            string code = Console.ReadLine();

            Console.Write("Име: ");
            string name = Console.ReadLine();

            Console.Write("Цена: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Категори ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Наличност: ");
            int stock = int.Parse(Console.ReadLine());

            productService.AddProduct(code, name, price, categoryId, stock);

            Console.WriteLine("Продуктът е добавен!");
            Console.ReadKey();
        }
        private void ApplyPromoUI()
        {
            Console.Write("Продажба ID: ");
            int saleId = int.Parse(Console.ReadLine());

            Console.Write("Промо Код: ");
            string code = Console.ReadLine();

            saleService.ApplyPromo(saleId, code);

            Console.WriteLine("Промото е приложено!");
            Console.ReadKey();
        }
        private void ViewSalesHistoryUI()
        {
            var sales = saleService.GetSalesHistory();
            foreach (var sale in sales)
            {
                Console.WriteLine($"Продажба ID: {sale.Id}, Общо: {sale.Total}, Дата: {sale.Date}");
            }
            Console.ReadKey();

        }
        private void AddProductToSaleUI(SaleService saleService)
        {
            Console.Write("Продажба ID: ");
            int saleId = int.Parse(Console.ReadLine());

            Console.Write("Продукт ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Количество: ");
            int qty = int.Parse(Console.ReadLine());

            saleService.AddProductToSale(saleId, productId, qty);

            Console.WriteLine("Продуктът е добавен към продажбата!");
            Console.ReadKey();
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
            int id = int.Parse(Console.ReadLine());

            Console.Write("Новo Име: ");
            string name = Console.ReadLine();

            Console.Write("Нова Цена: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Нова Категори ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            productService.EditProduct(id, name, price, categoryId);

            Console.WriteLine("Продуктът е обновен!");
            Console.ReadKey();
        }
        private void DeactivateProductUI()
        {
            Console.Write("Продукт ID: ");
            int id = int.Parse(Console.ReadLine());

            productService.DeactivateProduct(id);

            Console.WriteLine("Продуктът е деактивиран!");
            Console.ReadKey();
        }
        private void SearchProductsUI()
        {
            Console.Write("Търсене на текст: ");
            string text = Console.ReadLine();

            var results = productService.SearchProducts(text);

            foreach (var p in results)
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Code} | {p.Price}");

            Console.ReadKey();
        }
        private void FilterByCategoryUI()
        {
            Console.Write("Категори ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            var products = productService.GetProductsByCategory(categoryId);

            foreach (var p in products)
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price}");

            Console.ReadKey();
        }
        private void CheckStockUI()
        {
            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int qty = int.Parse(Console.ReadLine());

            bool hasStock = productService.HasStock(productId, qty);

            Console.WriteLine(hasStock ? "Наличен е наличен запас" : "Няма достатъчно наличен запас");
            Console.ReadKey();
        }
        private void AddPromoUI()
        {
            Console.Write("Промо Код: ");
            string code = Console.ReadLine();

            Console.Write("Тип Промо (0 = Процент, 1 = Фиксирана сума): ");
            PromoType type = (PromoType)int.Parse(Console.ReadLine());

            Console.Write("Стойност: ");
            decimal value = decimal.Parse(Console.ReadLine());

            Console.Write("Валидност От (yyyy-mm-dd): ");
            DateTime from = DateTime.Parse(Console.ReadLine());

            Console.Write("Валидност До (yyyy-mm-dd): ");
            DateTime until = DateTime.Parse(Console.ReadLine());

            promoService.AddPromo(code, type, value, from, until);

            Console.WriteLine("Промо кодът е добавен!");
            Console.ReadKey();
        }

        private void EditPromoUI()
        {
            Console.Write("Промо ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Нов Код: ");
            string code = Console.ReadLine();

            Console.Write("Нов Тип (0 = Процент, 1 = Фиксирана сума): ");
            PromoType type = (PromoType)int.Parse(Console.ReadLine());

            Console.Write("Нова Стойност: ");
            decimal value = decimal.Parse(Console.ReadLine());

            Console.Write("Нова Валидност От (yyyy-mm-dd): ");
            DateTime from = DateTime.Parse(Console.ReadLine());

            Console.Write("Нова Валидност До (yyyy-mm-dd): ");
            DateTime until = DateTime.Parse(Console.ReadLine());

            promoService.EditPromo(id, code, type, value, from, until);

            Console.WriteLine("Промо кодът е актуализиран!");
            Console.ReadKey();
        }

        private void DeactivatePromoUI()
        {
            Console.Write("Промо ID: ");
            int id = int.Parse(Console.ReadLine());

            promoService.DeactivatePromo(id);

            Console.WriteLine("Промо кодът е деактивиран!");
            Console.ReadKey();
        }

        private void CheckPromoValidityUI()
        {
            Console.Write("Промо Код: ");
            string code = Console.ReadLine();

            bool valid = promoService.IsPromoValid(code);

            Console.WriteLine(valid ? "Промо кодът е ВАЛИДЕН" : "Промо кодът НЕ е валиден");
            Console.ReadKey();
        }
    }
}