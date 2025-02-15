using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Helpers;
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.RegisterServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormProductManage : Form
    {
        private readonly RegisterService _registerServicel;
        private readonly ProductsService _productsService;
        private FormMover formMover;
        private int selectedProductId = -1; // 🔹 **選中的商品 ID**
        private string selectedImagePath = ""; // 🔹 **選中的圖片路徑**


        private int _userId;
        private int _roleId;

        public FormProductManage()
        {
            InitializeComponent();
            _productsService = new ProductsService();
            LoadProducts();
            ClockHelper.StartClock(lblTime);
            formMover = new FormMover(this);
            formMover.Attach(panel8);
            txtSearchProduct.TextChanged += async (s, e) => await SearchProducts(txtSearchProduct.Text);
            SetupCategoryPanels();


            PanelLabelButtonHelper.ApplyEffect(roundedPanelAllProducts, label12,
               Color.White, Color.Black,   // 正常狀態
               Color.LightYellow, Color.Black    // 懸停 
               );

            PanelLabelButtonHelper.ApplyEffect(roundedPanelCute, label6,
                 Color.White, Color.Black,   // 正常狀態
               Color.LightPink, Color.Black    // 懸停 
            );

            PanelLabelButtonHelper.ApplyEffect(roundedPanelAnimal, label8,
                 Color.White, Color.Black,   // 正常狀態
               Color.LightBlue, Color.Black    // 懸停 
            );

            PanelLabelButtonHelper.ApplyEffect(roundedPanelFood, label9,
               Color.White, Color.Black,   // 正常狀態
               Color.LightSalmon, Color.Black    // 懸停 
           );

            PanelLabelButtonHelper.ApplyEffect(roundedPanelAnime, label10,
                Color.White, Color.Black,   // 正常狀態
               Color.MistyRose, Color.Black    // 懸停 
           );

            PanelLabelButtonHelper.ApplyEffect(roundedPanelOther, label11,
                Color.White, Color.Black,   // 正常狀態
               Color.LightGray, Color.Black    // 懸停 
           );
        }

        //搜尋功能
        private async Task SearchProducts(string keyword)
        {
            var allProducts = await _productsService.GetAllProductsAsync();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                DisplayProducts(allProducts.ToList()); // 沒輸入則顯示全部商品
                return;
            }

            // 🔍 進行 **模糊比對**（不區分大小寫）
            var filteredProducts = allProducts
                .Where(p => p.P_Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            DisplayProducts(filteredProducts);
        }

        private void FormProductManage_Load(object sender, EventArgs e)
        {
            cbCategory.Items.AddRange(new object[] { $"可愛類", "動物類", "食物類", "動漫類", "其他類" });
            cbCategory.SelectedIndex = 4;
        }
        private async void LoadProducts()
        {
            var products = await _productsService.GetAllProductsAsync();

            if (products == null || !products.Any()) // ✅ 檢查是否有商品
            {
                MessageBox.Show("目前沒有商品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DisplayProducts(products.ToList()); // ✅ 顯示商品
        }
        private void DisplayProducts(List<Products> products)
        {
            flowLayoutPanelProducts.Controls.Clear(); // ✅ 清空當前顯示的商品

            foreach (var product in products)
            {
                // ✅ 使用 `RoundedPanel` 讓商品顯示更美觀
                RoundedPanel productPanel = new RoundedPanel()
                {
                    Size = new Size(200, 270),
                    BorderStyle = BorderStyle.None,
                    Padding = new Padding(10),
                    BackColor = Color.FromArgb(213, 234, 246),
                    CornerRadius = 20, // ✅ 設定圓角
                    Margin = new Padding(10),
                    Tag = product.P_ID // **儲存商品 ID**
                };

                // ✅ 加入到 `FormProductManage`
                this.Controls.Add(txtSearchProduct);


                // ✅ 商品圖片
                string fullImagePath = Path.Combine(@"C:\扭蛋圖片\封面圖", product.P_Image);
                PictureBox pbProductImage = new PictureBox()
                {
                    Size = new Size(200, 200),
                    Location = new Point(0, 0),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = File.Exists(fullImagePath) ? Image.FromFile(fullImagePath) : Image.FromFile(@"C:\扭蛋圖片\封面圖\default.png"),
                    Cursor = Cursors.Hand
                };

                // ✅ 商品名稱
                Label lblProductName = new Label()
                {
                    Text = product.P_Name,
                    AutoSize = false,
                    Size = new Size(180, 40),
                    Location = new Point(10, 200),
                    Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // ✅ 商品價格
                Label lblPrice = new Label()
                {
                    Text = $"G {product.P_Points}",
                    AutoSize = false,
                    Size = new Size(180, 30),
                    Location = new Point(10, 240),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(226, 102, 117),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // ✅ 點擊圖片刪除商品
                pbProductImage.Click += async (sender, e) =>
                {
                    selectedProductId = product.P_ID;
                    txtProductName.Text = product.P_Name;
                    txtPrice.Text = product.P_Points.ToString();
                    cbCategory.SelectedItem = product.P_Class;
                    selectedImagePath = fullImagePath;
                    pbProductPreview.Image = pbProductImage.Image; // ✅ 預覽圖片

                    MessageBox.Show($"已選擇：{product.P_Name}", "選擇商品", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                productPanel.Controls.Add(pbProductImage);
                productPanel.Controls.Add(lblProductName);
                productPanel.Controls.Add(lblPrice);
                flowLayoutPanelProducts.Controls.Add(productPanel);
            }
        }

        // ✅ **選擇圖片**

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "選擇商品圖片",
                Filter = "圖片文件 (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName; // ✅ 確保變數不會消失
                pbProductPreview.Image = Image.FromFile(selectedImagePath);
            }
            else
            {
                MessageBox.Show("請選擇商品圖片！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private async void ShowCategoryProducts(string category)
        {
            var products = await _productsService.GetProductsByCategoryAsync(category);

            var productList = products.ToList();
            if (productList == null || productList.Count == 0)
            {
                MessageBox.Show("目前沒有商品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            flowLayoutPanelProducts.Controls.Clear(); // 清除當前顯示的商品

            foreach (var product in products)
            {
                // 創建商品的圓角 Panel
                RoundedPanel productPanel = new RoundedPanel()
                {
                    Size = new Size(200, 270),
                    BorderStyle = BorderStyle.None,
                    Padding = new Padding(10),
                    BackColor = Color.FromArgb(213, 234, 246),
                    CornerRadius = 20, // ✅ 設定圓角
                    Margin = new Padding(10),
                    Tag = product.P_ID // **儲存商品 ID**
                };

                string fullImagePath = Path.Combine(@"C:\扭蛋圖片\封面圖", product.P_Image);
                // 商品圖片
                PictureBox pictureBox = new PictureBox()
                {
                    Size = new Size(200, 200),
                    Location = new Point(0, 0),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = File.Exists(fullImagePath) ? Image.FromFile(fullImagePath) : Image.FromFile(@"C:\扭蛋圖片\封面圖\default.png"),
                    Cursor = Cursors.Hand
                };

                // 商品名稱
                Label lblName = new Label()
                {
                    Text = product.P_Name,
                    AutoSize = false,
                    Size = new Size(180, 40),
                    Location = new Point(10, 200),
                    Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // ✅ 商品價格
                Label lblPrice = new Label()
                {
                    Text = $"G {product.P_Points}",
                    AutoSize = false,
                    Size = new Size(180, 30),
                    Location = new Point(10, 240),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(226, 102, 117),
                    TextAlign = ContentAlignment.MiddleCenter
                };


                // 加入元件到 Panel
                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(lblPrice);

                // ✅ 點擊圖片刪除商品
                pictureBox.Click += async (sender, e) =>
                {
                    selectedProductId = product.P_ID;
                    MessageBox.Show($"已選擇：{product.P_Name}", "選擇商品", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDeleteProduct.Text = ($"確定刪除 : \n{product.P_Name}");
                };

                // 加入 Panel 到 FlowLayoutPanel
                flowLayoutPanelProducts.Controls.Add(productPanel);
            }
        }

        private async void ShowAllProducts()
        {
            var products = await _productsService.GetAllProductsAsync();

            if (products == null || !products.Any())
            {
                MessageBox.Show("目前沒有任何商品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DisplayProducts(products.ToList()); // ✅ 顯示所有商品
        }

        private void SetupCategoryPanels()
        {
            roundedPanelCute.Click += (s, e) => ShowCategoryProducts("可愛類");
            roundedPanelAnimal.Click += (s, e) => ShowCategoryProducts("動物類");
            roundedPanelFood.Click += (s, e) => ShowCategoryProducts("食物類");
            roundedPanelAnime.Click += (s, e) => ShowCategoryProducts("動漫類");
            roundedPanelOther.Click += (s, e) => ShowCategoryProducts("其他類");
            label6.Click += (s, e) => ShowCategoryProducts("可愛類");
            label8.Click += (s, e) => ShowCategoryProducts("動物類");
            label9.Click += (s, e) => ShowCategoryProducts("食物類");
            label10.Click += (s, e) => ShowCategoryProducts("動漫類");
            label11.Click += (s, e) => ShowCategoryProducts("其他類");

            roundedPanelAllProducts.Click += (s, e) => ShowAllProducts(); // ✅ 顯示全部商品
            label12.Click += (s, e) => ShowAllProducts();
        }

        private async void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("請先選擇要修改的商品！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newName = txtProductName.Text.Trim();
            if (!int.TryParse(txtPrice.Text, out int newPrice))
            {
                MessageBox.Show("請輸入有效的價格！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newCategory = cbCategory.SelectedItem.ToString();
            string imageFileName = string.IsNullOrEmpty(selectedImagePath) ? null : Path.GetFileName(selectedImagePath);

            // ✅ 確保 `selectedProductId` 正確
            MessageBox.Show($"選擇的商品 ID: {selectedProductId}");

            // ✅ 建立 `Products` 物件
            Products updatedProduct = new Products
            {
                P_ID = selectedProductId,
                P_Name = newName,
                P_Points = newPrice,
                P_Class = newCategory,
                P_Image = imageFileName
            };

            // ✅ 顯示 `updatedProduct` 確保沒有錯誤
            MessageBox.Show($"更新商品:\nID: {updatedProduct.P_ID}\n名稱: {updatedProduct.P_Name}\n價格: {updatedProduct.P_Points}\n分類: {updatedProduct.P_Class}\n圖片: {updatedProduct.P_Image}",
                "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // ✅ 呼叫 `UpdateProductAsync`
            bool success = await _productsService.UpdateProductAsync(selectedProductId, updatedProduct);

            if (success)
            {
                MessageBox.Show("商品更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
            }
            else
            {
                MessageBox.Show("更新失敗！請檢查 API 設定", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // 🔹 新增商品
        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;

            if (!int.TryParse(txtPrice.Text, out int price))
            {
                MessageBox.Show("請輸入有效的價格！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string category = cbCategory.SelectedItem.ToString();

            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("請先選擇商品圖片！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = await _productsService.AddProductAsync(name, price, category, selectedImagePath);
            if (success)
            {
                MessageBox.Show("商品新增成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts(); // ✅ 重新載入商品

                // 🔹 清空 `selectedImagePath`
                selectedImagePath = "";
                pbProductPreview.Image = null;
            }
            else
            {
                MessageBox.Show("新增失敗！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 🔹 刪除商品
        private async void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("請先選擇要刪除的商品！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"確定要刪除商品 {txtProductName.Text} 嗎？", "刪除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool success = await _productsService.DeleteProductAsync(selectedProductId);
                if (success)
                {
                    MessageBox.Show("商品刪除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("刪除失敗！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FormManageSubProducts formManageSubProducts = new FormManageSubProducts();
            formManageSubProducts.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
