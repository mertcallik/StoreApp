using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Data.Abstract;

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

         
    }
}
