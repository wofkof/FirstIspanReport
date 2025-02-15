using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface ISubProductsRepository
    {
        // 獲取所有小商品
        public Task<IEnumerable<SubProducts>> GetAllSubProductsAsync();
        public  Task<IEnumerable<UpdateSubProducts>> UpdateGetAllSubProductsAsync();
        // 根據SP_ID獲取小商品
        public  Task<SubProducts> GetSubProductByIdAsync(int spId);
        // 新增小商品
        public  Task<bool> AddSubProductAsync(SubProducts subProduct);
        // 更新小商品
        public Task<bool> UpdateSubProductAsync(SubProducts subProduct);
        // 刪除小商品
        public  Task<bool> DeleteSubProductAsync(int spId);
        // 透過商品ID查詢小商品
        public  Task<IEnumerable<SubProducts>> GetSubProductsByProductIdAsync(int productId);
        // 更新小商品的庫存量
        Task<bool> UpdateSubProductAmountAsync(int spId, int newAmount);
    }
}
