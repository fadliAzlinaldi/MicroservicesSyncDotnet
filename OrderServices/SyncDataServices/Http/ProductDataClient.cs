    using OrderServices.Models;
using System;
using System.Text.Json;

namespace OrderServices.SyncDataServices.Http
{
    public class ProductDataClient : IProductDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Product>> ReturnAllProducts()
        {
            var response = await _httpClient.GetAsync(_configuration["ProductServices"]);
            // JSON dari GET /api/produts
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content}");
                var products = JsonSerializer.Deserialize<List<Product>>(content);
                if (products != null)
                {
                    Console.WriteLine($"{products.Count()} products returned from product Service");
                    return products;
                }
                throw new Exception("No products found");
            }
            else
            {
                throw new Exception("Unable to reach product Service");
            }
        }
    }
}
