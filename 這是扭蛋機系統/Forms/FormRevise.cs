using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.RegisterServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormRevise : Form
    {

        private readonly RegisterService _registerService;

        public FormRevise(int userId)
        {
            InitializeComponent();
            _registerService = new RegisterService();
            _userId = userId;
            this.Load += async (s, e) => await LoadUserDataAsync();
        }

        private int _userId; // 當前登入會員的 ID
        private string _userPhone; // 🔹 用來儲存手機號碼，但不顯示在 UI
        private string _userAccount; //🔹 用來儲存帳號，但不顯示在 UI
        private string _userPassword; //🔹 用來儲存密碼，但不顯示在 UI
        private async Task LoadUserDataAsync()
        {
            var member = await _registerService.GetMemberByIdAsync(_userId);

            if (member != null)
            {
                txtName.Text = member.Name;
                dtpBirthday.Value = member.Birthday ?? DateTime.Now;
                txtEmail.Text = member.Email;
                chkMarriage.Checked = member.Marriage;
                txtAddress.Text = member.Address;

                _userPhone = member.Phone; // ✅ 儲存手機號碼，但不顯示在畫面
                _userAccount = member.Account; // ✅ 儲存帳號，但不顯示在畫面
                _userPassword = member.Password; // ✅ 儲存密碼，但不顯示在畫面
            }
            else
            {
                MessageBox.Show("無法獲取會員資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //確定修改
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_userId == 0)
            {
                MessageBox.Show("會員 ID 不正確，無法更新！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var updatedMember = new Register
            {
                UserID = _userId,
                Name = txtName.Text,
                Birthday = dtpBirthday.Value,
                Email = txtEmail.Text,
                Marriage = chkMarriage.Checked,
                Address = txtAddress.Text,
                Phone = _userPhone,    // ✅ 確保 Phone 仍然存在，但不修改
                Account = _userAccount, // ✅ 確保 Account 仍然存在，但不修改
                Password = _userPassword // ✅ 確保 Password 仍然存在，但不修改
            };

            bool success = await _registerService.UpdateMemberAsync(_userId, updatedMember);

            if (success)
            {
                MessageBox.Show("個人資料更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                if (Application.OpenForms["Form1"] is Form1 form1)
                {
                    form1.UpdateUserName(txtName.Text);
                }

                ShowFormUserHome(); // ✅ 確保 `FormUserHome` 正確顯示
                this.Close(); // ✅ 關閉 `FormRevise`
            }
            else
            {
                MessageBox.Show("更新失敗，請重試！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // **新增方法：顯示 FormUserHome**
        private void ShowFormUserHome()
        {
            if (this.MdiParent is Form1 mdiContainer) // ✅ 確保 MDI 容器存在
            {
                // ✅ 檢查 `FormUserHome` 是否已經開啟
                FormUserHome formUserHome = mdiContainer.MdiChildren
                    .OfType<FormUserHome>()
                    .FirstOrDefault();

                if (formUserHome == null)
                {
                    formUserHome = new FormUserHome(_userId)
                    {
                        MdiParent = mdiContainer,
                        Dock = DockStyle.Fill
                    };
                    formUserHome.Show();
                }
                else
                {
                    formUserHome.LoadUserDataAsync(); // ✅ 重新加載會員數據
                    formUserHome.Activate(); // ✅ 讓 `FormUserHome` 成為焦點
                }
            }
        }

        //取消修改
        private void button1_Click(object sender, EventArgs e)
        {
            // ✅ 確保當前 Form (`FormRevise`) 有 MDI 容器
            if (this.MdiParent == null)
            {
                MessageBox.Show("錯誤：本視窗沒有 MDI 容器", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1 mdiContainer = (Form1)this.MdiParent; // ✅ 取得 `Form1` 作為 MDI 容器

            // ✅ 檢查是否已經開啟 `FormUserHome`
            FormUserHome formUserHome = mdiContainer.MdiChildren.OfType<FormUserHome>().FirstOrDefault();

            if (formUserHome == null)
            {
                formUserHome = new FormUserHome(_userId) // ✅ 傳遞 `UserID`
                {
                    MdiParent = mdiContainer, // ✅ 設定 MDI 容器
                    Dock = DockStyle.Fill // ✅ 讓 `FormUserHome` 充滿 `Form1`
                };
                formUserHome.Show();
            }
            else
            {
                formUserHome.Activate();
            }

            this.Close(); // ✅ 關閉 `FormRevise`
        }
    }
}
