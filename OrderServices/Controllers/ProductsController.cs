using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/order/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepo productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        [HttpPost("Sync")]
        public async Task<ActionResult> SyncProducts()
        {
            try
            {
                await _productRepo.CreateProduct();
                return Ok("Products Synced");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not sync product: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            Console.WriteLine("--> Getting Product from OrderService");
            var productItems = await _productRepo.GetAllProducts();
            return Ok(productItems);
        }
        [HttpGet("ProductOut")]
        public async Task<IActionResult> ProductOut()
        {
            var products = await _productRepo.GetAllProducts();

            // Melakukan mapping dari IEnumerable<Product> ke IEnumerable<ProductOutDto>
            var productDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductOutDto>>(products);
            return Ok(productDtos);
        }
    }
}
