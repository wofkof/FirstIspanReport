using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Repository;
using WebAPI.Repository.ProductsRepository;
using 這是扭蛋機系統;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // 獲取所有商品
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsRepository.GetAllProductsAsync();
            return Ok(products);
        }

        // 根據商品ID獲取商品
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productsRepository.GetProductByIdAsync(productId);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // 新增商品
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Products product)
        {
            if (product == null)
                return BadRequest("商品資訊不可為空");

            int newProductId = await _productsRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { productId = newProductId }, product);
        }

        // 更新商品
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Products product)
        {
            if (product == null || productId != product.P_ID)
                return BadRequest("商品資訊錯誤");

            var updated = await _productsRepository.UpdateProductAsync(product);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // 刪除商品
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var deleted = await _productsRepository.DeleteProductAsync(productId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
        // 根據商品名稱獲取商品
        [HttpGet("by-name/{productName}")]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var product = await _productsRepository.GetProductByNameAsync(productName);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
    }
}
