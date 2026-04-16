using System;
using System.Linq;
using System.Windows.Forms;
using BarkodOtomasyon.Services;

namespace BarkodOtomasyon
{
    public partial class ReportForm : Form
    {
        private readonly ProductService _productService;
        private readonly BarcodeService _barcodeService;

        public ReportForm(ProductService productService, BarcodeService barcodeService)
        {
            InitializeComponent();
            _productService = productService;
            _barcodeService = barcodeService;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            CreateUI();
            LoadReport();
        }

        private void CreateUI()
        {
            this.Text = "Stok ve Satış Raporu";
            this.Size = new System.Drawing.Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.AutoScaleMode = AutoScaleMode.Font;

            // Tab Control
            var tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Padding = new System.Drawing.Point(10, 10);

            // Tab 1: Stok Raporu
            var tabStock = new TabPage("Stok Raporu");
            var dataGridStock = new DataGridView();
            dataGridStock.Name = "dataGridStock";
            dataGridStock.Dock = DockStyle.Fill;
            dataGridStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridStock.ReadOnly = true;
            dataGridStock.AllowUserToAddRows = false;

            dataGridStock.Columns.Add("Barkod", "Barkod");
            dataGridStock.Columns.Add("UrunAdi", "Ürün Adı");
            dataGridStock.Columns.Add("Fiyat", "Birim Fiyat");
            dataGridStock.Columns.Add("Stok", "Stok Miktarı");
            dataGridStock.Columns.Add("Toplam", "Toplam Değer");

            tabStock.Controls.Add(dataGridStock);
            tabControl.TabPages.Add(tabStock);

            // Tab 2: Ürün Listesi
            var tabProducts = new TabPage("Ürün Listesi");
            var dataGridProducts = new DataGridView();
            dataGridProducts.Name = "dataGridProducts";
            dataGridProducts.Dock = DockStyle.Fill;
            dataGridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridProducts.ReadOnly = true;
            dataGridProducts.AllowUserToAddRows = false;

            dataGridProducts.Columns.Add("Id", "ID");
            dataGridProducts.Columns.Add("Barkod", "Barkod");
            dataGridProducts.Columns.Add("UrunAdi", "Ürün Adı");
            dataGridProducts.Columns.Add("Aciklama", "Açıklama");
            dataGridProducts.Columns.Add("Fiyat", "Fiyat");
            dataGridProducts.Columns.Add("Stok", "Stok");

            tabProducts.Controls.Add(dataGridProducts);
            tabControl.TabPages.Add(tabProducts);

            // Tab 3: Özet
            var tabSummary = new TabPage("Özet");
            var summaryPanel = new Panel();
            summaryPanel.Dock = DockStyle.Fill;
            summaryPanel.Padding = new Padding(20);
            tabSummary.Controls.Add(summaryPanel);
            tabControl.TabPages.Add(tabSummary);

            this.Controls.Add(tabControl);
        }

        private void LoadReport()
        {
            var products = _productService.GetAllProducts();
            var barcodes = _barcodeService.GetAllBarcodes().ToDictionary(b => b.Id, b => b.Code);

            // Stok Raporu
            var dataGridStock = this.Controls.Find("dataGridStock", true).FirstOrDefault() as DataGridView;
            if (dataGridStock != null)
            {
                dataGridStock.Rows.Clear();
                decimal totalValue = 0;
                int totalStock = 0;

                foreach (var product in products)
                {
                    var barcode = barcodes.ContainsKey(product.BarcodeId) ? barcodes[product.BarcodeId] : "N/A";
                    decimal itemValue = product.Price * product.Stock;
                    totalValue += itemValue;
                    totalStock += product.Stock;

                    dataGridStock.Rows.Add(
                        barcode,
                        product.Name,
                        product.Price.ToString("C2"),
                        product.Stock,
                        itemValue.ToString("C2")
                    );
                }

                dataGridStock.Rows.Add("", "TOPLAM", "", totalStock.ToString(), totalValue.ToString("C2"));
            }

            // Ürün Listesi
            var dataGridProducts = this.Controls.Find("dataGridProducts", true).FirstOrDefault() as DataGridView;
            if (dataGridProducts != null)
            {
                dataGridProducts.Rows.Clear();

                foreach (var product in products)
                {
                    var barcode = barcodes.ContainsKey(product.BarcodeId) ? barcodes[product.BarcodeId] : "N/A";
                    dataGridProducts.Rows.Add(
                        product.Id,
                        barcode,
                        product.Name,
                        product.Description,
                        product.Price.ToString("C2"),
                        product.Stock
                    );
                }
            }

            // Özet
            var summaryPanel = this.Controls.Find("summaryPanel", true).FirstOrDefault() as Panel;
            if (summaryPanel != null)
            {
                summaryPanel.Controls.Clear();

                var totalProducts = products.Count;
                var lowStockProducts = products.Count(p => p.Stock < 10);
                var totalValue = products.Sum(p => p.Price * p.Stock);
                var totalStock = products.Sum(p => p.Stock);

                var labels = new string[]
                {
                    $"Toplam Ürün: {totalProducts}",
                    $"Düşük Stok (< 10): {lowStockProducts}",
                    $"Toplam Stok: {totalStock}",
                    $"Depo Değeri: {totalValue:C2}",
                    $"Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm:ss}"
                };

                int yPos = 20;
                foreach (var labelText in labels)
                {
                    var label = new Label();
                    label.Text = labelText;
                    label.Location = new System.Drawing.Point(20, yPos);
                    label.Size = new System.Drawing.Size(500, 30);
                    label.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                    label.BackColor = System.Drawing.Color.LightGray;
                    label.Padding = new Padding(5);
                    summaryPanel.Controls.Add(label);
                    yPos += 50;
                }
            }
        }
    }

    partial class ReportForm
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
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
        }
    }
}
