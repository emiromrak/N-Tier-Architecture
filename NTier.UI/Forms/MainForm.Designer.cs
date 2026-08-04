namespace NTier.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabCategories = new TabPage();
            btnDeleteCategory = new Button();
            btnEditCategory = new Button();
            btnAddCategory = new Button();
            chkCategoryActive = new CheckBox();
            txtCategoryDesc = new TextBox();
            lblCategoryDesc = new Label();
            txtCategoryName = new TextBox();
            lblCategoryName = new Label();
            lstCategories = new ListBox();
            tabProducts = new TabPage();
            btnDeleteProduct = new Button();
            btnEditProduct = new Button();
            btnAddProduct = new Button();
            chkProductDiscontinued = new CheckBox();
            cmbCategory = new ComboBox();
            lblProductCategory = new Label();
            txtProductStock = new TextBox();
            lblProductStock = new Label();
            txtProductPrice = new TextBox();
            lblProductPrice = new Label();
            txtProductName = new TextBox();
            lblProductName = new Label();
            lstProducts = new ListBox();

            tabControl.SuspendLayout();
            tabCategories.SuspendLayout();
            tabProducts.SuspendLayout();
            SuspendLayout();

            tabControl.Controls.Add(tabCategories);
            tabControl.Controls.Add(tabProducts);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(900, 600);
            tabControl.TabIndex = 0;

            tabCategories.Controls.Add(lstCategories);
            tabCategories.Controls.Add(lblCategoryName);
            tabCategories.Controls.Add(txtCategoryName);
            tabCategories.Controls.Add(lblCategoryDesc);
            tabCategories.Controls.Add(txtCategoryDesc);
            tabCategories.Controls.Add(chkCategoryActive);
            tabCategories.Controls.Add(btnAddCategory);
            tabCategories.Controls.Add(btnEditCategory);
            tabCategories.Controls.Add(btnDeleteCategory);
            tabCategories.Location = new Point(4, 24);
            tabCategories.Name = "tabCategories";
            tabCategories.Padding = new Padding(10);
            tabCategories.Size = new Size(892, 572);
            tabCategories.TabIndex = 0;
            tabCategories.Text = "Kategoriler";
            tabCategories.UseVisualStyleBackColor = true;

            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(10, 10);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(29, 15);
            lblCategoryName.TabIndex = 0;
            lblCategoryName.Text = "Adý:";

            txtCategoryName.Location = new Point(100, 7);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(250, 23);
            txtCategoryName.TabIndex = 1;

            lblCategoryDesc.AutoSize = true;
            lblCategoryDesc.Location = new Point(10, 40);
            lblCategoryDesc.Name = "lblCategoryDesc";
            lblCategoryDesc.Size = new Size(52, 15);
            lblCategoryDesc.TabIndex = 2;
            lblCategoryDesc.Text = "Açýklama:";

            txtCategoryDesc.Location = new Point(100, 37);
            txtCategoryDesc.Multiline = true;
            txtCategoryDesc.Name = "txtCategoryDesc";
            txtCategoryDesc.Size = new Size(250, 60);
            txtCategoryDesc.TabIndex = 3;

            chkCategoryActive.AutoSize = true;
            chkCategoryActive.Checked = true;
            chkCategoryActive.CheckState = CheckState.Checked;
            chkCategoryActive.Location = new Point(100, 103);
            chkCategoryActive.Name = "chkCategoryActive";
            chkCategoryActive.Size = new Size(62, 19);
            chkCategoryActive.TabIndex = 4;
            chkCategoryActive.Text = "Aktif";
            chkCategoryActive.UseVisualStyleBackColor = true;

            btnAddCategory.Location = new Point(100, 128);
            btnAddCategory.Name = "btnAddCategory";
            btnAddCategory.Size = new Size(75, 23);
            btnAddCategory.TabIndex = 5;
            btnAddCategory.Text = "Ekle";
            btnAddCategory.UseVisualStyleBackColor = true;
            btnAddCategory.Click += btnAddCategory_Click;

            btnEditCategory.Location = new Point(183, 128);
            btnEditCategory.Name = "btnEditCategory";
            btnEditCategory.Size = new Size(75, 23);
            btnEditCategory.TabIndex = 6;
            btnEditCategory.Text = "Düzenle";
            btnEditCategory.UseVisualStyleBackColor = true;
            btnEditCategory.Click += btnEditCategory_Click;

            btnDeleteCategory.Location = new Point(266, 128);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new Size(75, 23);
            btnDeleteCategory.TabIndex = 7;
            btnDeleteCategory.Text = "Sil";
            btnDeleteCategory.UseVisualStyleBackColor = true;
            btnDeleteCategory.Click += btnDeleteCategory_Click;

            lstCategories.FormattingEnabled = true;
            lstCategories.ItemHeight = 15;
            lstCategories.Location = new Point(360, 10);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new Size(520, 549);
            lstCategories.TabIndex = 8;

            tabProducts.Controls.Add(lstProducts);
            tabProducts.Controls.Add(lblProductName);
            tabProducts.Controls.Add(txtProductName);
            tabProducts.Controls.Add(lblProductPrice);
            tabProducts.Controls.Add(txtProductPrice);
            tabProducts.Controls.Add(lblProductStock);
            tabProducts.Controls.Add(txtProductStock);
            tabProducts.Controls.Add(lblProductCategory);
            tabProducts.Controls.Add(cmbCategory);
            tabProducts.Controls.Add(chkProductDiscontinued);
            tabProducts.Controls.Add(btnAddProduct);
            tabProducts.Controls.Add(btnEditProduct);
            tabProducts.Controls.Add(btnDeleteProduct);
            tabProducts.Location = new Point(4, 24);
            tabProducts.Name = "tabProducts";
            tabProducts.Padding = new Padding(10);
            tabProducts.Size = new Size(892, 572);
            tabProducts.TabIndex = 1;
            tabProducts.Text = "Ürünler";
            tabProducts.UseVisualStyleBackColor = true;

            lblProductName.AutoSize = true;
            lblProductName.Location = new Point(10, 10);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(29, 15);
            lblProductName.TabIndex = 0;
            lblProductName.Text = "Adý:";

            txtProductName.Location = new Point(100, 7);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(250, 23);
            txtProductName.TabIndex = 1;

            lblProductPrice.AutoSize = true;
            lblProductPrice.Location = new Point(10, 40);
            lblProductPrice.Name = "lblProductPrice";
            lblProductPrice.Size = new Size(38, 15);
            lblProductPrice.TabIndex = 2;
            lblProductPrice.Text = "Fiyat:";

            txtProductPrice.Location = new Point(100, 37);
            txtProductPrice.Name = "txtProductPrice";
            txtProductPrice.Size = new Size(250, 23);
            txtProductPrice.TabIndex = 3;

            lblProductStock.AutoSize = true;
            lblProductStock.Location = new Point(10, 70);
            lblProductStock.Name = "lblProductStock";
            lblProductStock.Size = new Size(35, 15);
            lblProductStock.TabIndex = 4;
            lblProductStock.Text = "Stok:";

            txtProductStock.Location = new Point(100, 67);
            txtProductStock.Name = "txtProductStock";
            txtProductStock.Size = new Size(250, 23);
            txtProductStock.TabIndex = 5;

            lblProductCategory.AutoSize = true;
            lblProductCategory.Location = new Point(10, 100);
            lblProductCategory.Name = "lblProductCategory";
            lblProductCategory.Size = new Size(56, 15);
            lblProductCategory.TabIndex = 6;
            lblProductCategory.Text = "Kategori:";

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(100, 97);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(250, 23);
            cmbCategory.TabIndex = 7;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;

            chkProductDiscontinued.AutoSize = true;
            chkProductDiscontinued.Location = new Point(100, 128);
            chkProductDiscontinued.Name = "chkProductDiscontinued";
            chkProductDiscontinued.Size = new Size(74, 19);
            chkProductDiscontinued.TabIndex = 8;
            chkProductDiscontinued.Text = "Kaldýrýldý";
            chkProductDiscontinued.UseVisualStyleBackColor = true;

            btnAddProduct.Location = new Point(100, 153);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(75, 23);
            btnAddProduct.TabIndex = 9;
            btnAddProduct.Text = "Ekle";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;

            btnEditProduct.Location = new Point(183, 153);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(75, 23);
            btnEditProduct.TabIndex = 10;
            btnEditProduct.Text = "Düzenle";
            btnEditProduct.UseVisualStyleBackColor = true;
            btnEditProduct.Click += btnEditProduct_Click;

            btnDeleteProduct.Location = new Point(266, 153);
            btnDeleteProduct.Name = "btnDeleteProduct";
            btnDeleteProduct.Size = new Size(75, 23);
            btnDeleteProduct.TabIndex = 11;
            btnDeleteProduct.Text = "Sil";
            btnDeleteProduct.UseVisualStyleBackColor = true;
            btnDeleteProduct.Click += btnDeleteProduct_Click;

            lstProducts.FormattingEnabled = true;
            lstProducts.ItemHeight = 15;
            lstProducts.Location = new Point(360, 10);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new Size(520, 549);
            lstProducts.TabIndex = 12;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(tabControl);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;

            tabControl.ResumeLayout(false);
            tabCategories.ResumeLayout(false);
            tabCategories.PerformLayout();
            tabProducts.ResumeLayout(false);
            tabProducts.PerformLayout();
            ResumeLayout(false);
        }

        private TabControl tabControl;
        private TabPage tabCategories;
        private TabPage tabProducts;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Label lblCategoryDesc;
        private TextBox txtCategoryDesc;
        private CheckBox chkCategoryActive;
        private Button btnAddCategory;
        private Button btnEditCategory;
        private Button btnDeleteCategory;
        private ListBox lstCategories;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblProductPrice;
        private TextBox txtProductPrice;
        private Label lblProductStock;
        private TextBox txtProductStock;
        private Label lblProductCategory;
        private ComboBox cmbCategory;
        private CheckBox chkProductDiscontinued;
        private Button btnAddProduct;
        private Button btnEditProduct;
        private Button btnDeleteProduct;
        private ListBox lstProducts;
    }
}
