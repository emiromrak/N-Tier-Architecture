using NTier.Business.Services;
using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.UI.Forms
{
    public partial class CategoryForm : Form
    {
        private readonly ADBContext _context;
        private readonly CategoryService _categoryService;
        private Category? _category;

        public CategoryForm(ADBContext context, CategoryService categoryService, Category? category = null)
        {
            InitializeComponent();
            _context = context;
            _categoryService = categoryService;
            _category = category;
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            if (_category != null)
            {
                Text = "Kategori Düzenle";
                txtName.Text = _category.Name;
                txtDescription.Text = _category.Description;
                chkIsActive.Checked = _category.IsActive;
            }
            else
            {
                Text = "Kategori Ekle";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Kategori adý boþ olamaz.");
                return;
            }

            try
            {
                if (_category == null)
                {
                    _category = new Category
                    {
                        Name = txtName.Text,
                        Description = txtDescription.Text,
                        IsActive = chkIsActive.Checked
                    };
                    _categoryService.Create(_category);
                }
                else
                {
                    _category.Name = txtName.Text;
                    _category.Description = txtDescription.Text;
                    _category.IsActive = chkIsActive.Checked;
                    _categoryService.Update(_category);
                }

                MessageBox.Show("Kategori kaydedildi.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
