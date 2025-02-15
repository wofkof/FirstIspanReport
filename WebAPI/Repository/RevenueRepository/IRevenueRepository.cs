using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IRevenueRepository
    {
        // 查詢總營收
        public  Task<decimal> GetTotalRevenueAsync();
        // 查詢指定時間範圍內的營收
        public  Task<List<RevenueRecord>> GetRevenueByDateAsync(DateTime startDate, DateTime endDate);
    }
}
