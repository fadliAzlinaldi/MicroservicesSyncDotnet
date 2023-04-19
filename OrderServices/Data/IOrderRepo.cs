using OrderServices.Models;
using System.Threading.Tasks;

namespace OrderServices.Data
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrder();
        Task Create(Order order);
        Task<bool> CheckProduct(int productId);
        Task<bool> CheckWallet(string username);
        Task<bool> CheckProductStock(int productId, int quantity);
        Task<bool> CheckWalletCash(string username, int productId, int quantity);
        Task<int> PayAmount(int productId, int quantity);
        Task CashOut(string username, int payAmount);
        Task ProductOut(int productId, int quantity);
        bool SaveChanges();
    }
}
