using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    //輸入新密碼
    public class ResetPasswordRequest 
    {
        public string Phone { get; set; }
        public string NewPassword { get; set; }
        public string Code { get; set; }
    }
    //確認用戶手機
    public class ForgotPasswordRequest
    {
        public string Phone { get; set; } // ✅ 新增 Phone 屬性
    }
    //登入會員帳密
    public class LoginRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
    public class Register
    {
        //用戶編號  
        public int UserID { get; set; }
        //用戶帳號
        public string Account { get; set; }
        //用戶密碼
        public string Password { get; set; }
        //用戶姓名
        public string Name { get; set; }
        //用戶電話
        public string Phone { get; set; }
        //用戶信箱
        public string Email { get; set; }
        //用戶地址
        public string Address { get; set; }
        //用戶權限
        public int RoleID { get; set; }
        //用戶生日
        public DateTime? Birthday { get; set; }
        //用戶婚姻
        public bool Marriage { get; set; }
        //用戶點數
        public int Points { get; set; }
    }
    public class Member
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int Points { get; set; }
        public int RoleID { get; set; }
    }
   
}
