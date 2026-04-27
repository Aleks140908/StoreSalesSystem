using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application
{
    public interface ISaleItemRepository
    {
        SaleItem Add(SaleItem item);
        SaleItem? GetById(int id);
        IEnumerable<SaleItem> GetAll();
        IEnumerable<SaleItem> GetBySaleId(int saleId);
        void Update(SaleItem item);
        void Delete(int id);

    }
}
