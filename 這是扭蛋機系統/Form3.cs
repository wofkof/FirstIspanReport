using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using 這是扭蛋機系統.Services.RegisterServices;


namespace 這是扭蛋機系統
{
    public partial class Form3 : Form
    {
        private readonly RegisterService _registerService; 

        private Form previousForm;
        int curr_y, curr_x;
        bool isWndMove;

        private int GetSelectedRoleID()
        {
            if (rbManager.Checked) return 1; // 店長
            if (rbEmployee.Checked) return 2; // 員工
            if (rbMember.Checked) return 3; // 會員
            return 3; // 預設為 會員 (RoleID = 3)
        }

        public Form3()
        {
            InitializeComponent();
            _registerService = new RegisterService();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
                SmoothRoundedPanel1(panel3, 50, Color.FromArgb(255, 153, 208));
                SmoothRoundedPanel1(panel10, 50, Color.FromArgb(255,255,255));
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
        private void label7_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(); 
            form4.Show();
            this.Close();
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

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isWndMove)
                this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
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
        private void panel8_MouseUp(object sender, MouseEventArgs e)
        {
            this.isWndMove = false;
        }
        private void textBox6_Click(object sender, EventArgs e)
        {
            txtAccount.Clear();
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
        }
        private void textBox5_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            txtPhone.Clear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private async void panel10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccount.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                dtpBirthday.Value == default)
            {
                MessageBox.Show("請填寫所有必填欄位", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newMember = new Register
            {
                Account = txtAccount.Text,
                Password = txtPassword.Text,
                Name = txtName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                RoleID = GetSelectedRoleID(),
                Birthday = dtpBirthday.Value,
                Marriage = chkMarriage.Checked,
                Points = 0
            };

            try
            {
                bool result = await _registerService.RegisterMemberAsync(newMember);
                MessageBox.Show(result ? "會員註冊成功" : "會員註冊失敗");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"錯誤: {ex.Message}");
            }
            //返回登入畫面
            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            txtAddress.Clear();
        }

    }
}
