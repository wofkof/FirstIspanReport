using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IPointsHistoryRepository
    {
        //查詢某位會員的歷史訂單
        public Task<List<Order>> GetOrderHistory(int userId);
        //查詢某位會員的積分儲值紀錄
        public Task<List<PointsHistory>> GetPointsHistoryAsync(int userId);
        //當會員進行儲值時更新積分
        public Task AddPoints(int userId, int points, string description);
        //會員訂單的積分消費
        public Task CreateOrder(int userId, int totalAmount);
        //查詢會員訂單與積分紀錄
        public Task<ActionResult> GetMemberDetails(int userId);
    }
}
