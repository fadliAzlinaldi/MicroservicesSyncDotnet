using ProductServices.Dtos;
using ProductServices.Models;

namespace ProductServices.SyncDataServices.Http
{
    public interface IOrderDataClient
    {
        Task<IEnumerable<Product>> UpdateProducts();
    }
}
