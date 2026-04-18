using System;
using System.Linq;
using System.Windows.Forms;
using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services;

namespace StokTakipSistemi
{
    public partial class ProductEditForm : Form
    {
        private readonly ProductService _productService;
        private Product _product;

        public ProductEditForm(ProductService productService, Product product)
        {
            InitializeComponent();
            _productService = productService;
            _product = product;
        }

        private void ProductEditForm_Load(object sender, EventArgs e)
        {
            CreateUI();
            LoadProductData();
        }

        private void CreateUI()
        {
            this.Text = "Ürün Düzenle";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // Ürün Adı Label
            var labelProductName = new Label();
            labelProductName.Text = "Ürün Adı:";
            labelProductName.Location = new System.Drawing.Point(20, 20);
            labelProductName.Size = new System.Drawing.Size(100, 20);
            labelProductName.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelProductName);

            var textBoxProductName = new TextBox();
            textBoxProductName.Name = "textBoxProductName";
            textBoxProductName.Location = new System.Drawing.Point(130, 20);
            textBoxProductName.Size = new System.Drawing.Size(320, 25);
            textBoxProductName.Tag = "ProductName";
            mainPanel.Controls.Add(textBoxProductName);

            // Açıklama Label
            var labelDescription = new Label();
            labelDescription.Text = "Açıklama:";
            labelDescription.Location = new System.Drawing.Point(20, 60);
            labelDescription.Size = new System.Drawing.Size(100, 20);
            labelDescription.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelDescription);

            var textBoxDescription = new TextBox();
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Location = new System.Drawing.Point(130, 60);
            textBoxDescription.Size = new System.Drawing.Size(320, 80);
            textBoxDescription.Multiline = true;
            textBoxDescription.Tag = "Description";
            mainPanel.Controls.Add(textBoxDescription);

            // Fiyat Label
            var labelPrice = new Label();
            labelPrice.Text = "Fiyat (₺):";
            labelPrice.Location = new System.Drawing.Point(20, 150);
            labelPrice.Size = new System.Drawing.Size(100, 20);
            labelPrice.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelPrice);

            var numericPrice = new NumericUpDown();
            numericPrice.Name = "numericPrice";
            numericPrice.Location = new System.Drawing.Point(130, 150);
            numericPrice.Size = new System.Drawing.Size(150, 25);
            numericPrice.DecimalPlaces = 2;
            numericPrice.Minimum = 0;
            numericPrice.Maximum = 99999;
            numericPrice.Tag = "Price";
            mainPanel.Controls.Add(numericPrice);

            // Stok Label
            var labelStock = new Label();
            labelStock.Text = "Stok:";
            labelStock.Location = new System.Drawing.Point(20, 190);
            labelStock.Size = new System.Drawing.Size(100, 20);
            labelStock.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelStock);

            var numericStock = new NumericUpDown();
            numericStock.Name = "numericStock";
            numericStock.Location = new System.Drawing.Point(130, 190);
            numericStock.Size = new System.Drawing.Size(150, 25);
            numericStock.Minimum = 0;
            numericStock.Maximum = 999999;
            numericStock.Tag = "Stock";
            mainPanel.Controls.Add(numericStock);

            // Kaydet Button
            var buttonSave = new Button();
            buttonSave.Text = "Kaydet";
            buttonSave.Location = new System.Drawing.Point(200, 250);
            buttonSave.Size = new System.Drawing.Size(100, 35);
            buttonSave.BackColor = System.Drawing.Color.Green;
            buttonSave.ForeColor = System.Drawing.Color.White;
            buttonSave.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            buttonSave.Click += (s, e) => SaveChanges(textBoxProductName.Text, textBoxDescription.Text, 
                                                       (decimal)numericPrice.Value, (int)numericStock.Value);
            mainPanel.Controls.Add(buttonSave);

            // İptal Button
            var buttonCancel = new Button();
            buttonCancel.Text = "İptal";
            buttonCancel.Location = new System.Drawing.Point(320, 250);
            buttonCancel.Size = new System.Drawing.Size(100, 35);
            buttonCancel.BackColor = System.Drawing.Color.Red;
            buttonCancel.ForeColor = System.Drawing.Color.White;
            buttonCancel.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            buttonCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            mainPanel.Controls.Add(buttonCancel);

            this.Controls.Add(mainPanel);
        }

        private void LoadProductData()
        {
            var textBoxProductName = this.Controls.Find("textBoxProductName", true).FirstOrDefault() as TextBox;
            var textBoxDescription = this.Controls.Find("textBoxDescription", true).FirstOrDefault() as TextBox;
            var numericPrice = this.Controls.Find("numericPrice", true).FirstOrDefault() as NumericUpDown;
            var numericStock = this.Controls.Find("numericStock", true).FirstOrDefault() as NumericUpDown;

            if (textBoxProductName != null) textBoxProductName.Text = _product.Name;
            if (textBoxDescription != null) textBoxDescription.Text = _product.Description;
            if (numericPrice != null) numericPrice.Value = _product.Price;
            if (numericStock != null) numericStock.Value = _product.Stock;
        }

        private void SaveChanges(string productName, string description, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Lütfen ürün adını girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _product.Name = productName;
                _product.Description = description;
                _product.Price = price;
                _product.Stock = stock;

                _productService.UpdateProduct(_product);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    partial class ProductEditForm
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "ProductEditForm";
            this.Load += new System.EventHandler(this.ProductEditForm_Load);
        }
    }
}
