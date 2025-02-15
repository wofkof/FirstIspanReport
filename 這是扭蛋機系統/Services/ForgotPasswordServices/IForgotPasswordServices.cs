using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.ForgotPasswordServices
{
    internal interface IForgotPasswordServices
    {
        // 發送驗證碼到手機
          Task<bool> SendVerificationCodeAsync(string phone);
        // 驗證驗證碼並重設密碼
          Task<bool> ResetPasswordAsync(string phone, string code, string newPassword);
    }
}
