using System;
using System.Windows.Forms;
using BarkodOtomasyon.Data;
using BarkodOtomasyon.Models;
using BarkodOtomasyon.Services;

namespace BarkodOtomasyon
{
    public partial class ProductAddForm : Form
    {
        private readonly BarcodeService _barcodeService;
        private readonly ProductService _productService;
        private string _barcodeCode;

        public ProductAddForm(BarcodeService barcodeService, ProductService productService, string barcodeCode)
        {
            InitializeComponent();
            _barcodeService = barcodeService;
            _productService = productService;
            _barcodeCode = barcodeCode;
        }

        private void ProductAddForm_Load(object sender, EventArgs e)
        {
            CreateUI();
        }

        private void CreateUI()
        {
            this.Text = "Yeni Ürün Ekle";
            this.Size = new System.Drawing.Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Panel for form
            var mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // Barkod Label
            var labelBarcode = new Label();
            labelBarcode.Text = "Barkod:";
            labelBarcode.Location = new System.Drawing.Point(20, 20);
            labelBarcode.Size = new System.Drawing.Size(100, 20);
            labelBarcode.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelBarcode);

            // Barkod TextBox (Read-only)
            var textBoxBarcode = new TextBox();
            textBoxBarcode.Name = "textBoxBarcode";
            textBoxBarcode.Location = new System.Drawing.Point(130, 20);
            textBoxBarcode.Size = new System.Drawing.Size(320, 25);
            textBoxBarcode.Text = _barcodeCode;
            textBoxBarcode.ReadOnly = true;
            textBoxBarcode.BackColor = System.Drawing.Color.LightGray;
            mainPanel.Controls.Add(textBoxBarcode);

            // Ürün Adı Label
            var labelProductName = new Label();
            labelProductName.Text = "Ürün Adı:";
            labelProductName.Location = new System.Drawing.Point(20, 60);
            labelProductName.Size = new System.Drawing.Size(100, 20);
            labelProductName.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelProductName);

            // Ürün Adı TextBox
            var textBoxProductName = new TextBox();
            textBoxProductName.Name = "textBoxProductName";
            textBoxProductName.Location = new System.Drawing.Point(130, 60);
            textBoxProductName.Size = new System.Drawing.Size(320, 25);
            mainPanel.Controls.Add(textBoxProductName);

            // Açıklama Label
            var labelDescription = new Label();
            labelDescription.Text = "Açıklama:";
            labelDescription.Location = new System.Drawing.Point(20, 100);
            labelDescription.Size = new System.Drawing.Size(100, 20);
            labelDescription.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelDescription);

            // Açıklama TextBox
            var textBoxDescription = new TextBox();
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Location = new System.Drawing.Point(130, 100);
            textBoxDescription.Size = new System.Drawing.Size(320, 80);
            textBoxDescription.Multiline = true;
            mainPanel.Controls.Add(textBoxDescription);

            // Fiyat Label
            var labelPrice = new Label();
            labelPrice.Text = "Fiyat (₺):";
            labelPrice.Location = new System.Drawing.Point(20, 190);
            labelPrice.Size = new System.Drawing.Size(100, 20);
            labelPrice.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelPrice);

            // Fiyat NumericUpDown
            var numericPrice = new NumericUpDown();
            numericPrice.Name = "numericPrice";
            numericPrice.Location = new System.Drawing.Point(130, 190);
            numericPrice.Size = new System.Drawing.Size(150, 25);
            numericPrice.DecimalPlaces = 2;
            numericPrice.Minimum = 0;
            numericPrice.Maximum = 99999;
            mainPanel.Controls.Add(numericPrice);

            // Stok Label
            var labelStock = new Label();
            labelStock.Text = "Stok:";
            labelStock.Location = new System.Drawing.Point(20, 230);
            labelStock.Size = new System.Drawing.Size(100, 20);
            labelStock.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelStock);

            // Stok NumericUpDown
            var numericStock = new NumericUpDown();
            numericStock.Name = "numericStock";
            numericStock.Location = new System.Drawing.Point(130, 230);
            numericStock.Size = new System.Drawing.Size(150, 25);
            numericStock.Minimum = 0;
            numericStock.Maximum = 999999;
            mainPanel.Controls.Add(numericStock);

            // Kaydet Button
            var buttonSave = new Button();
            buttonSave.Text = "Kaydet";
            buttonSave.Location = new System.Drawing.Point(200, 290);
            buttonSave.Size = new System.Drawing.Size(100, 35);
            buttonSave.BackColor = System.Drawing.Color.Green;
            buttonSave.ForeColor = System.Drawing.Color.White;
            buttonSave.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            buttonSave.Click += (s, e) => SaveProduct(textBoxProductName.Text, textBoxDescription.Text, 
                                                       (decimal)numericPrice.Value, (int)numericStock.Value);
            mainPanel.Controls.Add(buttonSave);

            // İptal Button
            var buttonCancel = new Button();
            buttonCancel.Text = "İptal";
            buttonCancel.Location = new System.Drawing.Point(320, 290);
            buttonCancel.Size = new System.Drawing.Size(100, 35);
            buttonCancel.BackColor = System.Drawing.Color.Red;
            buttonCancel.ForeColor = System.Drawing.Color.White;
            buttonCancel.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            buttonCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            mainPanel.Controls.Add(buttonCancel);

            this.Controls.Add(mainPanel);
        }

        private void SaveProduct(string productName, string description, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Lütfen ürün adını girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Yeni Barkod oluştur
                var newBarcode = new Barcode { Code = _barcodeCode };
                _barcodeService.AddBarcode(newBarcode);

                // Yeni Ürün oluştur
                var product = new Product
                {
                    Name = productName,
                    Description = description,
                    Price = price,
                    Stock = stock,
                    BarcodeId = newBarcode.Id,
                    CreatedDate = DateTime.Now
                };
                _productService.AddProduct(product);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    partial class ProductAddForm
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
            this.Text = "ProductAddForm";
            this.Load += new System.EventHandler(this.ProductAddForm_Load);
        }
    }
}
