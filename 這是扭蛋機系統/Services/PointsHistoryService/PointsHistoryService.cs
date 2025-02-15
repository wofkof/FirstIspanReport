using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace 這是扭蛋機系統.Services.PointsHistoryService
{
    public class PointsHistoryService : IPointsHistoryService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/api/PointsHistory";

        public PointsHistoryService()
        {
            _httpClient = new HttpClient();
        }

        // ✅ **1. 新增儲值紀錄**
        public async Task<bool> AddPointsHistoryAsync(int userId, int pointsChanged, decimal cashSpent, string paymentMethod)
        {
            var request = new
            {
                UserID = userId,
                PointsChanged = pointsChanged,
                CashSpent = cashSpent,
                PaymentMethod = paymentMethod,
                Description = "會員儲值"
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/add", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ **2. 扣除會員 G 幣**
        public async Task<bool> DeductPointsAsync(int userId, int pointsToDeduct)
        {
            var request = new
            {
                UserID = userId,
                PointsToDeduct = pointsToDeduct
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/deduct", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ **3. 取得會員的所有儲值記錄**
        public async Task<IEnumerable<PointsHistory>> GetPointsHistoryAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PointsHistory>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<PointsHistory>();
        }

        // ✅ **4. 取得會員當前 G 幣**
        public async Task<int> GetTotalPointsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/total/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TotalPointsResponse>(json);
                return result?.TotalPoints ?? 0;
            }
            return 0;
        }
        public async Task<decimal> GetTotalCashSpentAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/total-cash/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TotalCashResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result != null)
                {
                    return result.TotalCashSpent;
                }
            }
            return 0; // 🚨 確保錯誤時回傳 0
        }


        // ✅ 1. 新增抽取紀錄
        public async Task<bool> AddDrawnItemHistoryAsync(int userId, int productId, int subProductId, string subProductName, int pointsUsed)
        {
            var request = new
            {
                UserID = userId,
                ProductID = productId,
                SubProductID = subProductId,
                SubProductName = subProductName,
                PointsUsed = pointsUsed
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/add-drawn", content);
            return response.IsSuccessStatusCode;
        }


        // ✅ 2. 取得會員的抽取紀錄
        public async Task<IEnumerable<DrawnItemsHistory>> GetDrawnItemsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/drawn-items/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<DrawnItemsHistory>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new List<DrawnItemsHistory>(); // ❌ 沒有資料時回傳空列表
        }

        public async Task<IEnumerable<PointsHistory>> GetUserPointsHistoryAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/points-history/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PointsHistory>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<PointsHistory>();
        }
        // ✅ 取得用戶的儲值紀錄
        public async Task<IEnumerable<TopUpHistory>> GetTopUpHistoryAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/topup-history/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<TopUpHistory>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<TopUpHistory>();
        }

        // ✅ 取得所有儲值紀錄
        public async Task<IEnumerable<PointsHistoryDto>> GetAllPointsHistoryAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/all");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PointsHistoryDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<PointsHistoryDto>();
        }

        // ✅ 透過姓名或日期篩選紀錄
        public async Task<IEnumerable<PointsHistoryDto>> SearchPointsHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate)
        {
            string query = $"?keyword={keyword}&startDate={startDate?.ToString("yyyy-MM-dd")}&endDate={endDate?.ToString("yyyy-MM-dd")}";
            var response = await _httpClient.GetAsync($"{BaseUrl}/search{query}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PointsHistoryDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<PointsHistoryDto>();
        }

        // ✅ 取得所有扭蛋紀錄
        public async Task<IEnumerable<DrawnItemsHistoryDTO>> GetAllDrawnHistoryAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/all-Drawn");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<DrawnItemsHistoryDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<DrawnItemsHistoryDTO>();
        }

        // ✅ 透過姓名或日期篩選紀錄
        public async Task<IEnumerable<DrawnItemsHistoryDTO>> SearchDrawnHistoryAsync(string keyword, DateTime? startDate, DateTime? endDate)
        {
            string query = $"?keyword={keyword}&startDate={startDate?.ToString("yyyy-MM-dd")}&endDate={endDate?.ToString("yyyy-MM-dd")}";
            var response = await _httpClient.GetAsync($"{BaseUrl}/search-Drawn{query}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<DrawnItemsHistoryDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<DrawnItemsHistoryDTO>();
        }
    }
}
    

   

