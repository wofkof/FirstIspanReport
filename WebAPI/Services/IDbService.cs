using System.Data;

namespace WebAPI.Services
{
    public interface IDbService
    {
        IDbConnection GetDbConnection();
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null);
    }
}
