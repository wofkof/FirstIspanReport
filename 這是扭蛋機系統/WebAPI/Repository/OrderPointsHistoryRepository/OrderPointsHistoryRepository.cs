using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

namespace WebAPI.Repository.NewFolder1
{
    public class OrderPointsHistoryRepository : IOrderPointsHistoryRepository
    {
        private readonly IDbService _dbService;

        public OrderPointsHistoryRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        // 新增積分使用紀錄
        public async Task<int> AddOrderPointsHistoryAsync(OrderPointsHistory history)
        {
            var sql = @"INSERT INTO OrderPointsHistory (OrderID, PointsUsed, DateUsed) 
                        VALUES (@OrderID, @PointsUsed, @DateUsed);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, history);
            }
        }

        // 獲取所有積分使用紀錄
        public async Task<IEnumerable<OrderPointsHistory>> GetAllOrderPointsHistoryAsync()
        {
            var sql = "SELECT * FROM OrderPointsHistory ORDER BY DateUsed DESC";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<OrderPointsHistory>(sql);
            }
        }

        // 根據訂單ID獲取積分使用紀錄
        public async Task<IEnumerable<OrderPointsHistory>> GetOrderPointsHistoryByOrderIdAsync(int orderId)
        {
            var sql = "SELECT * FROM OrderPointsHistory WHERE OrderID = @OrderID ORDER BY DateUsed DESC";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<OrderPointsHistory>(sql, new { OrderID = orderId });
            }
        }
    }
}

