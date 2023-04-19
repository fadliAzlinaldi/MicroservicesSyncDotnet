using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepo _walletRepo;

        public WalletsController(IWalletRepo walletRepo)
        {
            _walletRepo = walletRepo;
        }
        [HttpPost("Sync")]
        public async Task<ActionResult> SyncWallet()
        {
            try
            {
                await _walletRepo.CreateWallet();
                return Ok("Wallets Synced");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not sync wallet: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetWallets()
        {
            Console.WriteLine("--> Getting Product from OrderService");
            var walletItems = await _walletRepo.GetAllWallets();
            return Ok(walletItems);
        }
    }
}
