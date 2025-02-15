namespace 這是扭蛋機系統
{
    partial class FormAnimal
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
            this.flowLayoutPanelAnimalProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.btnNextPage.Location = new System.Drawing.Point(522, 583);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(93, 30);
            this.btnNextPage.TabIndex = 10;
            this.btnNextPage.Text = "下一頁";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // flowLayoutPanelAnimalProducts
            // 
            this.flowLayoutPanelAnimalProducts.AllowDrop = true;
            this.flowLayoutPanelAnimalProducts.AutoScroll = true;
            this.flowLayoutPanelAnimalProducts.Location = new System.Drawing.Point(28, 6);
            this.flowLayoutPanelAnimalProducts.Name = "flowLayoutPanelAnimalProducts";
            this.flowLayoutPanelAnimalProducts.Size = new System.Drawing.Size(847, 575);
            this.flowLayoutPanelAnimalProducts.TabIndex = 8;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.btnPreviousPage.Location = new System.Drawing.Point(281, 583);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(92, 30);
            this.btnPreviousPage.TabIndex = 9;
            this.btnPreviousPage.Text = "上一頁";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // FormAnimal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(901, 636);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.flowLayoutPanelAnimalProducts);
            this.Controls.Add(this.btnPreviousPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAnimal";
            this.Text = "FormAnimal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAnimalProducts;
        private System.Windows.Forms.Button btnPreviousPage;
    }
}