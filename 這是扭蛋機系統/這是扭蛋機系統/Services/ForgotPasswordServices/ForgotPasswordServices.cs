using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.ForgotPasswordServices
{
    internal class ForgotPasswordServices : IForgotPasswordServices
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7192/RegisterControllers"; // API 端點

        public ForgotPasswordServices()
        {
            _httpClient = new HttpClient();
        }

        // 發送驗證碼到手機
        public async Task<bool> SendVerificationCodeAsync(string phone)
        {
            var requestBody = new { Phone = phone };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/forgot-password", content);
            return response.IsSuccessStatusCode;
        }

        // 驗證驗證碼並重設密碼
        public async Task<bool> ResetPasswordAsync(string phone, string code, string newPassword)
        {
            var requestBody = new { Phone = phone, Code = code, NewPassword = newPassword };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/reset-password", content);
            return response.IsSuccessStatusCode;
        }
    }
}
