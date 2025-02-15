using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.NewFolder1;
using 這是扭蛋機系統;
using 這是扭蛋機系統.Services.PointsHistoryService;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsHistoryController : ControllerBase
    {
        private readonly IPointsHistoryRepository _pointsHistoryRepository;

        public PointsHistoryController(IPointsHistoryRepository pointsHistoryRepository)
        {
            _pointsHistoryRepository = pointsHistoryRepository;
        }

        // ✅ 1. 新增儲值記錄
        [HttpPost("add")]
        public async Task<IActionResult> AddPointsHistory([FromBody] PointsHistory history)
        {
            if (history == null)
                return BadRequest("請提供儲值紀錄資料");

            bool success = await _pointsHistoryRepository.AddPointsHistoryAsync(history);

            if (success)
                return Ok(new { message = "儲值成功，G 幣已更新！" });

            return BadRequest("儲值失敗，請稍後再試");
        }

        // ✅ 2. 扣除 G 幣 (抽取商品)
        [HttpPost("deduct")]
        public async Task<IActionResult> DeductPoints([FromBody] PointsHistory request)
        {
            if (request.UserID <= 0 || request.PointsToDeduct <= 0)
            {
                return BadRequest("請提供有效的會員 ID 和扣除點數");
            }

            bool success = await _pointsHistoryRepository.DeductPointsAsync(request.UserID, request.PointsToDeduct);

            if (success)
            {
                return Ok(new { message = "點數扣除成功！" });
            }
            else
            {
                return BadRequest("點數扣除失敗，請確認用戶點數是否足夠");
            }
        }


        // ✅ 3. 取得會員 G 幣歷史紀錄
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPointsHistory(int userId)
        {
            var history = await _pointsHistoryRepository.GetPointsHistoryByUserIdAsync(userId);
            return Ok(history);
        }

        // ✅ 4. 取得會員 G 幣總額
        [HttpGet("total/{userId}")]
        public async Task<IActionResult> GetTotalPoints(int userId)
        {
            int totalPoints = await _pointsHistoryRepository.GetTotalPointsByUserIdAsync(userId);
            return Ok(new { Points = totalPoints });
        }

        //查詢用戶 花費點數 & 抽獎記錄
        [HttpGet("drawn-items/{userId}")]
        public async Task<IActionResult> GetDrawnItems(int userId)
        {
            var drawnItems = await _pointsHistoryRepository.GetDrawnItemsByUserIdAsync(userId);

            if (drawnItems == null || !drawnItems.Any())
            {
                return NotFound(new { message = "沒有找到抽取紀錄。" });
            }

            return Ok(drawnItems);
        }


        // ✅ 新增抽取紀錄
        [HttpPost("add-drawn")]
        public async Task<IActionResult> AddDrawnItemHistory([FromBody] DrawnItemsHistory history)
        {
            if (history.UserID <= 0 || history.ProductID <= 0 || history.SubProductID <= 0)
            {
                return BadRequest("請提供有效的會員 ID、商品 ID 和小商品 ID");
            }

            bool success = await _pointsHistoryRepository.AddDrawnItemHistoryAsync(history.UserID, history.ProductID, history.SubProductID, history.SubProductName, history.PointsUsed);

            if (success)
            {
                return Ok(new { message = "抽取紀錄新增成功！" });
            }
            else
            {
                return BadRequest("抽取紀錄新增失敗");
            }
        }
        //查詢會員所有抽獎的花費點數
        [HttpGet("points-history/{userId}")]
        public async Task<IActionResult> GetUserPointsHistory(int userId)
        {
            var history = await _pointsHistoryRepository.GetUserPointsHistoryAsync(userId);
            return Ok(history);
        }
        //查詢會員累積金額
        [HttpGet("total-cash/{userId}")]
        public async Task<IActionResult> GetTotalCashSpent(int userId)
        {
            decimal totalCash = await _pointsHistoryRepository.GetTotalCashSpentAsync(userId);
            return Ok(new { totalCashSpent = totalCash });
        }

        // ✅ 查詢會員儲值紀錄
        [HttpGet("topup-history/{userId}")]
        public async Task<IActionResult> GetTopUpHistory(int userId)
        {
            var history = await _pointsHistoryRepository.GetTopUpHistoryAsync(userId);

            if (history == null)
            {
                return NotFound("找不到儲值紀錄");
            }

            return Ok(history);
        }

        // ✅ 取得所有儲值紀錄
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPointsHistory()
        {
            var history = await _pointsHistoryRepository.GetAllPointsHistoryAsync();
            return Ok(history);
        }

        // ✅ 搜尋儲值紀錄（依姓名或日期區間）
        [HttpGet("search")]
        public async Task<IActionResult> SearchPointsHistory(string? keyword, DateTime? startDate, DateTime? endDate)
        {
            var history = await _pointsHistoryRepository.SearchPointsHistoryAsync(keyword, startDate, endDate);
            return Ok(history);
        }

        // ✅ 取得所有扭蛋抽獎紀錄
        [HttpGet("all-Drawn")]
        public async Task<IActionResult> GetAllDrawnHistory()
        {
            var history = await _pointsHistoryRepository.GetAllDrawnHistoryAsync();
            return Ok(history);
        }

        // ✅ 搜尋扭蛋抽獎紀錄（依姓名或日期區間）
        [HttpGet("search-Drawn")]
        public async Task<IActionResult> SearchDrawnHistory(string? keyword, DateTime? startDate, DateTime? endDate)
        {
            var history = await _pointsHistoryRepository.SearchDrawnHistoryAsync(keyword, startDate, endDate);
            return Ok(history);
        }
    }
}


