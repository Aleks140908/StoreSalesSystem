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
       // public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public PromoCode() { }

        public PromoCode(string code, PromoType type, decimal value, DateTime validFrom, DateTime validUntil)
        {
            if (string.IsNullOrWhiteSpace(code))throw new ArgumentException("Промо кодът не може да бъде празен");
            if (value <= 0) throw new ArgumentException("Стойността на промо кода трябва да бъде положително число");
            if (validUntil <= validFrom) throw new ArgumentException("Крайната дата на промо кода трябва да бъде след началната дата");

            Code = code;
            Type = type;
            Value = value;
            ValidFrom = validFrom;
            ValidUntil = validUntil;
        }

        public void SetValue(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Стойността на промо кода трябва да бъде положително число");

            Value = value;
        }

        public void SetDates(DateTime from, DateTime until)
        {
            if (until <= from)
                throw new ArgumentException("Крайната дата на промо кода трябва да бъде след началната дата");

            ValidFrom = from;
            ValidUntil = until;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
