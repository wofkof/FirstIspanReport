using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using 這是扭蛋機系統.Services.RegisterServices;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormUserHome : Form
    {
        private readonly RegisterService _registerService;
        private int _userId; // 當前登入會員的 ID

        public FormUserHome(int userId)
        {
            InitializeComponent();
            _registerService = new RegisterService();
            _userId = userId;
            this.Load += async (s, e) => await LoadUserDataAsync();
        }

        public async Task LoadUserDataAsync()
        {
            var member = await _registerService.GetMemberByIdAsync(_userId);

            if (member != null)
            {
                lblUserID.Text = $"{member.UserID}";
                lblPhone.Text = $"{member.Phone}";
                lblEmail.Text = $"{member.Email}";
                lblAddress.Text = $"{member.Address}";
                lblPoints.Text = $"G {member.Points}";
            }
            else
            {
                MessageBox.Show("無法獲取會員資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormUserHome_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // ✅ 確保當前 Form (`FormUserHome`) 有 MDI 容器
            if (this.MdiParent == null)
            {
                MessageBox.Show("錯誤：本視窗沒有 MDI 容器", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1 mdiContainer = (Form1)this.MdiParent; // ✅ 取得 `Form1` 作為 MDI 容器

            // ✅ 檢查是否已經開啟 `FormRevise`
            FormRevise formRevise = mdiContainer.MdiChildren.OfType<FormRevise>().FirstOrDefault();

            if (formRevise == null)
            {
                formRevise = new FormRevise(_userId)
                {
                    MdiParent = mdiContainer, // ✅ 設定 MDI 容器
                    Dock = DockStyle.Fill // ✅ 讓 `FormRevise` 充滿 `Form1`
                };
                formRevise.Show();
            }
            else
            {
                formRevise.Activate();
            }

            this.Close(); // ✅ 關閉 `FormUserHome`
        }

        private void btnUpdate_MouseMove(object sender, MouseEventArgs e)
        {
            btnUpdate.BackColor = Color.FromArgb(255, 153, 208);
            btnUpdate.ForeColor = Color.White;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            btnUpdate.BackColor = Color.FromArgb(213, 234, 246);
            btnUpdate.ForeColor = Color.Gray;
        }

    }
}
