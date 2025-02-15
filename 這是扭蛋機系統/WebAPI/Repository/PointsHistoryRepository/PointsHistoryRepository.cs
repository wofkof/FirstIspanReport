using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;



namespace WebAPI.Repository.NewFolder1
{
    public class PointsHistoryRepository : IPointsHistoryRepository
    {
        private readonly IDbService _dbService;
        public PointsHistoryRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        //儲值積分的邏輯
        public async Task AddPoints(int userId, int points, string description)
        {
            var sql = "UPDATE Register SET Points = Points + @Points WHERE UserID = @UserID;";

            using (var connection = _dbService.GetDbConnection())
            {
                connection.Open();

                // 更新會員積分
                await connection.ExecuteAsync(sql, new { Points = points, UserID = userId });

                // 插入積分儲值紀錄
                var historySql = "INSERT INTO PointsHistory (UserID, PointsChanged, Description) VALUES (@UserID, @PointsChanged, @Description)";
                await connection.ExecuteAsync(historySql, new { UserID = userId, PointsChanged = points, Description = description });
            }
        }

        //會員訂單的積分消費
        public async Task CreateOrder(int userId, int totalAmount)
        {
            var sql = "INSERT INTO Orders (UserID, TotalAmount, Status) VALUES (@UserID, @TotalAmount, 'Completed')";

            using (var connection = _dbService.GetDbConnection())
            {
                connection.Open();

                // 插入訂單
                await connection.ExecuteAsync(sql, new { UserID = userId, TotalAmount = totalAmount });

                // 扣除會員積分
                var updatePointsSql = "UPDATE Register SET Points = Points - @TotalAmount WHERE UserID = @UserID";
                await connection.ExecuteAsync(updatePointsSql, new { TotalAmount = totalAmount, UserID = userId });

                // 插入積分扣除紀錄
                var historySql = "INSERT INTO PointsHistory (UserID, PointsChanged, Description) VALUES (@UserID, @PointsChanged, 'Order Payment')";
                await connection.ExecuteAsync(historySql, new { UserID = userId, PointsChanged = -totalAmount, Description = "Order Payment" });
            }
        }
        //查詢會員訂單與積分紀錄
        public async Task<ActionResult> GetMemberDetails(int userId)
        {
            var orders = await GetOrderHistory(userId); // 查詢訂單歷史
            var pointsHistory = await GetPointsHistoryAsync(userId); // 查詢積分歷史

            var memberDetails = new
            {
                Orders = orders,
                PointsHistory = pointsHistory
            };

            return new JsonResult(memberDetails);
        }

        //查詢某位會員的歷史訂單
        public async Task<List<Order>> GetOrderHistory(int userId)
        {
            var sql = "SELECT * FROM Orders WHERE UserID = @UserID ORDER BY OrderDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                var orders = await connection.QueryAsync<Order>(sql, new { UserID = userId });
                return orders.ToList();
            }
        }

        //查詢某位會員的積分儲值紀錄
        public async Task<List<PointsHistory>> GetPointsHistoryAsync(int userId)
        {
            var sql = "SELECT * FROM OrderPointsHistory WHERE UserID = @UserID ORDER BY DateUsed DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                var history = await connection.QueryAsync<PointsHistory>(sql, new { UserID = userId });
                return history.ToList();
            }
        }
    }
}
