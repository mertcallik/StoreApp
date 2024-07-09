using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreApp.Data.Model;

namespace StoreAppWeb.Models
{
    public class OrderViewModel
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public string? SurnName { get; set; } = string.Empty;
        [Required]
        public string? City { get; set; } = string.Empty;
        [Required]
        public string? Phone { get; set; } = string.Empty;
        [Required]
        public string? Email { get; set; } = string.Empty;
        [Required]
        public string? AdressLine { get; set; } = string.Empty;

        [BindNever]
        public Cart? Cart { get; set; } = null!;

        [DataType(DataType.CreditCard)]
        [Required]
        public string? CardHolderName { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? CardNumber { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? ExpireMonth { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? ExpireYear { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]

        public string? Cvc { get; set; }

    }
}
