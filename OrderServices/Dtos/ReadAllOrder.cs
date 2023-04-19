using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServices.Dtos
{
    public class ReadAllOrder
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
        public int ProductId { get; set; }
    }
}
