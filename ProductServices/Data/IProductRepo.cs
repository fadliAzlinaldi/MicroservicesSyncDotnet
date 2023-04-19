using ProductServices.Models;

namespace ProductServices.Data
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetById(int id);
        Task<Product> GetByName(string name);
        Task Create(Product product);
        Task Update(int id, Product product);
        Task Delete(int id);
        Task ProductOut();
        bool SaveChanges();
    }
}
