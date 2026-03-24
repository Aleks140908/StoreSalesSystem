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
    }
}
