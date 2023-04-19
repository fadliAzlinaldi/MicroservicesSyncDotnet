using Microsoft.AspNetCore.Mvc;
using OrderServices.Models;

namespace OrderServices.Data
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CashOut(string username,int payAmount)
        {
            var wallet = await _context.Wallets.FindAsync(username);

            if (wallet != null)
            {
                wallet.Cash -= payAmount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product != null;
        }

        public async Task<bool> CheckProductStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            return product.Stock >= quantity;
        }

        public async Task<bool> CheckWallet(string username)
        {
            var wallet = await _context.Wallets.FindAsync(username);
            return wallet != null;
        }

        public async Task<bool> CheckWalletCash(string username, int productId, int quantity)
        {
            var walletCash = await _context.Wallets.FindAsync(username);
            if (walletCash == null)
            {
                return false;
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }

            var payAmount = product.Price * quantity;

            return walletCash.Cash >= payAmount;
        }

        public Task Create(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Add(order);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return _context.Orders.ToList();
        }

        public async Task<int> PayAmount(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            var payAmount = product.Price * quantity;
            return payAmount;
        }

        public async Task ProductOut(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stock -= quantity;
                await _context.SaveChangesAsync();
            }
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
