using System.ComponentModel.DataAnnotations;

namespace WalletServices.Dtos
{
    public class ReadWalletDto
    {
        [Key]
        public string Username { get; set; }
        public string Fullname { get; set; }
        public int Cash { get; set; }
    }
}
