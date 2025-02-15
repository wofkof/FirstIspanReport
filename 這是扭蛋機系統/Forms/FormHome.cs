using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Forms;
using 這是扭蛋機系統.Services.ProductsService;

namespace 這是扭蛋機系統
{
    public partial class FormHome : Form
    {
        private readonly ProductsService _productsService;
        private int currentPage = 1; // 當前頁數
        private int itemsPerPage = 8; // 每頁顯示的商品數量
        private List<Products> allProducts = new List<Products>(); // 所有商品
        private List<Products> filteredProducts = new List<Products>(); //存儲搜尋結果
        private int _userId; // ✅ 存儲登入的 UserID

        public FormHome(int userId)
        {
            InitializeComponent();
            this._userId = userId; // ✅ 設定 UserID
            _productsService = new ProductsService();
            LoadProductsAsync();
        }

        public class RoundedPanel : Panel
        {
            private int cornerRadius = 20; // 設置圓角半徑

            public int CornerRadius
            {
                get { return cornerRadius; }
                set { cornerRadius = value; this.Invalidate(); }
            }


            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // 創建圓角區域
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
                path.AddArc(Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
                path.AddArc(Width - cornerRadius * 2, Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                path.AddArc(0, Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
                path.CloseFigure();

                // 設置 Region，讓 Panel 呈現圓角
                this.Region = new Region(path);

                // 自定義繪製邊框
                using (Pen pen = new Pen(Color.FromArgb(213, 234, 246), 2))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private async Task LoadProductsAsync()
        {
            allProducts = (List<Products>)await _productsService.GetAllProductsAsync(); // 取得所有商品
            filteredProducts = new List<Products>(allProducts); // 初始化搜尋結果
            ShowProductsForPage(currentPage); // 顯示第一頁
        }

        private void ShowProductsForPage(int page)
        {
            flowLayoutPanelProducts.Controls.Clear(); // 清空顯示的商品

            int startIndex = (page - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, filteredProducts.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                var product = filteredProducts[i]; // 🔹 使用 `filteredProducts`

                // ✅ 設定圓角面板
                RoundedPanel productPanel = new RoundedPanel()
                {
                    Size = new Size(200, 270),
                    BorderStyle = BorderStyle.None,
                    Padding = new Padding(10),
                    BackColor = Color.FromArgb(213, 234, 246),
                    CornerRadius = 20,
                };

                // ✅ 商品圖片
                string fullImagePath = Path.Combine(@"C:\扭蛋圖片\封面圖", product.P_Image);
                PictureBox pictureBox = new PictureBox()
                {
                    Size = new Size(200, 200),
                    Location = new Point(0, 0),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    ImageLocation = File.Exists(fullImagePath) ? fullImagePath : "C:\\Images\\default.png",
                    Cursor = Cursors.Hand
                };

                // **🛠 新增點擊事件**
                pictureBox.Click += (sender, e) => OpenSubProducts(product.P_Name);

                // ✅ 商品名稱
                Label lblName = new Label()
                {
                    Text = product.P_Name,
                    AutoSize = false,
                    Size = new Size(200, 37),
                    Location = new Point(0, 200),
                    Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // ✅ 商品價格 (左側)
                Label lblPoints = new Label()
                {
                    Text = $"G {product.P_Points}",
                    AutoSize = false,
                    Size = new Size(100, 30),
                    Location = new Point(0, 235),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(226, 102, 117),
                };

                // ✅ 商品分類 (右側)
                Label lblClass = new Label()
                {
                    Text = product.P_Class,
                    AutoSize = false,
                    Size = new Size(100, 30),
                    Location = new Point(lblPoints.Right + 50, lblPoints.Top),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(226, 102, 117),
                };

                // ✅ 加入 `RoundedPanel`
                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(lblPoints);
                productPanel.Controls.Add(lblClass);

                // ✅ 加入 `flowLayoutPanel`
                flowLayoutPanelProducts.Controls.Add(productPanel);
            }

            UpdatePaginationButtons(); // 更新分頁按鈕
        }


        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ShowProductsForPage(currentPage);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)allProducts.Count / itemsPerPage);
            if (currentPage < totalPages)
            {
                currentPage++;
                ShowProductsForPage(currentPage);
            }
        }

        private void UpdatePaginationButtons()
        {
            int totalPages = (int)Math.Ceiling((double)allProducts.Count / itemsPerPage);
            btnPreviousPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
        }

        private SubProductsForm subProductsForm = null;
        private void OpenSubProducts(string productName)
        {
            if (this.MdiParent == null)
            {
                MessageBox.Show("錯誤：FormHome 沒有 MDI 容器", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1 mdiContainer = (Form1)this.MdiParent; // ✅ 確保 Form1 是 MDI 容器

            if (subProductsForm == null || subProductsForm.IsDisposed)
            {
                subProductsForm = new SubProductsForm(productName, _userId)
                {
                    MdiParent = mdiContainer, // ✅ `SubProductsForm` 也開啟在 `Form1`
                    Dock = DockStyle.Fill
                };
                subProductsForm.FormClosed += SubProductsForm_FormClosed;
                subProductsForm.Show();
            }
            else
            {
                subProductsForm.Activate();
            }
        }

        // `SubProductsForm` 關閉時重設變數
        private void SubProductsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            subProductsForm = null;
        }

        private void TxtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text.Trim(); // 取得輸入的搜尋關鍵字

            if (string.IsNullOrEmpty(keyword))
            {
                filteredProducts = new List<Products>(allProducts); // 🔹 沒輸入時，顯示所有商品
            }
            else
            {
                // 🔹 即時篩選符合的商品 (忽略大小寫)
                filteredProducts = allProducts
                    .Where(p => p.P_Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            ShowProductsForPage(1); // 🔹 重新顯示商品 (顯示第 1 頁)
        }

        private async void FormHome_Load(object sender, EventArgs e)
        {
            txtSearchProduct.TextChanged += TxtSearchProduct_TextChanged; // ✅ 監聽輸入框變更
            await LoadProductsAsync(); // ✅ 載入商品
            this.ControlBox = false;
        }

    }
}
