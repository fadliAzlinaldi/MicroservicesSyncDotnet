using System.Text.Json;
using WalletServices.Models;

namespace WalletServices.SyncDataServices.Http
{
    public class OrderDataClient : IOrderDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        
        public OrderDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<IEnumerable<Wallet>> UpdateWallets()
        {
            var response = await _httpClient.GetAsync(_config["OrderServices"]);
            // Json from get /api/wallets/walletout
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{content}");
                var wallets = JsonSerializer.Deserialize<List<Wallet>>(content);
                if (wallets != null)
                {
                    Console.WriteLine($"{wallets.Count()} wallets returned from order services");
                    return wallets;
                }
                throw new Exception("No update wallet found");
            }
            else
            {
                throw new Exception("Unable to reach Order Services");
            }
        }
    }
}
