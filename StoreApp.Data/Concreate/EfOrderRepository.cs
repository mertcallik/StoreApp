using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;

namespace StoreApp.Data.Concreate
{
    public class EfOrderRepository:IOrderRepository
    {
        private readonly StoreDbContext _context;

        public EfOrderRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders;
        public async Task SaveOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
