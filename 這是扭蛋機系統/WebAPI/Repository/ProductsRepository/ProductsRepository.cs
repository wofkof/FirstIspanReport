using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;
using WebAPI.Repository.ProductsRepository;

namespace WebAPI.Repository.NewFolder1 
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDbService _dbService;
        public ProductsRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        // 獲取所有商品
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryAsync<Products>(sql);
            }
        }
        // 根據商品ID獲取商品
        public async Task<Products> GetProductByIdAsync(int productId)
        {
            var sql = "SELECT * FROM Products WHERE P_ID = @P_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Products>(sql, new { P_ID = productId });
            }
        }

        // 新增商品
        public async Task<int> AddProductAsync(Products product)
        {
            var sql = @"INSERT INTO Products (P_Name, P_Points, P_Image, P_Class) 
                        VALUES (@P_Name, @P_Points, @P_Image, @P_Class);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, product);
            }
        }

        // 更新商品
        public async Task<bool> UpdateProductAsync(Products product)
        {
            var sql = @"UPDATE Products 
                        SET P_Name = @P_Name, P_Points = @P_Points, P_Image = @P_Image, P_Class = @P_Class
                        WHERE P_ID = @P_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, product);
                return rowsAffected > 0;
            }
        }

        // 刪除商品
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var sql = "DELETE FROM Products WHERE P_ID = @P_ID";
            using (var connection = _dbService.GetDbConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new { P_ID = productId });
                return rowsAffected > 0;
            }
        }
        // 根據商品名稱獲取商品
        public async Task<Products> GetProductByNameAsync(string productName)
        {
            using (var connection = _dbService.GetDbConnection())
            {
                string query = "SELECT * FROM Products WHERE P_Name = @ProductName";
                return await connection.QueryFirstOrDefaultAsync<Products>(query, new { ProductName = productName });
            }
        }
    }
}

