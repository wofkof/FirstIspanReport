using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

namespace WebAPI.Repository.NewFolder1
{
    public class SubProductsRepository : ISubProductsRepository
    {
            private readonly IDbService _dbService;
            public SubProductsRepository(IDbService dbService)
            {
                _dbService = dbService;
            }
        // 新增小商品
        public async Task<bool> AddSubProductAsync(SubProducts subProduct)
        {
            string query = @"
    INSERT INTO SubProducts (P_ID, SP_Name, SP_Image, SP_Amount) 
    VALUES (@P_ID, @SP_Name, @SP_Image, @SP_Amount)";

            using (var connection = _dbService.GetDbConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(query, subProduct);
                return rowsAffected > 0;
            }
        }

        // 刪除小商品
        public async Task<bool> DeleteSubProductAsync(int spId)
        {
            var sql = "DELETE FROM SubProducts WHERE SP_ID = @SP_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new { SP_ID = spId });
                return rowsAffected > 0;
            }
        }

        // 獲取所有小商品
        public async Task<IEnumerable<SubProducts>> GetAllSubProductsAsync()
        {
            var sql = "SELECT * FROM SubProducts";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<SubProducts>(sql);
            }
        }

        public async Task<IEnumerable<UpdateSubProducts>> UpdateGetAllSubProductsAsync()
        {
            string sql = @"
        SELECT sp.SP_ID, sp.SP_Name, sp.SP_Amount, sp.SP_Image, sp.P_ID, p.P_Name 
        FROM SubProducts sp
        LEFT JOIN Products p ON sp.P_ID = p.P_ID"; // ✅ 確保關聯到 `Products`

            using (var connection = _dbService.GetDbConnection())
            {
                var subProducts = await connection.QueryAsync<UpdateSubProducts, Products, UpdateSubProducts>(
                    sql,
                    (subProduct, product) =>
                    {
                        subProduct.Product = product; // ✅ 確保 `Product` 物件正確載入
                        return subProduct;
                    },
                    splitOn: "P_ID"
                );

                return subProducts.ToList();
            }
        }

        // 根據SP_ID獲取小商品
        public async Task<SubProducts> GetSubProductByIdAsync(int spId)
        {
            var sql = "SELECT * FROM SubProducts WHERE SP_ID = @SP_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<SubProducts>(sql, new { SP_ID = spId });
            }
        }

        // 更新小商品
        public async Task<bool> UpdateSubProductAsync(SubProducts subProduct)
        {
            var sql = @"UPDATE SubProducts 
                        SET P_ID = @P_ID, SP_Name = @SP_Name, SP_Amount = @SP_Amount, SP_Image = @SP_Image
                        WHERE SP_ID = @SP_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                var rowsAffected = connection.ExecuteAsync(sql, subProduct);
                return await rowsAffected > 0;
            }
        }
        // 透過商品ID查詢小商品
        public async Task<IEnumerable<SubProducts>> GetSubProductsByProductIdAsync(int productId)
        {
            var sql = "SELECT * FROM SubProducts WHERE P_ID = @P_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<SubProducts>(sql, new { P_ID = productId });
            }
        }
        //更新小商品庫存量
        public async Task<bool> UpdateSubProductAmountAsync(int spId, int newAmount)
        {
            var sql = "UPDATE SubProducts SET SP_Amount = @SP_Amount WHERE SP_ID = @SP_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { SP_ID = spId, SP_Amount = newAmount });
                return affectedRows > 0;
            }
        }
    }
}
