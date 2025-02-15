using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;
using System.ComponentModel;

namespace WebAPI.Repository.AttendanceRepository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IDbService _dbService;

        public AttendanceRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        // ✅ 員工上班打卡
        public async Task<bool> ClockInAsync(int userId)
        {
            string query = @"
        INSERT INTO AttendanceHistory (UserID, ClockInTime)
        SELECT @UserID, GETDATE()
        WHERE NOT EXISTS (SELECT 1 FROM AttendanceHistory WHERE UserID = @UserID AND CAST(ClockInTime AS DATE) = CAST(GETDATE() AS DATE) AND ClockOutTime IS NULL)";
            using (var connection = _dbService.GetDbConnection()) 
            {
                int rowsAffected = await connection.ExecuteAsync(query, new { UserID = userId });
                return rowsAffected > 0;
            }
        }

        // ✅ 員工下班打卡
        public async Task<bool> ClockOutAsync(int userId)
        {
            string query = @"
        UPDATE AttendanceHistory 
        SET ClockOutTime = GETDATE() 
        WHERE UserID = @UserID 
        AND CAST(ClockInTime AS DATE) = CAST(GETDATE() AS DATE)
        AND ClockOutTime IS NULL";
            using (var connection = _dbService.GetDbConnection()) 
            {
                int rowsAffected = await connection.ExecuteAsync(query, new { UserID = userId });
                return rowsAffected > 0;
            }
              
        }

        // ✅ 透過「姓名」與「日期區間」查詢打卡紀錄
        public async Task<IEnumerable<AttendanceHistoryDto>> GetAttendanceByFilters(string name, DateTime? startDate, DateTime? endDate)
        {
            string query = @"
        SELECT 
            a.RecordID, r.UserID, r.Name, a.ClockInTime, a.ClockOutTime,
            CASE 
                WHEN CAST(a.ClockInTime AS TIME) <= '09:00:00' THEN '準時' 
                ELSE '遲到' 
            END AS ClockInStatus,
            CASE 
                WHEN a.ClockOutTime IS NULL THEN '未下班'
                WHEN CAST(a.ClockOutTime AS TIME) >= '18:00:00' THEN '準時'
                ELSE '早退' 
            END AS ClockOutStatus
        FROM AttendanceHistory a
        JOIN Register r ON a.UserID = r.UserID
        WHERE (@Name IS NULL OR r.Name LIKE '%' + @Name + '%')
        AND (@StartDate IS NULL OR CAST(a.ClockInTime AS DATE) >= @StartDate)
        AND (@EndDate IS NULL OR CAST(a.ClockInTime AS DATE) <= @EndDate)
        ORDER BY a.ClockInTime DESC";


            using (var connection = _dbService.GetDbConnection()) 
            {
                return await connection.QueryAsync<AttendanceHistoryDto>(query, new { Name = name, StartDate = startDate, EndDate = endDate });
            }
        }

        public async Task<IEnumerable<AttendanceHistoryDto>> GetAttendanceHistoryAsync()
        {
            string query = @"
    SELECT 
        a.RecordID, 
        r.Name, 
        a.ClockInTime, 
        a.ClockOutTime
    FROM AttendanceHistory a
    JOIN Register r ON a.UserID = r.UserID
    ORDER BY a.ClockInTime DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<AttendanceHistoryDto>(query);
            }
        }
        public async Task<Register> GetEmployeeByIdAsync(int userId)
        {
            string query = "SELECT UserID, Name FROM Register WHERE UserID = @UserID AND RoleID = 2";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Register>(query, new { UserID = userId });
            }
        }

    }

}
