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
    public class SQLCategoryRepository:ICategoryRepository
    {
        private readonly StoreDbContext context;

        public SQLCategoryRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public Category Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public void Delete(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null) return;

            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.AsNoTracking().ToList();
        }

        public Category? GetById(int id)
        {
            return context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }
    }
}
