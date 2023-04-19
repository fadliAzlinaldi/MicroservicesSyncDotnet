using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepo _walletRepo;
        private readonly IMapper _mapper;

        public WalletsController(IWalletRepo walletRepo,IMapper mapper)
        {
            _walletRepo = walletRepo;
            _mapper = mapper;
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
        [HttpGet("WalletOut")]
        public async Task<IActionResult> WalletOut()
        {
            var wallets = await _walletRepo.GetAllWallets();

            // Melakukan mapping dari IEnumerable<Wallet> ke IEnumerable<WalletOutDto>
            var walletDtos = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletOutDto>>(wallets);
            return Ok(walletDtos);
        }
    }
}
