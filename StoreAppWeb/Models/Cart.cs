using StoreApp.Data.Model;

namespace StoreAppWeb.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public virtual void AddItem(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item==null)
            {
                Items.Add(new CartItem(){Product = product,Quantity = quantity});
            }
            else
            {
               item.Quantity+= quantity;
            }
        }

        public virtual void RemoveItem(Product product)
        {
            Items.RemoveAll(c => c.Product.Id==product.Id);
        }

        public decimal CalculateTotal()
        {
            return Items.Sum(c => (int) c.Product.Price *  c.Quantity);
        }

        public virtual void Clear()
        {
            Items.Clear();
        }
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
    }




}
