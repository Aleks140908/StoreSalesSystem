using StoreSalesSystem.Domain.Entities;
using StoreSalesSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Application.Services
{
    public class CategoryService
    {
        private readonly FileCategoryRepository categoryRepo;
        private readonly FileProductRepository productRepo;

        public CategoryService(FileCategoryRepository categoryRepo, FileProductRepository productRepo)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
        }
    }
}
