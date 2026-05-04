using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Infrastructure
{
    public class FileSaleItemRepository : ISaleItemRepository
    {
        private FileStorage storage;
        public FileSaleItemRepository(FileStorage storage)
        {
            this.storage = storage;
        }
        public SaleItem Add(SaleItem item)
        {
           
            if (storage.SaleItems.Any()) item.Id = storage.SaleItems.Max(i => i.Id) + 1;
            else item.Id = 1;

            storage.SaleItems.Add(item);
            storage.Save();
            return item;
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item == null) return;

            storage.SaleItems.Remove(item);
            storage.Save();
        }

        public IEnumerable<SaleItem> GetAll()
        {
            return storage.SaleItems;
        }
        public SaleItem? GetById(int id)
        {
            return storage.SaleItems.FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<SaleItem> GetBySaleId(int saleId)
        {
            return storage.SaleItems.Where(i => i.SaleId == saleId);
        }

        public void Update(SaleItem item)
        {
            var existingItem = GetById(item.Id);

            existingItem.ProductId = item.ProductId;
            existingItem.Quantity = item.Quantity;
            existingItem.UnitPrice = item.UnitPrice;
            existingItem.LineTotal = item.LineTotal;

            storage.Save();
        }
    }
}
