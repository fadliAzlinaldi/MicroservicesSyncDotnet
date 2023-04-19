using OrderServices.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServices.Dtos
{
    public class ReadOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public int PayAmount { get; set; }

        [ForeignKey("Wallet")]
        public string Username { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
