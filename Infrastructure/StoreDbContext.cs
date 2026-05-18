using Microsoft.EntityFrameworkCore;
using StoreSalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSalesSystem.Infrastructure
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext()
        {
        }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseSqlServer(
   "Server=(localdb)\\MSSQLLocalDB;Database=SaleSystem;Integrated Security=True;");

        public DbSet<Domain.Entities.Product> Products { get; set; }
        public DbSet<Domain.Entities.Category> Categories { get; set; }
        public DbSet<Domain.Entities.Customer> Customers { get; set; }
        public DbSet<Domain.Entities.PromoCode> PromoCodes { get; set; }
        public DbSet<Domain.Entities.Sale> Sales { get; set; }
        public DbSet<Domain.Entities.SaleItem> SaleItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //product
            modelBuilder.Entity<Domain.Entities.Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Domain.Entities.Product>()
                .HasOne<Domain.Entities.Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
            //category
            modelBuilder.Entity<Domain.Entities.Category>()
                .HasKey(c => c.Id);

            //promo code
            modelBuilder.Entity<Domain.Entities.PromoCode>()
                .HasKey(p => p.Id);
            //customer
            modelBuilder.Entity<Domain.Entities.Customer>()
                .HasKey(c => c.Id);
            //sale
            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.PromoCode)
                .WithMany()
                .HasForeignKey(s => s.PromoCodeId)
                .OnDelete(DeleteBehavior.SetNull);

            //saleItem
            modelBuilder.Entity<SaleItem>()
                .HasKey(si => si.Id);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
