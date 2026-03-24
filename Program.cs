using StoreSalesSystem.Application;
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
            
            var storage = new FileStorage("sale.json");

            
            IProductRepository productRepo = new FileProductRepository(storage);
            ICategoryRepository categoryRepo = new FileCategoryRepository(storage);
            ICustomerRepository customerRepo = new FileCustomerRepository(storage);
            ISaleRepository saleRepo = new FileSaleRepository(storage);
            IPromoCodeRepository promoRepo = new FilePromoRepository(storage);

            
            var service = new SaleService(productRepo, categoryRepo, customerRepo, saleRepo, promoRepo);

            
            var ui = new SaleUI(service);

            
            ui.Run();
        }
    }
}
