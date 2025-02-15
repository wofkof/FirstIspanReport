using System.Data;
using System.Data.SqlClient;
namespace WebAPI.Services
{
    public class DbService : IDbService
    {
        public string _connectionString { get; set; }
        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
