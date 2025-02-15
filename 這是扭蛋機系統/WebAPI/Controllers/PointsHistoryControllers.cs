using Microsoft.AspNetCore.Mvc;
using WebAPI.Repository.NewFolder1;
using System.Threading.Tasks;
using 這是扭蛋機系統;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsHistoryController : ControllerBase
    {
        private readonly PointsHistoryRepository _pointsHistoryRepository;

        public PointsHistoryController(PointsHistoryRepository pointsHistoryRepository)
        {
            _pointsHistoryRepository = pointsHistoryRepository;
        }

        // 儲值積分
        [HttpPost("add-points")]
        public async Task<IActionResult> AddPoints(int userId, int points, string description)
        {
            await _pointsHistoryRepository.AddPoints(userId, points, description);
            return Ok("積分儲值成功");
        }

        // 創建訂單並扣除積分
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(int userId, int totalAmount)
        {
            await _pointsHistoryRepository.CreateOrder(userId, totalAmount);
            return Ok("訂單創建成功，積分已扣除");
        }

        // 查詢會員訂單與積分紀錄
        [HttpGet("member-details/{userId}")]
        public async Task<IActionResult> GetMemberDetails(int userId)
        {
            var result = await _pointsHistoryRepository.GetMemberDetails(userId);
            return result;
        }

        // 查詢會員歷史訂單
        [HttpGet("order-history/{userId}")]
        public async Task<IActionResult> GetOrderHistory(int userId)
        {
            var orders = await _pointsHistoryRepository.GetOrderHistory(userId);
            return Ok(orders);
        }

        // 查詢會員積分紀錄
        [HttpGet("points-history/{userId}")]
        public async Task<IActionResult> GetPointsHistory(int userId)
        {
            var history = await _pointsHistoryRepository.GetPointsHistoryAsync(userId);
            return Ok(history);
        }
    }
}

