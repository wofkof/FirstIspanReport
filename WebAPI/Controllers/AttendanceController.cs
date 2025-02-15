using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.AttendanceRepository;
using WebAPI.Repository.NewFolder1;
using 這是扭蛋機系統;
using 這是扭蛋機系統.Services.PointsHistoryService;


namespace WebAPI.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // ✅ 員工上班打卡
        [HttpPost("clock-in")]
        public async Task<IActionResult> ClockIn([FromBody] int userId)
        {
            if (userId <= 0)
                return BadRequest("請提供有效的員工 ID");

            bool success = await _attendanceRepository.ClockInAsync(userId);
            return success ? Ok(new { message = "上班打卡成功！" }) : BadRequest("打卡失敗，可能已經打過卡");
        }

        // ✅ 員工下班打卡
        [HttpPost("clock-out")]
        public async Task<IActionResult> ClockOut([FromBody] int userId)
        {
            if (userId <= 0)
                return BadRequest("請提供有效的員工 ID");

            bool success = await _attendanceRepository.ClockOutAsync(userId);
            return success ? Ok(new { message = "下班打卡成功！" }) : BadRequest("打卡失敗，可能未上班或已下班");
        }

        // ✅ 透過「姓名」與「日期區間」查詢打卡紀錄
        [HttpGet("search")]
        public async Task<IActionResult> GetAttendanceRecords(
            [FromQuery] string name,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var records = await _attendanceRepository.GetAttendanceByFilters(name, startDate, endDate);
            return Ok(records);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetAttendanceHistory()
        {
            var records = await _attendanceRepository.GetAttendanceHistoryAsync();
            return Ok(records);
        }

        [HttpGet("employee/{userId}")]
        public async Task<IActionResult> GetEmployeeInfo(int userId)
        {
            var employee = await _attendanceRepository.GetEmployeeByIdAsync(userId);
            if (employee == null)
            {
                return NotFound("找不到該員工");
            }
            return Ok(new EmployeeDto { UserID = employee.UserID, Name = employee.Name });
        }

    }
}

