using WalletServices.Models;

namespace WalletServices.Data
{
    public interface IWalletRepo
    {
        Task<IEnumerable<Wallet>> GetAllWallet();
        Task<Wallet> GetbyUsername(string name);
        Task Create(Wallet wallet);
        Task Edit(string username, Wallet wallet);
        Task Topup(int cash, string username);
        Task CashOut(int cash, string username);
        Task WalletUpdate();
        string GenerateId();
        bool SaveChanges();
    }
}
