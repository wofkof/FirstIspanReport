using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.ForgotPasswordServices;

namespace 這是扭蛋機系統
{
    public partial class ForgotPasswordForm : Form
    {
        private readonly ForgotPasswordServices _frgotPasswordServices;

        int curr_y, curr_x;
        bool isWndMove;

        public ForgotPasswordForm()
        {
            _frgotPasswordServices = new ForgotPasswordServices();
            InitializeComponent();
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.curr_x = e.X;
                this.curr_y = e.Y;
                this.isWndMove = true;
            }
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isWndMove)
                this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
        }

        private void panel8_MouseUp(object sender, MouseEventArgs e)
        {
            this.isWndMove = false;
        }

        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {

        }
        //發送驗證碼
        private async void btnSendCode_Click(object sender, EventArgs e)
        {
            string phone = txtPhone.Text;
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("請輸入手機號碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool result = await _frgotPasswordServices.SendVerificationCodeAsync(phone); // ✅ 調用 `SendVerificationCodeAsync`

                if (result)
                {
                    MessageBox.Show("驗證碼已發送至您的手機", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("發送失敗，未註冊的手機號碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"請求失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 重設密碼
        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            string phone = txtPhone.Text;
            string code = txtVerificationCode.Text;
            string newPassword = txtNewPassword.Text;

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("請輸入驗證碼與新密碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool result = await _frgotPasswordServices.ResetPasswordAsync(phone, code, newPassword); // ✅ 調用服務

                if (result)
                {
                    MessageBox.Show("密碼重設成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form4 form4 = new Form4();
                    form4.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("重設失敗，驗證碼輸入錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"請求失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void label7_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }

    }
}
