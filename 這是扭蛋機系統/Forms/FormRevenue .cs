using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Helpers;
using 這是扭蛋機系統.Services.RevenueService;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormRevenue : Form
    {
        private readonly RevenueService _revenueService;

        
        private FormMover formMover;
        public FormRevenue()
        {
            InitializeComponent();
            _revenueService = new RevenueService();
            SetupUI();
            ClockHelper.StartClock(lblTime);


            formMover = new FormMover(this);
            formMover.Attach(panel8);
        }

        private void SetupUI()
        {
            this.Text = "營收查詢";
            this.Size = new Size(450, 500);
            this.BackColor = Color.FromArgb(213, 234, 246);

            Label lblTitle = new Label()
            {
                Text = "營收查詢",
                Font = new Font("微軟正黑體", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(40, 40),
            };

            Label lblDateRange = new Label()
            {
                Text = "選擇日期範圍：",
                Location = new Point(40, 80),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                AutoSize = true
            };

            DateTimePicker dtpStart = new DateTimePicker()
            {
                Location = new Point(150, 75),
                Format = DateTimePickerFormat.Short,
                AutoSize = true,
                 Width = 100,
            };

            Label lblTo = new Label()
            {
                Text = "到",
                Location = new Point(277, 75),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                AutoSize = true
            };

            DateTimePicker dtpEnd = new DateTimePicker()
            {
                Location = new Point(330, 75),
                Format = DateTimePickerFormat.Short,
                AutoSize = true,
                 Width = 100,
            };

            Button btnSearch = new Button()
            {
                Text = "查詢營收",
                Location = new Point(220, 160),
                Width = 120,
                Height = 30,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                AutoSize = true
            };
            btnSearch.Click += async (s, e) => await LoadRevenue(dtpStart.Value, dtpEnd.Value);

            Label lblTotalRevenue = new Label()
            {
                Name = "lblTotalRevenue",  // ✅ 設定 Name 方便查找
                Text = "📊 總營收：$0",
                Location = new Point(40, 120),
                Font = new Font("微軟正黑體", 12, FontStyle.Bold),
                AutoSize = true
            };

            Label lblRevenue = new Label()
            {
                Name = "lblRevenue",  // ✅ 設定 Name 方便查找
                Text = "營收：$0",
                Location = new Point(220, 120),
                Font = new Font("微軟正黑體", 12, FontStyle.Bold),
                AutoSize = true
            };

            Button btnlRevenueAmount = new Button()
            {
                Text = "查詢全部營收",
                Location = new Point(40, 160),
                Width = 120,
                Height = 30,
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                AutoSize = true
            };

            btnlRevenueAmount.Click += async (s, e) => await LoadTotalRevenue();

            //顯示資料
            DataGridView dgvRevenue = new DataGridView()
            {
                Location = new Point(40, 200),
                Size = new Size(720, 280),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ScrollBars = ScrollBars.Both,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.FromArgb(213, 234, 246),
                GridColor = Color.FromArgb(213, 234, 246),
                AutoSize = true,
                BorderStyle = BorderStyle.None,
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblDateRange);
            this.Controls.Add(dtpStart);
            this.Controls.Add(lblTo);
            this.Controls.Add(dtpEnd);
            this.Controls.Add(btnSearch);
            this.Controls.Add(lblTotalRevenue);
            this.Controls.Add(lblRevenue); 
            this.Controls.Add(btnlRevenueAmount);
            this.Controls.Add(dgvRevenue);
        }

        private async Task LoadTotalRevenue()
        {
            // ✅ 取得總營收
            decimal totalRevenue = await _revenueService.GetTotalRevenueAsync();

            // ✅ 用 `Name` 來尋找 `Label`
            var lblTotalRevenue = this.Controls.Find("lblTotalRevenue", true).FirstOrDefault() as Label;

            if (lblTotalRevenue != null)
            {
                lblTotalRevenue.Text = $"📊 總營收：${totalRevenue:N0}"; // ✅ 美化格式
            }
            else
            {
                MessageBox.Show("找不到 `lblTotalRevenue`，請確認 UI 是否存在此 Label！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private async Task LoadRevenue(DateTime startDate, DateTime endDate)
        {
            var revenueData = await _revenueService.GetRevenueByDateAsync(startDate, endDate);
            var lblRevenue = this.Controls.Find("lblRevenue", true).FirstOrDefault() as Label;

            if (lblRevenue != null)
            {
                decimal totalAmount = revenueData.Sum(r => r.TotalAmount);
                lblRevenue.Text = $"📆 營收：${totalAmount:N0}";
            }

            // ✅ 更新 DataGridView
            var dgvRevenue = this.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgvRevenue != null)
            {
                dgvRevenue.DataSource = revenueData.Select(r => new
                {
                    訂單ID = r.OrderID,
                    交易時間 = r.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    交易金額 = $"${r.TotalAmount:N0}",
                    付款方式 = !string.IsNullOrEmpty(r.Payment) ? r.Payment : "未指定" // ✅ 處理 null 值
                }).ToList();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
