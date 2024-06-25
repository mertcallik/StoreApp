using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreAppWeb.Concreate;

namespace StoreApp.Data.Concreate
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product() { Id = 1, Name = "Iphone 15 Plus", Description = "Harika bir cihaz", Price = 62000, Category = "Telefon" },
                new Product() { Id = 2, Name = "Iphone 15", Description = "Harika bir cihaz", Price = 50000, Category = "Telefon" },
                new Product() { Id = 3, Name = "Iphone 15 Pro Max", Description = "Harika bir cihaz", Price = 90000, Category = "Telefon" },
                new Product() { Id = 4, Name = "Iphone 15 Pro", Description = "Harika bir cihaz", Price = 80000, Category = "Telefon" },
                new Product() { Id = 5, Name = "Iphone 14 Pro Max", Description = "Harika bir cihaz", Price = 60000, Category = "Telefon" },
                new Product() { Id = 6, Name = "Iphone 14 Pro", Description = "Harika bir cihaz", Price = 55000, Category = "Telefon" }
            });
        }

        public DbSet<Product> Products => Set<Product>();
    }
}
