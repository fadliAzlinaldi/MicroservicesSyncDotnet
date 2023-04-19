using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderServices.Dtos;
using OrderServices.Models;
using OrderServices.SyncDataServices.Http;

namespace OrderServices.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        private readonly IProductDataClient _client;
        private readonly IMapper _mapper;

        public ProductRepo(AppDbContext context, IProductDataClient client, IMapper mapper)
        {
            _context = context;
            _client = client;
            _mapper = mapper;
        }

        public async Task CreateProduct()
        {
            try
            {
                // Get all products from the database
                var existingProducts = _context.Products.ToList();

                // Remove all existing products from the database
                foreach (var productToDelete in existingProducts)
                {
                    _context.Products.Remove(productToDelete);
                }

                // Get all from products service
                var products = await _client.ReturnAllProducts();
                foreach (var item in products)
                {
                    _context.Products.Add(new Product
                    {
                        ProductId = item.ProductId,
                        Name = item.Name,
                        Stock = item.Stock,
                        Price = item.Price,
                    });
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not save changes to the database: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return _context.Products.ToList();
        }
    }
}
