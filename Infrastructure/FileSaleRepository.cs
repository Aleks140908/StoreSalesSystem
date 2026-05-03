using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;

namespace StoreSalesSystem.Infrastructure
{
   public class FileSaleRepository : ISaleRepository
    {
        private FileStorage storage;

        public FileSaleRepository(FileStorage storage)
        {
            this.storage = storage;
        }

        public Sale Add(Sale sale)
        {
            if (storage.Sales.Any()) sale.Id = storage.Sales.Max(s => s.Id) + 1;
            else sale.Id = 1;
           

            storage.Sales.Add(sale);
            storage.Save();
            return sale;
        }
        public IEnumerable<Sale> GetAll()
        {
            return storage.Sales;
        }
        public Sale? GetById(int id)
        {
            return storage.Sales.FirstOrDefault(s => s.Id == id);
        }

        public void Update(Sale sale)
        {
            var existingSale = storage.Sales.FirstOrDefault(s => s.Id == sale.Id);
            if (existingSale == null) return;

            existingSale.Date = sale.Date;
            existingSale.Subtotal = sale.Subtotal;
            existingSale.DiscountAmount = sale.DiscountAmount;
            existingSale.Total = sale.Total;
            existingSale.PaymentType = sale.PaymentType;
            existingSale.PromoCodeId = sale.PromoCodeId;
            existingSale.CustomerId = sale.CustomerId;
            existingSale.IsCompleted = sale.IsCompleted;

            storage.Save();
        }
    }
}