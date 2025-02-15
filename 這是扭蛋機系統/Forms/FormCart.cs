using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using 這是扭蛋機系統.Services.PointsHistoryService;
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.RegisterServices;
using 這是扭蛋機系統.Services.SubProductsService;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormCart : Form
    {
        private List<CartItem> cartItems = new List<CartItem>(); //  存放購物車商品
        private readonly ProductsService _productsService;
        private readonly SubProductsService _subProductsService;
        private readonly PointsHistoryService _pointsHistoryService;
        private readonly RegisterService _registerService;
        private Random random = new Random();
        private int _userId;
        private int _memberPoints = 0; // ✅ 存會員的 G 幣

        public FormCart(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _productsService = new ProductsService();
            _subProductsService = new SubProductsService();
            _pointsHistoryService = new PointsHistoryService();
            _registerService = new RegisterService();
            this.MdiParent = Application.OpenForms.OfType<Form1>().FirstOrDefault(); // ✅ 設定 MDI
            this.Dock = DockStyle.Fill; // ✅ 讓 `FormCart` 填滿 MDI 容器
            this.Load += async (s, e) => await LoadMemberDataAsync(); // ✅ 當視窗載入時加載會員數據
        }
    

        // ✅ 更新購物車內容
        public void AddToCart(CartItem item)
        {
            var existingItem = cartItems.FirstOrDefault(c => c.ProductID == item.ProductID);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity; // ✅ 增加數量
            }
            else
            {
                cartItems.Add(item); // ✅ 新增商品
            }

            UpdateCartUI(); // ✅ 更新畫面
        }

        // ✅ 更新購物車 UI
        private void UpdateCartUI()
        {
            flowLayoutPanelCart.Controls.Clear(); // 清空購物車 UI

            int totalPrice = 0; // ✅ 計算總金額
            int totalQuantity = 0; // ✅ 計算總商品數量
            int cartTotal = 0; // ✅ 計算購物車總額

            foreach (var item in cartItems)
            {
                // ✅ 商品 Panel（長條型）
                Panel productPanel = new Panel()
                {
                    Size = new Size(flowLayoutPanelCart.Width - 20, 100), // 寬度 = `FlowLayoutPanel` 的寬度
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5),
                    BackColor = Color.White
                };

                // ✅ 商品圖片
                PictureBox pbProductImage = new PictureBox()
                {
                    Size = new Size(80, 80),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = File.Exists(item.ImagePath) ? Image.FromFile(item.ImagePath) : Image.FromFile(@"C:\扭蛋圖片\封面圖\default.png"),
                    Location = new Point(10, 10) // 靠左
                };

                // ✅ 商品名稱
                Label lblProductName = new Label()
                {
                    Text = item.ProductName,
                    AutoSize = true,
                    Font = new Font("微軟正黑體", 10, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    Location = new Point(100, 15) // 位於圖片右側
                };

                // ✅ 單價
                Label lblPrice = new Label()
                {
                    Text = $"G {item.Price}",
                    AutoSize = true,
                    Font = new Font("微軟正黑體", 9),
                    ForeColor = Color.Gray,
                    Location = new Point(330, 40) // 單價顯示在名稱下方
                };

                // ✅ 數量選擇
                NumericUpDown numericQuantity = new NumericUpDown()
                {
                    Value = item.Quantity,
                    Minimum = 1,
                    Maximum = 10,
                    Size = new Size(50, 25),
                    Location = new Point(450, 37) // 位置靠右
                };

                numericQuantity.ValueChanged += (sender, e) =>
                {
                    item.Quantity = (int)numericQuantity.Value;
                    UpdateCartUI(); // 更新購物車
                };

                int itemTotal = item.Price * item.Quantity; // **計算單項商品小計**
                cartTotal += itemTotal; // **累加購物車總金額**

                // ✅ 小計
                Label lblTotal = new Label()
                {
                    Text = $"G {item.Price * item.Quantity}",
                    AutoSize = true,
                    Font = new Font("微軟正黑體", 9),
                    ForeColor = Color.Red,
                    Location = new Point(550, 40) // 位置調整
                };

                // ✅ 刪除按鈕
                Button btnRemove = new Button()
                {
                    Text = "🗑",
                    Size = new Size(30, 30),
                    Location = new Point(700, 30),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Red,
                    ForeColor = Color.White
                };

                btnRemove.Click += (sender, e) =>
                {
                    cartItems.Remove(item);
                    UpdateCartUI(); // 移除後更新 UI
                };

                // ✅ 加入元件到 Panel
                productPanel.Controls.Add(pbProductImage);
                productPanel.Controls.Add(lblProductName);
                productPanel.Controls.Add(lblPrice);
                productPanel.Controls.Add(numericQuantity);
                productPanel.Controls.Add(lblTotal);
                productPanel.Controls.Add(btnRemove);

                // ✅ **累加總金額與數量**
                totalPrice += item.Price * item.Quantity;
                totalQuantity += item.Quantity;

                // ✅ 加入 Panel 到購物車畫面
                flowLayoutPanelCart.Controls.Add(productPanel);
            }
            // ✅ **更新剩餘 G 幣**
            UpdateRemainingPoints(cartTotal);
            // ✅ **在迴圈外部更新總金額與總數量**
            label9.Text = $"G {totalPrice}";
            label7.Text = $"{totalQuantity} 個商品";
            // ✅ **更新購物車數量到 `Form1.label6`**
            UpdateCartItemCount(totalQuantity);
        }

        private void UpdateCartItemCount(int totalQuantity)
        {
            // ✅ 找到 `Form1` 並更新 `label6`
            Form1 mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (mainForm != null)
            {
                mainForm.UpdateCartLabel(totalQuantity); // ✅ 更新購物車數量
            }
        }

        private void UpdateRemainingPoints(int cartTotal)
        {
            int remainingPoints = _memberPoints - cartTotal; // ✅ 扣除購物車總額

            // ✅ **即時更新剩餘 G 幣顯示**
            lblRemainingPoints.Text = $"G {remainingPoints}";
        }

        private async void btnDraw_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("購物車內沒有商品，請先加入商品！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ 獲取會員當前 G 幣
            var member = await _registerService.GetMemberByIdAsync(_userId);
            if (member == null)
            {
                MessageBox.Show("會員資料異常，請重新登入", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int totalCost = cartItems.Sum(item => item.Price * item.Quantity); // ✅ 計算總花費

            if (member.Points < totalCost)
            {
                MessageBox.Show($"您的 G 幣不足，總共需要 {totalCost} G 幣", "餘額不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> drawnResults = new List<string>(); // ✅ 儲存抽取結果

            foreach (var cartItem in cartItems)
            {
                var product = await _productsService.GetProductByIdAsync(cartItem.ProductID);
                if (product == null)
                {
                    MessageBox.Show($"找不到商品：{cartItem.ProductName}，請重試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                // ✅ 獲取該商品的小商品清單
                var subProducts = await _subProductsService.GetSubProductsByProductIdAsync(cartItem.ProductID);
                if (subProducts == null || subProducts.Count == 0)
                {
                    MessageBox.Show($"無法獲取 {cartItem.ProductName} 的小商品，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                List<SubProducts> drawnItems = DrawRandomSubProducts(cartItem.Quantity, subProducts); // ✅ 抽取商品
                if (drawnItems == null || drawnItems.Count == 0)
                {
                    MessageBox.Show($"無法抽取 {cartItem.ProductName}，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                // ✅ 更新小商品庫存
                foreach (var item in drawnItems)
                {
                    bool success = await _subProductsService.UpdateSubProductAmountAsync(item.SP_ID, item.SP_Amount - 1);
                    if (success)
                    {
                        item.SP_Amount -= 1;
                    }
                }

                // ✅ **扣除會員點數**
                bool pointsDeducted = await _pointsHistoryService.DeductPointsAsync(_userId, cartItem.Price * cartItem.Quantity);
                if (!pointsDeducted)
                {
                    MessageBox.Show($"點數扣除失敗，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ **新增抽取紀錄到 `DrawnItemsHistory`**
                foreach (var item in drawnItems)
                {
                    bool historyAdded = await _pointsHistoryService.AddDrawnItemHistoryAsync(
                        _userId, cartItem.ProductID, item.SP_ID, item.SP_Name, cartItem.Price
                    );

                    if (!historyAdded)
                    {
                        MessageBox.Show($"抽取紀錄新增失敗: {item.SP_Name}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                

                // ✅ **格式化抽取結果**
                List<string> formattedItems = drawnItems.Select(item =>
                {
                    string variant = ExtractVariantSubProductName(item.SP_Name);
                    return $"{product.P_Name}{variant}"; // ✅ 組合成 `商品名稱 (變體名稱)`
                }).ToList();

                drawnResults.AddRange(formattedItems);
            }

            // ✅ **清空購物車**
            cartItems.Clear();
            UpdateCartUI();

            // ✅ **更新會員 G 幣**
            await LoadMemberDataAsync();
            // ✅ **自動刷新抽取紀錄**
            FormGashaponLog formGashaponLog = Application.OpenForms.OfType<FormGashaponLog>().FirstOrDefault();
            if (formGashaponLog != null)
            {
                await formGashaponLog.RefreshTopUpDrawnItems(); // ✅ 呼叫 `RefreshTopUpHistory()`
            }

            Form1 formMain = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (formMain != null)
            {
                await formMain.RefreshUserPoints(); // 🔹 直接呼叫更新方法
            }

            // ✅ **顯示抽取結果**
            MessageBox.Show($"抽取成功！您獲得了：\n{string.Join("\n", drawnResults)}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private async Task LoadMemberDataAsync()
        {
            var member = await _registerService.GetMemberByIdAsync(_userId);

            if (member != null)
            {
                _memberPoints = member.Points; // ✅ 存會員 G 幣
                lblMemberPoints.Text = $"G {member.Points}"; // ✅ 更新購物車內的會員 G 幣顯示
                UpdateRemainingPoints(); // ✅ 計算扣除購物車後剩餘 G 幣
            }
            else
            {
                MessageBox.Show("無法獲取會員資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRemainingPoints()
        {
            int cartTotal = cartItems.Sum(item => item.Price * item.Quantity); // ✅ 計算購物車總額
            int remainingPoints = _memberPoints - cartTotal; // ✅ 扣除後的 G 幣
            lblRemainingPoints.Text = $"G {remainingPoints}";
        }
        private List<SubProducts> DrawRandomSubProducts(int count, List<SubProducts> subProducts)
        {
            List<SubProducts> result = new List<SubProducts>();
            if (subProducts == null || subProducts.Count == 0) return result;

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

        string ExtractVariantSubProductName(string spName)
        {
            int startIndex = spName.IndexOf('(');
            int endIndex = spName.IndexOf(')');

            if (startIndex != -1 && endIndex > startIndex)
            {
                return spName.Substring(startIndex, endIndex - startIndex + 1); // ✅ 確保只取括號內的內容
            }

            return ""; // ✅ 如果沒有括號，回傳空字串
        }
    }
}


        
    

