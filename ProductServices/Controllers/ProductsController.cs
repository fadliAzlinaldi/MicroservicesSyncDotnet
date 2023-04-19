using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Data;
using ProductServices.Dtos;
using ProductServices.Models;
using ProductServices.SyncDataServices.Http;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;
        private readonly IOrderDataClient _orderDataClient;

        public ProductsController(IProductRepo repo, IMapper mapper, IOrderDataClient orderDataClient) 
        {
            _repo = repo;
            _mapper = mapper;
            _orderDataClient = orderDataClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        {
            Console.WriteLine("--> Getting Product <--");
            var productItem = await _repo.GetAllProduct();
            return Ok(productItem);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByProductId(int id)
        {
            var product = await _repo.GetById(id);
            var readProduct = _mapper.Map<ReadProductDto>(product);
            return Ok(readProduct);
        }
        [HttpGet("/find/{name}")]
        public async Task<ActionResult> GetByProductName(string name)
        {
            var product = await _repo.GetByName(name);
            var readProduct = _mapper.Map<ReadProductDto>(product);
            return Ok(readProduct);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                var product = _mapper.Map<Product>(updateProductDto);
                product.ProductId = id;
                await _repo.Update(id, product);
                _repo.SaveChanges();
                var readProductDto = _mapper.Map<ReadProductDto>(product);
                return Ok(readProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var productModel = _mapper.Map<Product>(createProductDto);
            await _repo.Create(productModel);
            _repo.SaveChanges();

            var productReadDto = _mapper.Map<ReadProductDto>(productModel);

            //try
            //{
            //    // send sync message
            //    await _orderDataClient.SendProductToOrder(productReadDto);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            //}

            return Ok(productReadDto);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _repo.Delete(id);
            _repo.SaveChanges();
            return Ok();
        }
        [HttpPost("Sync")]
        public async Task<ActionResult> SyncProducts()
        {
            try
            {
                await _repo.ProductOut();
                return Ok("Products update from order Synced");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not sync product: {ex.Message}");
            }
        }
    }
}
