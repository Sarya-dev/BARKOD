using System;
using System.Windows.Forms;
using BarkodOtomasyon.Models;
using BarkodOtomasyon.Services;

namespace BarkodOtomasyon
{
    public partial class SalesForm : Form
    {
        private readonly ProductService _productService;
        private Product _product;

        public SalesForm(ProductService productService, Product product)
        {
            InitializeComponent();
            _productService = productService;
            _product = product;
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            CreateUI();
        }

        private void CreateUI()
        {
            this.Text = "Satış İşlemi";
            this.Size = new System.Drawing.Size(450, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // Ürün Bilgisi
            var labelProductInfo = new Label();
            labelProductInfo.Text = $"Ürün: {_product.Name}";
            labelProductInfo.Location = new System.Drawing.Point(20, 20);
            labelProductInfo.Size = new System.Drawing.Size(400, 25);
            labelProductInfo.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            labelProductInfo.BackColor = System.Drawing.Color.LightBlue;
            labelProductInfo.Padding = new Padding(5);
            mainPanel.Controls.Add(labelProductInfo);

            // Mevcut Stok Label
            var labelCurrentStock = new Label();
            labelCurrentStock.Text = $"Mevcut Stok: {_product.Stock}";
            labelCurrentStock.Location = new System.Drawing.Point(20, 60);
            labelCurrentStock.Size = new System.Drawing.Size(400, 25);
            labelCurrentStock.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            labelCurrentStock.BackColor = System.Drawing.Color.LightYellow;
            labelCurrentStock.Padding = new Padding(5);
            mainPanel.Controls.Add(labelCurrentStock);

            // Satış Miktarı Label
            var labelQuantity = new Label();
            labelQuantity.Text = "Satış Miktarı:";
            labelQuantity.Location = new System.Drawing.Point(20, 100);
            labelQuantity.Size = new System.Drawing.Size(120, 20);
            labelQuantity.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelQuantity);

            // Satış Miktarı NumericUpDown
            var numericQuantity = new NumericUpDown();
            numericQuantity.Name = "numericQuantity";
            numericQuantity.Location = new System.Drawing.Point(150, 100);
            numericQuantity.Size = new System.Drawing.Size(100, 25);
            numericQuantity.Minimum = 1;
            numericQuantity.Maximum = _product.Stock;
            numericQuantity.Value = 1;
            mainPanel.Controls.Add(numericQuantity);

            // Toplam Fiyat Label
            var labelTotalPrice = new Label();
            labelTotalPrice.Text = "Toplam Fiyat:";
            labelTotalPrice.Location = new System.Drawing.Point(20, 140);
            labelTotalPrice.Size = new System.Drawing.Size(120, 20);
            labelTotalPrice.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            mainPanel.Controls.Add(labelTotalPrice);

            var textBoxTotalPrice = new TextBox();
            textBoxTotalPrice.Name = "textBoxTotalPrice";
            textBoxTotalPrice.Location = new System.Drawing.Point(150, 140);
            textBoxTotalPrice.Size = new System.Drawing.Size(100, 25);
            textBoxTotalPrice.ReadOnly = true;
            textBoxTotalPrice.BackColor = System.Drawing.Color.WhiteSmoke;
            textBoxTotalPrice.Text = (_product.Price * numericQuantity.Value).ToString("C2");
            mainPanel.Controls.Add(textBoxTotalPrice);

            numericQuantity.ValueChanged += (s, e) =>
            {
                textBoxTotalPrice.Text = (_product.Price * numericQuantity.Value).ToString("C2");
            };

            // Tamam Button
            var buttonOK = new Button();
            buttonOK.Text = "Satışı Tamamla";
            buttonOK.Location = new System.Drawing.Point(120, 200);
            buttonOK.Size = new System.Drawing.Size(120, 35);
            buttonOK.BackColor = System.Drawing.Color.Green;
            buttonOK.ForeColor = System.Drawing.Color.White;
            buttonOK.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            buttonOK.Click += (s, e) => CompleteSale((int)numericQuantity.Value);
            mainPanel.Controls.Add(buttonOK);

            // İptal Button
            var buttonCancel = new Button();
            buttonCancel.Text = "İptal";
            buttonCancel.Location = new System.Drawing.Point(260, 200);
            buttonCancel.Size = new System.Drawing.Size(100, 35);
            buttonCancel.BackColor = System.Drawing.Color.Red;
            buttonCancel.ForeColor = System.Drawing.Color.White;
            buttonCancel.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            buttonCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            mainPanel.Controls.Add(buttonCancel);

            this.Controls.Add(mainPanel);
        }

        private void CompleteSale(int quantity)
        {
            if (quantity > _product.Stock)
            {
                MessageBox.Show("Yeterli stok yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _product.Stock -= quantity;
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

    partial class SalesForm
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
            this.Text = "SalesForm";
            this.Load += new System.EventHandler(this.SalesForm_Load);
        }
    }
}
