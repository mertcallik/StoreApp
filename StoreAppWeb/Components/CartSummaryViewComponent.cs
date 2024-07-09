using Microsoft.AspNetCore.Mvc;
using StoreAppWeb.Models;

namespace StoreAppWeb.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }
        private Cart cart;
        public IViewComponentResult Invoke()
        {
            var model = cart;
            return View(model);
        }
    }
}
