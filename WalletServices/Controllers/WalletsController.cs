using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletServices.Data;
using WalletServices.Dtos;
using WalletServices.Models;

namespace WalletServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepo _repo;
        private readonly IMapper _mapper;
        public WalletsController(IWalletRepo repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetWallets() 
        {
            Console.WriteLine("--> Getting Wallet <--");
            var walletItem = await _repo.GetAllWallet();
            return Ok(walletItem);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletDto createWalletDto)
        {
            var walletModel = _mapper.Map<Wallet>(createWalletDto);
            var usernameWallet = _repo.GenerateId();
            walletModel.Username = usernameWallet;
            walletModel.Cash = 0;
            await _repo.Create(walletModel);
            _repo.SaveChanges();

            var readWallet = _mapper.Map<ReadWalletDto>(walletModel);
            return Ok(readWallet);
        }
        [HttpPut("{username}")]
        public async Task<IActionResult> EditWallet(string username ,EditWalletDto editWalletDto)
        {
            try
            {
                var walletModel = _mapper.Map<Wallet>(editWalletDto);
                walletModel.Username = username;
                await _repo.Edit(username, walletModel);
                _repo.SaveChanges();

                var readWalletDto = _mapper.Map<ReadWalletDto>(walletModel);
                return Ok(readWalletDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/topup/{username}")]
        public async Task<IActionResult> TopUpWallet(string username, int cash)
        {
            try
            {
                await _repo.Topup(cash, username);
                _repo.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/cashout/{username}")]
        public async Task<IActionResult> CashOutWallet(string username, int cash)
        {
            try
            {
                await _repo.CashOut(cash, username);
                _repo.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Sync")]
        public async Task<ActionResult> SyncWallets()
        {
            try
            {
                await _repo.WalletUpdate();
                return Ok("Wallets update from order synced");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not sync product: {ex.Message}");
            }
        }
    }
}
