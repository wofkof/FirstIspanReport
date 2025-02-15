using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

namespace WebAPI.Repository.NewFolder1
{
    public class PointsHistoryRepository : IPointsHistoryRepository
    {
        private readonly IDbService _dbService;

        public PointsHistoryRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        // ✅ 1. 新增儲值記錄
        public async Task<bool> AddPointsHistoryAsync(PointsHistory history)
        {
            using (var connection = _dbService.GetDbConnection() as SqlConnection) // ✅ 轉型成 `SqlConnection`
            {
                await connection.OpenAsync(); // ✅ 確保 `OpenAsync()` 可用

                using (var transaction = connection.BeginTransaction()) // ✅ 使用同步 `BeginTransaction()`
                {
                    try
                    {
                        // **插入儲值紀錄**
                        string insertQuery = @"
                INSERT INTO PointsHistory (UserID, ProductID, PointsChanged, CashSpent, PaymentMethod, Description, ChangeDate) 
                VALUES (@UserID, @ProductID, @PointsChanged, @CashSpent, @PaymentMethod, @Description, GETDATE())";

                        int rowsAffected = await connection.ExecuteAsync(insertQuery, new
                        {
                            history.UserID,
                            history.ProductID,   // ✅ **確保 `ProductID` 被存入**
                            history.PointsChanged,
                            history.CashSpent,
                            history.PaymentMethod,
                            history.Description
                        }, transaction);

                        if (rowsAffected > 0)
                        {
                            // **同步更新會員 G 幣**
                            string updatePointsQuery = @"
                    UPDATE Register 
                    SET Points = Points + @PointsChanged
                    WHERE UserID = @UserID";

                            await connection.ExecuteAsync(updatePointsQuery, new { history.UserID, history.PointsChanged }, transaction);

                            // **提交交易**
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        //負責扣除會員的 G 幣
        public async Task<bool> DeductPointsAsync(int userId, int pointsToDeduct)
        {
            using (var connection = _dbService.GetDbConnection() as SqlConnection) // ✅ 轉型成 `SqlConnection`
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 🔹 檢查會員點數是否足夠
                        string checkBalanceQuery = "SELECT Points FROM Register WHERE UserID = @UserID";
                        int currentPoints = await connection.ExecuteScalarAsync<int>(checkBalanceQuery, new { UserID = userId }, transaction);

                        if (currentPoints < pointsToDeduct)
                        {
                            await transaction.RollbackAsync();
                            return false; // 點數不足
                        }

                        // 🔹 扣除會員點數
                        string deductPointsQuery = @"
                    UPDATE Register 
                    SET Points = Points - @PointsToDeduct
                    WHERE UserID = @UserID";
                        await connection.ExecuteAsync(deductPointsQuery, new { UserID = userId, PointsToDeduct = pointsToDeduct }, transaction);

                        // 🔹 插入點數變更記錄
                        string insertHistoryQuery = @"
                    INSERT INTO PointsHistory (UserID, PointsChanged, CashSpent, PaymentMethod, Description, ChangeDate) 
                    VALUES (@UserID, @PointsChanged, 0, '系統', '抽取商品', GETDATE())";
                        await connection.ExecuteAsync(insertHistoryQuery, new { UserID = userId, PointsChanged = -pointsToDeduct }, transaction);

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
        }

        // ✅ 2. 取得會員所有儲值與消費記錄
        public async Task<IEnumerable<PointsHistory>> GetPointsHistoryByUserIdAsync(int userId)
        {
            string query = "SELECT * FROM PointsHistory WHERE UserID = @UserID ORDER BY ChangeDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<PointsHistory>(query, new { UserID = userId });
            }
        }

        // ✅ 查詢所有儲值紀錄
        public async Task<IEnumerable<PointsHistoryDto>> GetAllPointsHistoryAsync()
        {
            string sql = @"
                 SELECT 
            m.Name AS Name, 
            p.ChangeDate, 
            p.CashSpent, 
            p.PaymentMethod
        FROM PointsHistory p
        JOIN Register m ON p.UserID = m.UserID
        WHERE p.CashSpent > 0  -- ✅ 只顯示有儲值的紀錄
        ORDER BY p.ChangeDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<PointsHistoryDto>(sql);
            }
        }

        // ✅ 透過姓名 / 日期區間搜尋
        public async Task<IEnumerable<PointsHistoryDto>> SearchPointsHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate)
        {
            string sql = @"
               SELECT 
            m.Name AS Name, 
            p.ChangeDate, 
            p.CashSpent, 
            p.PaymentMethod
        FROM PointsHistory p
        JOIN Register m ON p.UserID = m.UserID
        WHERE p.CashSpent > 0  -- ✅ 過濾掉 `CashSpent = 0` 的紀錄
            AND (@Keyword IS NULL OR m.Name LIKE '%' + @Keyword + '%')
            AND (@StartDate IS NULL OR p.ChangeDate >= @StartDate)
            AND (@EndDate IS NULL OR p.ChangeDate <= @EndDate)
        ORDER BY p.ChangeDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<PointsHistoryDto>(sql, new { Keyword = keyword, StartDate = startDate, EndDate = endDate });
            }
        }

        // ✅ 查詢所有扭蛋抽獎紀錄
        public async Task<IEnumerable<DrawnItemsHistoryDTO>> GetAllDrawnHistoryAsync()
        {
            string sql = @"
                SELECT 
                    m.Name AS Name, 
                    d.DrawDate, 
                    d.PointsUsed, 
                    d.SubProductName
                FROM DrawnItemsHistory d
                JOIN Register m ON d.UserID = m.UserID
                ORDER BY d.DrawDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<DrawnItemsHistoryDTO>(sql);
            }
        }

        // ✅ 透過姓名 / 日期區間搜尋
        public async Task<IEnumerable<DrawnItemsHistoryDTO>> SearchDrawnHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate)
        {
            string sql = @"
                SELECT 
                    m.Name AS Name, 
                    d.DrawDate, 
                    d.PointsUsed, 
                    d.SubProductName
                FROM DrawnItemsHistory d
                JOIN Register m ON d.UserID = m.UserID
                WHERE 
                    (@Keyword IS NULL OR m.Name LIKE '%' + @Keyword + '%')
                    AND (@StartDate IS NULL OR d.DrawDate >= @StartDate)
                    AND (@EndDate IS NULL OR d.DrawDate <= @EndDate)
                ORDER BY d.DrawDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<DrawnItemsHistoryDTO>(sql, new { Keyword = keyword, StartDate = startDate, EndDate = endDate });
            }
        }

        // ✅ 3. 取得會員當前 G 幣總額
        public async Task<int> GetTotalPointsByUserIdAsync(int userId)
        {
            string query = "SELECT COALESCE(SUM(PointsChanged), 0) FROM PointsHistory WHERE UserID = @UserID";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query, new { UserID = userId });
            }
        }

        //讓用戶查詢 抽到的商品清單
        public async Task<IEnumerable<DrawnItemsHistory>> GetDrawnItemsByUserIdAsync(int userId)
        {
            string query = @"
    SELECT 
        dih.DrawID, 
        dih.UserID, 
        dih.ProductID, 
        dih.SubProductID, 
        RTRIM(p.P_Name) + ' ' + 
        CASE 
            WHEN CHARINDEX('(', sp.SP_Name) > 0 
            THEN SUBSTRING(sp.SP_Name, CHARINDEX('(', sp.SP_Name), LEN(sp.SP_Name)) 
            ELSE '' 
        END 
        AS SubProductName,  
        dih.PointsUsed, 
        dih.DrawDate
    FROM DrawnItemsHistory dih
    JOIN Products p ON dih.ProductID = p.P_ID
    JOIN SubProducts sp ON dih.SubProductID = sp.SP_ID
    WHERE dih.UserID = @UserID
    ORDER BY dih.DrawDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<DrawnItemsHistory>(query, new { UserID = userId });
            }
        }

        public async Task<bool> AddDrawnItemHistoryAsync(int userId, int productId, int subProductId, string subProductName, int pointsUsed)
        {

            // ✅ 檢查 ProductID 是否存在
            var productExists = await _dbService.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Products WHERE P_ID = @ProductID",
                new { ProductID = productId });

            if (productExists == 0)
            {
                Console.WriteLine($"❌ ProductID {productId} 不存在，無法插入 DrawnItemsHistory");
                return false;
            }

            using (var connection = _dbService.GetDbConnection() as SqlConnection)
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = @"
                    INSERT INTO DrawnItemsHistory (UserID, ProductID, SubProductID, SubProductName, PointsUsed, DrawDate) 
                    VALUES (@UserID, @ProductID, @SubProductID, @SubProductName, @PointsUsed, GETDATE())";

                        int rowsAffected = await connection.ExecuteAsync(insertQuery, new
                        {
                            UserID = userId,
                            ProductID = productId,
                            SubProductID = subProductId,
                            SubProductName = subProductName,
                            PointsUsed = pointsUsed
                        }, transaction);

                        transaction.Commit();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ SQL 執行失敗: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        //讓用戶查詢抽取商品所花的點數
        public async Task<IEnumerable<PointsHistory>> GetUserPointsHistoryAsync(int userId)
        {
            string query = @"
     SELECT 
    ChangeDate, 
    PaymentMethod, 
    Description, 
    CASE 
        WHEN PointsChanged > 0 THEN '+' + CAST(PointsChanged AS NVARCHAR)
        ELSE CAST(PointsChanged AS NVARCHAR)
    END AS PointsChanged
FROM PointsHistory
WHERE UserID = @UserID and Description = '抽取商品'
ORDER BY ChangeDate DESC;";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<PointsHistory>(query, new { UserID = userId });
            }
        }
        ////讓用戶查詢儲值所花的總現金
        public async Task<decimal> GetTotalCashSpentAsync(int userId)
        {
            string query = @"
    SELECT COALESCE(SUM(CashSpent), 0) 
    FROM PointsHistory 
    WHERE UserID = @UserID AND PointsChanged > 0";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.ExecuteScalarAsync<decimal>(query, new { UserID = userId });
            }
        }

        // ✅ 取得用戶的儲值紀錄
        public async Task<IEnumerable<TopUpHistory>> GetTopUpHistoryAsync(int userId)
        {
            string query = @"
                SELECT 
                    ChangeDate AS TopUpDate, 
                    CashSpent AS AmountSpent, 
                    PaymentMethod 
                FROM PointsHistory
                WHERE UserID = @UserID AND CashSpent > 0
                ORDER BY ChangeDate DESC";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<TopUpHistory>(query, new { UserID = userId });
            }
        }

    }
}

            
        
    


