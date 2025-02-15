using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.NewFolder1;
using 這是扭蛋機系統;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductsController : ControllerBase
    {
        private readonly ISubProductsRepository _subProductsRepository;

        public SubProductsController(ISubProductsRepository subProductsRepository)
        {
            _subProductsRepository = subProductsRepository;
        }

        // 獲取所有小商品
        [HttpGet]
        public async Task<IActionResult> GetAllSubProducts()
        {
            var subProducts = await _subProductsRepository.GetAllSubProductsAsync();
            return Ok(subProducts);
        }


        [HttpGet("by-product")]
        public async Task<IActionResult> UpdateGetAllSubProducts()
        {
            var subProducts = await _subProductsRepository.UpdateGetAllSubProductsAsync();
            return Ok(subProducts);
        }

        // 根據SP_ID獲取小商品
        [HttpGet("{spId}")]
        public async Task<IActionResult> GetSubProductById(int spId)
        {
            var subProduct = await _subProductsRepository.GetSubProductByIdAsync(spId);
            if (subProduct == null)
                return NotFound();
            return Ok(subProduct);
        }

        // 根據商品ID查詢小商品
        [HttpGet("by-product/{productId}")]
        public async Task<IActionResult> GetSubProductsByProductId(int productId)
        {
            var subProducts = await _subProductsRepository.GetSubProductsByProductIdAsync(productId);
            return Ok(subProducts);
        }

        // 新增小商品
        [HttpPost]
        public async Task<IActionResult> AddSubProduct([FromBody] SubProducts subProduct)
        {
            if (subProduct.P_ID <= 0 || string.IsNullOrEmpty(subProduct.SP_Name) || subProduct.SP_Amount < 0)
            {
                return BadRequest("請提供完整的小商品資訊");
            }

            bool success = await _subProductsRepository.AddSubProductAsync(subProduct);
            if (success)
            {
                return Ok(new { message = "小商品新增成功！" });
            }
            else
            {
                return BadRequest("小商品新增失敗");
            }
        }

        // 更新小商品
        [HttpPut("{spId}")]
        public async Task<IActionResult> UpdateSubProduct(int spId, [FromBody] SubProducts subProduct)
        {
            if (subProduct == null || spId != subProduct.SP_ID)
                return BadRequest("小商品資訊錯誤");

            var updated = await _subProductsRepository.UpdateSubProductAsync(subProduct);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // 刪除小商品
        [HttpDelete("{spId}")]
        public async Task<IActionResult> DeleteSubProduct(int spId)
        {
            var deleted = await _subProductsRepository.DeleteSubProductAsync(spId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        //根據小商品ID更新小商品數量
        [HttpPut("{spId}/update-amount")]
        public async Task<IActionResult> UpdateSubProductAmount(int spId, [FromBody] SubProductUpdateRequest request)
        {
            bool success = await _subProductsRepository.UpdateSubProductAmountAsync(spId, request.SP_Amount);
            if (success)
            {
                return Ok(new { message = "小商品庫存更新成功" });
            }
            return BadRequest(new { message = "更新失敗" });
        }
    }
}
