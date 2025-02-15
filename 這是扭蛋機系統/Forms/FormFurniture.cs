using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Forms;
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.SubProductsService;
using System.Data;
using static 這是扭蛋機系統.FormHome;

namespace 這是扭蛋機系統
{
    public partial class FormFurniture : Form
    {
        private readonly ProductsService _productsService;
        private int currentPage = 1;
        private int itemsPerPage = 8;
        private List<Products> animeProducts = new List<Products>();
        private int _userId;
        public FormFurniture(int userId)
        {
            InitializeComponent();
            _productsService = new ProductsService();
            this.Load += async (s, e) => await LoadFurnitureProductsAsync();
            this._userId = userId; // ✅ 設定 UserID
        }

        private async Task LoadFurnitureProductsAsync()
        {
            var products = await _productsService.GetAllProductsAsync();
            animeProducts = products.Where(p => p.P_Class == "食物類").ToList();
            ShowProductsForPage(currentPage);
        }

        private void ShowProductsForPage(int page)
        {
            flowLayoutPanelFurniturelProducts.Controls.Clear();

            int startIndex = (page - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, animeProducts.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                var product = animeProducts[i];

                RoundedPanel productPanel = new RoundedPanel()
                {
                    Size = new Size(200, 270),
                    BorderStyle = BorderStyle.None,
                    Padding = new Padding(10),
                    BackColor = Color.FromArgb(213, 234, 246),
                    CornerRadius = 20,
                };

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

                string fullImagePath = Path.Combine(@"C:\扭蛋圖片\封面圖", product.P_Image);
                PictureBox pictureBox = new PictureBox()
                {
                    Size = new Size(200, 200),
                    Location = new Point(0, 0),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    ImageLocation = File.Exists(fullImagePath) ? fullImagePath : "C:\\Images\\default.png",
                    Cursor = Cursors.Hand
                };

                pictureBox.Click += (sender, e) => OpenSubProducts(product.P_Name);

                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(lblPoints);
                productPanel.Controls.Add(lblClass);

                flowLayoutPanelFurniturelProducts.Controls.Add(productPanel);
            }
            UpdatePaginationButtons();
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
            int totalPages = (int)Math.Ceiling((double)animeProducts.Count / itemsPerPage);
            if (currentPage < totalPages)
            {
                currentPage++;
                ShowProductsForPage(currentPage);
            }
        }

        private void UpdatePaginationButtons()
        {
            int totalPages = (int)Math.Ceiling((double)animeProducts.Count / itemsPerPage);
            btnPreviousPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
        }

        private void SubProductsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            subProductsForm = null;
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
    }
}
