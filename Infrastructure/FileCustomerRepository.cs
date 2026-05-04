using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;

namespace StoreSalesSystem.Infrastructure
{
    public class FileCustomerRepository : ICustomerRepository
    {
        private readonly FileStorage storage;

        public FileCustomerRepository(FileStorage storage)
        {
            this.storage = storage;
        }
        public Customer Add(Customer customer)
        {
            if (storage.Customers.Any())
                customer.Id = storage.Customers.Max(c => c.Id) + 1;
            else
                customer.Id = 1;

            storage.Customers.Add(customer);
            storage.Save();
            return customer;
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer == null) return;

            storage.Customers.Remove(customer);
            storage.Save();
        }

        public IEnumerable<Customer> GetAll()
        {
            return storage.Customers;
        }

        public Customer? GetById(int id)
        {
            return storage.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.Id);
            if (existingCustomer == null) return;

            existingCustomer.Name = customer.Name;

            storage.Save();
        }
    }
}