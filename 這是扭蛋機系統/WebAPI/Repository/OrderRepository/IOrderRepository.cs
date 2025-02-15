using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IOrderRepository
    {
        // 創建訂單
        public Task<int> CreateOrderAsync(Order order);

        // 創建訂單明細
        public Task CreateOrderDetailsAsync(IEnumerable<OrderDetail> orderDetails);

        // 根據訂單ID獲取訂單詳細資料
        public Task<Order> GetOrderByIdAsync(int orderId);

        // 根據會員ID獲取所有歷史訂單
        public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        // 根據訂單ID獲取訂單明細
        public Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);

        // 紀錄積分使用歷史
        public Task CreateOrderPointsHistoryAsync(OrderPointsHistory orderPointsHistory);
    }
}