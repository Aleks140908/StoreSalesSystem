using StoreSalesSystem.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace StoreSalesSystem.Domain.Entities
{
    public class Category : ICategoryRepository
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Category(int id, string name)
        {
            if (id < 0) throw new ArgumentException("Id must be a positive number");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("You need to enter the category's name!");

            Id = id;
            Name = name;
        }
    }
}
