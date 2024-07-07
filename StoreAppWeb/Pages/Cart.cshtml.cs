using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreAppWeb.Pages
{
    public class CartModel : PageModel
    {
        public string message { get; set; } = string.Empty;
        public void OnGet(int id)
        {
            message= $"Product Id: {id}";
        }
    }
}
