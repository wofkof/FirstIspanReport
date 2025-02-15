using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using 這是扭蛋機系統;
using WebAPI.Repository.NewFolder1;


namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // 創建訂單
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Invalid order data.");

            int orderId = await _orderRepository.CreateOrderAsync(order);
            return Ok(new { OrderId = orderId });
        }

        // 獲取會員所有訂單
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders != null ? Ok(orders) : NotFound();
        }

        // 獲取單筆訂單
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order != null ? Ok(order) : NotFound();
        }

        // 獲取訂單明細
        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var details = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId);
            return details != null ? Ok(details) : NotFound();
        }

        // 紀錄訂單積分使用
        [HttpPost("points/history")]
        public async Task<IActionResult> CreateOrderPointsHistory([FromBody] OrderPointsHistory history)
        {
            if (history == null)
                return BadRequest("Invalid history data.");

            await _orderRepository.CreateOrderPointsHistoryAsync(history);
            return Ok("Points history recorded.");
        }
    }
}