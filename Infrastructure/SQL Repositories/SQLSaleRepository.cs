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
    public class SQLSaleRepository : ISaleRepository
    {
        private readonly StoreDbContext context;

        public SQLSaleRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public Sale Add(Sale sale)
        {
            context.Sales.Add(sale);
            context.SaveChanges();
            return sale;
        }

        public Sale? GetById(int id)
        {
            return context.Sales
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Sale> GetAll()
        {
            return context.Sales
                .AsNoTracking()
                .ToList();
        }

        public void Update(Sale sale)
        {
            context.Sales.Update(sale);
            context.SaveChanges();
        }
    }
}
