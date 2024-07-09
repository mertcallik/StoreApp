using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Model
{
    public class Order
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int Id { get; set; }
        public DateTime OrderData { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AdressLine { get; set; } = string.Empty;
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
        public double  Price { get; set; }
        public int  Quantity { get; set; }
    }
}
