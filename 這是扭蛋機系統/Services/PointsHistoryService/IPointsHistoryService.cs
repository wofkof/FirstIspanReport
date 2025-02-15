using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.PointsHistoryService
{
    internal interface IPointsHistoryService
    {
        Task<bool> AddPointsHistoryAsync(int userId, int pointsChanged, decimal cashSpent, string paymentMethod);
        Task<IEnumerable<PointsHistory>> GetPointsHistoryAsync(int userId);
        Task<int> GetTotalPointsAsync(int userId);
        Task<bool> DeductPointsAsync(int userId, int pointsToDeduct);
        // ✅ 1. 新增抽取紀錄
        Task<bool> AddDrawnItemHistoryAsync(int userId, int productId, int subProductId, string subProductName, int pointsUsed);
        // ✅ 2. 取得會員的抽取紀錄
        Task<IEnumerable<DrawnItemsHistory>> GetDrawnItemsAsync(int userId);
        Task<IEnumerable<PointsHistory>> GetUserPointsHistoryAsync(int userId);
        Task<IEnumerable<TopUpHistory>> GetTopUpHistoryAsync(int userId);
        Task<IEnumerable<PointsHistoryDto>> GetAllPointsHistoryAsync();
        Task<IEnumerable<PointsHistoryDto>> SearchPointsHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<DrawnItemsHistoryDTO>> GetAllDrawnHistoryAsync();
        Task<IEnumerable<DrawnItemsHistoryDTO>> SearchDrawnHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate);
    }
}
