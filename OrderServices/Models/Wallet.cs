using System.ComponentModel.DataAnnotations;

namespace OrderServices.Models
{
    public class Wallet
    {
        [Key]
        public string Username { get; set; }
        public string Fullname { get; set; }
        public int Cash { get; set; }
    }
}
