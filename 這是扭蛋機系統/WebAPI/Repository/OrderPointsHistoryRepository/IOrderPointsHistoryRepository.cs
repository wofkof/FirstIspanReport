using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IOrderPointsHistoryRepository
    {
        // 新增積分使用紀錄
        public  Task<int> AddOrderPointsHistoryAsync(OrderPointsHistory history);
        // 獲取所有積分使用紀錄
        public  Task<IEnumerable<OrderPointsHistory>> GetAllOrderPointsHistoryAsync();
        // 根據訂單ID獲取積分使用紀錄
        public  Task<IEnumerable<OrderPointsHistory>> GetOrderPointsHistoryByOrderIdAsync(int orderId);
    }
}
