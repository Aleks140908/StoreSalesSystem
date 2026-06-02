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
    public class SQLPromoRepository : IPromoCodeRepository
    {
        private readonly StoreDbContext context;

        public SQLPromoRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public PromoCode Add(PromoCode code)
        {
            context.PromoCodes.Add(code);
            context.SaveChanges();
            return code;
        }

        public PromoCode? GetById(int id)
        {
            return context.PromoCodes
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public PromoCode? GetByCode(string code)
        {
            return context.PromoCodes
                .AsNoTracking()
                .FirstOrDefault(p => p.Code == code);
        }

        public IEnumerable<PromoCode> GetAll()
        {
            return context.PromoCodes
                .AsNoTracking()
                .ToList();
        }

        public void Update(PromoCode code)
        {
            context.PromoCodes.Update(code);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var promo = context.PromoCodes.Find(id);
            if (promo == null) return;

            context.PromoCodes.Remove(promo);
            context.SaveChanges();
        }
    }
}
