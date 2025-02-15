using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;


namespace WebAPI.Repository.NewFolder1
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbService _dbService;
        public OrderRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        //創建訂單
        public async Task<int> CreateOrderAsync(Order order)
        {
            var sql = @"INSERT INTO Orders (UserID, TotalPoints)
                                VALUES (@UserID, @TotalPoints);
                                SELECT CAST(SCOPE_IDENTITY() AS INT);";  // 返回插入後的訂單 ID

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QuerySingleAsync<int>(sql, order);
            }
        }
        //創建訂單明細
        public async Task CreateOrderDetailsAsync(IEnumerable<OrderDetail> orderDetails)
        {
            var sql = @"INSERT INTO OrderDetails (OrderID, P_ID, SP_ID, Quantity)
                               VALUES (@OrderID, @P_ID, @SP_ID, @Quantity)";

            using (var connection = _dbService.GetDbConnection())
            {
                connection.ExecuteAsync(sql, orderDetails);
            }
        }

        // 紀錄積分使用歷史
        public Task CreateOrderPointsHistoryAsync(OrderPointsHistory orderPointsHistory)
        {
            using (var connection = _dbService.GetDbConnection())
            {
                string sql = @"INSERT INTO OrderPointsHistory (OrderID, PointsUsed, DateUsed)
                               VALUES (@OrderID, @PointsUsed, @DateUsed)";
                return connection.ExecuteAsync(sql, orderPointsHistory);
            }
        }

        // 根據訂單ID獲取訂單詳細資料
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var sql = "SELECT * FROM Orders WHERE OrderID = @OrderID";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Order>(sql, new { OrderID = orderId });
            }
        }

        // 根據訂單ID獲取訂單明細
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var sql = @"
                SELECT od.OrderDetailID, od.OrderID, od.P_ID, od.SP_ID, od.Quantity, 
                       p.P_Name, sp.SP_Name
                FROM OrderDetails od
                LEFT JOIN Products p ON od.P_ID = p.P_ID
                LEFT JOIN SubProducts sp ON od.SP_ID = sp.SP_ID
                WHERE od.OrderID = @OrderID;";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<OrderDetail>(sql, new { OrderID = orderId });
            }
        }

        // 根據會員ID獲取所有歷史訂單
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var sql = "SELECT * FROM Orders WHERE UserID = @UserID";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<Order>(sql, new { UserID = userId });
            }
        }
    }
}
