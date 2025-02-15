using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 這是扭蛋機系統.Services.PointsHistoryService;
using 這是扭蛋機系統.Services.PointsHistoryService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace 這是扭蛋機系統.Forms
{
    public partial class FormBuyMoney : Form
    {

        private readonly PointsHistoryService _pointsHistoryService;
        private int _userId; // 當前登入的會員 ID
        private int selectedAmount = 0; // 選擇的代幣金額
        private int fee = 0; // 手續費
        private int totalAmount = 0; // 總金額 (代幣金額 + 手續費)
        private string selectedPaymentMethod = ""; // 儲存用戶選擇的付款方式

        public FormBuyMoney(int userId)
        {
            InitializeComponent();
            _pointsHistoryService = new PointsHistoryService();
            _userId = userId;
        }

        private void FormBuyMoney_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("請選擇購買代幣金額");
            comboBox1.Items.AddRange(new object[] { $"代幣{100}點",$"代幣{200}點", $"代幣{300}點",$"代幣{400}點", $"代幣{500}點"
                , $"代幣{600}點", $"代幣{700}點", $"代幣{800}點", $"代幣{900}點", $"代幣{1000}點",$"代幣{1500}點"
                , $"代幣{2000}點", $"代幣{3000}點", $"代幣{5000}點", $"代幣{10000}點", $"代幣{20000}點" });

            comboBox1.SelectedIndex = 0;
        
            // 🔹 隱藏付款方式
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            label14.Visible = false;
            label15.Visible = false;

            // 🔹 綁定 RadioButton 變更事件
            radioButton1.CheckedChanged += PaymentMethodChanged;
            radioButton2.CheckedChanged += PaymentMethodChanged;
            radioButton3.CheckedChanged += PaymentMethodChanged;
        }

        private void ResetUI()
        {
            label3.Visible = true;
            label3.Text = "請先選擇購買金額";
            label11.Text = "$ 0";
            label12.Text = "未選擇付款方式";
            label13.Text = "未選擇";
            button1.Text = "🛒 確認付款 $ 0";

            // 🔹 按鈕無法點擊
            button1.Enabled = false;

            // 🔹 隱藏付款方式
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) // 🔹 如果沒有選擇金額
            {
                ResetUI();
                return;
            }

            // 🔹 從選擇的項目中擷取數字
            string selectedText = comboBox1.SelectedItem.ToString();
            string amountString = new string(selectedText.Where(char.IsDigit).ToArray());

            if (!int.TryParse(amountString, out selectedAmount)) // ✅ 存到類別變數 `selectedAmount`
            {
                MessageBox.Show("選擇金額時發生錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetUI();
                return;
            }

            // ✅ 顯示付款方式
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            // ✅ 設定手續費與付款方式
            if (selectedAmount < 300)
            {
                radioButton1.Text = "信用卡                           手續費 2.5%";
                radioButton2.Text = "ATM 轉帳                     免手續費";
                radioButton3.Text = "超商繳費                       手續費 $30元";
                label14.Text = "購買達300免收手續費";
                label15.Text = "購買達1000免收手續費";
                label14.Visible = true;
                label15.Visible = true;
            }
            else if (selectedAmount >= 300 && selectedAmount < 1000)
            {
                radioButton1.Text = "信用卡                           免手續費";
                radioButton2.Text = "ATM 轉帳                     免手續費";
                radioButton3.Text = "超商繳費                       手續費 $30元";
                label14.Visible = false;
                label15.Visible = true;
            }
            else
            {
                radioButton1.Text = "信用卡                           免手續費";
                radioButton2.Text = "ATM 轉帳                     免手續費";
                radioButton3.Text = "超商繳費                       免手續費";
                label14.Visible = false;
                label15.Visible = false;
            }

            // ✅ 更新金額
            label13.Text = $"$ {selectedAmount}";
            label12.Text = "未選擇付款方式";
            label11.Text = $"$ {selectedAmount}";
            button1.Text = $"🛒 確認付款 $ {selectedAmount}";

            // ✅ 檢查是否可以啟用 `button1`
            UpdateButtonState();
        }


        private void PaymentMethodChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) return;

            int fee = 0;

            if (radioButton1.Checked) // 信用卡
            {
                selectedPaymentMethod = "信用卡";
                fee = selectedAmount < 300 ? (int)(selectedAmount * 0.025) : 0;
                if (fee < 1 && selectedAmount < 300) fee = 1;
            }
            else if (radioButton2.Checked) // ATM 轉帳
            {
                selectedPaymentMethod = "ATM 轉帳";
                fee = 0;
            }
            else if (radioButton3.Checked) // 超商繳費
            {
                selectedPaymentMethod = "超商繳費";
                fee = (selectedAmount >= 1000) ? 0 : 30;
            }

            totalAmount = selectedAmount + fee; // ✅ 更新 `totalAmount`
            label12.Text = $"$ {fee}";
            label11.Text = $"$ {totalAmount}";
            button1.Text = $"🛒 確認付款 $ {totalAmount}";

            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            // ✅ 只有當「選擇了金額」且「選擇了付款方式」時，才允許點擊 `button1`
            button1.Enabled = comboBox1.SelectedIndex != 0 &&
                              (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked);
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            int pointsChanged = selectedAmount;
            decimal cashSpent = totalAmount;
            string paymentMethod = selectedPaymentMethod;

            if (string.IsNullOrEmpty(selectedPaymentMethod) || selectedAmount == 0 || totalAmount == 0)
            {
                MessageBox.Show("請選擇付款方式與金額", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"確定使用 {selectedPaymentMethod} 購買 {selectedAmount} G 幣？\n總金額: ${totalAmount}",
                "確認購買",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                bool success = await _pointsHistoryService.AddPointsHistoryAsync(_userId, pointsChanged, cashSpent, paymentMethod);

                if (success)
                {
                    MessageBox.Show("儲值成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ **顯示 `FormUserHome` 並更新 `dgvTopUpHistory`**
                    await ShowFormUserHome();

                    this.Close(); // ✅ 關閉當前視窗

                    // ✅ 回到 `FormHome`
                    ReturnToFormHome();

                    // ✅ **同步更新 Form1 的 lblPoints**
                    if (this.MdiParent is Form1 mdiContainer)
                    {
                        await mdiContainer.LoadMemberDataAsync(); // ✅ 重新載入會員資料
                    }
                    //同步更新會員累積的總金額
                    FormUserHome formUserHome = Application.OpenForms.OfType<FormUserHome>().FirstOrDefault();
                    if (formUserHome != null)
                    {
                        await formUserHome.LoadTotalCashSpent(); // ✅ 呼叫 `RefreshTopUpHistory()`
                    }
                }
                else
                {
                    MessageBox.Show("儲值失敗，請稍後再試！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ReturnToFormHome()
        {
            // 🔹 嘗試找到 `Form1`
            Form1 mdiContainer = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            if (mdiContainer == null)
            {
                MessageBox.Show("無法回到主畫面，請確認 Form1 是否為 MDI 容器。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ 檢查 `FormHome` 是否已經開啟
            FormHome formHome = mdiContainer.MdiChildren.OfType<FormHome>().FirstOrDefault();

            if (formHome == null)
            {
                formHome = new FormHome(_userId)
                {
                    MdiParent = mdiContainer, // ✅ 設定 MDI 容器
                    Dock = DockStyle.Fill // ✅ 讓 FormHome 充滿 MDI 容器
                };
                formHome.Show();
            }
            else
            {
                formHome.Activate(); // 讓已存在的 FormHome 顯示在最前面
            }
        }


        private async Task ShowFormUserHome()
        {
            if (this.MdiParent is Form1 mdiContainer) // ✅ 確保 MDI 容器存在
            {
                // ✅ 檢查 `FormUserHome` 是否已經開啟
                FormUserHome formUserHome = mdiContainer.MdiChildren.OfType<FormUserHome>().FirstOrDefault();

                // ✅ **同步更新 `FormUserHome` 的 `dgvTopUpHistory`**
                FormGashaponLog formGashaponLog = Application.OpenForms.OfType<FormGashaponLog>().FirstOrDefault();
                if (formGashaponLog != null)
                {
                    await formGashaponLog.RefreshTopUpHistory(); // ✅ 呼叫 `RefreshTopUpHistory()`
                }

                if (formUserHome == null)
                {
                    formUserHome = new FormUserHome(_userId)
                    {
                        MdiParent = mdiContainer,
                        Dock = DockStyle.Fill
                    };
                    formUserHome.Show();
                }
                else
                {
                    await formUserHome.LoadUserDataAsync(); // ✅ 重新加載會員數據
                    formUserHome.Activate(); // ✅ 讓 `FormUserHome` 成為焦點
                }
            }
        }



        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.FromArgb(213, 234, 246);
            button1.ForeColor = Color.FromArgb(255, 153, 208);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(255, 153, 208);
            button1.ForeColor = Color.White;
        }
    }
}

    
