using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Helpers;
using 這是扭蛋機系統.Services.ProductsService;
using 這是扭蛋機系統.Services.SubProductsService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormManageSubProducts : Form
    {
        private FormMover formMover;

        private readonly ProductsService _productsService;
        private readonly SubProductsService _subProductsService;
        private string selectedImagePath = "";

        public FormManageSubProducts()
        {
            InitializeComponent();
            _productsService = new ProductsService();
            _subProductsService = new SubProductsService();
            this.Load += async (s, e) => await LoadProducts();

            formMover = new FormMover(this);
            formMover.Attach(panel8);

            txtSearchSubProduct.TextChanged += async (s, e) => await SearchSubProducts(txtSearchSubProduct.Text);
        }

        private async Task SearchSubProducts(string keyword)
        {
            var allSubProducts = await _subProductsService.UpdateGetAllSubProductsAsync();

            // 🔍 **進行模糊搜尋（比對 SP_Name 和 P_Name）**
            var filteredSubProducts = string.IsNullOrWhiteSpace(keyword)
                ? allSubProducts
                : allSubProducts.Where(sp =>
                    (sp.SP_Name != null && sp.SP_Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (sp.Product != null && sp.Product.P_Name != null && sp.Product.P_Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                    .ToList();

            // ✅ **確保 `P_Name` 存在**
            var subProductsWithProductName = filteredSubProducts.Select(sp => new
            {
                P_Name = sp.Product != null ? sp.Product.P_Name : "未知商品", // 確保顯示商品名稱
                sp.SP_Name,
                sp.SP_Image,
                sp.SP_Amount,
                sp.SP_ID,
            }).ToList();

            dgvSubProducts.DataSource = subProductsWithProductName;
        }


        // ✅ **載入商品清單**
        private async Task LoadProducts()
        {
            var subproducts = await _subProductsService.UpdateGetAllSubProductsAsync();

            // ✅ 確保 `P_Name` 存在
            var subProductsWithProductName = subproducts.Select(sp => new
            {
                P_Name = sp.Product != null ? sp.Product.P_Name : "未知商品", // ✅ 確保有值
                sp.SP_Name,
                sp.SP_Image,
                sp.SP_Amount,
                sp.SP_ID,
            }).ToList();

            dgvSubProducts.DataSource = subProductsWithProductName;

            var products = await _productsService.GetAllProductsAsync();
            cbProducts.DataSource = products;
            cbProducts.DisplayMember = "P_Name";
            cbProducts.ValueMember = "P_ID";
        }


        // ✅ **選擇圖片**
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "選擇小商品圖片",
                Filter = "圖片文件 (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;
                pbSubProductImage.Image = Image.FromFile(selectedImagePath);
            }
        }

        // 🔹 刪除商品
        private async void btnDeleteSubProduct_Click(object sender, EventArgs e)
        {
            int spId = int.Parse(txtSubProductID.Text);
            bool success = await _subProductsService.DeleteSubProductAsync(spId);
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

        // ✅ **新增小商品**
        private async void btnAddSubProduct_Click(object sender, EventArgs e)
        {
            if (cbProducts.SelectedItem == null || string.IsNullOrEmpty(txtSubProductName.Text) || string.IsNullOrEmpty(selectedImagePath) || numStock.Value <= 0)
            {
                MessageBox.Show("請填寫完整資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = (int)cbProducts.SelectedValue;
            string subProductName = txtSubProductName.Text;
            int stock = (int)numStock.Value;

            // ✅ **取得圖片檔名**
            string imageFileName = Path.GetFileName(selectedImagePath);

            // ✅ **確保 `imageFileName` 不是空的**
            if (string.IsNullOrEmpty(imageFileName))
            {
                MessageBox.Show("圖片無效，請重新選擇！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = await _subProductsService.AddSubProductAsync(productId, subProductName, imageFileName, stock);

            if (success)
            {
                MessageBox.Show("小商品新增成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubProductName.Clear();
                numStock.Value = 1;
                pbSubProductImage.Image = null;
                await LoadProducts();
            }
            else
            {
                MessageBox.Show("小商品新增失敗！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSubProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSubProducts.Rows[e.RowIndex];

                txtSubProductID.Text = row.Cells["SP_ID"].Value.ToString();
                txtSubProductName.Text = row.Cells["SP_Name"].Value.ToString();
                numStock.Value = Convert.ToInt32(row.Cells["SP_Amount"].Value);

                // **🔹 取得商品名稱 (`P_Name`)**
                string productName = row.Cells["P_Name"].Value?.ToString();
                string subProductImage = row.Cells["SP_Image"].Value?.ToString();

                if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(subProductImage))
                {
                    pbSubProductImage.Image = Image.FromFile(@"C:\扭蛋圖片\default.png");
                    selectedImagePath = "";
                    return;
                }

                // **✅ 正確拼接圖片路徑**
                string imagePath = Path.Combine(@"C:\扭蛋圖片", productName, subProductImage);

                // **✅ 檢查檔案是否存在**
                if (File.Exists(imagePath))
                {
                    pbSubProductImage.Image = Image.FromFile(imagePath);
                    selectedImagePath = imagePath;
                }
                else
                {
                    pbSubProductImage.Image = Image.FromFile(@"C:\扭蛋圖片\default.png");
                    selectedImagePath = "";
                }
            }
        }


        private async void btnUpdateSubProduct_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSubProductID.Text, out int subProductId))
            {
                MessageBox.Show("請選擇要修改的小商品", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newSubProductName = txtSubProductName.Text.Trim();
            int newStock = (int)numStock.Value;

            if (string.IsNullOrWhiteSpace(newSubProductName) || newStock < 0)
            {
                MessageBox.Show("請填寫正確的資訊！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string imageFileName = string.IsNullOrEmpty(selectedImagePath) ? null : Path.GetFileName(selectedImagePath);

            // ✅ 建立 `SubProducts` 物件
            SubProducts updatedSubProduct = new SubProducts
            {
                SP_ID = subProductId,
                P_ID = (int)cbProducts.SelectedValue,
                SP_Name = newSubProductName,
                SP_Amount = newStock,
                SP_Image = imageFileName
            };

            // ✅ 確保呼叫 `UpdateSubProductAsync` 時提供所有必要的參數
            bool success = await _subProductsService.UpdateSubProductAsync(subProductId, updatedSubProduct);

            if (success)
            {
                MessageBox.Show("小商品更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProducts();
            }
            else
            {
                MessageBox.Show("更新失敗！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
