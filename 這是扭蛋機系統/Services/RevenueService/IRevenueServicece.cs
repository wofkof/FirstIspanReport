using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 這是扭蛋機系統.Models;

namespace 這是扭蛋機系統.Services.RevenueService
{
    internal interface IRevenueServicece
    {
        // 🔹 **查詢總營收**
        Task<decimal> GetTotalRevenueAsync();
        // 🔹 **查詢指定時間範圍內的營收**
        Task<List<RevenueRecord>> GetRevenueByDateAsync(DateTime startDate, DateTime endDate);
    }
}
