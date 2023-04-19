using Microsoft.EntityFrameworkCore;
using WalletServices.Models;

namespace WalletServices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
