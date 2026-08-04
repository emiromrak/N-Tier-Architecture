namespace NTier.UI.Forms
{
    partial class CategoryForm
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
            lblDescription = new Label();
            txtDescription = new TextBox();
            chkIsActive = new CheckBox();
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

            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(12, 45);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(52, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Açýklama:";

            txtDescription.Location = new Point(120, 42);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(250, 80);
            txtDescription.TabIndex = 3;

            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(120, 130);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(62, 19);
            chkIsActive.TabIndex = 4;
            chkIsActive.Text = "Aktif";
            chkIsActive.UseVisualStyleBackColor = true;

            btnSave.Location = new Point(120, 160);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 5;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;

            btnCancel.Location = new Point(205, 160);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Ýptal";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 200);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkIsActive);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Name = "CategoryForm";
            Text = "CategoryForm";
            Load += CategoryForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;
    }
}
