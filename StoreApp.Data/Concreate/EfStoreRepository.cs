using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;

namespace StoreApp.Data.Concreate
{
    public class EfStoreRepository<T> : IStoreRepository<T> where T : class
    {
        private readonly StoreDbContext _context;
        public EfStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll => _context.Set<T>();

        public async Task Create(T entity)
        {
            _context.Add(entity);
           await _context.SaveChangesAsync();
        }

        [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
        public int GetProductCount(string category)
        {
            return category == null
                ? _context.Products.Count()
                : _context.Products.Include(p => p.Categories).Where(p => p.Categories.Any(a => a.Url == category))
                    .Count();
        }

        public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(p => p.Categories).Where(w => w.Categories.Any(a => a.Url == category));
            }
            return products.Skip((page-1)*pageSize).Take(pageSize);

        }
    }
}
