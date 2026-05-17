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
        private readonly FileCategoryRepository categoryRepo;
        private readonly FileProductRepository productRepo;

        public CategoryService(FileCategoryRepository categoryRepo, FileProductRepository productRepo)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
        }
        public Category AddCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Category name cannot be empty");

            if (categoryRepo.GetAll().Any(c => c.Name.ToLower() == name.ToLower()))
                throw new Exception("Category already exists");

            var category = new Category(name);
            return categoryRepo.Add(category);
        }
        public void EditCategory(int id, string newName)
        {
            var category = categoryRepo.GetById(id);
            if (category == null)
                throw new Exception("Category not found");

            if (string.IsNullOrWhiteSpace(newName))
                throw new Exception("Name cannot be empty");

            category.GetType().GetProperty("Name")!.SetValue(category, newName);

            categoryRepo.Update(category);
        }
        public void DeleteCategory(int id)
        {
            var category = categoryRepo.GetById(id);
            if (category == null)
                return;

            if (productRepo.GetAll().Any(p => p.CategoryId == id))
                throw new Exception("Cannot delete category with products");

            categoryRepo.Delete(id);
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return categoryRepo.GetAll();
        }

    }
}
