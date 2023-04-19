using System.ComponentModel.DataAnnotations;

namespace ProductServices.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
