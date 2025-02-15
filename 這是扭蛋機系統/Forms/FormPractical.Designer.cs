namespace 這是扭蛋機系統
{
    partial class FormPractical
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.flowLayoutPanelPracticalProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.btnNextPage.Location = new System.Drawing.Point(487, 583);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(93, 30);
            this.btnNextPage.TabIndex = 13;
            this.btnNextPage.Text = "下一頁";
            this.btnNextPage.UseVisualStyleBackColor = false;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.btnPreviousPage.Location = new System.Drawing.Point(280, 583);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(92, 30);
            this.btnPreviousPage.TabIndex = 12;
            this.btnPreviousPage.Text = "上一頁";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanelPracticalProducts
            // 
            this.flowLayoutPanelPracticalProducts.AllowDrop = true;
            this.flowLayoutPanelPracticalProducts.AutoScroll = true;
            this.flowLayoutPanelPracticalProducts.Location = new System.Drawing.Point(14, 6);
            this.flowLayoutPanelPracticalProducts.Name = "flowLayoutPanelPracticalProducts";
            this.flowLayoutPanelPracticalProducts.Size = new System.Drawing.Size(847, 575);
            this.flowLayoutPanelPracticalProducts.TabIndex = 11;
            // 
            // FormPractical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(901, 636);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.flowLayoutPanelPracticalProducts);
            this.Controls.Add(this.btnPreviousPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPractical";
            this.Text = "FormPractical";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPracticalProducts;
    }
}