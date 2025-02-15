using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.AttendanceService
{
    public class AttendanceService : IAttendanceService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/api/Attendance";

        public AttendanceService()
        {
            _httpClient = new HttpClient();
        }

        // ✅ 取得員工打卡紀錄
        public async Task<List<AttendanceHistoryDto>> GetAttendanceRecordsAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/history");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<AttendanceHistoryDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<AttendanceHistoryDto>();
        }



        // ✅ 上班打卡
        public async Task<bool> ClockInAsync(int userId)
        {
            var response = await _httpClient.PostAsync($"{BaseUrl}/clock-in",
                new StringContent(JsonSerializer.Serialize(userId), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        // ✅ 下班打卡
        public async Task<bool> ClockOutAsync(int userId)
        {
            var response = await _httpClient.PostAsync($"{BaseUrl}/clock-out",
                new StringContent(JsonSerializer.Serialize(userId), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        // ✅ 查詢特定員工的打卡紀錄
        public async Task<List<AttendanceHistoryDto>> GetAttendanceRecordsAsync(string name, DateTime? startDate, DateTime? endDate)
        {
            string url = $"{BaseUrl}/search?name={name}&startDate={startDate?.ToString("yyyy-MM-dd")}&endDate={endDate?.ToString("yyyy-MM-dd")}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<AttendanceHistoryDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<AttendanceHistoryDto>();
        }

        public async Task<EmployeeDto> GetEmployeeInfoAsync(int userId)
        {
            string url = $"{BaseUrl}/employee/{userId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EmployeeDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return null; // 如果 API 沒有回應成功，回傳 null
        }


    }
}
