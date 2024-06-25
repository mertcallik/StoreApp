using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface IStoreRepository<T> where T : class
    {
        IQueryable<T> GetAll { get; }
        Task Create(T entity);
    }
}
