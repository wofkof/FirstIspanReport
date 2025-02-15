using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.SubProductsService;

namespace 這是扭蛋機系統.Forms
{
    public partial class SubProductsForm : Form
    {
        private int _userId; // ✅ 存儲使用者 ID
        private string productName;

      
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
        //抽取按鈕
        private async void btnDraw_Click(object sender, EventArgs e)
        {
            if (productId == 0) // 🚨 確保 productId 有值
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

            subProducts = await _subProductsService.GetSubProductsByProductIdAsync(productId); // ✅ 修正 productId 紅字問題

            List<SubProducts> drawnItems = DrawRandomSubProducts(drawCount);
            foreach (var item in drawnItems)
            {
                bool success = await _subProductsService.UpdateSubProductAmountAsync(item.SP_ID, item.SP_Amount - 1);
                if (success)
                {
                    item.SP_Amount -= 1;
                }
            }
            MessageBox.Show("抽取成功！請查看您獲得的商品。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            int quantity = (int)numericQuantity.Value;
            if (quantity > 0)
            {
                MessageBox.Show($"已將 {productName} ({quantity} 個) 加入購物車！", "加入購物車", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ✅ 這裡可以加入購物車邏輯，例如存入資料庫或變數
            }
            else
            {
                MessageBox.Show("請選擇至少一個數量！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
