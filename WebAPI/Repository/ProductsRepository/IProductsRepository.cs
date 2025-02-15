using 這是扭蛋機系統;

namespace WebAPI.Repository.ProductsRepository
{
    public interface IProductsRepository
    {
        // 獲取所有商品
        public Task<IEnumerable<Products>> GetAllProductsAsync();
        // 獲取所有商品
        public Task<IEnumerable<Products>> GetProductsByCategoryAsync(string category);
        // 根據商品ID獲取商品
        public  Task<Products> GetProductByIdAsync(int productId);
        // 新增商品
        public Task<bool> AddProductAsync(Products product);
        // 更新商品
        public  Task<bool> UpdateProductAsync(Products product);
        // 刪除商品
        public  Task<bool> DeleteProductAsync(int productId);
        // 根據商品名稱獲取商品
        public Task<Products> GetProductByNameAsync(string productName);
    }
}
