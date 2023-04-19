using ProductServices.Models;

namespace ProductServices.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateAsyncScope()) 
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context) 
        {
            if (!context.Products.Any())
            {
                Console.WriteLine("--> Seeding Data.. <--");
                context.Products.AddRange(
                    new Product()
                    {
                        Name = "Macbook",
                        Stock = 45,
                        Description = "Laptop",
                        Price = 45
                    },
                    new Product()
                    {
                        Name = "Iphone",
                        Stock = 68,
                        Description = "Phone",
                        Price = 20
                    },
                    new Product()
                    {
                        Name = "Asus V5200",
                        Stock = 84,
                        Description = "Laptop",
                        Price = 20
                    });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Sudah ada data <--");
            }
        }
    }
}
