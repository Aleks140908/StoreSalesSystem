using StoreSalesSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Domain.Entities
{
   public class Sale
   {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }

        public PaymentType PaymentType { get; set; }

        public int? PromoCodeId { get; set; }
        public PromoCode? PromoCode { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Sale()
        {
            Items = new List<SaleItem>();
            CreatedAt = DateTime.Now;
        }

        public Sale(decimal subtotal, decimal discountAmount, decimal total, PaymentType paymentType, int? promoCodeId = null, int? customerId = null)
        {
            Subtotal = subtotal;
            DiscountAmount = discountAmount;
            Total = total;
            PaymentType = paymentType;
            PromoCodeId = promoCodeId;
            CustomerId = customerId;

            Items = new List<SaleItem>();
            CreatedAt = DateTime.Now;
        }
   }
}
