using StoreSalesSystem.Application.Interfaces;
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
        private readonly IProductRepository productRepo;
        private readonly ICategoryRepository categoryRepo;

        public ProductService(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
        }
        public Product AddProduct(string code, string name, decimal price, int categoryId, int stockQuantity = 0)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new Exception("Задължително е да въведете код на продукта");

            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Името не може да бъде празно поле");

            if (price < 0) throw new Exception("Цената трябва да бъде положителна");

            if (productRepo.GetByCode(code) != null) throw new Exception("Кодът на продукта вече съществува");
            var category = categoryRepo.GetById(categoryId);
            if (category == null)
                throw new Exception("Категорията не е намерена");

            var product = new Product(code, name, price, categoryId, stockQuantity);

            return productRepo.Add(product);
        }
        public void EditProduct(int id, string name, decimal price, int categoryId)
        {
            var product = productRepo.GetById(id);

            if (product == null) throw new Exception("Продуктът не е намерен");
            if (!product.IsActive) throw new Exception("Не може да се редактира неактивен продукт");
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Името не може да бъде празно поле");
            if (price < 0) throw new Exception("Цената трябва да бъде положителна");

            var category = categoryRepo.GetById(categoryId);
            if (category == null)
                throw new Exception("Категорията не е намерена");
            product.Name = name;
            product.Price = price;
            product.CategoryId = categoryId;
            productRepo.Update(product);
        }
        public void DeactivateProduct(int id)
        {
            var product = productRepo.GetById(id);
            if (product == null)
                throw new Exception("Продуктът не е намерен");

            product.IsActive = false;
            productRepo.Update(product);
        }
        public bool HasStock(int productId, int quantity)
        {
            var product = productRepo.GetById(productId);
            if (product == null)
                throw new Exception("Продуктът не е намерен");

            return product.StockQuantity >= quantity;
        }
        public IEnumerable<Product> SearchProducts(string text)
        {
            return productRepo.GetAll()
                .Where(p => p.Name.Contains(text, StringComparison.OrdinalIgnoreCase) || p.Code.Contains(text, StringComparison.OrdinalIgnoreCase));
        }
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return productRepo.GetAll()
                .Where(p => p.CategoryId == categoryId && p.IsActive);
        }
        public IEnumerable<Product> GetProducts()
        {
            return productRepo.GetAll();
        }
    }
}
