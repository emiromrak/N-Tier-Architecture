using NTier.Business.Services;
using NTier.DataAccess.Context;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly ADBContext _context;
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private List<Category> categories = new List<Category>();
        private List<Product> products = new List<Product>();

        public MainForm()
        {
            InitializeComponent();
            _context = new ADBContext();
            var categoryRepo = new CategoryRepository(_context);
            var productRepo = new ProductRepository(_context);
            _categoryService = new CategoryService(categoryRepo);
            _productService = new ProductService(productRepo);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = "N-Tier Ürün Yönetimi";
            RefreshCategories();
            LoadCategoryCombo();
            RefreshProducts();
        }

        private void RefreshCategories()
        {
            try
            {
                categories = _categoryService.GetAll().ToList();
                lstCategories.Items.Clear();
                foreach (var cat in categories)
                {
                    lstCategories.Items.Add(cat.Name + " (" + (cat.IsActive ? "Aktif" : "Pasif") + ")");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void LoadCategoryCombo()
        {
            try
            {
                cmbCategory.Items.Clear();
                foreach (var cat in categories)
                {
                    cmbCategory.Items.Add(cat.Name);
                }
                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void RefreshProducts()
        {
            try
            {
                products = _productService.GetAll().ToList();
                lstProducts.Items.Clear();
                foreach (var prod in products)
                {
                    lstProducts.Items.Add(prod.Name + " - " + prod.UnitPrice + "TL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Kategori adý giriniz.");
                return;
            }

            try
            {
                var category = new Category
                {
                    Name = txtCategoryName.Text,
                    Description = txtCategoryDesc.Text,
                    IsActive = chkCategoryActive.Checked
                };
                _categoryService.Create(category);
                MessageBox.Show("Kategori eklendi.");
                txtCategoryName.Clear();
                txtCategoryDesc.Clear();
                chkCategoryActive.Checked = true;
                RefreshCategories();
                LoadCategoryCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedIndex < 0)
            {
                MessageBox.Show("Kategori seçiniz.");
                return;
            }

            try
            {
                var category = categories[lstCategories.SelectedIndex];
                new CategoryForm(_context, _categoryService, category).ShowDialog();
                RefreshCategories();
                LoadCategoryCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedIndex < 0)
            {
                MessageBox.Show("Kategori seçiniz.");
                return;
            }

            try
            {
                var category = categories[lstCategories.SelectedIndex];
                if (MessageBox.Show("Silmek istediðinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _categoryService.Delete(category.ID);
                    MessageBox.Show("Kategori silindi.");
                    RefreshCategories();
                    LoadCategoryCombo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Tüm alanlarý doldurunuz.");
                return;
            }

            if (!double.TryParse(txtProductPrice.Text, out double price) || !int.TryParse(txtProductStock.Text, out int stock))
            {
                MessageBox.Show("Fiyat ve stok sayýsal olmalýdýr.");
                return;
            }

            try
            {
                var selectedCategoryName = cmbCategory.SelectedItem.ToString();
                var selectedCategory = categories.FirstOrDefault(c => c.Name == selectedCategoryName);
                
                if (selectedCategory == null)
                {
                    MessageBox.Show("Kategori seçilmedi.");
                    return;
                }

                var product = new Product
                {
                    Name = txtProductName.Text,
                    UnitPrice = price,
                    UnitInStock = stock,
                    Discontinued = chkProductDiscontinued.Checked,
                    CategoryID = selectedCategory.ID,
                    Category = selectedCategory
                };
                _productService.Create(product);
                MessageBox.Show("Ürün eklendi.");
                txtProductName.Clear();
                txtProductPrice.Clear();
                txtProductStock.Clear();
                chkProductDiscontinued.Checked = false;
                RefreshProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex < 0)
            {
                MessageBox.Show("Ürün seçiniz.");
                return;
            }

            try
            {
                var product = products[lstProducts.SelectedIndex];
                var fullProduct = _productService.GetById(product.ID);
                new ProductForm(_context, _productService, _categoryService, fullProduct).ShowDialog();
                RefreshProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex < 0)
            {
                MessageBox.Show("Ürün seçiniz.");
                return;
            }

            try
            {
                var product = products[lstProducts.SelectedIndex];
                if (MessageBox.Show("Silmek istediðinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _productService.Delete(product.ID);
                    MessageBox.Show("Ürün silindi.");
                    RefreshProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
