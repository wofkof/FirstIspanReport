using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.SubProductsService
{
    internal class SubProductsService : ISubProductsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/api/SubProducts"; // 替換成你的 WebAPI 伺服器 URL
        public SubProductsService()
        {
            _httpClient = new HttpClient();
        }
        // 新增小商品
        public async Task<bool> AddSubProductAsync(SubProducts subProduct)
        {
            var json = JsonSerializer.Serialize(subProduct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}", content);
            return response.IsSuccessStatusCode;
        }
        // 刪除小商品
        public async Task<bool> DeleteSubProductAsync(int spId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{spId}");
            return response.IsSuccessStatusCode;
        }
        // 取得所有小商品
        public async Task<List<SubProducts>> GetAllSubProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SubProducts>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<SubProducts>();
        }

        // 依據 SP_ID 取得單個小商品
        public async Task<SubProducts> GetSubProductByIdAsync(int spId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{spId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SubProducts>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }
        // 依據 P_ID 取得所有小商品
        public async Task<List<SubProducts>> GetSubProductsByProductIdAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/by-product/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SubProducts>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<SubProducts>();
        }
        //更新小商品
        public async Task<bool> UpdateSubProductAsync(int spId, SubProducts subProduct)
        {
            var json = JsonSerializer.Serialize(subProduct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{spId}", content);
            return response.IsSuccessStatusCode;
        }

        //根據小商品ID更新小商品庫存
        public async Task<bool> UpdateSubProductAmountAsync(int spId, int newAmount)
        {
            var json = JsonSerializer.Serialize(new { SP_Amount = newAmount });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{spId}/update-amount", content);
            return response.IsSuccessStatusCode;
        }
    }
}
