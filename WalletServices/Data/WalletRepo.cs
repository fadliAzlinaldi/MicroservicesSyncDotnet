using Microsoft.EntityFrameworkCore;
using WalletServices.Controllers;
using WalletServices.Models;
using WalletServices.SyncDataServices.Http;

namespace WalletServices.Data
{
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext _context;
        private readonly IOrderDataClient _client;

        public WalletRepo(AppDbContext context,IOrderDataClient client)
        {
            _context = context;
            _client = client;
        }
        public Task Create(Wallet wallet)
        {
            if (wallet == null )
            {
                throw new ArgumentNullException(nameof(wallet));
            }
            _context.Wallets.Add(wallet);
            return Task.CompletedTask;
        }

        public async Task Edit(string username, Wallet wallet)
        {
            try
            {
                var editWallet = await GetbyUsername(username);
                editWallet.Fullname = wallet.Fullname;
                wallet.Cash = editWallet.Cash;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erorr edited wallet");
            }
        }

        public string GenerateId()
        {
            var count = _context.Wallets.Count();
            count++;
            var usernameWallet = $"usr{count}";
            return usernameWallet;
        }

        public async Task<IEnumerable<Wallet>> GetAllWallet()
        {
            return _context.Wallets.ToList();
        }

        public async Task<Wallet> GetbyUsername(string name)
        {
            var nameWallet = name.ToLower();
            var wallet = await _context.Wallets.FirstOrDefaultAsync(p => p.Username == nameWallet);
            if (wallet == null)
            {
                throw new Exception("Username Name is not found");
            }
            return wallet;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task Topup(int cash, string username)
        {
            try
            {
                var wallet = await _context.Wallets.FirstOrDefaultAsync(p => p.Username == username);
                wallet.Cash += cash;
                _context.Wallets.Update(wallet);

            }
            catch (Exception ex)
            {
                throw new Exception("Error top up wallet");
            }
        }
        public async Task CashOut(int cash, string username)
        {
            try
            {
                var wallet = await _context.Wallets.FirstOrDefaultAsync(p => p.Username == username);
                wallet.Cash -= cash;
                _context.Wallets.Update(wallet);

            }
            catch (Exception ex)
            {
                throw new Exception("Error top up wallet");
            }
        }

        public async Task WalletUpdate()
        {
            try
            {
                var wallets = await GetAllWallet();
                var walletUpdate = await _client.UpdateWallets();
                foreach ( var item in walletUpdate )
                {
                    var wallet = await _context.Wallets.FindAsync(item.Username);
                    if (wallet != null)
                    {
                        wallet.Cash = item.Cash;
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not save changes to the database: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
