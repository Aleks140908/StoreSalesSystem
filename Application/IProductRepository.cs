using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application
{
    public interface IProductRepository
    {
        Product Add(Product product);
        Product? GetById(int id);

        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Delete(int id);

        Product? GetByCode(string code);
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> GetActive();
    }
}
