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
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IProductRepository productRepo;

        public CategoryService(ICategoryRepository categoryRepo, IProductRepository productRepo)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
        }
<<<<<<< HEAD
=======
        public Category AddCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Задължително е да въведете име на категорията");

            if (categoryRepo.GetAll().Any(c => c.Name.ToLower() == name.ToLower()))
                throw new Exception("Категорията вече съществува");

            var category = new Category(name);
            return categoryRepo.Add(category);
        }
        
>>>>>>> 0c81fa4c0f0a9560bc19ad9f679ebed064bc16e5
    }
}
