using StoreSalesSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application.Services
{
    public class ProductService
    {
        private readonly FileProductRepository productRepo;
        private readonly FileCategoryRepository categoryRepo;

        public ProductService(FileProductRepository productRepo, FileCategoryRepository categoryRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
        }
    }
}
