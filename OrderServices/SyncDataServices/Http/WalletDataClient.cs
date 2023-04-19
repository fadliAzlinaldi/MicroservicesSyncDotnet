using OrderServices.Models;
using System.Text.Json;

namespace OrderServices.SyncDataServices.Http
{
    public class WalletDataClient : IWalletDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WalletDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Wallet>> ReturnAllWallet()
        {
            var response = await _httpClient.GetAsync(_configuration["WalletServices"]);
            // JSON dari GET /api/produts
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content}");
                var wallets = JsonSerializer.Deserialize<List<Wallet>>(content);
                if (wallets != null)
                {
                    Console.WriteLine($"{wallets.Count()} wallet returned from product Service");
                    return wallets;
                }
                throw new Exception("No wallet found");
            }
            else
            {
                throw new Exception("Unable to reach wallet Service");
            }
        }
    }
}
