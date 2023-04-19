using OrderServices.Models;

namespace OrderServices.SyncDataServices.Http
{
    public interface IWalletDataClient
    {
        Task<IEnumerable<Wallet>> ReturnAllWallet();
    }
}
