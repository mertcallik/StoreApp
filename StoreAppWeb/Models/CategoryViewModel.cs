using StoreApp.Data.Model;

namespace StoreAppWeb.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;

    }
}
