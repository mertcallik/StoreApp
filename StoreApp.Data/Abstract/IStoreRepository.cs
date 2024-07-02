using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Data.Model;

namespace StoreApp.Data.Abstract
{
    public interface IStoreRepository<T> where T : class
    {
        IQueryable<T> GetAll { get; }
        Task Create(T entity);
        int GetProductCount(string category);
        IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize);
    }
}
