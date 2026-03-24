using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public Customer(string name, ICollection<Sale>? sales = null)
        {
            Name = name;
            Sales = sales;
            new List<Sale>();
        }
    }
}
