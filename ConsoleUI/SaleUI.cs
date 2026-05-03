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
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Create Sale");
                Console.WriteLine("3. Add Product to Sale");
                Console.WriteLine("4. Apply Promo");
                Console.WriteLine("5. Complete Sale");
                Console.WriteLine("6. View Sales History");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProductUI(); break;
                    case "2": CreateSaleUI(); break;
                    case "3": AddProductToSaleUI(); break;
                    case "4": ApplyPromoUI(); break;
                    case "5": CompleteSaleUI(); break;
                    case "6": ViewSalesHistoryUI(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid choice. Pick again!");
                        Console.ReadKey();
                        break;
                }
            }
        
        }
        private void ApplyPromoUI()
        {
            Console.Write("Sale ID: ");
            int saleId = int.Parse(Console.ReadLine());

            Console.Write("Promo Code: ");
            string code = Console.ReadLine();

            saleService.ApplyPromo(saleId, code);

            Console.WriteLine("Promo applied!");
            Console.ReadKey();
        }
       
    }
}