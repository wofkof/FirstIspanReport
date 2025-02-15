using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.NewFolder1;
using 這是扭蛋機系統;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        /// <summary>
        /// 根據訂單 ID 查詢訂單明細
        /// </summary>
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderIdAsync(orderId);
            if (orderDetails == null)
            {
                return NotFound(new { message = "找不到該訂單的明細" });
            }
            return Ok(orderDetails);
        }

        /// <summary>
        /// 新增訂單明細
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrderDetails([FromBody] List<OrderDetail> orderDetails)
        {
            if (orderDetails == null || orderDetails.Count == 0)
            {
                return BadRequest(new { message = "訂單明細不能為空" });
            }

            await _orderDetailRepository.AddOrderDetailsAsync(orderDetails);
            return Ok(new { message = "訂單明細新增成功" });
        }
    }
}
