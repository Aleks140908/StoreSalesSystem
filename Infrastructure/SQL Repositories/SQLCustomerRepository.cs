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
    public class SQLCustomerRepository:ICustomerRepository
    {
        private readonly StoreDbContext context;

        public SQLCustomerRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public Customer Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public void Delete(int id)
        {
            var customer = context.Customers.Find(id);
            if (customer == null) return;

            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers
                .AsNoTracking()
                .ToList();
        }

        public Customer? GetById(int id)
        {
            return context.Customers
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(Customer customer)
        {
            context.Customers.Update(customer);
            context.SaveChanges();
        }
    }
}
