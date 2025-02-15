using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAPI.Repository.NewFolder1;
using 這是扭蛋機系統;
using 這是扭蛋機系統.Services.RegisterServices;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterControllers : ControllerBase
    {
        private IGashaponRepository _gashaponRepository;
        public RegisterControllers(IGashaponRepository gashaponRepository)
        {
            _gashaponRepository = gashaponRepository;
        }

        //登入帳號
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] 這是扭蛋機系統.LoginRequest loginRequest)
        {
            var user = await _gashaponRepository.Authenticate(loginRequest.Account, loginRequest.Password);

            if (user == null)
                return Unauthorized(new { message = "帳號或密碼錯誤" });

            return Ok(user);
        }


        [HttpGet]
        //查詢會員列表
        public List<Register> GetList()
        {
            return  _gashaponRepository.GetAllMember();
        }
        //查詢會員ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _gashaponRepository.GetMember(id);

            if (member == null)
            {
                return NotFound(new { message = "找不到該會員" }); // ❌ 會員不存在時回傳 404
            }

            return Ok(member); // ✅ 會員存在時回傳 200 + JSON 資料
        }
        //新增會員
        [HttpPost]
        public async Task<IActionResult> RegisterMember([FromBody] Register register)
        {
            if (register == null)
                return BadRequest("會員資料不能為空");

            // 🔹 檢查必填欄位
            if (string.IsNullOrWhiteSpace(register.Account) ||
                string.IsNullOrWhiteSpace(register.Password) ||
                string.IsNullOrWhiteSpace(register.Name) ||
                string.IsNullOrWhiteSpace(register.Phone) ||
                register.RoleID <= 0 ||
                register.Birthday == default)
            {
                return BadRequest("請填寫所有必填欄位");
            }

            try
            {
                bool success = await _gashaponRepository.RegisterMemberAsync(register);
                if (!success)
                    return BadRequest("註冊失敗，請檢查資料");

                return Ok("註冊成功");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message); // 🔹 回傳 409 Conflict (帳號重複等問題)
            }
        }
        //更新會員
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateMember(int userId, [FromBody] Register memberData)
        {
            if (memberData == null || userId != memberData.UserID)
            {
                return BadRequest("會員 ID 錯誤");
            }

            bool updated = await _gashaponRepository.UpdateMemberAsync(memberData);

            if (!updated)
            {
                return NotFound("找不到該會員或更新失敗");
            }

            return NoContent(); // ✅ 更新成功回傳 `204 No Content`
        }


        //刪除會員
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool isDeleted = await _gashaponRepository.DeleteMember(id);

            if (isDeleted)
            {
                return Ok(new { message = "會員已成功刪除" });
            }
            else
            {
                return NotFound(new { message = "會員不存在" });
            }
        }

        //發送驗證碼
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] 這是扭蛋機系統.ForgotPasswordRequest request)
        {
            var user = await _gashaponRepository.GetMemberByPhoneAsync(request.Phone);
            if (user == null)
                return NotFound("此手機號碼未註冊");

           
            string verificationCode = new Random().Next(100000, 999999).ToString();
            await _gashaponRepository.SaveVerificationCodeAsync(user.UserID, verificationCode);

            Console.WriteLine($"發送驗證碼到 {request.Phone}，驗證碼: {verificationCode}");

            return Ok("驗證碼已發送至您的手機");
        }

        //驗證 & 重設密碼
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] 這是扭蛋機系統.ResetPasswordRequest request)
        {
            var isValid = await _gashaponRepository.ValidateVerificationCodeAsync(request.Phone, request.Code);
            if (!isValid)
                return BadRequest("驗證碼錯誤或已過期");

            await _gashaponRepository.UpdatePasswordAsync(request.Phone, request.NewPassword);
            return Ok("密碼重設成功");
        }

        // ✅ 會員搜尋 API
        [HttpGet("search")]
        public async Task<IActionResult> SearchMembers(
            [FromQuery] string keyword = "",
            [FromQuery] string searchType = "姓名",
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var members = await _gashaponRepository.SearchMembersAsync(keyword, searchType, startDate, endDate);
            return Ok(members);
        }
    }
}

