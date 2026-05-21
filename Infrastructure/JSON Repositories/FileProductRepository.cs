using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;

namespace StoreSalesSystem.Infrastructure
{
    public class FileProductRepository : IProductRepository
    {
        private readonly FileStorage storage;

        public FileProductRepository(FileStorage storage)
        {
            this.storage = storage;
        }

       

        public Product Add(Product product)
        {
            if (storage.Products.Any())
                product.Id = storage.Products.Max(p => p.Id) + 1;
            else
                product.Id = 1;

            storage.Products.Add(product);
            storage.Save();
            return product;
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return;

            storage.Products.Remove(product);
            storage.Save();
        }

        public IEnumerable<Product> GetAll()
        {
            return storage.Products;
        }

        public Product? GetById(int id)
        {
            return storage.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId)
        {
            return storage.Products.Where(p => p.CategoryId == categoryId);
        }

        public Product? GetByCode(string code)
        {
            return storage.Products.FirstOrDefault(p => p.Code == code);
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct == null) return;

            storage.Products.Remove(existingProduct);
            storage.Products.Add(product);

            storage.Save();
        }

        IEnumerable<Product> IProductRepository.GetByCategory(int categoryId)
        {
          return GetByCategoryId(categoryId);
        }

        IEnumerable<Product> IProductRepository.GetActive()
        {
            return storage.Products.Where(p => p.IsActive);
        }
    }
}