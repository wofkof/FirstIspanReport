using System.ComponentModel;
using 這是扭蛋機系統;

namespace WebAPI.Repository.NewFolder1
{
    public interface IGashaponRepository
    {
        public Task<Register> Authenticate(string account, string password);
        //會員登入
        public Task<Register> GetMemberByAccountAsync(string account);
        //查詢會員
        public Task<Register> GetMember(int Id);
        //新增會員
        public Task<bool> RegisterMemberAsync(Register register);
        //修改會員
        public Task<bool> UpdateMemberAsync(Register memberData);
        //刪除會員
        public Task<bool> DeleteMember(int id);
        //查詢全部會員
        public List<Register> GetAllMember();
        //查詢手機號碼的會員
        public  Task<Register> GetMemberByPhoneAsync(string phone);
        //儲存驗證碼
        public  Task SaveVerificationCodeAsync(int userId, string code);
        //驗證驗證碼
        public  Task<bool> ValidateVerificationCodeAsync(string phone, string code);
        //更新會員密碼
        public  Task UpdatePasswordAsync(string phone, string newPassword);
    }
}
