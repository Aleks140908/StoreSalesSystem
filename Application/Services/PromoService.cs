using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;
using StoreSalesSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application.Services
{
    public class PromoService
    {
        private readonly IPromoCodeRepository promoRepo;

        public PromoService(IPromoCodeRepository promoRepo)
        {
            this.promoRepo = promoRepo;
        }
        public PromoCode AddPromo(string code, PromoType type, decimal value, DateTime from, DateTime until)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new Exception("Промоционалният код не може да бъде празен");
            }

            if (value <= 0)
            {
                throw new Exception("Стойността на промоцията трябва да е положителна");
            }

            if (from >= until)
            { 
            throw new Exception("Невалиден диапазон от дати");
            }

            var promo = new PromoCode(code, type, value, from, until);

            
            return promoRepo.Add(promo);
            

        }
        public void EditPromo(int id, string code, PromoType type, decimal value, DateTime from, DateTime until)
        {
            var promo = promoRepo.GetById(id);
            if (promo == null)
            {
                throw new Exception("Промоцията не е намерена");
            }

            promo.SetValue(value);
            promo.SetDates(from, until);

            promoRepo.Update(promo);
        }
        public void DeactivatePromo(int id)
        {
            var promo = promoRepo.GetById(id);
              if (promo == null)
              {
                throw new Exception("Промото не е намерено");
              }

                if (!promo.IsActive)
                throw new Exception("Промото вече е неактивно");

            promo.Deactivate();
            promoRepo.Update(promo);
        }
        public bool IsPromoValid(string code)
        {
            var promo = promoRepo.GetByCode(code);

            if (promo == null)
            { 
            return false;
            }

            if (!promo.IsActive)
            {
                return false;
            }

            if (promo.ValidFrom > DateTime.Now || promo.ValidUntil < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
