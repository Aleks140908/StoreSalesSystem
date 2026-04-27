using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application
{
    public interface ICustomerRepository
    {
        Customer Add(Customer customer);
        Customer? GetById(int id);

        IEnumerable<Customer> GetAll();
        void Update(Customer customer);
        void Delete(int id);
    }
}
