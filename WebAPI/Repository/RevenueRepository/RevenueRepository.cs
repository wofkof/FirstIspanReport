using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using 這是扭蛋機系統;
using WebAPI.Services;
using WebAPI.Repository.NewFolder1;

namespace WebAPI.Repository.RevenueRepository
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly IDbService _dbService;

        public RevenueRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        // 🔹 **查詢總營收**
        public async Task<decimal> GetTotalRevenueAsync()
        {
            string query = "SELECT SUM(CashSpent) FROM PointsHistory WHERE CashSpent > 0";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.ExecuteScalarAsync<decimal>(query);
            }
        }

        // 🔹 **查詢指定時間範圍內的營收**
        public async Task<List<RevenueRecord>> GetRevenueByDateAsync(DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT ph.HistoryID AS OrderID, ph.ChangeDate AS TransactionDate, ph.CashSpent AS TotalAmount, ph.PaymentMethod AS Payment
                FROM PointsHistory ph
                WHERE ph.CashSpent > 0 AND ph.ChangeDate BETWEEN @StartDate AND @EndDate
                ORDER BY ph.ChangeDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return (await connection.QueryAsync<RevenueRecord>(query, new { StartDate = startDate, EndDate = endDate })).AsList();
            }
        }
        
    }
}
