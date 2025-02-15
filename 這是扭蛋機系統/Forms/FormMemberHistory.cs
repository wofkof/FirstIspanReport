using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Helpers;
using 這是扭蛋機系統.Services;
using 這是扭蛋機系統.Services.PointsHistoryService;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormMemberHistory : Form
    {
        private readonly PointsHistoryService _pointsHistoryService;

        private FormMover formMover;

        public FormMemberHistory()
        {
            InitializeComponent();
            _pointsHistoryService = new PointsHistoryService();

            formMover = new FormMover(this);
            formMover.Attach(panel10);

            this.Load += async (s, e) => await LoadHistories();

            txtSearch.TextChanged += async (s, e) => await SearchHistories();
            dtpStartDate.ValueChanged += async (s, e) => await SearchHistories();
            dtpEndDate.ValueChanged += async (s, e) => await SearchHistories();
        }

        // ✅ 預設載入所有紀錄
        private async Task LoadHistories()
        {
            var pointsHistories = await _pointsHistoryService.GetAllPointsHistoryAsync();
            var drawnHistories = await _pointsHistoryService.GetAllDrawnHistoryAsync();

            DisplayPointsHistory(pointsHistories);
            DisplayDrawnHistory(drawnHistories);
        }

        // ✅ 搜尋功能
        private async Task SearchHistories()
        {
            string keyword = txtSearch.Text.Trim();
            DateTime? startDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null;
            DateTime? endDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null;

            var pointsHistories = await _pointsHistoryService.SearchPointsHistoryAsync(keyword, startDate, endDate);
            var drawnHistories = await _pointsHistoryService.SearchDrawnHistoryAsync(keyword, startDate, endDate);

            DisplayPointsHistory(pointsHistories);
            DisplayDrawnHistory(drawnHistories);
        }

        // ✅ 顯示儲值紀
        private void DisplayPointsHistory(IEnumerable<PointsHistoryDto> histories)
        {
            dgvPointsHistory.DataSource = histories.Select(h => new
            {
                會員名稱 = h.Name,
                儲值時間 = h.ChangeDate.ToString("yyyy-MM-dd HH:mm:ss"),
                儲值金額 = $"${h.CashSpent:N0}",
                付款方式 = h.PaymentMethod
            }).ToList();
        }

        // ✅ 顯示扭蛋抽獎紀錄
        private void DisplayDrawnHistory(IEnumerable<DrawnItemsHistoryDTO> histories)
        {
            dgvDrawnHistory.DataSource = histories.Select(h => new
            {
                會員名稱 = h.Name,
                抽取時間 = h.DrawDate.ToString("yyyy-MM-dd HH:mm:ss"),
                消耗點數 = $"G {h.PointsUsed:N0}",
                抽取商品 = h.SubProductName
            }).ToList();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
