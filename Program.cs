using StoreSalesSystem.Application;
using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Application.Services;
using StoreSalesSystem.ConsoleUI;
using StoreSalesSystem.Domain.Entities;
using StoreSalesSystem.Infrastructure;
using System.Text.Json;

namespace StoreSalesSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var productRepo = new FileProductRepository();
            var categoryRepo = new FileCategoryRepository();
            var customerRepo = new FileCustomerRepository();
            var saleRepo = new FileSaleRepository();
            var saleItemRepo = new FileSaleItemRepository();
            var promoRepo = new FilePromoCodeRepository();

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
