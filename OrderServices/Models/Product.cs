using System.ComponentModel.DataAnnotations;

namespace OrderServices.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int Price { get; set; }
    }
}
