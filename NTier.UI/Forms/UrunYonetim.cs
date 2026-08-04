using NTier.Business.Services;
using NTier.DataAccess.Repositories;
using NTier.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NTier.Entities.Models;

namespace NTier.UI.Forms
{
    public partial class UrunYonetim : Form
    {
        private ADBContext context = new ADBContext();
        private CategoryRepository categoryRepository;
        private CategoryService CategoryService;
        public UrunYonetim()
        {
            InitializeComponent();
            categoryRepository = new CategoryRepository(context);
            CategoryService = new CategoryService(categoryRepository);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Category categoryTest = new Category();
            categoryTest.Name = textBox1.Text;
            categoryTest.Description = textBox2.Text;
            categoryTest.IsActive = checkBox1.Checked;
            CategoryService.Create(categoryTest);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
