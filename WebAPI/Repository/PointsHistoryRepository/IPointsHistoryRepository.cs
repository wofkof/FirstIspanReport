using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IPointsHistoryRepository
    {
        // ✅ 1. 新增儲值記錄
        public Task<bool> AddPointsHistoryAsync(PointsHistory history);
        //負責扣除會員的 G 幣
        public Task<bool> DeductPointsAsync(int userId, int pointsToDeduct);
        // ✅ 2. 取得會員所有儲值與消費記錄
        public Task<IEnumerable<PointsHistory>> GetPointsHistoryByUserIdAsync(int userId);
        // ✅ 3. 取得會員當前 G 幣總額
        public Task<int> GetTotalPointsByUserIdAsync(int userId);
        public  Task<IEnumerable<DrawnItemsHistory>> GetDrawnItemsByUserIdAsync(int userId);
        public  Task<bool> AddDrawnItemHistoryAsync(int userId, int productId, int subProductId, string subProductName, int pointsUsed);
        public  Task<IEnumerable<PointsHistory>> GetUserPointsHistoryAsync(int userId);
        public  Task<decimal> GetTotalCashSpentAsync(int userId);
        public  Task<IEnumerable<TopUpHistory>> GetTopUpHistoryAsync(int userId);

        public  Task<IEnumerable<PointsHistoryDto>> GetAllPointsHistoryAsync();

        public  Task<IEnumerable<PointsHistoryDto>> SearchPointsHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate);

        // ✅ 查詢所有扭蛋抽獎紀錄
        public Task<IEnumerable<DrawnItemsHistoryDTO>> GetAllDrawnHistoryAsync();
        // ✅ 透過姓名 / 日期區間搜尋
        public  Task<IEnumerable<DrawnItemsHistoryDTO>> SearchDrawnHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate);
    }
}
