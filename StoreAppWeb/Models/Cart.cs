using StoreApp.Data.Model;

namespace StoreAppWeb.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();

        public void AddItem(Product product,int quanity)
        {
            var item = Items.Where(c => c.Product.Id == product.Id).FirstOrDefault();
            if (item==null)
            {
                Items.Add(new CartItem(){Product = product,Quanity = quanity});
            }
            else
            {
                item.Quanity+=quanity;
            }
        }

        public void RemoveItem(Product product)
        {
            Items.RemoveAll(i => i.Product.Id == product.Id);
        }

        public decimal CalculateTotal()
        {
            return Items.Sum(i => i.Product.Price * i.Quanity);
        }

    }
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new();
        public int Quanity { get; set; }
    }
}
