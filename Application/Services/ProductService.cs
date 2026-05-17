using StoreSalesSystem.Domain.Entities;
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
        public Product AddProduct(string code, string name, decimal price, int categoryId, int stockQuantity = 0)
        {
            var category = categoryRepo.GetById(categoryId);
            if (category == null)
                throw new Exception("Category not found");

            var product = new Product(code, name, price, categoryId, stockQuantity);

            return productRepo.Add(product);
        }
    }
}
