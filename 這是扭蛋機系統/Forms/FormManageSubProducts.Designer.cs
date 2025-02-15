namespace 這是扭蛋機系統.Forms
{
    partial class FormManageSubProducts
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
            this.cbProducts = new System.Windows.Forms.ComboBox();
            this.txtSubProductName = new System.Windows.Forms.TextBox();
            this.numStock = new System.Windows.Forms.NumericUpDown();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnAddSubProduct = new System.Windows.Forms.Button();
            this.btnDeleteSubProduct = new System.Windows.Forms.Button();
            this.txtSubProductID = new System.Windows.Forms.TextBox();
            this.dgvSubProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pbSubProductImage = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSearchSubProduct = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnUpdateSubProduct = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSubProductImage)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbProducts
            // 
            this.cbProducts.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbProducts.FormattingEnabled = true;
            this.cbProducts.Location = new System.Drawing.Point(29, 58);
            this.cbProducts.Name = "cbProducts";
            this.cbProducts.Size = new System.Drawing.Size(246, 27);
            this.cbProducts.TabIndex = 0;
            // 
            // txtSubProductName
            // 
            this.txtSubProductName.Location = new System.Drawing.Point(56, 6);
            this.txtSubProductName.Name = "txtSubProductName";
            this.txtSubProductName.Size = new System.Drawing.Size(111, 22);
            this.txtSubProductName.TabIndex = 1;
            // 
            // numStock
            // 
            this.numStock.Location = new System.Drawing.Point(222, 6);
            this.numStock.Name = "numStock";
            this.numStock.Size = new System.Drawing.Size(68, 22);
            this.numStock.TabIndex = 2;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(13, 351);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(179, 38);
            this.btnSelectImage.TabIndex = 4;
            this.btnSelectImage.Text = "選擇圖片";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Visible = false;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnAddSubProduct
            // 
            this.btnAddSubProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(208)))));
            this.btnAddSubProduct.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnAddSubProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddSubProduct.Location = new System.Drawing.Point(13, 395);
            this.btnAddSubProduct.Name = "btnAddSubProduct";
            this.btnAddSubProduct.Size = new System.Drawing.Size(276, 60);
            this.btnAddSubProduct.TabIndex = 5;
            this.btnAddSubProduct.Text = "確定新增";
            this.btnAddSubProduct.UseVisualStyleBackColor = false;
            this.btnAddSubProduct.Click += new System.EventHandler(this.btnAddSubProduct_Click);
            // 
            // btnDeleteSubProduct
            // 
            this.btnDeleteSubProduct.BackColor = System.Drawing.Color.LightBlue;
            this.btnDeleteSubProduct.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteSubProduct.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSubProduct.Location = new System.Drawing.Point(13, 9);
            this.btnDeleteSubProduct.Name = "btnDeleteSubProduct";
            this.btnDeleteSubProduct.Size = new System.Drawing.Size(120, 69);
            this.btnDeleteSubProduct.TabIndex = 6;
            this.btnDeleteSubProduct.Text = "確定刪除";
            this.btnDeleteSubProduct.UseVisualStyleBackColor = false;
            this.btnDeleteSubProduct.Click += new System.EventHandler(this.btnDeleteSubProduct_Click);
            // 
            // txtSubProductID
            // 
            this.txtSubProductID.Location = new System.Drawing.Point(675, 539);
            this.txtSubProductID.Name = "txtSubProductID";
            this.txtSubProductID.Size = new System.Drawing.Size(86, 22);
            this.txtSubProductID.TabIndex = 7;
            // 
            // dgvSubProducts
            // 
            this.dgvSubProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSubProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.dgvSubProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.dgvSubProducts.Location = new System.Drawing.Point(11, 70);
            this.dgvSubProducts.Name = "dgvSubProducts";
            this.dgvSubProducts.RowTemplate.Height = 24;
            this.dgvSubProducts.Size = new System.Drawing.Size(487, 582);
            this.dgvSubProducts.TabIndex = 8;
            this.dgvSubProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubProducts_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "名稱";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(91, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "放入扭蛋商品";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(174, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "庫存";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(576, 538);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "小商品ID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(84, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "選擇小商品圖片";
            // 
            // pbSubProductImage
            // 
            this.pbSubProductImage.BackColor = System.Drawing.Color.White;
            this.pbSubProductImage.Location = new System.Drawing.Point(13, 115);
            this.pbSubProductImage.Name = "pbSubProductImage";
            this.pbSubProductImage.Size = new System.Drawing.Size(274, 274);
            this.pbSubProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSubProductImage.TabIndex = 3;
            this.pbSubProductImage.TabStop = false;
            this.pbSubProductImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(822, 23);
            this.panel8.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(796, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(19, 21);
            this.label7.TabIndex = 18;
            this.label7.Text = "X";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(10, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "新增小商品";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbProducts);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtSubProductName);
            this.panel1.Controls.Add(this.numStock);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pbSubProductImage);
            this.panel1.Controls.Add(this.btnSelectImage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnAddSubProduct);
            this.panel1.Location = new System.Drawing.Point(508, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 464);
            this.panel1.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel2.Controls.Add(this.btnDeleteSubProduct);
            this.panel2.Location = new System.Drawing.Point(662, 566);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(149, 86);
            this.panel2.TabIndex = 27;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Location = new System.Drawing.Point(0, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(630, 42);
            this.panel3.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(528, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 21);
            this.label8.TabIndex = 31;
            this.label8.Text = "搜尋小商品:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(62, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 21);
            this.label9.TabIndex = 30;
            this.label9.Text = "系統時間 : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(144, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 21);
            this.label10.TabIndex = 29;
            this.label10.Text = "時間";
            // 
            // txtSearchSubProduct
            // 
            this.txtSearchSubProduct.Location = new System.Drawing.Point(630, 35);
            this.txtSearchSubProduct.Name = "txtSearchSubProduct";
            this.txtSearchSubProduct.Size = new System.Drawing.Size(100, 22);
            this.txtSearchSubProduct.TabIndex = 38;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel4.Location = new System.Drawing.Point(630, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 12);
            this.panel4.TabIndex = 39;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel5.Location = new System.Drawing.Point(630, 55);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(100, 10);
            this.panel5.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel6.Location = new System.Drawing.Point(730, 23);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(92, 42);
            this.panel6.TabIndex = 1;
            // 
            // btnUpdateSubProduct
            // 
            this.btnUpdateSubProduct.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnUpdateSubProduct.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUpdateSubProduct.ForeColor = System.Drawing.Color.White;
            this.btnUpdateSubProduct.Location = new System.Drawing.Point(16, 9);
            this.btnUpdateSubProduct.Name = "btnUpdateSubProduct";
            this.btnUpdateSubProduct.Size = new System.Drawing.Size(120, 69);
            this.btnUpdateSubProduct.TabIndex = 40;
            this.btnUpdateSubProduct.Text = "確定修改";
            this.btnUpdateSubProduct.UseVisualStyleBackColor = false;
            this.btnUpdateSubProduct.Click += new System.EventHandler(this.btnUpdateSubProduct_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightCyan;
            this.panel7.Controls.Add(this.btnUpdateSubProduct);
            this.panel7.Location = new System.Drawing.Point(508, 566);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(149, 86);
            this.panel7.TabIndex = 28;
            // 
            // FormManageSubProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(822, 666);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.txtSubProductID);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.txtSearchSubProduct);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.dgvSubProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormManageSubProducts";
            this.Text = "FormManageSubProducts";
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSubProductImage)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbProducts;
        private System.Windows.Forms.TextBox txtSubProductName;
        private System.Windows.Forms.NumericUpDown numStock;
        private System.Windows.Forms.PictureBox pbSubProductImage;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnAddSubProduct;
        private System.Windows.Forms.Button btnDeleteSubProduct;
        private System.Windows.Forms.TextBox txtSubProductID;
        private System.Windows.Forms.DataGridView dgvSubProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSearchSubProduct;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnUpdateSubProduct;
        private System.Windows.Forms.Panel panel7;
    }
}