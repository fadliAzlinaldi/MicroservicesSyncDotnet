using OrderServices.Models;

namespace OrderServices.SyncDataServices.Http
{
    public interface IProductDataClient
    {
        Task<IEnumerable<Product>> ReturnAllProducts();
    }
}
