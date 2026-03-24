using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application
{
    internal class SaleService
    {
        private IProductRepository productRepo;
        private ICategoryRepository categoryRepo;
        private ICustomerRepository customerRepo;
        private ISaleRepository saleRepo;
        private IPromoCodeRepository promoRepo;

        public SaleService(IProductRepository productRepo, ICategoryRepository categoryRepo, ICustomerRepository customerRepo, ISaleRepository saleRepo, IPromoCodeRepository promoRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
            this.customerRepo = customerRepo;
            this.saleRepo = saleRepo;
            this.promoRepo = promoRepo;
        }
    }
}
