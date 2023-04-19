using WalletServices.Models;

namespace WalletServices.Data
{
    public class PrepDb
    {
        private readonly WalletRepo _repo;
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateAsyncScope()) 
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Wallets.Any()) 
            {
                Console.WriteLine("--> Seeding data <--");
                context.Wallets.AddRange(
                    new Wallet()
                    {
                        Username = "usr1",
                        Fullname = "Fadli Azlinaldi",
                        Cash = 5000
                    },
                    new Wallet()
                    {
                        Username = "usr2",
                        Fullname = "Budi Setiadi",
                        Cash = 3000
                    },
                    new Wallet()
                    {
                        Username = "usr3",
                        Fullname = "Yunus manyus",
                        Cash = 7000
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
