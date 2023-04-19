using Microsoft.EntityFrameworkCore;
using ProductServices.Models;
using ProductServices.SyncDataServices.Http;

namespace ProductServices.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        private readonly IOrderDataClient _client;
        public ProductRepo(AppDbContext context,IOrderDataClient client)
        {
            _context = context;
            _client = client;
        }
        public Task Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
            return Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            var productDelete = await GetById(id);
            _context.Products.Remove(productDelete);
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return _context.Products.ToList();
            //throw new NotImplementedException();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new Exception("product not found");
            }
            return product;
            //throw new NotImplementedException();
        }

        public async Task<Product> GetByName(string name)
        {
            var nameProduct = name.ToLower();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == name);
            if (product == null)
            {
                throw new Exception("Product Name is not found");
            }
            return product;
        }

        public async Task Update(int id,Product product)
        {
            try
            {
                var existingProduct = await GetById(product.ProductId);
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
            catch(Exception ex) 
            {
                throw new Exception($"Error updating product: {ex.Message}");
            }
            //throw new NotImplementedException();
        }
        public bool SaveChanges() 
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task ProductOut()
        {
            try
            {
                var products = await GetAllProduct();
                var productsUpdate = await _client.UpdateProducts();
                foreach (var item in productsUpdate)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        product.Stock = item.Stock;
                    }
                }
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not save changes to the database: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
