using Microsoft.AspNetCore.Mvc;
using ProductServices.Dtos;
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

        public async Task GetProductOut(int productId, int quantity)
        {
            //var response = await _httpClient.GetAsync(_configuration["OrderServices"]);
            //// json dari get product out
            //return Ok();
        }
    }
}
