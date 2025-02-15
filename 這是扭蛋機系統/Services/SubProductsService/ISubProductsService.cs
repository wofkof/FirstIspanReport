using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.SubProductsService
{
    internal interface ISubProductsService
    {
        // 取得所有小商品
         Task<List<SubProducts>> GetAllSubProductsAsync();
        // 依據 SP_ID 取得單個小商品
         Task<SubProducts> GetSubProductByIdAsync(int spId);
        // 依據 P_ID 取得所有小商品
         Task<List<SubProducts>> GetSubProductsByProductIdAsync(int productId);
        // 新增小商品
        Task<bool> AddSubProductAsync(int productId, string subProductName, string imagePath, int stock);
        // 更新小商品
         Task<bool> UpdateSubProductAsync(int spId, SubProducts subProduct);
        // 刪除小商品
         Task<bool> DeleteSubProductAsync(int spId);
        //根據小商品ID更新小商品庫存
         Task<bool> UpdateSubProductAmountAsync(int spId, int newAmount);
    }
}
