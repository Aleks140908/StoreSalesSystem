using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreSalesSystem.Infrastructure
{
    internal class FileStorage
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public List<Sale> Sales { get; set; } = new();
        public List<SaleItem> SaleItems { get; set; } = new();
        public List<PromoCode> PromoCodes { get; set; } = new();

        private readonly string path;

        public FileStorage(string Filepath)
        {
            path = Filepath;
        }

        public FileStorage Load()
        {
            if (!File.Exists(path))
            {
                return this;
            }

            var json = File.ReadAllText(path);

            var storage = JsonSerializer.Deserialize<FileStorage>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return storage ?? this;
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);

        }
    }
}
