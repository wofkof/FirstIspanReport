using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.RegisterServices
{
    internal interface IRegisterServices
    {
        //取得所有會員
          Task<List<Register>> GetAllMembersAsync();
        //依據 UserID 取得會員
          Task<Register> GetMemberByIdAsync(int userId);
        //註冊會員
          Task<bool> RegisterMemberAsync(Register register);
        //更新會員資料
        Task<bool> UpdateMemberAsync(int userId , Register memberData);
        //刪除會員
          Task<bool> DeleteMemberAsync(int userId);
        //會員登入
        Task<Register> LoginAsync(string account, string password);
    }
}
