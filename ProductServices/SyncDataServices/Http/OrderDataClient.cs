using Microsoft.AspNetCore.Mvc;
using ProductServices.Dtos;
using ProductServices.Models;
using System.Text;
using System.Text.Json;

namespace ProductServices.SyncDataServices.Http
{
    public class OrderDataClient : IOrderDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public OrderDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> UpdateProducts()
        {
            var response = await _httpClient.GetAsync(_configuration["OrderServices"]);
            // JSON dari get /api/products/productout
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content}");
                var products = JsonSerializer.Deserialize<List<Product>>(content);
                if(products != null) 
                {
                    Console.WriteLine($"{products.Count()} products returned from order Service");
                    return products;
                }
                throw new Exception("No update product found");
            }
            else
            {
                throw new Exception("Unable to reach Orders Service");
            }
        }
    }
}
