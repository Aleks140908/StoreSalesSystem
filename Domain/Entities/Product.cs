using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Domain.Entities
{
   public class Product
   {
        public int Id { get; set; }
        public string Code { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public Product() { }
        public Product(string code, string name, decimal price, int categoryId, int stockQuantity = 0)
        {
           
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("You need to enter the product's code!");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("You need to enter the produc's name!");
            if (price < 0) throw new ArgumentException("Price must be a positive number!");
            if (categoryId <= 0) throw new ArgumentException("CategoryId mustn't be negative nor 0!");
            if (stockQuantity < 0) throw new ArgumentException("Stock cannot be negative!");


            Code = code;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            StockQuantity = stockQuantity;
            IsActive = true;
        }
        public void IncreaseStock(int amount)//увеличава бройката
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive numbr!");
            StockQuantity += amount;
        }
        public void DecreaseStock(int amount)//намалява бройката
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive numbr!");
            if (StockQuantity < amount) throw new ArgumentException("Not enough stock!");
            StockQuantity -= amount;
        }
        public void Deactivate()//деактивира продукта от системата
        {
            IsActive = false;
        }
   }
}
