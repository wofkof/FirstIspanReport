using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using 這是扭蛋機系統.Models;

namespace 這是扭蛋機系統.Services.RevenueService
{
    public class RevenueService : IRevenueServicece
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/api/Revenue"; // ✅ 設定 API 路徑

        public RevenueService()
        {
            _httpClient = new HttpClient();
        }

        // 🔹 **查詢總營收**
        public async Task<decimal> GetTotalRevenueAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/total");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<decimal>(json);
            }
            return 0;
        }

        // 🔹 **查詢指定時間範圍內的營收**
        public async Task<List<RevenueRecord>> GetRevenueByDateAsync(DateTime startDate, DateTime endDate)
        {
            string url = $"{BaseUrl}/range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<RevenueRecord>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new List<RevenueRecord>();
        }
    }
}
