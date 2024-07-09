using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreAppWeb.Pages
{
    public class ComplatedModel : PageModel
    {
        public int OrderId { get; set; }
        public void OnGet(int orderId)
        {
            OrderId= orderId;
        }
    }
}
