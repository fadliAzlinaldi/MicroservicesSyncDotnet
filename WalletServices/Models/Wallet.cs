using System.ComponentModel.DataAnnotations;

namespace WalletServices.Models
{
    public class Wallet
    {
        [Key]
        public string Username { get; set; }
        public string Fullname { get; set; }
        public int Cash { get; set; }
    }
}
