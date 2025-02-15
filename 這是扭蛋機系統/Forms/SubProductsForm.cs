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
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.SubProductsService;
using 這是扭蛋機系統.Services.PointsHistoryService;
using 這是扭蛋機系統.Services.RegisterServices;

namespace 這是扭蛋機系統.Forms
{
    public partial class SubProductsForm : Form
    {
        private int _userId; // ✅ 存儲使用者 ID
        private string productName;

        private readonly RegisterService _registerService;
        private readonly PointsHistoryService _pointsHistoryService;
        private readonly ProductsService _productsService;
        private readonly SubProductsService _subProductsService;
        private List<string> imagePaths = new List<string>();
        private int currentImageIndex = 0;
        private bool coverImageAdded = false;
        private Random random = new Random();
        private List<SubProducts> subProducts;
        private int productId;

        public SubProductsForm(string productName, int userId)
        {
            InitializeComponent();
            this._userId = userId; // ✅ 設定 UserID
            this.productName = productName;
            _productsService = new ProductsService();
            _subProductsService = new SubProductsService();
            _registerService = new RegisterService(); // ✅ 確保這行存在
            _pointsHistoryService = new PointsHistoryService();
            this.Load += async (s, e) => await SubProductsForm_LoadAsync();
        }

        private async Task SubProductsForm_LoadAsync()
        {
            await LoadProductDetailsAsync();
            await LoadSubProductImagesAsync();
            DisplayImage();
        }

        private async Task LoadProductDetailsAsync()
        {
            var product = await _productsService.GetProductByNameAsync(productName);
            if (product != null)
            {
                productId = product.P_ID; // ✅ 先設定 productId
                this.productName = product.P_Name; // ✅ 確保 `productName` 來自 API
                lblProductName.Text = product.P_Name;
                lblPoints.Text = $"G {product.P_Points}";

                string coverImagePath = Path.Combine(@"C:\扭蛋圖片\封面圖", product.P_Image);

                if (File.Exists(coverImagePath) && !coverImageAdded)
                {
                    pbMainImage.Image = Image.FromFile(coverImagePath);
                    imagePaths.Insert(0, coverImagePath);

                    PictureBox pbCoverImage = new PictureBox()
                    {
                        Size = new Size(50, 50),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(coverImagePath),
                        Cursor = Cursors.Hand,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    pbCoverImage.Click += (s, e) => pbMainImage.Image = pbCoverImage.Image;
                    flowLayoutPanelSubProductImages.Controls.Add(pbCoverImage);
                    coverImageAdded = true;
                }

                // ✅ 獲取小商品清單
                subProducts = await _subProductsService.GetSubProductsByProductIdAsync(product.P_ID);
                if (subProducts != null && subProducts.Count > 0)
                {
                    DisplaySubProductList(subProducts); // ✅ 顯示所有小商品
                }
            }
            else
            {
                MessageBox.Show("找不到此商品", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DisplaySubProductList(List<SubProducts> subProducts)
        {
            flowLayoutPanelSubProduct.Controls.Clear();
            int totalStock = subProducts.Sum(p => p.SP_Amount);

            foreach (var subProduct in subProducts)
            {
                double probability = totalStock > 0 ? (double)subProduct.SP_Amount / totalStock * 100 : 0;

                Label lblSubProduct = new Label()
                {
                    Text = $"{ExtractVariant(subProduct.SP_Name)}           {probability:F2}%",
                    AutoSize = false,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 9 , FontStyle.Bold),
                };

                Label stockIndicator = new Label()
                {
                    Text = subProduct.SP_Amount > 0 ? "○" : "✖ ",
                    ForeColor = subProduct.SP_Amount > 0 ? Color.Green : Color.Red,
                    AutoSize = false,
                };

                Panel panel = new Panel()
                {
                    Size = new Size(250, 20), // ✅ 減少 Panel 的寬度和高度
                    BorderStyle = BorderStyle.None,
                };

                panel.Controls.Add(lblSubProduct);
                lblSubProduct.Location = new Point(65, 5);
                panel.Controls.Add(stockIndicator);
                stockIndicator.Location = new Point(190, 5);

                flowLayoutPanelSubProduct.Controls.Add(panel);
            }
        }

        private string ExtractVariant(string productName)
        {
            int startIndex = productName.LastIndexOf('(');
            int endIndex = productName.LastIndexOf(')');
            if (startIndex >= 0 && endIndex > startIndex)
            {
                return productName.Substring(startIndex, endIndex - startIndex + 1); // ✅ 只取括號內文字
            }
            return productName;
        }

        private async Task LoadSubProductImagesAsync()
        {
            var product = await _productsService.GetProductByNameAsync(productName);
            if (product == null) return;

            var subProducts = await _subProductsService.GetSubProductsByProductIdAsync(product.P_ID);

            foreach (var subProduct in subProducts)
            {
                string imagePath = Path.Combine(@"C:\扭蛋圖片", product.P_Name, subProduct.SP_Image);
                if (File.Exists(imagePath))
                {
                    PictureBox pbSubImage = new PictureBox()
                    {
                        Size = new Size(50, 50),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(imagePath),
                        Cursor = Cursors.Hand,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    pbSubImage.Click += (s, e) => pbMainImage.Image = pbSubImage.Image;
                    flowLayoutPanelSubProductImages.Controls.Add(pbSubImage);
                    imagePaths.Add(imagePath);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % imagePaths.Count;
                DisplayImage();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count > 0)
            {
                currentImageIndex = (currentImageIndex - 1 + imagePaths.Count) % imagePaths.Count;
                DisplayImage();
            }
        }

        private void DisplayImage()
        {
            if (imagePaths.Count > 0)
            {
                pbMainImage.Image = Image.FromFile(imagePaths[currentImageIndex]);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnDraw_Click(object sender, EventArgs e)
        {
            if (productId == 0)
            {
                MessageBox.Show("未能獲取商品ID，請重試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int drawCount = (int)numericDrawCount.Value;
            if (drawCount < 1 || drawCount > 10)
            {
                MessageBox.Show("請選擇 1 到 10 次的抽取數量", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 獲取會員當前 G 幣
            var member = await _registerService.GetMemberByIdAsync(_userId);
            if (member == null)
            {
                MessageBox.Show("會員資料異常，請重新登入", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 🔹 獲取當前商品資訊
            var product = await _productsService.GetProductByIdAsync(productId);
            if (product == null)
            {
                MessageBox.Show("無法獲取商品資訊，請重試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productPrice = product.P_Points; // ✅ 設定商品價格
            int totalCost = drawCount * productPrice; // 🔹 計算總花費

            if (member.Points < totalCost)
            {
                MessageBox.Show($"您的 G 幣不足，抽取 {drawCount} 次需要 {totalCost} G 幣", "餘額不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 獲取小商品清單
            subProducts = await _subProductsService.GetSubProductsByProductIdAsync(productId);
            if (subProducts == null || subProducts.Count == 0)
            {
                MessageBox.Show("無法獲取小商品資料，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 抽取小商品
            List<SubProducts> drawnItems = DrawRandomSubProducts(drawCount);
            if (drawnItems == null || drawnItems.Count == 0)
            {
                MessageBox.Show("無法抽取商品，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 更新小商品庫存
            foreach (var item in drawnItems)
            {
                if (item == null) continue;

                bool success = await _subProductsService.UpdateSubProductAmountAsync(item.SP_ID, item.SP_Amount - 1);
                if (success)
                {
                    item.SP_Amount -= 1;
                }
            }

            // 🔹 **扣除會員點數**
            bool pointsDeducted = await _pointsHistoryService.DeductPointsAsync(_userId, totalCost);
            if (!pointsDeducted)
            {
                MessageBox.Show("點數扣除失敗，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 🔹 **新增抽取紀錄到 `DrawnItemsHistory`**
            foreach (var item in drawnItems)
            {
                bool historyAdded = await _pointsHistoryService.AddDrawnItemHistoryAsync(
                    _userId, productId, item.SP_ID, item.SP_Name, productPrice
                );

                if (!historyAdded)
                {
                    MessageBox.Show($"抽取紀錄新增失敗: {item.SP_Name}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // ✅ **自動刷新抽取紀錄**
            FormGashaponLog formGashaponLog = Application.OpenForms.OfType<FormGashaponLog>().FirstOrDefault();
            if (formGashaponLog != null)
            {
                await formGashaponLog.RefreshTopUpDrawnItems(); // ✅ 呼叫 `RefreshTopUpHistory()`
            }

            // ✅ **處理小商品名稱**
            List<string> formattedItems = drawnItems.Select(item =>
            {
                string variant = ExtractVariantSubProductName(item.SP_Name);
                return $"{product.P_Name}{variant}"; // ✅ 組合成 `商品名稱 (變體名稱)`
            }).ToList();

            // 🔹 更新 UI
            await LoadMemberDataAsync(); // ✅ 重新載入會員資訊
            MessageBox.Show($"抽取成功！您獲得了：\n{string.Join("\n", formattedItems)}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // ✅ **重新獲取會員最新 G 幣**
        private async Task LoadMemberDataAsync()
        {
            var member = await _registerService.GetMemberByIdAsync(_userId);

            if (member != null)
            {
                if (lblPoints != null)
                {
                    lblPoints.Text = $"G {member.Points}"; // ✅ 確保 `lblPoints` 不為 `null`
                }
            }
            else
            {
                MessageBox.Show("無法獲取會員資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ✅ **這個方法用來提取變體部分，例如 `(A)`**
        private string ExtractVariantSubProductName(string spName)
        {
            int startIndex = spName.IndexOf('(');
            int endIndex = spName.IndexOf(')');

            if (startIndex != -1 && endIndex > startIndex)
            {
                return spName.Substring(startIndex, endIndex - startIndex + 1); // ✅ 確保只取括號內的內容
            }

            return ""; // ✅ 如果沒有括號，回傳空字串
        }
        
        private List<SubProducts> DrawRandomSubProducts(int count)
        {
            List<SubProducts> result = new List<SubProducts>();
            if (subProducts == null || subProducts.Count == 0) return result;

            // **重新計算總庫存**
            int totalStock = subProducts.Sum(p => p.SP_Amount);
            if (totalStock == 0) return result;

            for (int i = 0; i < count; i++)
            {
                int roll = random.Next(totalStock);
                int cumulative = 0;

                foreach (var subProduct in subProducts)
                {
                    if (subProduct.SP_Amount > 0) // **確保有庫存**
                    {
                        cumulative += subProduct.SP_Amount;
                        if (roll < cumulative)
                        {
                            result.Add(subProduct);
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private async void btnAddToCart_Click(object sender, EventArgs e)
        {
            var product = await _productsService.GetProductByNameAsync(productName);
            int quantity = (int)numericQuantity.Value;

            if (quantity > 0)
            {
                string imagePath = Path.Combine(@"C:\扭蛋圖片\封面圖", product.P_Image);
                if (!File.Exists(imagePath))
                {
                    imagePath = @"C:\扭蛋圖片\封面圖\default.png"; // ✅ 預設圖片
                }

                // ✅ 取得 `FormCart`，但不開啟
                FormCart cartForm = Application.OpenForms.OfType<FormCart>().FirstOrDefault();
                if (cartForm == null)
                {
                    cartForm = new FormCart(_userId);
                    cartForm.MdiParent = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                    cartForm.Dock = DockStyle.Fill;
                    cartForm.Show();
                }

                // ✅ 加入商品到購物車
                cartForm.AddToCart(new CartItem
                {
                    ProductID = product.P_ID,
                    ProductName = product.P_Name,
                    ImagePath = imagePath,
                    Quantity = quantity,
                    Price = int.Parse(lblPoints.Text.Replace("G ", ""))
                });

                MessageBox.Show($"已將 {product.P_Name} ({quantity} 個) 加入購物車！", "加入購物車", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("請選擇至少一個數量！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
