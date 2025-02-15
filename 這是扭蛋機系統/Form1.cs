using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Forms;
using 這是扭蛋機系統.Services.RegisterServices;

namespace 這是扭蛋機系統
{
    public partial class Form1 : Form
    {
        public readonly RegisterService _registerService;
        private int _userId; // ✅ 存儲傳遞過來的 userId


        int curr_y, curr_x;
        bool isWndMove;
        bool booltime;
        bool booltime1;
        FormHome Home;
        FormCute Cute;
        FormAnimal Animal;
        FormFurniture Furniture;
        FormAnime Anime;
        FormUserHome UserHome;
        FormGashaponLog GashaponLog;
        FormBuyMoney BuyMoney;
        FormPractical Practical;
        FormCart Cart;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true; // 設定 Form1 為 MDI 容器
        }

        public Form1(int userId)
        {

            InitializeComponent();
            _registerService = new RegisterService();
            this._userId = userId; // ✅ 設定 `_userId`
            this.Load += async (s, e) => await LoadMemberDataAsync(); // ✅ 當畫面加載時執行
            this.IsMdiContainer = true; // ✅ 設定 MDI 容器
        }

        public void UpdateUserName(string newName)
        {
            lblUserName.Text = $"{newName}"; // ✅ 即時更新
        }

        //取得權限
        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1: return "店長";
                case 2: return "店員";
                case 3: return "會員";
                default: return "未知角色";
            }
        }
        public async Task RefreshUserPoints()
        {
            var member = await _registerService.GetMemberByIdAsync(_userId);
            if (member != null)
            {
                lblPoints.Text = $"{member.Points:N0}"; // ✅ 更新 G 幣顯示
            }
        }

        public void UpdatePointsLabel(int points)
        {
            lblPoints.Text = $"{points}"; // ✅ 即時更新 Form1 的 G 幣
        }

        //取得會員資料
        public async Task LoadMemberDataAsync()
        {
            if (_userId == 0)
            {
                MessageBox.Show("UserID 無效，請重新登入！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var member = await _registerService.GetMemberByIdAsync(_userId);

            if (member != null)
            {
                lblUserName.Text = member.Name;
                lblPoints.Text = $"{member.Points}"; 
                lblRoleID.Text = GetRoleName(member.RoleID);
            }
            else
            {
                MessageBox.Show("無法獲取會員資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void time_Tick(object sender, EventArgs e)
        {
            if (booltime)
            {
                FLP_Menu.Width -= 10;
                if (FLP_Menu.Width == FLP_Menu.MinimumSize.Width)
                {
                    booltime = false;
                    time.Stop();

                }
            }
            else
            {
                FLP_Menu.Width += 10;
                if (FLP_Menu.Width == FLP_Menu.MaximumSize.Width)
                {
                    booltime = true;
                    time.Stop();

                }
            }
        }
        private void PTB_Menu_Click(object sender, EventArgs e)
        {
            time.Start();
        }
        private void time1_Tick(object sender, EventArgs e)
        {
            if (booltime1)
            {
                FLP_Menu1.Height -= 10;
                if (FLP_Menu1.Height == FLP_Menu1.MinimumSize.Height)
                {
                    booltime1 = false;
                    time1.Stop();
                }
            }
            else
            {
                FLP_Menu1.Height += 10;
                if (FLP_Menu1.Height == FLP_Menu1.MaximumSize.Height)
                {
                    booltime1 = true;
                    time1.Stop();
                }
            }
        }
        private void BTN_Menu_Click(object sender, EventArgs e)
        {
            time1.Start();
        }
        private void FLP_Menu1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home = null;
        }
        private void Error_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.curr_x = e.X;
                this.curr_y = e.Y;
                this.isWndMove = true;
            }
        }
        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isWndMove)
                this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
        }
        private void Home_Click(object sender, EventArgs e)
        {
            if (Home == null)
            {
                Home = new FormHome(_userId);
                Home.FormClosed += Home_FormClosed;
                Home.MdiParent = this;
                Home.Dock = DockStyle.Fill;
                Home.Show();
            }
            else
            {
                Home.Activate();
            }
        }

        private void History_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home = null;
        }

        private void cute_Click(object sender, EventArgs e)
        {
            if (Cute == null)
            {
                Cute = new FormCute(_userId);
                Cute.FormClosed += Cute_FormClosed;
                Cute.MdiParent = this;
                Cute.Dock = DockStyle.Fill;
                Cute.Show();
            }
            else
            {
                Cute.Activate();
            }
        }
        private void Cute_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cute = null;
        }
        private void animal_Click(object sender, EventArgs e)
        {
            if (Animal == null)
            {
                Animal = new FormAnimal(_userId);
                Animal.FormClosed += Animal_FormClosed;
                Animal.MdiParent = this;
                Animal.Dock = DockStyle.Fill;
                Animal.Show();
            }
            else
            {
                Animal.Activate();
            }
        }

        private void Animal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Animal = null;
        }

        private void furniture_Click(object sender, EventArgs e)
        {
            if (Furniture == null)
            {
                Furniture = new FormFurniture(_userId);
                Furniture.FormClosed += Furniture_FormClosed;
                Furniture.MdiParent = this;
                Furniture.Dock = DockStyle.Fill;
                Furniture.Show();
            }
            else
            {
                Furniture.Activate();
            }
        }

        private void Furniture_FormClosed(object sender, FormClosedEventArgs e)
        {
            Furniture = null;
        }

        private void anime_Click(object sender, EventArgs e)
        {
            if (Anime == null)
            {
                Anime = new FormAnime(_userId);
                Anime.FormClosed += Anime_FormClosed;
                Anime.MdiParent = this;
                Anime.Dock = DockStyle.Fill;
                Anime.Show();
            }
            else
            {
                Anime.Activate();
            }
        }

        private void Anime_FormClosed(object sender, FormClosedEventArgs e)
        {
            Anime = null;
        }
        private void UserHome_Click(object sender, EventArgs e)
        {
            // ✅ 確保 `FormUserHome` 只開啟一次
            if (UserHome == null || UserHome.IsDisposed)
            {
                UserHome = new FormUserHome(_userId)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };
                // ✅ 當 `FormUserHome` 關閉時，釋放變數
                UserHome.FormClosed += (s, ev) => UserHome = null;
                UserHome.Show();
            }
            else
            {
                UserHome.Activate();
            }
        }

        private void btnBuyMoney_Click(object sender, EventArgs e)
        {
            if (BuyMoney == null || BuyMoney.IsDisposed)
            {
                BuyMoney = new FormBuyMoney(_userId)
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };
               
                BuyMoney.FormClosed += (s, ev) => BuyMoney = null;
                BuyMoney.Show();
            }
            else
            {
                BuyMoney.Activate();
            }
        }

        private void btnGashaponLog_Click(object sender, EventArgs e)
        {
            if (GashaponLog == null)
            {
                GashaponLog = new FormGashaponLog(_userId);
                GashaponLog.FormClosed += GashaponLog_FormClosed;
                GashaponLog.MdiParent = this;
                GashaponLog.Dock = DockStyle.Fill;
                GashaponLog.Show();
            }
            else
            {
                GashaponLog.Activate();
            }
        }

        private void GashaponLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            GashaponLog.Activate();
        }

        private void practical_Click(object sender, EventArgs e)
        {
            if (Practical == null)
            {
                Practical = new FormPractical(_userId);
                Practical.FormClosed += Practical_FormClosed;
                Practical.MdiParent = this;
                Practical.Dock = DockStyle.Fill;
                Practical.Show();
            }
            else
            {
                Practical.Activate();
            }
        }

        private void Practical_FormClosed(object sender, FormClosedEventArgs e)
        {
            Practical.Activate();
        }

        private void ptbOpenCart_Click(object sender, EventArgs e)
        {
            // ✅ 檢查是否已經有開啟 `FormCart`
            FormCart cartForm = this.MdiChildren.OfType<FormCart>().FirstOrDefault();

            if (cartForm == null)
            {
                cartForm = new FormCart(_userId)
                {
                    MdiParent = this, // ✅ 設定 MDI
                    Dock = DockStyle.Fill // ✅ 讓 `FormCart` 填滿 MDI 容器
                };
                cartForm.Show();
            }
            else
            {
                cartForm.Activate(); // ✅ 已開啟則切換至 `FormCart`
            }
        }

        private void flowLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isWndMove = false;
        }

        private void btnOutPassword_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }

        public void UpdateCartLabel(int totalItems)
        {
            label6.Text = $"{totalItems}";
        }
    }
}
