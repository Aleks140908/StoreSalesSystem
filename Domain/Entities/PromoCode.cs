using StoreSalesSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Domain.Entities
{
    public class PromoCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;

        public PromoType Type { get; set; }      
        public decimal Value { get; set; }       

        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public bool IsActive { get; set; } = true;
        public PromoCode() { }

        public PromoCode(string code, PromoType type, decimal value, DateTime validFrom, DateTime validUntil)
        {
            if (string.IsNullOrWhiteSpace(code))throw new ArgumentException("Promo code shouldn't be empty");
            if (value <= 0) throw new ArgumentException("Promo value must be a positive number");
            if (validUntil <= validFrom) throw new ArgumentException("The expiration date of the promo code must be after the manufactored date");

            Code = code;
            Type = type;
            Value = value;
            ValidFrom = validFrom;
            ValidUntil = validUntil;
        }
    }
}
