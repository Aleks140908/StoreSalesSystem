using Microsoft.EntityFrameworkCore;
using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Infrastructure.SQL_Repositories
{
    public class SQLProductRepository:IProductRepository
    {
        private readonly StoreDbContext context;

        public SQLProductRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return;

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products
                .AsNoTracking()
                .ToList();
        }

        public Product? GetById(int id)
        {
            return context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId)
        {
            return context.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public Product? GetByCode(string code)
        {
            return context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Code == code);
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        IEnumerable<Product> IProductRepository.GetByCategory(int categoryId)
        {
            return GetByCategoryId(categoryId);
        }

        IEnumerable<Product> IProductRepository.GetActive()
        {
            return context.Products
           .AsNoTracking()
           .Where(p => p.IsActive)
           .ToList();
        }
    }
}
