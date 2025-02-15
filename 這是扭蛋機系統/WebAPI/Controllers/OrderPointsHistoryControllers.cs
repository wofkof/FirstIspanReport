using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.NewFolder1;
using 這是扭蛋機系統;

namespace WebAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class OrderPointsHistoryController : ControllerBase
        {
            private readonly IOrderPointsHistoryRepository _orderPointsHistoryRepository;

            public OrderPointsHistoryController(IOrderPointsHistoryRepository orderPointsHistoryRepository)
            {
                _orderPointsHistoryRepository = orderPointsHistoryRepository;
            }

            // 新增積分使用紀錄
            [HttpPost]
            public async Task<IActionResult> AddOrderPointsHistory([FromBody] OrderPointsHistory history)
            {
                if (history == null)
                    return BadRequest("積分紀錄不能為空");

                int historyId = await _orderPointsHistoryRepository.AddOrderPointsHistoryAsync(history);
                return CreatedAtAction(nameof(GetOrderPointsHistoryByOrderId), new { orderId = history.OrderID }, history);
            }

            // 獲取所有積分使用紀錄
            [HttpGet]
            public async Task<IActionResult> GetAllOrderPointsHistory()
            {
                var history = await _orderPointsHistoryRepository.GetAllOrderPointsHistoryAsync();
                return Ok(history);
            }

            // 根據訂單ID獲取積分使用紀錄
            [HttpGet("{orderId}")]
            public async Task<IActionResult> GetOrderPointsHistoryByOrderId(int orderId)
            {
                var history = await _orderPointsHistoryRepository.GetOrderPointsHistoryByOrderIdAsync(orderId);
                if (history == null)
                    return NotFound();
                return Ok(history);
            }
        }
    }


