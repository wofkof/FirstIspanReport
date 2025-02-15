using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IOrderDetailRepository
    {
        // 新增訂單明細
        Task AddOrderDetailsAsync(IEnumerable<OrderDetail> orderDetails);

        // 依據訂單 ID 取得訂單明細
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
    }
}
