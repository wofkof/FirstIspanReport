using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

namespace WebAPI.Repository.NewFolder1
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IDbService _dbService;

        public OrderDetailRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        // 新增訂單明細
        public async Task AddOrderDetailsAsync(IEnumerable<OrderDetail> orderDetails)
        {
            var sql = @"
                INSERT INTO OrderDetails (OrderID, ProductID, SubProductID, Quantity, Points) 
                VALUES (@OrderID, @ProductID, @SubProductID, @Quantity, @Points)";

            using (var connection = _dbService.GetDbConnection())
            {
                await connection.ExecuteAsync(sql, orderDetails);
            }
        }

        // 依據訂單 ID 取得訂單明細
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var sql = "SELECT * FROM OrderDetails WHERE OrderID = @OrderID";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<OrderDetail>(sql, new { OrderID = orderId });
            }
        }
    }
}

