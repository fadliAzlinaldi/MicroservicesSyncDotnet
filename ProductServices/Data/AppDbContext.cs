using Microsoft.EntityFrameworkCore;
using ProductServices.Models;

namespace ProductServices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product>Products { get; set; }
    }
}
