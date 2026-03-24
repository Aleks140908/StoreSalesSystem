using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Domain.Entities
{
    internal class Product
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;   
        public string Gender { get; set; } = "Unisex";     

        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
        public int StockQuantity { get; set; }

        
    }
}
