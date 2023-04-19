using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Data
{
    public interface IProductRepo
    {
        Task CreateProduct();
        Task <IEnumerable<Product>> GetAllProducts();

    }
}
