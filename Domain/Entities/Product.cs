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
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public Product() { }
        public Product(string code, string name, decimal price, int categoryId, int stockQuantity = 0)
        {

            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Трябва да въведете кода на продукта!");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Трябва да въведете името на продукта!");
            if (price < 0) throw new ArgumentException("Цената трябва да бъде положително число!");
            if (categoryId <= 0) throw new ArgumentException("Идентификаторът на категорията не може да бъде отрицателен или 0!");
            if (stockQuantity < 0) throw new ArgumentException("Наличността не може да бъде отрицателна!");

            Code = code;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            StockQuantity = stockQuantity;
            IsActive = true;
        }
        public void IncreaseStock(int amount)//увеличава бройката
        {
            if (amount <= 0) throw new ArgumentException("Бройката трябва да бъде положително число!");
            StockQuantity += amount;
        }
        public void DecreaseStock(int amount)//намалява бройката
        {
            if (amount <= 0) throw new ArgumentException("Бройката трябва да бъде положително число!");
            if (StockQuantity < amount) throw new ArgumentException("Няма достатъчно наличност!");
            StockQuantity -= amount;
        }
        public void Deactivate()//деактивира продукта от системата
        {
            IsActive = false;
        }
    }
}
