using WalletServices.Models;

namespace WalletServices.SyncDataServices.Http
{
    public interface IOrderDataClient
    {
        Task<IEnumerable<Wallet>> UpdateWallets();
    }
}
