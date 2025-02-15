using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統;

namespace 這是扭蛋機系統.Services.RegisterServices
{
    public class RegisterService : IRegisterServices
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/RegisterControllers"; // ✅ WebAPI 端點 (請確保你的 API 正確)

        public RegisterService()
        {
            _httpClient = new HttpClient();
        }

        //會員登入
        public async Task<Register> LoginAsync(string account, string password)
        {
            var loginRequest = new { Account = account, Password = password };
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Register>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"登入失敗: {errorMessage}");
        }



        // 🔹 刪除會員
        public async Task<bool> DeleteMemberAsync(int userId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{userId}");
            return response.IsSuccessStatusCode;
        }
        // 🔹 取得所有會員
        public async Task<List<Register>> GetAllMembersAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Register>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Register>();
        }

        // 🔹 依據 UserID 取得會員
        public async Task<Register> GetMemberByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{userId}"); // ✅ 確保 API 端點正確

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Register>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return null; // 如果失敗，回傳 `null`
        }

        // 🔹 註冊會員
        public async Task<bool> RegisterMemberAsync(Register register)
        {
            var json = JsonSerializer.Serialize(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(new Uri(BaseUrl), content);

            if (response.IsSuccessStatusCode)
            {
                return true; // ✅ 註冊成功 
            }

            // ✅ 處理 `409 Conflict` 或其他錯誤
            string errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"API 回應錯誤: {errorMessage}");
        }
        // 🔹 更新會員資料
        public async Task<bool> UpdateMemberAsync(int userId, Register memberData)
        {
            var json = JsonSerializer.Serialize(memberData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // ✅ 確保 `userId` 傳遞到 URL
            var response = await _httpClient.PutAsync($"{BaseUrl}/{userId}", content);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"API 更新失敗: {errorMessage}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return response.IsSuccessStatusCode;
        }
    }
}
    