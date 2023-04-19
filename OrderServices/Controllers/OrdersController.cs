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
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _repo;

        public OrdersController(IMapper mapper, IOrderRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadAllOrder>>> GetOrders()
        {
            var order = await _repo.GetAllOrder();
            var readOrder = _mapper.Map<IEnumerable<ReadAllOrder>>(order);
            return Ok(readOrder);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var resultCheckProduct = await _repo.CheckProduct(createOrderDto.ProductId);
            if (!resultCheckProduct)
            {
                return BadRequest("Product not found");
            }

            var resultCheckWallet = await _repo.CheckWallet(createOrderDto.Username);

            if (!resultCheckWallet)
            {
                return BadRequest("Wallet not found");
            }

            var resultCheckStock = await _repo.CheckProductStock(createOrderDto.ProductId, createOrderDto.Quantity);

            if (!resultCheckStock)
            {
                return BadRequest("No stock products");
            }

            var resultCheckCash = await _repo.CheckWalletCash(createOrderDto.Username, createOrderDto.ProductId, createOrderDto.Quantity);
            if (!resultCheckCash)
            {
                return BadRequest("the balance is not sufficient");
            }

            var modelOrder = _mapper.Map<Order>(createOrderDto);
            modelOrder.OrderDate = DateTime.Now;
            await _repo.Create(modelOrder);
            _repo.SaveChanges();

            var readOrder = _mapper.Map<ReadOrderDto>(modelOrder);
            readOrder.PayAmount = await _repo.PayAmount(modelOrder.ProductId, modelOrder.Quantity);
            // cash out
            await _repo.CashOut(modelOrder.Username, readOrder.PayAmount);
            // product out
            await _repo.ProductOut(modelOrder.ProductId, modelOrder.Quantity);
            return Ok(readOrder);
        }
    }
}
