using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Helpers;
using 這是扭蛋機系統.Services.RegisterServices;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormMemberSearch : Form
    {
        private FormMover formMover;
        private List<Member> _members = new List<Member>(); // ✅ 會員搜尋結果
        private readonly RegisterService _registerService = new RegisterService();

        public FormMemberSearch()
        {
            InitializeComponent();
            _registerService = new RegisterService();

            SetupUI(); // ✅ 美化 UI
            this.Load += async (s, e) => await LoadMembers();

            formMover = new FormMover(this);
            formMover.Attach(panel10);
        }

        private void SetupUI()
        {
            this.Size = new Size(900, 600);
            this.BackColor = Color.FromArgb(213, 234, 246);
            

            // **🔹 搜尋類型選單**
            Label lblSearchType = new Label()
            {
                Text = "🔍 搜尋類型：",
                Location = new Point(20, 50),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold)
            };

            cmbSearchType = new ComboBox()
            {
                Location = new Point(120, 48),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSearchType.Items.AddRange(new string[] { "姓名", "電話", "地址", "信箱" });
            cmbSearchType.SelectedIndex = 0; // 預設選擇 "姓名"

            txtSearch = new TextBox()
            {
                Location = new Point(280, 48),
                Width = 200,
                Text = ""
            };

            // **📅 生日範圍**
            Label lblDateRange = new Label()
            {
                Text = "📅 生日區間：",
                Location = new Point(500, 50),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold)
            };

            dtpStartDate = new DateTimePicker()
            {
                Location = new Point(600, 48),
                Format = DateTimePickerFormat.Short,
                Width = 80,
                Value = new DateTime(1800, 1, 1)
            };

            Label lblTo = new Label()
            {
                Text = " ~ ",
                Location = new Point(680, 52),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                AutoSize = true
            };

            dtpEndDate = new DateTimePicker()
            {
                Location = new Point(707, 48),
                Format = DateTimePickerFormat.Short,
                Width = 80,
                Value = DateTime.Today
            };

            // **🔍 搜尋按鈕**
            Button btnSearch = new Button()
            {
                Text = "🔍 搜尋",
                Location = new Point(790, 45),
                Width = 80,
                Height = 30,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearch.Click += async (s, e) => await SearchMembers();

            // **📋 會員列表**
            dgvMembers = new DataGridView()
            {
                Location = new Point(20, 80),
                Size = new Size(850, 230),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(213, 234, 246),
                GridColor = Color.FromArgb(213, 234, 246),
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.FromArgb(213, 234, 246),
                    ForeColor = Color.Black,
                    Font = new Font("微軟正黑體", 10, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Font = new Font("微軟正黑體", 9),
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.FromArgb(255, 153, 208),
                    SelectionForeColor = Color.White
                }
            };

            dgvMembers.SelectionChanged += DgvMembers_SelectionChanged;

            // 🔹 **加入控件**
            this.Controls.Add(lblSearchType);
            this.Controls.Add(cmbSearchType);
            this.Controls.Add(txtSearch);
            this.Controls.Add(lblDateRange);
            this.Controls.Add(dtpStartDate);
            this.Controls.Add(lblTo);
            this.Controls.Add(dtpEndDate);
            this.Controls.Add(btnSearch);
            this.Controls.Add(dgvMembers);
            

            // 🔹 **設定事件**
            txtSearch.TextChanged += async (s, e) => await SearchMembers();
            cmbSearchType.SelectedIndexChanged += async (s, e) => await SearchMembers();
            dtpStartDate.ValueChanged += async (s, e) => await SearchMembers();
            dtpEndDate.ValueChanged += async (s, e) => await SearchMembers();
        }



        // ✅ 預設載入所有會員
        private async Task LoadMembers()
        {
            _members = (await _registerService.SearchMembersAsync("", "姓名", null, null)).ToList();
            DisplayMembers(_members);
        }

        // ✅ 搜尋會員
        private async Task SearchMembers()
        {
            if (cmbSearchType == null || txtSearch == null || dtpStartDate == null || dtpEndDate == null)
            {
                MessageBox.Show("UI 控件尚未正確初始化！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string keyword = txtSearch.Text?.Trim() ?? "";
            string searchType = cmbSearchType.SelectedItem?.ToString() ?? "姓名";
            DateTime? startDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null;
            DateTime? endDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null;

            try
            {
                _members = (await _registerService.SearchMembersAsync(keyword, searchType, startDate, endDate))?.ToList() ?? new List<Member>();
                DisplayMembers(_members);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜尋會員時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ✅ 顯示會員資料到 DataGridView
        private void DisplayMembers(List<Member> members)
        {
            dgvMembers.DataSource = null;
            dgvMembers.DataSource = members.Select(m => new
            {
                ID = m.UserID,
                姓名 = m.Name,
                電話 = m.Phone,
                信箱 = m.Email,
                地址 = m.Address,
                生日 = m.Birthday?.ToString("yyyy-MM-dd") ?? "未填寫",
                會員點數 = $"G {m.Points}"
            }).ToList();
        }

        // ✅ 點擊 `dgvMembers` 時更新 `lblSelectedMember`
        private void DgvMembers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                var selectedRow = dgvMembers.SelectedRows[0];
                lblName.Text = $"{selectedRow.Cells["姓名"].Value}";
                lblPhone.Text = $"{selectedRow.Cells["電話"].Value}";
                lblMail.Text = $"{selectedRow.Cells["信箱"].Value}";
                lblAddress.Text = $"{selectedRow.Cells["地址"].Value}";
                lblBirthday.Text = $"{selectedRow.Cells["生日"].Value}";
                lblPoints.Text = $"{selectedRow.Cells["會員點數"].Value}";
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
