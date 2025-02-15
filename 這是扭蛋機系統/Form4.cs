using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.RegisterServices;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using 這是扭蛋機系統.Forms;


namespace 這是扭蛋機系統
{
    public partial class Form4 : Form
    {
        public readonly RegisterService _registerService;

        private int PictureIndex = 0;
        int curr_y, curr_x;
        bool isWndMove;
        //圖片輪播
        List<Bitmap> PicturesRun = new List<Bitmap>()
          {
            Properties.Resources.吉1,
            Properties.Resources.吉2,
            Properties.Resources.吉3,
            Properties.Resources.Cool,
            Properties.Resources.kitty,
            Properties.Resources.cute1
          };
        //記住密碼
        private void SaveLoginDetails(string account, string password)
        {
            Properties.Settings.Default.Account = account;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.RememberMe = true;
            Properties.Settings.Default.Save(); // ✅ 儲存到本地設定
        }
        //不記住密碼
        private void ClearLoginDetails()
        {
            Properties.Settings.Default.Account = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.RememberMe = false;
            Properties.Settings.Default.Save();
        }
        public Form4()
        {
            InitializeComponent();
            _registerService = new RegisterService();
            if (Properties.Settings.Default.RememberMe)
            {
                txtAccount.Text = Properties.Settings.Default.Account;
                txtPassword.Text = Properties.Settings.Default.Password;
                chkRememberMe.Checked = true;
            }
            timer1.Start();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            SmoothRoundedPanel1(panel1, 50, Color.FromArgb(255,255,255));
            SmoothRoundedPanel1(panel2, 50, Color.FromArgb(255, 153, 208));
            SmoothRoundedPanel1(panel3, 50, Color.FromArgb(255, 153, 208));
            SmoothRoundedPanel1(panel10, 50, Color.FromArgb(255, 255, 255));
            SmoothRoundedPanel1(panel13, 50, Color.FromArgb(255, 153, 208));
            SmoothRoundedPanel1(panel14, 50, Color.FromArgb(213, 234, 246));
            SmoothRoundedPanel1(panel15, 50, Color.FromArgb(255, 153, 208));
            SmoothRoundedPanel1(panel16, 50, Color.FromArgb(213, 234, 246));
        }
        private void SmoothRoundedPanel1(Panel panel, int cornerRadius, Color baseColor)
        {
            panel.BackColor = Color.Transparent;

            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
                    path.AddArc(rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
                    path.AddArc(rect.Width - cornerRadius, rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                    path.AddArc(rect.X, rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                    path.CloseAllFigures();

                    using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(255, baseColor), Color.FromArgb(255, baseColor), LinearGradientMode.Horizontal))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
                txtPassword.PasswordChar = '●';
            }
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text == "Account ")
            {
                txtAccount.Clear();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '●')
            {
                txtPassword.PasswordChar = '\0';
                pictureBox2.Image = Properties.Resources.show_password_53105611;
            }
            else
            {
                txtPassword.PasswordChar = '●';
                pictureBox2.Image = Properties.Resources.invisible_5310560;
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

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.curr_x = e.X;
                this.curr_y = e.Y;
                this.isWndMove = true;
            }
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖1;
            label2.ForeColor = Color.Yellow;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖;
            label2.ForeColor = Color.White;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖1;
            label2.ForeColor = Color.Yellow;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖;
            label2.ForeColor = Color.White;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            label4.ForeColor = Color.Blue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖1;
            label3.ForeColor = Color.Yellow;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖;
            label3.ForeColor = Color.White;
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖1;
            label3.ForeColor = Color.Yellow;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.插圖;
            label3.ForeColor = Color.White;
        }

        private async void panel10_Click(object sender, EventArgs e)
        {
            string account = txtAccount.Text;
            string password = txtPassword.Text;

            var user = await _registerService.LoginAsync(account, password);

            if (user != null)
            {
                MessageBox.Show($"歡迎 {user.Name}", "登入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ **記住密碼或清除密碼**
                if (chkRememberMe.Checked)
                {
                    SaveLoginDetails(account, password); // ✅ 記住密碼
                }
                else
                {
                    ClearLoginDetails(); // ✅ 清除密碼
                }

                if (user.RoleID == 2) // ✅ 員工
                {
                    FormAdmin adminForm = new FormAdmin(user.UserID,user.RoleID);
                    adminForm.Show();
                }
                else if (user.RoleID == 3) // ✅ 會員
                {
                    Form1 mainForm = new Form1(user.UserID);
                    mainForm.Show();
                }
                else if (user.RoleID == 1) // ✅ 店長
                {
                    FormAdmin adminForm = new FormAdmin(user.UserID, user.RoleID);
                    adminForm.Show();
                }
                this.Hide(); // ✅ 隱藏登入視窗
            }
            else
            {
                MessageBox.Show("登入失敗，請檢查帳號或密碼！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm();
            forgotPasswordForm.ShowDialog();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = PicturesRun[PictureIndex];
            PictureIndex++;
            if (PictureIndex >= PicturesRun.Count)
            {
                PictureIndex = 0;
            }
        }
    }
}
