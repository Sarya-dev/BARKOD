using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarkodOtomasyon.Data;
using BarkodOtomasyon.Models;
using BarkodOtomasyon.Services;

namespace BarkodOtomasyon
{
    public partial class Form1 : Form
    {
        private readonly BarcodeService _barcodeService;
        private readonly ProductService _productService;
        private TextBox textBoxBarcode;
        private DataGridView dataGridViewProducts;
        private Label labelInfo;

        public Form1(BarcodeService barcodeService, ProductService productService)
        {
            InitializeComponent();
            _barcodeService = barcodeService;
            _productService = productService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateUI();
            RefreshProductList();
            textBoxBarcode.Focus();
        }

        private void CreateUI()
        {
            this.Text = "Barkod Okuma Sistemi";
            this.Size = new System.Drawing.Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.Font;

            // Panel for barcode input
            var panelBarcode = new Panel();
            panelBarcode.Location = new System.Drawing.Point(10, 10);
            panelBarcode.Size = new System.Drawing.Size(1070, 90);
            panelBarcode.BorderStyle = BorderStyle.FixedSingle;

            var labelBarcode = new Label();
            labelBarcode.Text = "Barkod Okut:";
            labelBarcode.Location = new System.Drawing.Point(10, 10);
            labelBarcode.Size = new System.Drawing.Size(100, 20);
            labelBarcode.Font = new Font("Arial", 10, FontStyle.Bold);
            panelBarcode.Controls.Add(labelBarcode);

            textBoxBarcode = new TextBox();
            textBoxBarcode.Name = "textBoxBarcode";
            textBoxBarcode.Location = new System.Drawing.Point(120, 10);
            textBoxBarcode.Size = new System.Drawing.Size(250, 20);
            textBoxBarcode.KeyDown += TextBoxBarcode_KeyDown;
            panelBarcode.Controls.Add(textBoxBarcode);

            var buttonSearch = new Button();
            buttonSearch.Text = "Ara";
            buttonSearch.Location = new System.Drawing.Point(380, 10);
            buttonSearch.Size = new System.Drawing.Size(80, 25);
            buttonSearch.Click += (s, e) => SearchBarcode(textBoxBarcode.Text);
            panelBarcode.Controls.Add(buttonSearch);

            var buttonAdd = new Button();
            buttonAdd.Text = "Yeni Ürün Ekle";
            buttonAdd.Location = new System.Drawing.Point(470, 10);
            buttonAdd.Size = new System.Drawing.Size(120, 25);
            buttonAdd.Click += (s, e) => AddNewProduct(textBoxBarcode.Text);
            panelBarcode.Controls.Add(buttonAdd);

            var buttonRefresh = new Button();
            buttonRefresh.Text = "Yenile";
            buttonRefresh.Location = new System.Drawing.Point(600, 10);
            buttonRefresh.Size = new System.Drawing.Size(80, 25);
            buttonRefresh.Click += (s, e) => RefreshProductList();
            panelBarcode.Controls.Add(buttonRefresh);

            var buttonReport = new Button();
            buttonReport.Text = "Rapor";
            buttonReport.Location = new System.Drawing.Point(690, 10);
            buttonReport.Size = new System.Drawing.Size(80, 25);
            buttonReport.Click += (s, e) => ShowReport();
            panelBarcode.Controls.Add(buttonReport);

            labelInfo = new Label();
            labelInfo.Name = "labelInfo";
            labelInfo.Location = new System.Drawing.Point(10, 45);
            labelInfo.Size = new System.Drawing.Size(1050, 35);
            labelInfo.AutoSize = false;
            labelInfo.Text = "Barkod okutun veya manuel olarak girin. ENTER tuşu ile ara.";
            labelInfo.Font = new Font("Arial", 9);
            panelBarcode.Controls.Add(labelInfo);

            this.Controls.Add(panelBarcode);

            // DataGridView for products
            dataGridViewProducts = new DataGridView();
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.Location = new System.Drawing.Point(10, 110);
            dataGridViewProducts.Size = new System.Drawing.Size(1070, 450);
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.ReadOnly = true;
            dataGridViewProducts.AllowUserToAddRows = false;
            dataGridViewProducts.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridViewProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridViewProducts.MultiSelect = false;

            dataGridViewProducts.Columns.Add("Id", "ID");
            dataGridViewProducts.Columns.Add("Barkod", "Barkod");
            dataGridViewProducts.Columns.Add("UrunAdi", "Ürün Adı");
            dataGridViewProducts.Columns.Add("Aciklama", "Açıklama");
            dataGridViewProducts.Columns.Add("Fiyat", "Fiyat");
            dataGridViewProducts.Columns.Add("Stok", "Stok");

            this.Controls.Add(dataGridViewProducts);

            // Action Panel
            var panelActions = new Panel();
            panelActions.Location = new System.Drawing.Point(10, 570);
            panelActions.Size = new System.Drawing.Size(1070, 60);
            panelActions.BorderStyle = BorderStyle.FixedSingle;

            var buttonEdit = new Button();
            buttonEdit.Text = "Düzenle";
            buttonEdit.Location = new System.Drawing.Point(10, 15);
            buttonEdit.Size = new System.Drawing.Size(100, 30);
            buttonEdit.BackColor = System.Drawing.Color.Orange;
            buttonEdit.ForeColor = System.Drawing.Color.White;
            buttonEdit.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            buttonEdit.Click += (s, e) => EditSelectedProduct();
            panelActions.Controls.Add(buttonEdit);

            var buttonSale = new Button();
            buttonSale.Text = "Satış";
            buttonSale.Location = new System.Drawing.Point(120, 15);
            buttonSale.Size = new System.Drawing.Size(100, 30);
            buttonSale.BackColor = System.Drawing.Color.Blue;
            buttonSale.ForeColor = System.Drawing.Color.White;
            buttonSale.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            buttonSale.Click += (s, e) => ProcessSale();
            panelActions.Controls.Add(buttonSale);

            var buttonDelete = new Button();
            buttonDelete.Text = "Sil";
            buttonDelete.Location = new System.Drawing.Point(230, 15);
            buttonDelete.Size = new System.Drawing.Size(100, 30);
            buttonDelete.BackColor = System.Drawing.Color.Crimson;
            buttonDelete.ForeColor = System.Drawing.Color.White;
            buttonDelete.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            buttonDelete.Click += (s, e) => DeleteSelectedProduct();
            panelActions.Controls.Add(buttonDelete);

            var labelSelected = new Label();
            labelSelected.Name = "labelSelected";
            labelSelected.Text = "Seçili ürün: Yok";
            labelSelected.Location = new System.Drawing.Point(400, 15);
            labelSelected.Size = new System.Drawing.Size(600, 30);
            labelSelected.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic);
            panelActions.Controls.Add(labelSelected);

            this.Controls.Add(panelActions);

            dataGridViewProducts.RowEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridViewProducts.Rows.Count)
                {
                    var row = dataGridViewProducts.Rows[e.RowIndex];
                    var productName = row.Cells["UrunAdi"].Value?.ToString() ?? "Bilinmiyor";
                    labelSelected.Text = $"Seçili ürün: {productName}";
                }
            };
        }

        private void TextBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                SearchBarcode(textBoxBarcode.Text);
            }
        }

        private void RefreshProductList()
        {
            if (dataGridViewProducts == null) return;

            dataGridViewProducts.Rows.Clear();

            try
            {
                var products = _productService.GetAllProducts();
                var barcodes = _barcodeService.GetAllBarcodes().ToDictionary(b => b.Id, b => b.Code);

                foreach (var product in products)
                {
                    var barcode = barcodes.ContainsKey(product.BarcodeId) ? barcodes[product.BarcodeId] : "N/A";
                    dataGridViewProducts.Rows.Add(
                        product.Id,
                        barcode,
                        product.Name,
                        product.Description,
                        product.Price.ToString("C2"),
                        product.Stock
                    );
                }

                labelInfo.Text = $"Toplam {products.Count} ürün bulunuyor.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchBarcode(string barcodeCode)
        {
            if (string.IsNullOrWhiteSpace(barcodeCode))
            {
                MessageBox.Show("Lütfen barkod girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var barcode = _barcodeService.GetAllBarcodes().FirstOrDefault(b => b.Code == barcodeCode.Trim());

                if (barcode == null)
                {
                    labelInfo.ForeColor = Color.Red;
                    labelInfo.Text = $"'{barcodeCode}' barcodu bulunamadı!";
                    return;
                }

                var product = _productService.GetAllProducts().FirstOrDefault(p => p.BarcodeId == barcode.Id);

                if (product != null)
                {
                    labelInfo.ForeColor = Color.Green;
                    labelInfo.Text = $"✓ Bulundu: {product.Name} - Fiyat: {product.Price:C2} - Stok: {product.Stock}";
                    
                    MessageBox.Show(
                        $"Ürün: {product.Name}\n" +
                        $"Fiyat: {product.Price:C2}\n" +
                        $"Stok: {product.Stock}\n" +
                        $"Açıklama: {product.Description}",
                        "Ürün Bilgisi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                textBoxBarcode.Clear();
                textBoxBarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewProduct(string barcodeCode)
        {
            if (string.IsNullOrWhiteSpace(barcodeCode))
            {
                MessageBox.Show("Lütfen barkod girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            barcodeCode = barcodeCode.Trim();

            try
            {
                var existingBarcode = _barcodeService.GetAllBarcodes().FirstOrDefault(b => b.Code == barcodeCode);
                if (existingBarcode != null)
                {
                    MessageBox.Show("Bu barkod zaten kayıtlı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ProductAddForm'u aç
                using (var addForm = new ProductAddForm(_barcodeService, _productService, barcodeCode))
                {
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        textBoxBarcode.Clear();
                        textBoxBarcode.Focus();
                        MessageBox.Show("Ürün başarıyla eklendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditSelectedProduct()
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek için bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var row = dataGridViewProducts.SelectedRows[0];
                int productId = (int)row.Cells["Id"].Value;

                var product = _productService.GetProductById(productId);
                if (product == null)
                {
                    MessageBox.Show("Ürün bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var editForm = new ProductEditForm(_productService, product))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        MessageBox.Show("Ürün başarıyla güncellendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessSale()
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen satış yapmak için bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var row = dataGridViewProducts.SelectedRows[0];
                int productId = (int)row.Cells["Id"].Value;

                var product = _productService.GetProductById(productId);
                if (product == null)
                {
                    MessageBox.Show("Ürün bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (product.Stock <= 0)
                {
                    MessageBox.Show("Bu ürünün stoku yoktur!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var salesForm = new SalesForm(_productService, product))
                {
                    if (salesForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        MessageBox.Show("Satış başarıyla tamamlandı!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedProduct()
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var row = dataGridViewProducts.SelectedRows[0];
                int productId = (int)row.Cells["Id"].Value;
                string productName = row.Cells["UrunAdi"].Value?.ToString() ?? "Bilinmiyor";

                var result = MessageBox.Show(
                    $"'{productName}' ürününü silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    _productService.DeleteProduct(productId);
                    RefreshProductList();
                    MessageBox.Show("Ürün başarıyla silindi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowReport()
        {
            try
            {
                using (var reportForm = new ReportForm(_productService, _barcodeService))
                {
                    reportForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}