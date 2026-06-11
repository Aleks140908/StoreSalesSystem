using StoreSalesSystem.Application;
using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Application.Services;
using StoreSalesSystem.Domain.Entities;
using StoreSalesSystem.Infrastructure;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoreSalesSystem.ConsoleUI
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var storage = new FileStorage("Data/storage.json").Load();
            storage.Save();


            var productRepo = new  FileProductRepository(storage);
            var categoryRepo = new FileCategoryRepository(storage);
            var customerRepo = new FileCustomerRepository(storage);
            var saleRepo = new FileSaleRepository(storage);
            var saleItemRepo = new FileSaleItemRepository(storage);
            var promoRepo = new FilePromoRepository(storage);
            if (!categoryRepo.GetAll().Any())
            {
                categoryRepo.Add(new Category("Дамски"));
                categoryRepo.Add(new Category("Мъжки"));
                categoryRepo.Add(new Category("Детски"));

                Console.WriteLine("✔ Категориите за дрехи са добавени!");
            }

                // Services
                var productService = new ProductService(productRepo, categoryRepo);
            var categoryService = new CategoryService(categoryRepo, productRepo);
            var promoService = new PromoService(promoRepo);
            var saleService = new SaleService(productRepo, categoryRepo, customerRepo, saleRepo, saleItemRepo, promoRepo);
            
            // UI
            var ui = new SaleUI(productService, categoryService, promoService, saleService);

            ui.Run();

        }
    }
}
