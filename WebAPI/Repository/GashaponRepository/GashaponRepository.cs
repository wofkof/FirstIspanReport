using System.Data;
using WebAPI.Services;
using 這是扭蛋機系統;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

namespace WebAPI.Repository.NewFolder1
{
    public class GashaponRepository : IGashaponRepository
    {
        private readonly IDbService _dbService;
        public GashaponRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        //帳號密碼登入
        public async Task<Register> Authenticate(string account, string password)
        {
            using (var connection = _dbService.GetDbConnection())
            {
                string sql = "SELECT * FROM Register WHERE Account = @Account AND Password = @Password";
                return await connection.QueryFirstOrDefaultAsync<Register>(sql, new { Account = account, Password = password });
            }
        }

        //會員登入
        public async Task<Register> GetMemberByAccountAsync(string account)
        {
            var sql = "SELECT * FROM Register WHERE Account = @Account";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Register>(sql, new { Account = account });
            }
        }
        //新增會員
        public async Task<bool> RegisterMemberAsync(Register register)
        {
            var sql = @"INSERT INTO Register (Account, Password, Name, Phone, Email, Address, RoleID, Birthday, Marriage, Points) 
                VALUES (@Account, @Password, @Name, @Phone, @Email, @Address, @RoleID, @Birthday, @Marriage, @Points)";

            using (var connection = _dbService.GetDbConnection())
            {
                try
                {
                    int rowsAffected = await connection.ExecuteAsync(sql, register);
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // 🔹 SQL Server `UNIQUE` 限制錯誤
                    {
                        throw new Exception("帳號或手機已被註冊");
                    }
                    throw;
                }
            }
        }
        //刪除會員
        public async Task<bool> DeleteMember(int id)
        {
            var sql = "DELETE FROM Register WHERE UserID = @Id"; // 使用參數化查詢

            using (var connection = _dbService.GetDbConnection())
            {
                connection.Open();

                // 執行刪除操作，並返回刪除影響的行數
                int rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

                return rowsAffected > 0; // 如果刪除成功，返回 true，否則返回 false
            }
        }
        //查詢全部會員
        public List<Register> GetAllMember()
        {
            var sql = "SELECT * FROM Register"; // 查詢所有資料

            var members = new List<Register>();

            using (var connection = _dbService.GetDbConnection()) // 使用你的資料庫連接
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;

                    // 執行查詢操作
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var member = new Register
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Account = reader.IsDBNull(reader.GetOrdinal("Account")) ? null : reader.GetString(reader.GetOrdinal("Account")),
                                Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                Birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? null : reader.GetDateTime(reader.GetOrdinal("Birthday")),
                                Marriage = reader.GetBoolean(reader.GetOrdinal("Marriage")),
                                Points = reader.GetInt32(reader.GetOrdinal("Points")),
                            };
                            members.Add(member); // 將資料加入到 List 中
                        }
                    }
                }
            }

            return members; // 返回所有資料
        }
        //取得會員
        public async Task<Register> GetMember(int id)
        {
            var sql = "SELECT * FROM Register WHERE UserID = @Id";
            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Register>(sql, new { Id = id });
            }
        }


        //更新會員
        public async Task<bool> UpdateMemberAsync(Register memberData)
        {
            var sql = @" UPDATE Register SET Account = @Account, Password = @Password, Name = @Name, Phone = @Phone, Email = @Email, Address = @Address, Birthday = @Birthday, Marriage = @Marriage
                                 WHERE UserID = @UserID;";

            using (var connection = _dbService.GetDbConnection())
            {
                connection.Open();

                // 使用 Dapper 執行更新操作，並返回受影響的行數
                int rowsAffected = await connection.ExecuteAsync(sql, memberData);

                return rowsAffected > 0; // 如果有行數受影響，表示更新成功
            }
        }
        //查詢手機號碼的會員
        public async Task<Register> GetMemberByPhoneAsync(string phone) 
        {
            var sql = "SELECT * FROM Register WHERE Phone = @Phone";

            using (var connection = _dbService.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Register>(sql, new { Phone = phone });
            }
        }
        //儲存驗證碼
        public async Task SaveVerificationCodeAsync(int userId, string code) 
        {
            var sql = "UPDATE Register SET VerificationCode = @Code WHERE UserID = @UserID";

            using (var connection = _dbService.GetDbConnection())
            {
                await connection.ExecuteAsync(sql, new { Code = code, UserID = userId });
            }
        }
        //驗證驗證碼
        public async Task<bool> ValidateVerificationCodeAsync(string phone, string code)
        {
            var sql = "SELECT COUNT(*) FROM Register WHERE Phone = @Phone AND VerificationCode = @Code";

            using (var connection = _dbService.GetDbConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(sql, new { Phone = phone, Code = code });
                return count > 0;
            }
        }
        //更新會員密碼
        public async Task UpdatePasswordAsync(string phone, string newPassword)
        {
            var sql = "UPDATE Register SET Password = @NewPassword WHERE Phone = @Phone";

            using (var connection = _dbService.GetDbConnection())
            {
                await connection.ExecuteAsync(sql, new { NewPassword = newPassword, Phone = phone });
            }
        }
        public async Task<IEnumerable<Member>> SearchMembersAsync(string keyword, string searchType, DateTime? startDate, DateTime? endDate)
        {
            string query = @"
        SELECT * FROM Register 
        WHERE 
        (@Keyword = '' OR 
        (@SearchType = '姓名' AND Name LIKE '%' + @Keyword + '%') OR
        (@SearchType = '電話' AND Phone LIKE '%' + @Keyword + '%') OR
        (@SearchType = '地址' AND Address LIKE '%' + @Keyword + '%') OR
        (@SearchType = '信箱' AND Email LIKE '%' + @Keyword + '%'))
        AND (@StartDate IS NULL OR Birthday >= @StartDate)
        AND (@EndDate IS NULL OR Birthday <= @EndDate)";
            using (var connection = _dbService.GetDbConnection()) 
            {
                return await connection.QueryAsync<Member>(query, new
                {
                    Keyword = keyword,
                    SearchType = searchType,
                    StartDate = startDate,
                    EndDate = endDate
                });
            }
        }
    }
}
