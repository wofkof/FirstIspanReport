using System;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.RegisterServices;
using 這是扭蛋機系統.Services.PointsHistoryService;
using 這是扭蛋機系統.Services.RevenueService;
using 這是扭蛋機系統.Helpers;
using System.Drawing;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormAdmin : Form
    {
        private FormMover formMover;

        private readonly ProductsService _productsService;
        private readonly RegisterService _registerService;
        private readonly PointsHistoryService _pointsHistoryService;
        private readonly RevenueService _revenueService;
        private int _userId;
        private int _roleId; // ✅ 用來判斷是否為店長
       

        public FormAdmin(int userId, int roleId)
        {
            InitializeComponent();

            ClockHelper.StartClock(lblTime);

            this.Load += async (s, e) => await LoadMemberDataAsync();
            _productsService = new ProductsService();
            _registerService = new RegisterService();
            _pointsHistoryService = new PointsHistoryService();
            _revenueService = new RevenueService();
            _userId = userId;
            _roleId = roleId;

            formMover = new FormMover(this);
            formMover.Attach(panel8); // 🔹 設定 Panel 可以拖動 Form

            
            ButtonStyleHelper.ApplyHoverEffect(btnProductManage,
                Color.FromArgb(213, 234, 246), // 懸停背景色
                Color.FromArgb(255, 153, 208), // 懸停前景色
                Color.FromArgb(255, 153, 208), // 正常背景色
                Color.White                   // 正常前景色
            );
            ButtonStyleHelper.ApplyHoverEffect(btnMemberManage,

               Color.FromArgb(213, 234, 246), // 懸停背景色
               Color.FromArgb(255, 153, 208), // 懸停前景色
               Color.FromArgb(255, 153, 208), // 正常背景色
               Color.White                   // 正常前景色
           );
            ButtonStyleHelper.ApplyHoverEffect(btnRevenue, 

               Color.FromArgb(213, 234, 246), // 懸停背景色
               Color.FromArgb(255, 153, 208), // 懸停前景色
               Color.FromArgb(255, 153, 208), // 正常背景色
               Color.White                   // 正常前景色
           );
            ButtonStyleHelper.ApplyHoverEffect(btnHistory, 

               Color.FromArgb(213, 234, 246), // 懸停背景色
               Color.FromArgb(255, 153, 208), // 懸停前景色
               Color.FromArgb(255, 153, 208), // 正常背景色
               Color.White                   // 正常前景色
           );
            ButtonStyleHelper.ApplyHoverEffect(btnClockOut,

               Color.FromArgb(213, 234, 246), // 懸停背景色
               Color.FromArgb(255, 153, 208), // 懸停前景色
               Color.FromArgb(255, 153, 208), // 正常背景色
               Color.White                   // 正常前景色
           );
            ButtonStyleHelper.ApplyHoverEffect(btnOut,

              Color.FromArgb(213, 234, 246), // 懸停背景色
              Color.FromArgb(255, 153, 208), // 懸停前景色
              Color.FromArgb(255, 153, 208), // 正常背景色
              Color.White                   // 正常前景色
          );
        }
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
                lblRoleID.Text = GetRoleName(member.RoleID);
            }
            else
            {
                MessageBox.Show("無法獲取會員資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 開啟商品管理視窗
        private void btnProductManage_Click(object sender, EventArgs e)
        {
            FormProductManage form = new FormProductManage();
            form.ShowDialog();

        }

        // 🔹 開啟會員查詢視窗
        private void btnMemberManage_Click(object sender, EventArgs e)
        {
            FormMemberSearch form = new FormMemberSearch();
            form.ShowDialog();
        }

        // 🔹 開啟會員紀錄查詢視窗
        private void btnHistory_Click(object sender, EventArgs e)
        {
            FormMemberHistory form = new FormMemberHistory();
            form.ShowDialog();
        }

        // 🔹 打卡 / 下班
        private void btnClockOut_Click(object sender, EventArgs e)
        {
            FormAttendanceSearch form = new FormAttendanceSearch(_userId);
            form.ShowDialog();
        }
        //查詢營收
        private void btnRevenue_Click(object sender, EventArgs e)
        {
            if (_roleId != 1)
            {
                MessageBox.Show("您沒有權限查詢營收", "權限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormRevenue formRevenue = new FormRevenue();
            formRevenue.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }
    }
}
