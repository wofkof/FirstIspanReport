using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.ProductsService
{
    internal interface IProductsService
    {
        // 獲取所有商品
        Task<IEnumerable<Products>> GetAllProductsAsync();
        // 根據商品ID獲取商品
        Task<Products> GetProductByIdAsync(int productId);
        // 新增商品
        Task<int> AddProductAsync(Products product);
        // 更新商品
        Task<bool> UpdateProductAsync(Products product);
        // 刪除商品
        Task<bool> DeleteProductAsync(int productId);
        // 根據商品名稱獲取商品
        Task<Products> GetProductByNameAsync(string productName);
    }
}
