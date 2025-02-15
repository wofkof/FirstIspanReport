using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.ProductsService
{
    internal class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/api/Products"; // ✅ 替換成你的 API URL

        public ProductsService()
        {
            _httpClient = new HttpClient();
        }

        // 獲取所有商品
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Products>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Products>();
        }

        // 根據商品ID獲取商品
        public async Task<Products> GetProductByIdAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Products>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        // 新增商品
        public async Task<int> AddProductAsync(Products product)
        {
            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return int.Parse(result); // 假設 API 返回的是新商品的 ID
            }
            return 0;
        }

        // 更新商品
        public async Task<bool> UpdateProductAsync(Products product)
        {
            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{product.P_ID}", content);
            return response.IsSuccessStatusCode;
        }

        // 刪除商品
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{productId}");
            return response.IsSuccessStatusCode;
        }

        // 根據商品名稱獲取商品
        public async Task<Products> GetProductByNameAsync(string productName)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/by-name/{productName}");
            if (response.IsSuccessStatusCode) 
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Products>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }
    }
}
