using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application
{
    public interface IPromoCodeRepository
    {
        PromoCode Add (PromoCode code);
        PromoCode? GetById (int id);
        PromoCode? GetByCode (string code);
        IEnumerable<PromoCode> GetAll ();
        void Update(PromoCode code);
        void Delete (int id);


    }
}
