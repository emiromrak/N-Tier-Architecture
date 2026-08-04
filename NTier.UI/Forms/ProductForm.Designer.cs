namespace NTier.UI.Forms
{
    partial class ProductForm
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
            lblName = new Label();
            txtName = new TextBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            lblUnitInStock = new Label();
            txtUnitInStock = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            chkDiscontinued = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            lblName.AutoSize = true;
            lblName.Location = new Point(12, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(32, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Adý:";

            txtName.Location = new Point(120, 12);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 23);
            txtName.TabIndex = 1;

            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Location = new Point(12, 45);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(41, 15);
            lblUnitPrice.TabIndex = 2;
            lblUnitPrice.Text = "Fiyat:";

            txtUnitPrice.Location = new Point(120, 42);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(250, 23);
            txtUnitPrice.TabIndex = 3;

            lblUnitInStock.AutoSize = true;
            lblUnitInStock.Location = new Point(12, 75);
            lblUnitInStock.Name = "lblUnitInStock";
            lblUnitInStock.Size = new Size(38, 15);
            lblUnitInStock.TabIndex = 4;
            lblUnitInStock.Text = "Stok:";

            txtUnitInStock.Location = new Point(120, 72);
            txtUnitInStock.Name = "txtUnitInStock";
            txtUnitInStock.Size = new Size(250, 23);
            txtUnitInStock.TabIndex = 5;

            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(12, 105);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(59, 15);
            lblCategory.TabIndex = 6;
            lblCategory.Text = "Kategori:";

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(120, 102);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(250, 23);
            cmbCategory.TabIndex = 7;

            chkDiscontinued.AutoSize = true;
            chkDiscontinued.Location = new Point(120, 135);
            chkDiscontinued.Name = "chkDiscontinued";
            chkDiscontinued.Size = new Size(78, 19);
            chkDiscontinued.TabIndex = 8;
            chkDiscontinued.Text = "Kaldýrýldý";
            chkDiscontinued.UseVisualStyleBackColor = true;

            btnSave.Location = new Point(120, 165);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 9;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;

            btnCancel.Location = new Point(205, 165);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Ýptal";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 220);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkDiscontinued);
            Controls.Add(cmbCategory);
            Controls.Add(lblCategory);
            Controls.Add(txtUnitInStock);
            Controls.Add(lblUnitInStock);
            Controls.Add(txtUnitPrice);
            Controls.Add(lblUnitPrice);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Name = "ProductForm";
            Text = "ProductForm";
            Load += ProductForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblName;
        private TextBox txtName;
        private Label lblUnitPrice;
        private TextBox txtUnitPrice;
        private Label lblUnitInStock;
        private TextBox txtUnitInStock;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private CheckBox chkDiscontinued;
        private Button btnSave;
        private Button btnCancel;
    }
}
