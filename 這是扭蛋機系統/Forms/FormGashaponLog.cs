using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.PointsHistoryService;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormGashaponLog : Form
    {
        private readonly PointsHistoryService _pointsHistoryService;
        private int _userId;

        public FormGashaponLog(int userId)
        {
            InitializeComponent();
            _pointsHistoryService = new PointsHistoryService();
            _userId = userId;
        }

        private async void FormGashaponLog_Load(object sender, EventArgs e)
        {
            await LoadTopUpHistory();
            await LoadDrawnItems();

            RoundedPanel panel6 = new RoundedPanel()
            {
                
                Size = new Size(200, 100),
                Location = new Point(74, 140),
                BackColor = Color.FromArgb(200, 150, 200),
                CornerRadius = 30, // 圓角幅度
                TopLeft = true,    // 左上角圓角
                TopRight = true,   // 右上角圓角
                BottomLeft = false, // 左下角沒有圓角
                BottomRight = false // 右下角沒有圓角
            };

            RoundedPanel panel7 = new RoundedPanel()
            {
                Size = new Size(200, 100),
                Location = new Point(494, 140),
                BackColor = Color.FromArgb(255, 150, 150),
                CornerRadius = 30, // 圓角幅度
                TopLeft = true,    // 左上角圓角
                TopRight = true,   // 右上角圓角
                BottomLeft = false, // 左下角沒有圓角
                BottomRight = false // 右下角沒有圓角
            };
            this.Controls.Add(panel6);
            this.Controls.Add(panel7);
        }


        public async Task RefreshTopUpHistory()
        {
            await LoadTopUpHistory(); // ✅ 重新載入儲值紀錄
        }

        public async Task RefreshTopUpDrawnItems() 
        {
            await LoadDrawnItems();// ✅ 重新載入抽獎紀錄
        }
       

        private async Task LoadDrawnItems()
        {
            var drawnItems = await _pointsHistoryService.GetDrawnItemsAsync(_userId);

            if (dgvDrawnItems.InvokeRequired)
            {
                dgvDrawnItems.Invoke(new Action(async () => await LoadDrawnItems())); // 避免 UI 線程錯誤
                return;
            }

            dgvDrawnItems.DataSource = null; // ✅ 清空 DataSource，確保 UI 更新

            if (drawnItems == null || !drawnItems.Any())
            {
                MessageBox.Show("目前沒有抽取紀錄！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvDrawnItems.DataSource = drawnItems.Select(d => new
            {
                抽取時間 = d.DrawDate.ToString("yyyy-MM-dd HH:mm:ss"),
                消耗點數 = d.PointsUsed,
                商品名稱 = FormatSubProductName(d.SubProductName),
            }).ToList();
        }



        private async Task LoadTopUpHistory()
        {
            var topUpHistory = await _pointsHistoryService.GetTopUpHistoryAsync(_userId);

            if (dgvTopUpHistory.InvokeRequired)
            {
                dgvTopUpHistory.Invoke(new Action(async () => await LoadTopUpHistory())); // ✅ 確保 UI 線程安全
            }
            else
            {
                dgvTopUpHistory.DataSource = topUpHistory.Select(t => new
                {
                    儲值時間 = t.TopUpDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    消費金額 = $"${t.AmountSpent}",
                    付款方式 = t.PaymentMethod
                }).ToList();
            }
        }


        // ✅ **格式化 `SubProductName`**
        private string FormatSubProductName(string subProductName)
        {
            // 🔹 找到括號內的變體 (A) (B) ...
            int startIndex = subProductName.LastIndexOf('(');
            int endIndex = subProductName.LastIndexOf(')');

            if (startIndex != -1 && endIndex > startIndex)
            {
                string variant = subProductName.Substring(startIndex, endIndex - startIndex + 1); // ✅ 取得 `(A)`
                string cleanName = subProductName.Substring(0, startIndex).Trim(); // ✅ 移除英文部分
                return $"{cleanName} {variant}"; // ✅ 重新組合
            }

            return subProductName; // 🔹 如果沒有括號，則維持原始名稱
        }

    }
}

