using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServices.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }   

        [ForeignKey("Wallet")]
        public string Username { get; set; }
        public virtual Wallet Wallet { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
