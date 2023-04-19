using System.ComponentModel.DataAnnotations;

namespace ProductServices.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
