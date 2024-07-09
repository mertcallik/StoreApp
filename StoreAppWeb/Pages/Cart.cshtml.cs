// Gerekli namespace ve kütüphanelerin eklenmesi
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;
using StoreAppWeb.Helpers;
using StoreAppWeb.Models;

namespace StoreAppWeb.Pages
{
    // CartModel sınıfı, Razor Page için model sınıfıdır ve sepet işlemlerini yönetir
    public class CartModel : PageModel
    {
        private IStoreRepository<Product> _productRepository;

        // CartModel sınıfının yapıcı metodu, productRepository ve cartService bağımlılıklarını alır
        public CartModel(IStoreRepository<Product> productRepository, Cart cartService)
        {
            _productRepository = productRepository;
            Cart = cartService;
        }

        // Sepet nesnesi, sayfada kullanılmak üzere tanımlanır
        public Cart? Cart { get; set; }

        // OnGet metodu, sayfa yüklendiğinde çalışır
        public void OnGet()
        {
            // Sayfa yüklendiğinde herhangi bir işlem yapılmaz
        }

        // OnPost metodu, bir ürün sepete eklendiğinde çalışır
        public IActionResult OnPost(int id)
        {
            // Veritabanından ürün bilgilerini alır
            var product = _productRepository.GetAll.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                // Ürün sepete eklenir
                Cart?.AddItem(product, 1);
            }
            // Sepet sayfasına yönlendirme yapılır
            return RedirectToPage("/Cart");
        }

        // OnPostRemove metodu, bir ürün sepetten çıkarıldığında çalışır
        public IActionResult OnPostRemove(int id)
        {
            // Sepetteki ürünü bulur
            var item = Cart.Items.FirstOrDefault(c => c.Product.Id == id)?.Product;
            if (item != null)
            {
                // Ürün sepetten çıkarılır
                Cart?.RemoveItem(item);
            }
            // Sepet sayfasına yönlendirme yapılır
            return RedirectToPage("/Cart");
        }
    }
}
