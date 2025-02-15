using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Helpers;
using 這是扭蛋機系統.Services.AttendanceService;



namespace 這是扭蛋機系統.Forms
{
    public partial class FormAttendanceSearch : Form
    {
        private readonly AttendanceService _attendanceService;
        
        private FormMover formMover;

        private int _userId; // 當前登入的會員 ID

        public FormAttendanceSearch(int userId)
        {
            InitializeComponent();
            _attendanceService = new AttendanceService();
            _userId = userId;
            SetupUI();
            formMover = new FormMover(this);
            formMover.Attach(panel8);
            ClockHelper.StartClock(lblTime);
        }

        private void SetupUI()
        {
            this.Text = "員工打卡系統";
            this.Size = new Size(830, 590);
            this.BackColor = Color.FromArgb(213, 234, 246);

            // **🔹 員工資訊顯示**
            Label lblEmployee = new Label()
            {
                Text = "員工姓名：",
                Location = new Point(20, 100),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
            };
            Label lblEmployeeName = new Label()
            {
                Text = "（載入中...）",
                Location = new Point(120, 100),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,  // ✅ 讓 Label 自動適應大小
                TextAlign = ContentAlignment.MiddleLeft, // ✅ 文字靠左對齊
                MaximumSize = new Size(200, 0), // ✅ 限制最大寬度，避免超出範圍
                BackColor = Color.Transparent, // ✅ 避免有白色背景
            };

            // **🔹 打卡按鈕**
            Button btnClockIn = new Button()
            {
                Text = "上班打卡",
                Location = new Point(20, 140),
                Width = 150,
                Height = 40,
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnClockIn.Click += async (s, e) => await ClockIn();

            Button btnClockOut = new Button()
            {
                Text = "下班打卡",
                Location = new Point(200, 140),
                Width = 150,
                Height = 40,
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnClockOut.Click += async (s, e) => await ClockOut();

            // **🔹 搜尋區塊**
            Label lblSearch = new Label()
            {
                Text = "搜尋員工：",
                Location = new Point(20, 200),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
            };
            TextBox txtSearch = new TextBox()
            {
                Location = new Point(120, 198),
                Width = 200,
            };

            Label lblDateRange = new Label()
            {
                Text = "日期區間：",
                Location = new Point(350, 200),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                AutoSize = true,
            };
            DateTimePicker dtpStartDate = new DateTimePicker()
            {
                Location = new Point(430, 198),
                Format = DateTimePickerFormat.Short,
                Width = 100,

            };
            Label lblTo = new Label()
            {
                Text = " ~ ",
                Location = new Point(533, 202),
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                AutoSize = true,
            };
            DateTimePicker dtpEndDate = new DateTimePicker()
            {
                Location = new Point(560, 198),
                Format = DateTimePickerFormat.Short,
                Width = 100,
            };

            Button btnSearch = new Button()
            {
                Text = "搜尋",
                Location = new Point(700, 195),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            Button btnShowAll = new Button()
            {
                Text = "顯示全部",
                Location = new Point(400, 140), // 放在 DataGridView 右上方
                Width = 120,
                Height = 40,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            // ✅ 綁定點擊事件，查詢所有員工的打卡記錄
            btnShowAll.Click += async (s, e) => await LoadAllAttendanceRecords();
            btnSearch.Click += async (s, e) => await LoadAttendanceRecords(txtSearch.Text, dtpStartDate.Value, dtpEndDate.Value);


            // **🔹 員工打卡紀錄 DataGridView**
            DataGridView dgvAttendance = new DataGridView()
            {
                Location = new Point(20, 240),
                Size = new Size(800, 320),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
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

            // **🔹 加入元件**
            this.Controls.Add(lblTo); 
            this.Controls.Add(lblEmployee);
            this.Controls.Add(lblEmployeeName);
            this.Controls.Add(btnClockIn);
            this.Controls.Add(btnClockOut);
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(lblDateRange);
            this.Controls.Add(dtpStartDate);
            this.Controls.Add(dtpEndDate);
            this.Controls.Add(btnSearch);
            this.Controls.Add(dgvAttendance);
            this.Controls.Add(btnShowAll);

            // **🔹 載入員工資訊**
            this.Load += async (s, e) => await LoadEmployeeInfo(lblEmployeeName);

            // **🔹 載入所有員工打卡記錄**
            this.Load += async (s, e) => await LoadAttendanceRecords("", null, null);
        }
        
        private async Task LoadEmployeeInfo(Label lblEmployeeName)
        {
            var employee = await _attendanceService.GetEmployeeInfoAsync(_userId);
            if (employee != null)
            {
                lblEmployeeName.Text = employee.Name;
            }
            else
            {
                lblEmployeeName.Text = "無法取得員工資訊";
            }
        }

        private async Task ClockIn()
        {
            bool success = await _attendanceService.ClockInAsync(_userId);
            if (success)
            {
                MessageBox.Show("上班打卡成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadAttendanceRecords("", null, null); // ✅ 重新載入打卡記錄
            }
            else
            {
                MessageBox.Show("打卡失敗，請稍後再試。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ClockOut()
        {
            bool success = await _attendanceService.ClockOutAsync(_userId);
            if (success)
            {
                MessageBox.Show("下班打卡成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadAttendanceRecords("", null, null); // ✅ 重新載入打卡記錄
            }
            else
            {
                MessageBox.Show("打卡失敗，請稍後再試。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadAllAttendanceRecords()
        {
            var records = await _attendanceService.GetAttendanceRecordsAsync();

            var formattedRecords = records.Select(r => new
            {
                員工姓名 = r.Name,
                上班時間 = r.ClockInTime.ToString("yyyy-MM-dd HH:mm:ss"),
                下班時間 = r.ClockOutTime.HasValue ? r.ClockOutTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未下班",
                是否準時上班 = r.ClockInTime.TimeOfDay <= new TimeSpan(9, 0, 0) ? "✅ 準時" : "❌ 遲到",
                是否準時下班 = r.ClockOutTime.HasValue && r.ClockOutTime.Value.TimeOfDay >= new TimeSpan(18, 0, 0) ? "✅ 準時" : "❌ 早退"
            }).ToList();

            var dgvAttendance = this.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgvAttendance != null)
            {
                dgvAttendance.DataSource = formattedRecords;
            }
        }

        private async Task LoadAttendanceRecords(string name, DateTime? startDate, DateTime? endDate)
        {
            var records = await _attendanceService.GetAttendanceRecordsAsync(name, startDate, endDate);

            var formattedRecords = records.Select(r => new
            {
                員工姓名 = r.Name,
                上班時間 = r.ClockInTime.ToString("yyyy-MM-dd HH:mm:ss"),
                下班時間 = r.ClockOutTime.HasValue ? r.ClockOutTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "未下班",
                是否準時上班 = r.ClockInTime.TimeOfDay <= new TimeSpan(9, 0, 0) ? "✅ 準時" : "❌ 遲到",
                是否準時下班 = r.ClockOutTime.HasValue && r.ClockOutTime.Value.TimeOfDay >= new TimeSpan(18, 0, 0) ? "✅ 準時" : "❌ 早退"
            }).ToList();

            var dgvAttendance = this.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgvAttendance != null)
            {
                dgvAttendance.DataSource = formattedRecords;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

