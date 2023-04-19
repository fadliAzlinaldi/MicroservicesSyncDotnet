using System.ComponentModel.DataAnnotations;

namespace OrderServices.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
