using Microsoft.EntityFrameworkCore;

namespace OrderServices.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }     
        public DbSet<Product> Products { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
