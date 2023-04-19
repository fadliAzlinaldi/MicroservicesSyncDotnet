using OrderServices.Models;

namespace OrderServices.Data
{
    public interface IWalletRepo
    {
        Task CreateWallet();
        Task<IEnumerable<Wallet>> GetAllWallets();
    }
}
