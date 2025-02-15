namespace 這是扭蛋機系統.Forms
{
    partial class FormProductManage
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
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnUpdateProduct = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pbProductPreview = new System.Windows.Forms.PictureBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.roundedPanelAllProducts = new RoundedPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.roundedPanelCute = new RoundedPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.roundedPanelAnimal = new RoundedPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.roundedPanelFood = new RoundedPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.roundedPanelAnime = new RoundedPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.roundedPanelOther = new RoundedPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel8.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProductPreview)).BeginInit();
            this.roundedPanelAllProducts.SuspendLayout();
            this.roundedPanelCute.SuspendLayout();
            this.roundedPanelAnimal.SuspendLayout();
            this.roundedPanelFood.SuspendLayout();
            this.roundedPanelAnime.SuspendLayout();
            this.roundedPanelOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(208)))));
            this.btnAddProduct.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(10, 113);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(183, 59);
            this.btnAddProduct.TabIndex = 0;
            this.btnAddProduct.Text = "新增商品";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.BackColor = System.Drawing.Color.LightBlue;
            this.btnDeleteProduct.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDeleteProduct.ForeColor = System.Drawing.Color.White;
            this.btnDeleteProduct.Location = new System.Drawing.Point(10, 318);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(183, 93);
            this.btnDeleteProduct.TabIndex = 2;
            this.btnDeleteProduct.Text = "確定刪除";
            this.btnDeleteProduct.UseVisualStyleBackColor = false;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(14, 57);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(182, 22);
            this.txtProductName.TabIndex = 4;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(43, 11);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(56, 22);
            this.txtPrice.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(60, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "商品名稱";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(2, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "G幣:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(98, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "類別:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(10, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 56);
            this.button1.TabIndex = 12;
            this.button1.Text = "新增小商品";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(145, 12);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(61, 20);
            this.cbCategory.TabIndex = 13;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(949, 23);
            this.panel8.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(926, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(19, 21);
            this.label7.TabIndex = 18;
            this.label7.Text = "X";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "新增商品";
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(25, 65);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(690, 543);
            this.flowLayoutPanelProducts.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(208)))));
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 25);
            this.label3.TabIndex = 34;
            this.label3.Text = "第一步";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label14.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(11, 169);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(181, 25);
            this.label14.TabIndex = 35;
            this.label14.Text = "第二步";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Location = new System.Drawing.Point(799, 34);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(100, 22);
            this.txtSearchProduct.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(62, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 21);
            this.label13.TabIndex = 30;
            this.label13.Text = "系統時間 : ";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(144, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(42, 21);
            this.lblTime.TabIndex = 29;
            this.lblTime.Text = "時間";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 42);
            this.panel1.TabIndex = 36;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(711, 11);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(78, 21);
            this.lblSearch.TabIndex = 31;
            this.lblSearch.Text = "搜尋商品:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel2.Location = new System.Drawing.Point(898, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(51, 42);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel3.Location = new System.Drawing.Point(798, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(100, 10);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel4.Location = new System.Drawing.Point(798, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 12);
            this.panel4.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel5.Controls.Add(this.btnDeleteProduct);
            this.panel5.Controls.Add(this.btnUpdateProduct);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.btnAddProduct);
            this.panel5.Controls.Add(this.txtProductName);
            this.panel5.Controls.Add(this.txtPrice);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.cbCategory);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Location = new System.Drawing.Point(715, 302);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(210, 415);
            this.panel5.TabIndex = 0;
            // 
            // btnUpdateProduct
            // 
            this.btnUpdateProduct.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnUpdateProduct.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdateProduct.ForeColor = System.Drawing.Color.White;
            this.btnUpdateProduct.Location = new System.Drawing.Point(10, 262);
            this.btnUpdateProduct.Name = "btnUpdateProduct";
            this.btnUpdateProduct.Size = new System.Drawing.Size(183, 58);
            this.btnUpdateProduct.TabIndex = 39;
            this.btnUpdateProduct.Text = "確定修改";
            this.btnUpdateProduct.UseVisualStyleBackColor = false;
            this.btnUpdateProduct.Click += new System.EventHandler(this.btnUpdateProduct_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel6.Controls.Add(this.roundedPanelAllProducts);
            this.panel6.Controls.Add(this.roundedPanelCute);
            this.panel6.Controls.Add(this.roundedPanelAnimal);
            this.panel6.Controls.Add(this.roundedPanelFood);
            this.panel6.Controls.Add(this.roundedPanelAnime);
            this.panel6.Controls.Add(this.roundedPanelOther);
            this.panel6.Location = new System.Drawing.Point(25, 608);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(690, 109);
            this.panel6.TabIndex = 0;
            // 
            // pbProductPreview
            // 
            this.pbProductPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbProductPreview.Location = new System.Drawing.Point(715, 107);
            this.pbProductPreview.Name = "pbProductPreview";
            this.pbProductPreview.Size = new System.Drawing.Size(210, 195);
            this.pbProductPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProductPreview.TabIndex = 37;
            this.pbProductPreview.TabStop = false;
            this.pbProductPreview.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.BackColor = System.Drawing.Color.White;
            this.btnSelectImage.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.btnSelectImage.Location = new System.Drawing.Point(715, 258);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(210, 42);
            this.btnSelectImage.TabIndex = 38;
            this.btnSelectImage.Text = "選擇封面";
            this.btnSelectImage.UseVisualStyleBackColor = false;
            this.btnSelectImage.Visible = false;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(763, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 31);
            this.label15.TabIndex = 29;
            this.label15.Text = "選擇封面";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roundedPanelAllProducts
            // 
            this.roundedPanelAllProducts.BackColor = System.Drawing.Color.White;
            this.roundedPanelAllProducts.BottomLeft = true;
            this.roundedPanelAllProducts.BottomRight = true;
            this.roundedPanelAllProducts.Controls.Add(this.label12);
            this.roundedPanelAllProducts.CornerRadius = 20;
            this.roundedPanelAllProducts.Location = new System.Drawing.Point(7, 10);
            this.roundedPanelAllProducts.Name = "roundedPanelAllProducts";
            this.roundedPanelAllProducts.Size = new System.Drawing.Size(95, 90);
            this.roundedPanelAllProducts.TabIndex = 29;
            this.roundedPanelAllProducts.TopLeft = true;
            this.roundedPanelAllProducts.TopRight = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(17, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 31);
            this.label12.TabIndex = 28;
            this.label12.Text = "全部";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roundedPanelCute
            // 
            this.roundedPanelCute.BackColor = System.Drawing.Color.White;
            this.roundedPanelCute.BottomLeft = true;
            this.roundedPanelCute.BottomRight = true;
            this.roundedPanelCute.Controls.Add(this.label6);
            this.roundedPanelCute.CornerRadius = 20;
            this.roundedPanelCute.Location = new System.Drawing.Point(209, 10);
            this.roundedPanelCute.Name = "roundedPanelCute";
            this.roundedPanelCute.Size = new System.Drawing.Size(95, 90);
            this.roundedPanelCute.TabIndex = 26;
            this.roundedPanelCute.TopLeft = true;
            this.roundedPanelCute.TopRight = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(17, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 31);
            this.label6.TabIndex = 28;
            this.label6.Text = "可愛";
            // 
            // roundedPanelAnimal
            // 
            this.roundedPanelAnimal.BackColor = System.Drawing.Color.White;
            this.roundedPanelAnimal.BottomLeft = true;
            this.roundedPanelAnimal.BottomRight = true;
            this.roundedPanelAnimal.Controls.Add(this.label8);
            this.roundedPanelAnimal.CornerRadius = 20;
            this.roundedPanelAnimal.Location = new System.Drawing.Point(108, 9);
            this.roundedPanelAnimal.Name = "roundedPanelAnimal";
            this.roundedPanelAnimal.Size = new System.Drawing.Size(95, 90);
            this.roundedPanelAnimal.TabIndex = 27;
            this.roundedPanelAnimal.TopLeft = true;
            this.roundedPanelAnimal.TopRight = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(17, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 31);
            this.label8.TabIndex = 29;
            this.label8.Text = "動物";
            // 
            // roundedPanelFood
            // 
            this.roundedPanelFood.BackColor = System.Drawing.Color.White;
            this.roundedPanelFood.BottomLeft = true;
            this.roundedPanelFood.BottomRight = true;
            this.roundedPanelFood.Controls.Add(this.label9);
            this.roundedPanelFood.CornerRadius = 20;
            this.roundedPanelFood.Location = new System.Drawing.Point(310, 10);
            this.roundedPanelFood.Name = "roundedPanelFood";
            this.roundedPanelFood.Size = new System.Drawing.Size(95, 90);
            this.roundedPanelFood.TabIndex = 27;
            this.roundedPanelFood.TopLeft = true;
            this.roundedPanelFood.TopRight = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(17, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 31);
            this.label9.TabIndex = 30;
            this.label9.Text = "食物";
            // 
            // roundedPanelAnime
            // 
            this.roundedPanelAnime.BackColor = System.Drawing.Color.White;
            this.roundedPanelAnime.BottomLeft = true;
            this.roundedPanelAnime.BottomRight = true;
            this.roundedPanelAnime.Controls.Add(this.label10);
            this.roundedPanelAnime.CornerRadius = 20;
            this.roundedPanelAnime.Location = new System.Drawing.Point(411, 9);
            this.roundedPanelAnime.Name = "roundedPanelAnime";
            this.roundedPanelAnime.Size = new System.Drawing.Size(95, 90);
            this.roundedPanelAnime.TabIndex = 27;
            this.roundedPanelAnime.TopLeft = true;
            this.roundedPanelAnime.TopRight = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(17, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 31);
            this.label10.TabIndex = 31;
            this.label10.Text = "動漫";
            // 
            // roundedPanelOther
            // 
            this.roundedPanelOther.BackColor = System.Drawing.Color.White;
            this.roundedPanelOther.BottomLeft = true;
            this.roundedPanelOther.BottomRight = true;
            this.roundedPanelOther.Controls.Add(this.label11);
            this.roundedPanelOther.CornerRadius = 20;
            this.roundedPanelOther.Location = new System.Drawing.Point(512, 9);
            this.roundedPanelOther.Name = "roundedPanelOther";
            this.roundedPanelOther.Size = new System.Drawing.Size(95, 90);
            this.roundedPanelOther.TabIndex = 32;
            this.roundedPanelOther.TopLeft = true;
            this.roundedPanelOther.TopRight = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(18, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 31);
            this.label11.TabIndex = 31;
            this.label11.Text = "其他";
            // 
            // FormProductManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(949, 748);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.pbProductPreview);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtSearchProduct);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.Controls.Add(this.panel8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProductManage";
            this.Text = "FormProductManage";
            this.Load += new System.EventHandler(this.FormProductManage_Load);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProductPreview)).EndInit();
            this.roundedPanelAllProducts.ResumeLayout(false);
            this.roundedPanelAllProducts.PerformLayout();
            this.roundedPanelCute.ResumeLayout(false);
            this.roundedPanelCute.PerformLayout();
            this.roundedPanelAnimal.ResumeLayout(false);
            this.roundedPanelAnimal.PerformLayout();
            this.roundedPanelFood.ResumeLayout(false);
            this.roundedPanelFood.PerformLayout();
            this.roundedPanelAnime.ResumeLayout(false);
            this.roundedPanelAnime.PerformLayout();
            this.roundedPanelOther.ResumeLayout(false);
            this.roundedPanelOther.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private RoundedPanel roundedPanelCute;
        private RoundedPanel roundedPanelAnimal;
        private RoundedPanel roundedPanelFood;
        private RoundedPanel roundedPanelAnime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private RoundedPanel roundedPanelOther;
        private System.Windows.Forms.Label label11;
        private RoundedPanel roundedPanelAllProducts;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pbProductPreview;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnUpdateProduct;
        private System.Windows.Forms.Label label15;
    }
}