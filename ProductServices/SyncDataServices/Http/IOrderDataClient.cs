using ProductServices.Dtos;

namespace ProductServices.SyncDataServices.Http
{
    public interface IOrderDataClient
    {
        Task GetProductOut(int productId, int quantity);
    }
}
