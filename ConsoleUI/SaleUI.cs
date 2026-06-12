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

                Console.WriteLine("\n=== ПРОДУКТИ ===");
                Console.WriteLine("1. Добави Продукт");
                Console.WriteLine("2. Редактирай Продукт");
                Console.WriteLine("3. Деактивирай Продукт");
                Console.WriteLine("4. Търсене на продукт");
                Console.WriteLine("5. Филтрирай по категория");
                Console.WriteLine("6. Провери наличност");
                Console.WriteLine("7. Увеличи количество");

                Console.WriteLine("\n=== КАТЕГОРИИ ===");
                Console.WriteLine("8. Добави категория");
                Console.WriteLine("9. Редактирай категория");
                Console.WriteLine("10. Изтрий Категория");
                Console.WriteLine("11. Покажи всички категории");

                Console.WriteLine("\n=== ПРОДАЖБИ  ===");
                Console.WriteLine("12. Създай Продажба");
                Console.WriteLine("13. Добави продукт към продажба ");
                Console.WriteLine("14. Промени количество в продажба");
                Console.WriteLine("15. Премахни продукт от продажба");
                Console.WriteLine("16. Приложи промо код");
                Console.WriteLine("17. Премахни промо код");
                Console.WriteLine("18. Пресметни продажба");
                Console.WriteLine("19. Завърши продажба");
                Console.WriteLine("20. Издай касова бележка");
                Console.WriteLine("21. История на продажбите");

                Console.WriteLine("\n=== ПРОМО КОДОВЕ ===");
                Console.WriteLine("22. Добави промо код");
                Console.WriteLine("23. Редактирай промо код");
                Console.WriteLine("24. Деактивирай промо код");
                Console.WriteLine("25. Провери валидност на промо код");

                Console.WriteLine("\n=== РЕПОРТИ ===");
                Console.WriteLine("26. Най-продавани продукти");
                Console.WriteLine("27. Продажби по период");
                Console.WriteLine("0. Изход");
                Console.Write("Избери: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProductUI(); break;
                    case "2": EditProductUI(); break;
                    case "3": DeactivateProductUI(); break;
                    case "4": SearchProductsUI(); break;
                    case "5": FilterByCategoryUI(); break;
                    case "6": CheckStockUI(); break;
                    case "7": AddQuantityOfProductUI(); break;

                    case "8": AddCategoryUI(); break;
                    case "9": UpdateCategoryUI(); break;
                    case "10": DeleteCategoryUI(); break;
                    case "11": ShowAllCategoriesUI(); break;

                    case "12": CreateSaleUI(); break;
                    case "13": AddProductToSaleUI(saleService); break;
                    case "14": ChangeQuantityInSaleUI(); break;
                    case "15": RemoveProductFromSaleUI(); break;
                    case "16": ApplyPromoCodeUI(); break;
                    case "17": RemovePromoCodeUI(); break;
                    case "18": CalculateSaleUI(); break;
                    case "19": CompleteSaleUI(); break;
                    case "20": GenerateReceiptUI(); break;
                    case "21": ShowSalesHistoryUI(); break;

                    case "22": AddPromoCodeUI(); break;
                    case "23": EditPromoCodeUI(); break;
                    case "24": DeactivatePromoCodeUI(); break;
                    case "25": CheckPromoCodeValidityUI(); break;

                    case "26": ShowMostPurchasedProductsUI(); break;
                    case "27": ShowSalesByPeriodUI(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Избери отново");
                        Console.ReadKey();
                        break;
                }
            }

        }
        private void ShowSalesByPeriodUI()
        {
            Console.Write("Начална дата (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime from))
            {
                Console.WriteLine("Невалидна начална дата.");
                Console.ReadKey();
                return;
            }

            Console.Write("Крайна дата (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime to))
            {
                Console.WriteLine("Невалидна крайна дата.");
                Console.ReadKey();
                return;
            }

            if (to < from)
            {
                Console.WriteLine("Крайната дата трябва да е след началната.");
                Console.ReadKey();
                return;
            }

            try
            {
                var sales = saleService.GetSalesByPeriod(from, to);

                if (!sales.Any())
                {
                    Console.WriteLine("Няма продажби за този период.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nПродажби от {from:yyyy-MM-dd} до {to:yyyy-MM-dd}:\n");

                foreach (var sale in sales)
                {
                    Console.WriteLine($"ID: {sale.Id} | Total: {sale.Total:C} | Date: {sale.Date:yyyy-MM-dd HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        private void RemovePromoCodeUI()
        {
            Console.Write("Продажба ID: ");
            if (!int.TryParse(Console.ReadLine(), out int saleId))
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            try
            {
                saleService.RemovePromo(saleId);
                Console.WriteLine("Промо кодът е премахнат.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        private void CompleteSaleUI()
        {
            Console.Write("Продажба ID: ");
            if (!int.TryParse(Console.ReadLine(), out int saleId))
            {
                Console.WriteLine("Невалидно ID.");

                return;
            }

            Console.Write("Тип плащане (0=Cash, 1=Card): ");
            if (!int.TryParse(Console.ReadLine(), out int type))
            {
                Console.WriteLine("Невалиден тип.");
                return;
            }

            try
            {
                saleService.CompleteSale(saleId, (PaymentType)type);
                Console.WriteLine("Продажбата е завършена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        private void ShowAllCategoriesUI()
        {
            var categories = categoryService.GetAllCategories();

            foreach (var c in categories)
                Console.WriteLine($"{c.Id} | {c.Name}");

            Console.ReadKey();
        }
        private void DeleteCategoryUI()
        {
            Console.Write("Категория ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            try
            {
                categoryService.DeleteCategory(id);
                Console.WriteLine("Категорията е изтрита.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        private void GenerateReceiptUI()
        {
            Console.Write("Продажба ID: ");
            string saleInput = Console.ReadLine()!;
            if (!int.TryParse(saleInput, out int saleId) || saleId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            try
            {
                var receipt = saleService.GenerateReceipt(saleId);
                Console.WriteLine(receipt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void ShowMostPurchasedProductsUI()
        {
            Console.Write("Колко топ продукта да покажем? (по подразбиране 10): ");
            string input = Console.ReadLine();
            int top = 10;
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int parsed) && parsed > 0)
                top = parsed;

            try
            {
                var list = saleService.GetMostPurchasedProducts(top);
                if (list == null || !list.Any())
                {
                    Console.WriteLine("Няма продажби или записи на продажби.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Топ {top} най-продавани продукти:");
                foreach (var (product, qty) in list)
                {
                    Console.WriteLine($"{product.Id} | {product.Name} | Sold: {qty}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void RemoveProductFromSaleUI()
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

            try
            {
                saleService.RemoveProductFromSale(saleId, productId);
                Console.WriteLine("Продуктът е премахнат от продажбата.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void UpdateCategoryUI()
        {
            Console.Write("Категория ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Ново име на категория: ");
            string newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Името не може да бъде празно.");
                return;
            }

            try
            {
                categoryService.EditCategory(id, newName);
                Console.WriteLine("Категорията е обновена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void AddQuantityOfProductUI()
        {
            Console.Write("Продукт ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int productId) || productId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                return;
            }

            Console.Write("Количество за добавяне: ");
            string qtyInput = Console.ReadLine()!;
            if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
            {
                Console.WriteLine("Невалидно количество.");
                return;
            }

            try
            {
                productService.AddQuantityOfProduct(productId, qty);
                Console.WriteLine("Наличното количество е обновено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void ChangeQuantityInSaleUI()
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

            Console.Write("Ново количество: ");
            string qtyInput = Console.ReadLine()!;
            if (!int.TryParse(qtyInput, out int newQty) || newQty <= 0)
            {
                Console.WriteLine("Невалидно количество.");
                return;
            }

            try
            {
                var item = saleService.ChangeQuantityInSale(saleId, productId, newQty);
                Console.WriteLine($"Количество обновено: {item.Quantity}, Нова обща цена: {item.LineTotal:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void CalculateSaleUI()
        {
            Console.Write("Продажба ID: ");
            string saleInput = Console.ReadLine()!;
            if (!int.TryParse(saleInput, out int saleId) || saleId <= 0)
            {
                Console.WriteLine("Невалидно ID.");
                Console.ReadKey();
                return;
            }

            try
            {
                saleService.RecalculateSale(saleId);


                var sale = saleService.GetSaleById(saleId);
                if (sale == null)
                {
                    Console.WriteLine("Продажбата не е намерена.");
                    Console.ReadKey();
                    return;
                }


                Console.WriteLine($"Нова междинна сума: {sale.Subtotal:C}");
                Console.WriteLine($"Нова отстъпка: {sale.DiscountAmount:C}");
                Console.WriteLine($"Нова обща сума: {sale.Total:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }

            Console.ReadKey();
        }
        private void AddCategoryUI()
        {
            Console.Write("Въведи име на категория: ");
            string name = Console.ReadLine();

            try
            {
                var category = categoryService.AddCategory(name);
                Console.WriteLine($"Категорията е добавена успешно! ID: {category.Id}, Име: {category.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }

            Console.ReadKey();
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

        private void ApplyPromoCodeUI()
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

        private void ShowSalesHistoryUI()
        {
            var sales = saleService.GetSalesHistory();

            if (sales == null || sales.Count() == 0)
            {
                Console.WriteLine("Няма продажби.");
                return;
            }

            foreach (var sale in sales)
            {
                Console.WriteLine($"{sale.Id} | Обща сума: {sale.Total} | Дата: {sale.Date}");
            }
            Console.ReadKey();
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
            Console.ReadKey();
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
            Console.ReadKey();
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

            Console.Write("Количество: ");
            string qtyInput = Console.ReadLine()!;
            if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
            {
                Console.WriteLine("Невалидно количество.");
                return;
            }

            bool hasStock = productService.HasStock(productId, qty);

            Console.WriteLine(hasStock ? "Има наличност." : "Няма достатъчно наличност.");
            Console.ReadKey();
        }
        private void AddPromoCodeUI()
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


        private void EditPromoCodeUI()
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


        private void DeactivatePromoCodeUI()
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

        private void CheckPromoCodeValidityUI()
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