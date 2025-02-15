using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.NewFolder1;
using WebAPI.Repository.RevenueRepository;
using 這是扭蛋機系統;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {

        private readonly IRevenueRepository _revenueRepository;

        public RevenueController(IRevenueRepository revenueRepository)
        {
            _revenueRepository = revenueRepository;
        }

        // 🔹 **查詢總營收**
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            decimal totalRevenue = await _revenueRepository.GetTotalRevenueAsync();
            return Ok(totalRevenue);
        }

        // 🔹 **查詢指定時間範圍內的營收**
        [HttpGet("range")]
        public async Task<IActionResult> GetRevenueByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
                return BadRequest("起始日期不能大於結束日期");

            var records = await _revenueRepository.GetRevenueByDateAsync(startDate, endDate);
            return Ok(records);
        }
    }
}
