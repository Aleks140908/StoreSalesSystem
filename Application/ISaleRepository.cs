using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application
{
    public interface ISaleRepository
    {
        Sale Add(Sale sale);
        Sale? GetById(int id);
        IEnumerable<Sale> GetAll();
    }
}
