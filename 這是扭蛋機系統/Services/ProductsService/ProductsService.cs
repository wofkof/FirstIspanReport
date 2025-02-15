using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public async Task<bool> AddProductAsync(string name, int price, string category, string imagePath)
        {
            var request = new
            {
                P_Name = name,
                P_Points = price,
                P_Class = category,
                P_Image = Path.GetFileName(imagePath) // ✅ 只存檔案名稱，避免存入完整路徑
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/add-product", content);
            return response.IsSuccessStatusCode;
        }

        // 更新商品
        public async Task<bool> UpdateProductAsync(int productId, Products updatedProduct)
        {
            if (updatedProduct == null)
                return false; // 確保不會發送空的 `Product`

            var json = JsonSerializer.Serialize(updatedProduct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // ✅ **檢查 URL 是否正確**
            string url = $"{BaseUrl}/{productId}";
            var response = await _httpClient.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"更新失敗！\n錯誤訊息: {errorMsg}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return response.IsSuccessStatusCode;
        }



        // 刪除商品
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{productId}");
            return response.IsSuccessStatusCode;
        }

        // ✅ **取得特定分類的商品**
        public async Task<IEnumerable<Products>> GetProductsByCategoryAsync(string category)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/category/{category}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Products>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Products>();
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
