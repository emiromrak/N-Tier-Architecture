using NTier.Business.Services;
using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.UI.Forms
{
    public partial class ProductForm : Form
    {
        private readonly ADBContext _context;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private Product? _product;

        public ProductForm(ADBContext context, ProductService productService, CategoryService categoryService, Product? product = null)
        {
            InitializeComponent();
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _product = product;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadCategories();

            if (_product != null)
            {
                Text = "Ürün Düzenle";
                txtName.Text = _product.Name;
                txtUnitPrice.Text = _product.UnitPrice.ToString();
                txtUnitInStock.Text = _product.UnitInStock.ToString();
                chkDiscontinued.Checked = _product.Discontinued;
                cmbCategory.SelectedValue = _product.CategoryID;
            }
            else
            {
                Text = "Ürün Ekle";
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetAll().ToList();
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategoriler yüklenirken hata: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Ürün adı ve kategori boş olamaz.");
                return;
            }

            if (!double.TryParse(txtUnitPrice.Text, out double price) || !int.TryParse(txtUnitInStock.Text, out int stock))
            {
                MessageBox.Show("Fiyat ve stok sayısal değer olmalıdır.");
                return;
            }

            try
            {
                var selectedCategory = (Category)cmbCategory.SelectedItem;

                if (_product == null)
                {
                    _product = new Product
                    {
                        Name = txtName.Text,
                        UnitPrice = price,
                        UnitInStock = stock,
                        Discontinued = chkDiscontinued.Checked,
                        IsActive = !chkDiscontinued.Checked,
                        CategoryID = selectedCategory.ID,
                        Category = selectedCategory
                    };
                    _productService.Create(_product);
                }
                else
                {
                    _product.Name = txtName.Text;
                    _product.UnitPrice = price;
                    _product.UnitInStock = stock;
                    _product.Discontinued = chkDiscontinued.Checked;
                    _product.IsActive = !chkDiscontinued.Checked;
                    _product.CategoryID = selectedCategory.ID;
                    _product.Category = selectedCategory;
                    _productService.Update(_product);
                }

                MessageBox.Show("Ürün kaydedildi.");
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
