using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Model;

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

            modelBuilder.Entity<Product>().HasMany(p => p.Categories).WithMany(c => c.Products)
                .UsingEntity<ProductCategory>();
            modelBuilder.Entity<Category>().HasIndex(u=>u.Url).IsUnique();

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product() { Id = 1, Name = "Iphone 15 Plus", Description = "Harika bir cihaz", Price = 62000},
                new Product() { Id = 2, Name = "Iphone 15", Description = "Harika bir cihaz", Price = 50000},
                new Product() { Id = 3, Name = "Iphone 15 Pro Max", Description = "Harika bir cihaz", Price = 90000 },
                new Product() { Id = 4, Name = "Iphone 15 Pro", Description = "Harika bir cihaz", Price = 80000 },
                new Product() { Id = 5, Name = "Iphone 14 Pro Max", Description = "Harika bir cihaz", Price = 60000},
                new Product() { Id = 6, Name = "Iphone 14 Pro", Description = "Harika bir cihaz", Price = 55000}
            });
            modelBuilder.Entity<Category>().HasData(new List<Category>()
            {
                new Category() { Id = 1, Name = "Telefon", Url = "telefon" },
                new Category() { Id = 2, Name = "Tablet", Url = "tablet" },
                new Category() { Id = 3, Name = "Bilgisayar", Url = "bilgisayar" },
                new Category() { Id = 4, Name = "Elektronik", Url = "elektronik" },
                new Category() { Id = 5, Name = "Ev Aletleri", Url = "ev-aletleri" } //dotnet slug
            });

            //junction table
            modelBuilder.Entity<ProductCategory>().HasData(new List<ProductCategory>()
            {
                new ProductCategory(){CategoryId = 1,ProductId = 1},
                new ProductCategory(){CategoryId = 1,ProductId = 2},
                new ProductCategory(){CategoryId = 1,ProductId = 3},
                new ProductCategory(){CategoryId = 1,ProductId = 4},
                new ProductCategory(){CategoryId = 1,ProductId = 5},
                new ProductCategory(){CategoryId = 1,ProductId = 6},
                new ProductCategory(){CategoryId = 4,ProductId = 6},
            });

        }

        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<Category> Categories => Set<Category>();
        public virtual DbSet<Order> Orders => Set<Order>();
    }
}
