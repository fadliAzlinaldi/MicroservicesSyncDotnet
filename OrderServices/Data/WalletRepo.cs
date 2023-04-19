using OrderServices.Models;
using OrderServices.SyncDataServices.Http;

namespace OrderServices.Data
{
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext _context;
        private readonly IWalletDataClient _client;

        public WalletRepo(AppDbContext context, IWalletDataClient client)
        {
            _context = context;
            _client = client;
        }

        public async Task CreateWallet()
        {
            try
            {
                // Get all wallets from the database
                var existingWallets = _context.Wallets.ToList();

                // Remove all existing products from the database
                foreach (var walletToDelete in existingWallets)
                {
                    _context.Wallets.Remove(walletToDelete);
                }

                // Get all from products service
                var wallets = await _client.ReturnAllWallet();
                foreach (var item in wallets)
                {
                    _context.Wallets.Add(new Wallet
                    {
                        Username = item.Username,
                        Fullname = item.Fullname,
                        Cash = item.Cash,
                    });
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not save changes to the database: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return _context.Wallets.ToList();
        }
    }
}
