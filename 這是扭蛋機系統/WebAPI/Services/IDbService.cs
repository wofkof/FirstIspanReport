using System.Data;

namespace WebAPI.Services
{
    public interface IDbService
    {
        IDbConnection GetDbConnection();
    }
}
