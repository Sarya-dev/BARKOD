using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services;

namespace StokTakipSistemi
{
    public partial class Form1 : Form
    {
        Dictionary<string, DataTable> sayfaTablolar = new Dictionary<string, DataTable>();
        string excelDosyaYolu = "lastikpark.xlsx";
        List<string> depoAdlari = new List<string> { "Depo 1", "Depo 2", "Depo 3", "Depo 4", "Depo 5" };

        // Geri al (Undo) için yığın
        private Stack<Dictionary<string, DataTable>> undoStack = new Stack<Dictionary<string, DataTable>>();

        // Arama modu takibi
        private bool aramaModuAktif = false;

        // Services
        private readonly BarcodeService _barcodeService;
        private readonly ProductService _productService;
        private readonly StockService _stockService;
        private readonly DepotService _depotService;

        public Form1(BarcodeService barcodeService, ProductService productService, StockService stockService, DepotService depotService)
        {
            _barcodeService = barcodeService;
            _productService = productService;
            _stockService = stockService;
            _depotService = depotService;
            
            InitializeComponent();
            
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Form ayarları
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                this.Size = new System.Drawing.Size(1400, 800);
                this.MinimumSize = new System.Drawing.Size(800, 600);
                this.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                this.StartPosition = FormStartPosition.CenterScreen;
                
                // Panel boyutlarını ayarla
                if (panel1 != null)
                {
                    panel1.Width = 320;
                    panel1.Dock = DockStyle.Left;
                }
                
                // UI ayarları
                SetupResponsiveLayout();
                AddTextBoxBorders();
                FixControlLayout();
                SetupLabels();
                
                // Depo seçimi hazırla
                InitializeDepots();
                
                // Event handlers
                if (dataGridView1 != null)
                {
                    dataGridView1.CellClick += dataGridView1_CellClick;
                    dataGridView1.SelectionChanged += dataGridView1_SelectionChanged!;
                    dataGridView1.CellMouseEnter += dataGridView1_CellMouseEnter!;
                    dataGridView1.CellMouseLeave += dataGridView1_CellMouseLeave!;
                    dataGridView1.CellFormatting += dataGridView1_CellFormatting;
                }
                if (textBox1 != null)
                {
                    // KeyDown event kaldırıldı - ENTER işlemesi yapılmıyor
                }
                if (comboBoxSayfaSec != null)
                    comboBoxSayfaSec.SelectedIndexChanged += comboBoxSayfaSec_SelectedIndexChanged;
                if (comboBoxMevsimFiltre != null)
                    comboBoxMevsimFiltre.SelectedIndexChanged += (s, e2) => RefreshGrid();
                
                ApplyModernStyling();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Form1_Load Error: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Başlatma Hatası:\n\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FixControlLayout()
        {
            if (panel1 == null) return;
            panel1.Dock = DockStyle.Left;
            panel1.Width = 320;
            panel1.AutoScroll = true;
            panel1.Height = this.ClientSize.Height - 120; // Panel3 için yer bırak

            int y = 10;
            int controlHeight = 30;
            int padding = 10;
            int labelWidth = 90;
            int inputWidth = 220;
            int leftMargin = 10;

            try { label1.Location = new Point(leftMargin, y); label1.Size = new Size(labelWidth, 21); label1.Text = "Ürün Kodu:"; } catch { }
            y += 25;
            try { textBox1.Location = new Point(leftMargin, y); textBox1.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { label2.Location = new Point(leftMargin, y); label2.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { textBox2.Location = new Point(leftMargin, y); textBox2.Size = new Size(100, controlHeight); } catch { }
            try { labelMevsim.Location = new Point(115, y - 5); labelMevsim.Size = new Size(72, 19); } catch { }
            try { comboBoxMevsim.Location = new Point(115, y); comboBoxMevsim.Size = new Size(115, controlHeight); } catch { }
            y += controlHeight + padding;

            try { label3.Location = new Point(leftMargin, y); label3.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { richTextBox1.Location = new Point(leftMargin, y); richTextBox1.Size = new Size(inputWidth, 40); } catch { }
            y += 50 + padding;

            try { labelSeri.Location = new Point(leftMargin, y); labelSeri.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { textBoxSeri.Location = new Point(leftMargin, y); textBoxSeri.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelFiyat.Location = new Point(leftMargin, y); labelFiyat.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { textBoxFiyat.Location = new Point(leftMargin, y); textBoxFiyat.Size = new Size(100, controlHeight); } catch { }
            try { labelKDV.Location = new Point(115, y - 5); labelKDV.Size = new Size(37, 19); } catch { }
            try { comboBoxKDV.Location = new Point(115, y); comboBoxKDV.Size = new Size(115, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelTarih.Location = new Point(leftMargin, y); labelTarih.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { textBoxTarih.Location = new Point(leftMargin, y); textBoxTarih.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelTelefon.Location = new Point(leftMargin, y); labelTelefon.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { textBoxTelefon.Location = new Point(leftMargin, y); textBoxTelefon.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelGelen.Location = new Point(leftMargin, y); labelGelen.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { numericGelen.Location = new Point(leftMargin, y); numericGelen.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelGiden.Location = new Point(leftMargin, y); labelGiden.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { numericGiden.Location = new Point(leftMargin, y); numericGiden.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelKalanStok.Location = new Point(leftMargin, y); labelKalanStok.Size = new Size(labelWidth, 21); } catch { }
            try { labelKalanStokValue.Location = new Point(leftMargin + labelWidth, y); labelKalanStokValue.Size = new Size(100, 30); } catch { }
            y += controlHeight + padding;

            try { labelSiparis.Location = new Point(leftMargin, y); labelSiparis.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { numericSiparis.Location = new Point(leftMargin, y); numericSiparis.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding;

            try { labelGelis.Location = new Point(leftMargin, y); labelGelis.Size = new Size(labelWidth, 21); } catch { }
            y += 25;
            try { dateTimeGelis.Location = new Point(leftMargin, y); dateTimeGelis.Size = new Size(inputWidth, controlHeight); } catch { }
            y += controlHeight + padding + 10;

            try { button_kaydet.Location = new Point(leftMargin, y); button_kaydet.Size = new Size(100, 35); } catch { }
            try { button_sil.Location = new Point(115, y); button_sil.Size = new Size(100, 35); } catch { }
            y += 45;
            try { button1.Location = new Point(leftMargin, y); button1.Size = new Size(100, 35); button1.Text = "✏️ Güncelle"; } catch { }
            y += 45;

            // Panel2 (Grid panel) yerleştir - panel1 yanından başla, panel3 için yer bırak
            if (panel2 != null)
            {
                panel2.Location = new Point(320, 0);
                panel2.Size = new Size(this.ClientSize.Width - 320, this.ClientSize.Height - 105);
            }

            // Panel3 kontrol yerleşimi
            if (panel3 != null)
            {
                panel3.Location = new Point(321, this.ClientSize.Height - 115);
                panel3.Size = new Size(this.ClientSize.Width - 335, 105);
                
                int p3x = 15;
                int p3y = 10;

                try { labelSayfa.Location = new Point(p3x, p3y); labelSayfa.Size = new Size(80, 21); labelSayfa.Text = "Depo:"; labelSayfa.Parent = panel3; } catch { }
                try { comboBoxSayfaSec.Location = new Point(p3x + 90, p3y); comboBoxSayfaSec.Size = new Size(150, 28); comboBoxSayfaSec.Parent = panel3; } catch { }

                try { labelMevsimFiltre.Location = new Point(p3x + 250, p3y); labelMevsimFiltre.Size = new Size(72, 21); labelMevsimFiltre.Text = "Mevsim:"; labelMevsimFiltre.Parent = panel3; } catch { }
                try { comboBoxMevsimFiltre.Location = new Point(p3x + 330, p3y); comboBoxMevsimFiltre.Size = new Size(120, 28); comboBoxMevsimFiltre.Parent = panel3; } catch { }

                p3y += 40;
                try { lblAra.Location = new Point(p3x, p3y); lblAra.Size = new Size(150, 21); lblAra.Text = "Ara (Barkod/Ürün):"; lblAra.Parent = panel3; } catch { }
                try { txtAra.Location = new Point(p3x + 160, p3y); txtAra.Size = new Size(250, 28); txtAra.Parent = panel3; } catch { }
                
                // Ara butonu
                try 
                { 
                    Button btnAra = new Button();
                    btnAra.Text = "🔍 Ara";
                    btnAra.Location = new Point(p3x + 420, p3y);
                    btnAra.Size = new Size(50, 28);
                    btnAra.Click += (s, e) => RefreshGrid();
                    panel3.Controls.Add(btnAra);
                } 
                catch { }
                
                // Excel'e Aktar butonu
                try 
                { 
                    Button btnExcel = new Button();
                    btnExcel.Text = "📊 Excel";
                    btnExcel.Location = new Point(p3x + 475, p3y);
                    btnExcel.Size = new Size(70, 28);
                    btnExcel.Click += (s, e) => ExportToExcel();
                    panel3.Controls.Add(btnExcel);
                } 
                catch { }
                
                try { labelKayitSayisi.Location = new Point(p3x + 555, p3y); labelKayitSayisi.Size = new Size(200, 21); labelKayitSayisi.Parent = panel3; } catch { }
            }
        }

        private void SetupLabels()
        {
            if (labelSayfa != null)
                labelSayfa.Text = "Depo Seçimi:";
            if (lblAra != null)
                lblAra.Text = "Ara: Barkod, Ürün Adı, Marka, Ebat:" ;
        }

        private void SetupTextBoxes()
        {
            if (textBox1 == null) return;
            
            textBox1.Text = "Barkod Okut veya Gir (ENTER ile ara)";
            textBox1.ForeColor = Color.Gray;
            
            textBox1.GotFocus += (s, e) =>
            {
                if (textBox1.Text == "Barkod Okut veya Gir (ENTER ile ara)")
                {
                    textBox1.Text = "";
                    textBox1.ForeColor = Color.Black;
                }
            };
            
            textBox1.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    textBox1.Text = "Barkod Okut veya Gir (ENTER ile ara)";
                    textBox1.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupResponsiveLayout()
        {
            // Panel1 sol tarafta sabitlensinde dikeylik kontrol
            if (panel1 != null)
            {
                panel1.Dock = DockStyle.Left;
                panel1.AutoScroll = true;
            }

            // Panel2 orta alanda - elle yerleştirilecek (FixControlLayout'ta)
            if (panel2 != null)
            {
                panel2.Dock = DockStyle.None;
            }

            // Panel3 alt tarafta
            if (panel3 != null)
            {
                panel3.Dock = DockStyle.Bottom;
                panel3.Height = 100;
            }

            // DataGridView1 responsive - panel2'yi tamamen doldur
            if (dataGridView1 != null)
            {
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Parent = panel2;
            }

            // StatusStrip alt tarafında sabitlensinde
            if (statusStrip1 != null)
            {
                statusStrip1.Dock = DockStyle.Bottom;
            }
        }

        private void InitializeDepots()
        {
            var depots = _depotService.GetAllDepots();
            if (depots.Count == 0)
            {
                // Varsayılan depoları oluştur
                foreach (var depotName in depoAdlari)
                {
                    _depotService.AddDepot(new Depot { Name = depotName });
                }
                depots = _depotService.GetAllDepots();
            }

            comboBoxSayfaSec.Items.Clear();
            foreach (var depot in depots)
            {
                comboBoxSayfaSec.Items.Add(depot);
            }
            
            if (comboBoxSayfaSec.Items.Count > 0)
            {
                comboBoxSayfaSec.SelectedIndex = 0;
                RefreshGrid(); // Seçimi set ettikten sonra tetikle
            }

            comboBoxSayfaSec.SelectedIndexChanged += comboBoxSayfaSec_SelectedIndexChanged;

            // Mevsim filtresi seçeneklerini doldur
            if (comboBoxMevsimFiltre != null)
            {
                comboBoxMevsimFiltre.Items.Clear();
                comboBoxMevsimFiltre.Items.Add("Tümü");
                comboBoxMevsimFiltre.Items.Add("Yaz");
                comboBoxMevsimFiltre.Items.Add("Kış");
                comboBoxMevsimFiltre.Items.Add("4 Mevsim");
                comboBoxMevsimFiltre.SelectedIndex = 0;
            }
        }

        // Modern görünüm ayarları
        private void ApplyModernStyling()
        {
            // Form kenar yuvarlatma efekti kaldırıldı - Normal Windows görünümü
            // this.FormBorderStyle = FormBorderStyle.None;  // Bu satır kaldırıldı
            this.BackColor = Color.White;

            // TextBox'lara çerçeve ekle
            AddTextBoxBorders();
        }

        private void AddTextBoxBorders()
        {
            try
            {
                if (textBox1 != null)
                    textBox1.Paint += TextBox_Paint!;
                if (textBox2 != null)
                    textBox2.Paint += TextBox_Paint!;
                if (txtAra != null)
                    txtAra.Paint += TextBox_Paint!;
                
                // TextBox1 - Barkod Okuma Alanı
                if (textBox1 != null)
                {
                    textBox1.Name = "BarcodeInput";
                    if (textBox1.PlaceholderText == null || string.IsNullOrEmpty(textBox1.PlaceholderText))
                    {
                        textBox1.Text = "Barkod, ürün adı, marka, ebat ile ara (ENTER ile ara)";
                        textBox1.ForeColor = Color.Gray;
                    }
                    textBox1.KeyDown += TextBox1_BarcodeInput_KeyDown;
                    textBox1.GotFocus += (s, e) =>
                    {
                        if (textBox1.Text == "Barkod, ürün adı, marka, ebat ile ara (ENTER)")
                        {
                            textBox1.Text = "";
                            textBox1.ForeColor = Color.Black;
                        }
                    };
                    textBox1.LostFocus += (s, e) =>
                    {
                        if (string.IsNullOrWhiteSpace(textBox1.Text))
                        {
                            textBox1.Text = "Barkod, ürün adı, marka, ebat ile ara (ENTER)";
                            textBox1.ForeColor = Color.Gray;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddTextBoxBorders error: {ex.Message}");
            }
        }

        private void TextBox1_BarcodeInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                SearchByBarcode(textBox1.Text.Trim());
            }
        }

        private void SearchByBarcode(string barcodeCode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcodeCode) || barcodeCode == "Barkod, ürün adı, marka, ebat ile ara (ENTER)")
                {
                    MessageBox.Show("Lütfen barkod girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Veritabanından ürünü bul
                var product = _stockService.GetProductByBarcode(barcodeCode);
                if (product == null)
                {
                    MessageBox.Show($"Barkod '{barcodeCode}' bulunamadı.\n\nYeni ürün eklemek ister misiniz?", "Ürün Bulunamadı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return;
                }

                // Seçili depoyu al
                var selectedDepot = comboBoxSayfaSec.SelectedItem as Depot;
                if (selectedDepot == null)
                {
                    MessageBox.Show("Lütfen bir depo seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ürün stoğunu seçili depoya ekle
                _stockService.AddStock(product.Id, selectedDepot.Id, 1, $"Barkod: {barcodeCode}");

                // Ürün bilgisi göster
                MessageBox.Show($"✓ Ürün Eklendi:\n\nAdı: {product.Name}\nMarka: {product.Brand}\nFiyat: {product.Price:C}\n\nDepo: {selectedDepot.Name}\nStok: +1", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Grid'i güncelle
                RefreshGrid();

                // TextBox temizle
                textBox1.Text = "";
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Barkod işlemi hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, textBox.Width - 1, textBox.Height - 1);
                }
            }
        }

        private void comboBoxSayfaSec_SelectedIndexChanged(object? sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                var selectedDepot = comboBoxSayfaSec.SelectedItem as Depot;
                if (selectedDepot == null)
                    return;

                var products = _stockService.GetProductsByDepot(selectedDepot.Id);
                
                // Arama metni
                string searchText = (txtAra?.Text ?? "").ToLower().Trim();
                
                // Mevsim filtresi
                string mevsimSelected = (comboBoxMevsimFiltre?.SelectedItem?.ToString() ?? "").Trim();
                string mevsimFilter = (mevsimSelected == "Tümü" || string.IsNullOrEmpty(mevsimSelected)) 
                    ? "" 
                    : mevsimSelected.ToLower();

                // Ürünleri filtrele
                var filteredProducts = products.Where(p =>
                {
                    // Arama metni kontrolü
                    bool matchesSearch = string.IsNullOrEmpty(searchText) || 
                        (p.Name?.ToLower() ?? "").Contains(searchText) ||
                        (p.Brand?.ToLower() ?? "").Contains(searchText) ||
                        (p.Size?.ToLower() ?? "").Contains(searchText) ||
                        (p.Season?.ToLower() ?? "").Contains(searchText) ||
                        (p.Series?.ToLower() ?? "").Contains(searchText) ||
                        (_barcodeService.GetBarcode(p.BarcodeId)?.Code?.ToLower() ?? "").Contains(searchText);

                    // Mevsim filtresi kontrolü
                    bool matchesSeasonFilter = string.IsNullOrEmpty(mevsimFilter) || 
                        (p.Season?.ToLower() ?? "").Contains(mevsimFilter);

                    return matchesSearch && matchesSeasonFilter;
                }).ToList();

                // DataTable oluştur
                var dataTable = new DataTable();
                dataTable.Columns.Add("Barkod");
                dataTable.Columns.Add("Ürün Adı");
                dataTable.Columns.Add("Marka");
                dataTable.Columns.Add("Ebat");
                dataTable.Columns.Add("Mevsim");
                dataTable.Columns.Add("Seri");
                dataTable.Columns.Add("Fiyat");
                dataTable.Columns.Add("Stok");

                // Filtrelenmiş ürünleri ekle
                foreach (var product in filteredProducts)
                {
                    var barcode = _barcodeService.GetBarcode(product.BarcodeId);
                    var stock = _stockService.GetStockByProductAndDepot(product.Id, selectedDepot.Id);
                    
                    dataTable.Rows.Add(
                        barcode?.Code ?? "",
                        product.Name ?? "",
                        product.Brand ?? "",
                        product.Size ?? "",
                        product.Season ?? "",
                        product.Series ?? "",
                        product.Price,
                        stock
                    );
                }

                dataGridView1.DataSource = dataTable;
                
                // Sonuç sayısını göster
                string resultText = filteredProducts.Count == products.Count 
                    ? $"Toplam: {products.Count} ürün"
                    : $"Toplam: {filteredProducts.Count} / {products.Count} ürün";
                    
                if (labelKayitSayisi != null)
                    labelKayitSayisi.Text = resultText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Grid yükleme hatası: {ex.Message}\n\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtAra_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void SayfaSecimiHazirla()
        {
            // Artık InitializeDepots tarafından yapılıyor
        }

        private void GridHazirla()
        {
            RefreshGrid();
        }

        // Kayıt sayısını güncelle
        private void KayitSayisiniGuncelle()
        {
            // RefreshGrid() içinde yapılıyor
        }

        // Toplam adet ve toplam fiyatı hesapla (Kalan Stok × Fiyat)
        private void UpdateTotals()
        {
            try
            {
                // Excel-based calculation disabled
                return;
                var dt = GetSeciliTablo();
                int toplamAdet = 0;
                decimal toplamFiyat = 0m;

                foreach (DataRow r in dt.Rows)
                {
                    int adet = 0;
                    if (dt.Columns.Contains("Kalan Stok"))
                        int.TryParse(r["Kalan Stok"]?.ToString(), out adet);

                    decimal fiyat = 0m;
                    if (dt.Columns.Contains("Fiyat (KDVli)") && decimal.TryParse(r["Fiyat (KDVli)"]?.ToString(), out var fk))
                        fiyat = fk;
                    else if (dt.Columns.Contains("Fiyat") && decimal.TryParse(r["Fiyat"]?.ToString(), out var f))
                        fiyat = f;

                    toplamAdet += adet;
                    toplamFiyat += fiyat * adet;
                }

                // Kayıt sayısı label'ına toplam adet ve toplam fiyatı ekle
                var baseText = labelKayitSayisi.Text?.Split('|')[0].Trim() ?? "";
                labelKayitSayisi.Text = $"{baseText} | Toplam Adet: {toplamAdet} | Toplam Fiyat: {toplamFiyat:F2} ₺";
            }
            catch
            {
                // ignore
            }
        }

        private bool ColumnExists(string colName)
        {
            try
            {
                return dataGridView1.Columns.Cast<DataGridViewColumn>().Any(c => string.Equals(c.HeaderText, colName, StringComparison.OrdinalIgnoreCase) || string.Equals(c.Name, colName, StringComparison.OrdinalIgnoreCase));
            }
            catch { return false; }
        }

        // Apply row coloring based on stock values
        private void ApplyRowColoring()
        {
            try
            {
                int stokColIndex = -1;
                if (ColumnExists("Kalan Stok"))
                    stokColIndex = dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().FindIndex(c => string.Equals(c.HeaderText, "Kalan Stok", StringComparison.OrdinalIgnoreCase) || string.Equals(c.Name, "Kalan Stok", StringComparison.OrdinalIgnoreCase));

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (stokColIndex >= 0 && stokColIndex < row.Cells.Count)
                    {
                        var stokCell = row.Cells[stokColIndex];
                        if (stokCell != null && decimal.TryParse(stokCell.Value?.ToString(), out var stok))
                        {
                            if (stok <= 0)
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                            }
                            else if (stok <= 5)
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(243, 156, 18);
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
                            }
                        }
                    }
                }
            }
            catch
            {
                // ignore
            }
        }

        // Keep designer event handlers referenced: update totals when price or KDV selection changes
        private void textBoxFiyat_TextChanged(object sender, EventArgs e)
        {
            try { UpdateTotals(); } catch { }
        }

        private void comboBoxKDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { UpdateTotals(); } catch { }
        }

        // Helper to compute KDV'li fiyat
        private string ComputeKdvli(string fiyatText)
        {
            if (!decimal.TryParse(fiyatText, out var fiyat)) return "0.00";
            decimal percent = 18m; // default
            string kdvText = comboBoxKDV?.Text?.Replace("%", "").Trim() ?? "";
            if (!string.IsNullOrEmpty(kdvText) && decimal.TryParse(kdvText, out var p))
            {
                percent = p;
            }
            var kdvli = fiyat * (1 + percent / 100m);
            return kdvli.ToString("F2");
        }

        // Kaydet
        private void button_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Barkod ve Ebat boş geçilemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string barcodeCode = textBox1.Text.Trim();
                string productName = richTextBox1.Text.Trim() ?? "Ürün";
                string brand = richTextBox1.Text.Trim() ?? "";
                string size = textBox2.Text.Trim() ?? "";
                string season = comboBoxMevsim.SelectedItem?.ToString() ?? "Yaz";
                string series = textBoxSeri.Text.Trim() ?? "";
                
                if (!decimal.TryParse(textBoxFiyat.Text, out var price))
                    price = 0;

                var depot = comboBoxSayfaSec.SelectedItem as Depot;
                if (depot == null)
                {
                    MessageBox.Show("Lütfen bir depo seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Barkod var mı kontrolü
                var existingBarcode = _barcodeService.GetAllBarcodes().FirstOrDefault(b => b.Code == barcodeCode);
                Barcode barcode;
                if (existingBarcode == null)
                {
                    barcode = new Barcode { Code = barcodeCode };
                    _barcodeService.AddBarcode(barcode);
                }
                else
                {
                    barcode = existingBarcode;
                }

                // Ürün var mı kontrolü
                var existingProduct = _productService.GetAllProducts().FirstOrDefault(p => p.BarcodeId == barcode.Id);
                if (existingProduct != null)
                {
                    MessageBox.Show($"Bu barkoda ait ürün zaten mevcut: {existingProduct.Name}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Yeni ürün ekle
                var product = new Product
                {
                    Name = productName,
                    Brand = brand,
                    Size = size,
                    Season = season,
                    Series = series,
                    Price = price,
                    BarcodeId = barcode.Id,
                    Stock = 0
                };
                _productService.AddProduct(product);

                // Stok hareketi ekle (her zaman ekle, 0 olsa bile)
                int gelen = (int)numericGelen.Value;
                _stockService.AddStock(product.Id, depot.Id, gelen, "İlk stok girişi");

                MessageBox.Show($"✓ Ürün başarıyla kaydedildi!\n\n{productName}\nBarkod: {barcodeCode}\nStok: {gelen}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                RefreshGrid();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Sil
        private void button_sil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Lütfen silinecek satırı seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show("Seçili satırı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                var row = dataGridView1.CurrentRow;
                
                // Barkod sütunundan barkod kodunu al (ilk sütun)
                string barcodeCode = row.Cells["Barkod"]?.Value?.ToString() ?? "";
                
                if (string.IsNullOrEmpty(barcodeCode))
                {
                    MessageBox.Show("Barkod bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Barkoda göre ürünü bul
                var product = _stockService.GetProductByBarcode(barcodeCode);
                if (product == null)
                {
                    MessageBox.Show("Ürün bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Veritabanından sil
                _productService.DeleteProduct(product.Id);
                
                // Grid'i yenile
                RefreshGrid();
                
                MessageBox.Show("Ürün başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Delete Error: {ex.Message}\n{ex.StackTrace}");
            }
        }

        // Güncelle - Artık form alanlarından güncelleme yapıyor
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSelectedRowFromForm();
            Temizle(); // Form alanlarını temizle
        }

        // Dosya çakışmalarını önlemek için benzersiz dosya adı üretir
        private string GetUniqueFileName(string path)
        {
            string? directory = Path.GetDirectoryName(path);
            if (directory == null)
            {
                throw new InvalidOperationException("Path.GetDirectoryName(path) null döndü. Geçerli bir yol sağlamalısınız.");
            }
            
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            int count = 1;
            string newFullPath = path;
            while (File.Exists(newFullPath))
            {
                newFullPath = Path.Combine(directory, $"{filename}{count}{extension}");
                count++;
            }
            return newFullPath;
        }

        // ExcelKaydet fonksiyonunu parametreli hale getir
        private void ExcelKaydet(string dosyaYolu)
        {
            using (var wb = new ClosedXML.Excel.XLWorkbook())
            {
                foreach (var kvp in sayfaTablolar)
                {
                    wb.Worksheets.Add(kvp.Value, kvp.Key);
                }
                wb.SaveAs(dosyaYolu);
            }
        }

        private void KaydetVeYenile()
        {
            ExcelKaydet();
            ExceldenYukle();
            KayitSayisiniGuncelle();
            UpdateTotals();
            ApplyRowColoring();
        }

        private void ExcelKaydet()
        {
            try
            {
                using (var wb = new XLWorkbook())
                {
                    foreach (var kvp in sayfaTablolar)
                    {
                        wb.Worksheets.Add(kvp.Value, kvp.Key);
                    }
                    wb.SaveAs(excelDosyaYolu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel dosyası kaydedilemedi! Dosya başka bir program tarafından açık olabilir.\n" + ex.Message,
                    "Kaydetme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExceldenYukle()
        {
            if (!File.Exists(excelDosyaYolu)) return;

            using (var wb = new XLWorkbook(excelDosyaYolu))
            {
                foreach (var sayfa in depoAdlari)
                {
                    if (wb.Worksheets.Contains(sayfa))
                    {
                        var ws = wb.Worksheet(sayfa);
                        var dt = sayfaTablolar[sayfa];
                        dt.Clear();
                        foreach (var row in ws.RangeUsed()?.RowsUsed()?.Skip(1) ?? Enumerable.Empty<IXLRangeRow>())
                        {
                            // 14 sütun oku (Gelen/Giden/Kalan Stok)
                            string urunKodu = row.Cell(1)?.Value.ToString() ?? "";
                            string tfNumarasi = row.Cell(2)?.Value.ToString() ?? "";
                            string mevsim = row.Cell(3)?.Value.ToString() ?? "";
                            string ebat = row.Cell(4)?.Value.ToString() ?? "";
                            string marka = row.Cell(5)?.Value.ToString() ?? "";
                            string seri = row.Cell(6)?.Value.ToString() ?? "";
                            string fiyat = row.Cell(7)?.Value.ToString() ?? "";
                            string fiyatKdvli = row.Cell(8)?.Value.ToString() ?? "";
                            string tarih = row.Cell(9)?.Value.ToString() ?? "";
                            string gelenStr = row.Cell(10)?.Value.ToString() ?? "0";
                            string gidenStr = row.Cell(11)?.Value.ToString() ?? "0";
                            string kalanStokStr = row.Cell(12)?.Value.ToString() ?? "0";
                            string siparisAdeti = row.Cell(13)?.Value.ToString() ?? "";
                            string gelisTarihi = row.Cell(14)?.Value.ToString() ?? "";
                            dt.Rows.Add(urunKodu, tfNumarasi, mevsim, ebat, marka, seri, fiyat, fiyatKdvli, tarih, gelenStr, gidenStr, kalanStokStr, siparisAdeti, gelisTarihi);
                        }
                    }
                }
            }
            GridHazirla();
        }

        private DataTable GetSeciliTablo()
        {
            var seciliSayfa = comboBoxSayfaSec.SelectedItem?.ToString();
            if (seciliSayfa != null && sayfaTablolar.ContainsKey(seciliSayfa))
                return sayfaTablolar[seciliSayfa];
            return sayfaTablolar[depoAdlari[0]];
        }

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            textBoxSeri.Clear();
            textBoxFiyat.Clear();
            if (textBoxTarih != null) textBoxTarih.Text = "";
            if (textBoxTelefon != null) textBoxTelefon.Text = "";
            comboBoxMevsim.SelectedIndex = -1;
            numericGelen.Value = 0;
            numericGiden.Value = 0;
            labelKalanStokValue.Text = "0";
            numericSiparis.Value = 0;
            dateTimeGelis.Value = DateTime.Now;
        }

        // Mevsim filtre değiştiğinde aramayı yeniden tetikle
        private void comboBoxMevsimFiltre_SelectedIndexChanged(object? sender, EventArgs e)
        {
            RefreshGrid();
        }

        // Arama kutusu: tüm depolarda arama ve depo adını gösterme
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string kolonAdi = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                if (kolonAdi == "Dosya")
                {
                    string dosyaYolu = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                    if (!string.IsNullOrEmpty(dosyaYolu) && File.Exists(dosyaYolu))
                        System.Diagnostics.Process.Start(dosyaYolu);
                    else
                        MessageBox.Show("Dosya bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Tek tıkla dosya açma
        private void dataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string kolonAdi = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                if (kolonAdi == "Dosya")
                {
                    string dosyaYolu = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                    if (!string.IsNullOrEmpty(dosyaYolu) && File.Exists(dosyaYolu))
                        System.Diagnostics.Process.Start(dosyaYolu);
                    else
                        MessageBox.Show("Dosya bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Undo işlemi

        private void PushUndo()
        {
            // Tüm sayfaTablolar'ın derin kopyasını yığına ekle
            var copy = new Dictionary<string, DataTable>();
            foreach (var kvp in sayfaTablolar)
            {
                copy[kvp.Key] = kvp.Value.Copy();
            }
            undoStack.Push(copy);
        }

        // DISABLED - Legacy code
        /*
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Dosyası|*.xlsx;*.xls";
            ofd.Title = "Excel Dosyası Seç";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var wb = new ClosedXML.Excel.XLWorkbook(ofd.FileName))
                    {
                        var ws = wb.Worksheets.First();
                        var firstRow = ws.FirstRowUsed();
                        if (firstRow == null)
                        {
                            MessageBox.Show("Excel dosyası boş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var tablo = GetSeciliTablo();
                        PushUndo();
                        tablo.Clear();

                        // Sütun sayısına göre otomatik eşleme yap
                        foreach (var row in ws.RowsUsed().Skip(1))
                        {
                            var newRow = tablo.NewRow();
                            int colCount = Math.Min(row.CellsUsed().Count(), tablo.Columns.Count);
                            for (int col = 0; col < colCount; col++)
                            {
                                newRow[col] = row.Cell(col + 1).Value.ToString();
                            }
                            tablo.Rows.Add(newRow);
                        }

                        dataGridView1.DataSource = tablo;
                        KaydetVeYenile();
                        MessageBox.Show("Excel verileri başarıyla yüklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Excel dosyası okunamadı!\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        */



        // Yazdırma işlemi
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var tablo = GetSeciliTablo();
                if (tablo.Rows.Count == 0)
                {
                    MessageBox.Show("Yazdırılacak veri yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var printDialog = new PrintDialog();
                var printDocument = new System.Drawing.Printing.PrintDocument();

                printDocument.PrintPage += (s, ev) =>
                {
                    var font = new Font("Arial", 8);
                    var headerFont = new Font("Arial", 10, FontStyle.Bold);
                    float yPos = 50;
                    float leftMargin = 30;

                    ev.Graphics?.DrawString("LastikPark - Stok Listesi", headerFont, Brushes.Black, leftMargin, yPos);
                    yPos += 35;

                    // Doğru sütun isimleri
                    string[] kolonlar = { "Ürün Kodu", "TF Numarasi", "Ebat", "Marka", "Seri", "Fiyat", "Fiyat (KDVli)", "Stok" };
                    float[] genislikler = { 70, 80, 80, 70, 90, 55, 75, 50 };
                    float xPos = leftMargin;
                    for (int idx = 0; idx < kolonlar.Length; idx++)
                    {
                        ev.Graphics.DrawString(kolonlar[idx], headerFont, Brushes.Black, xPos, yPos);
                        xPos += genislikler[idx];
                    }
                    yPos += 25;

                    foreach (DataRow row in tablo.Rows)
                    {
                        if (yPos > ev.MarginBounds.Bottom - 50) break;
                        xPos = leftMargin;
                        for (int idx = 0; idx < kolonlar.Length; idx++)
                        {
                            string val = tablo.Columns.Contains(kolonlar[idx]) ? row[kolonlar[idx]]?.ToString() ?? "" : "";
                            ev.Graphics.DrawString(val, font, Brushes.Black, xPos, yPos);
                            xPos += genislikler[idx];
                        }
                        yPos += 18;
                    }
                };

                printDialog.Document = printDocument;
                if (printDialog.ShowDialog() == DialogResult.OK)
                    printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yazdırma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CSV dışa aktarma işlemi
        private void buttonExportCSV_Click(object sender, EventArgs e)
        {

        }

        // Tema değiştirme butonu


        // Yedekleme butonu
        private void buttonBackup_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Yedek dosyasını kaydetmek için konum seçin";
                    sfd.Filter = "Yedek Dosyası|*.bak";
                    sfd.FileName = $"yedek_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string yedekYolu = GetUniqueFileName(sfd.FileName);

                        // Excel dosyasını yedek olarak kopyala
                        if (File.Exists(excelDosyaYolu))
                        {
                            File.Copy(excelDosyaYolu, yedekYolu, true);
                            MessageBox.Show($"Yedek başarıyla oluşturuldu:\n{yedekYolu}",
                                          "Yedekleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Yedeklenecek Excel dosyası bulunamadı!",
                                          "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yedekleme hatası: " + ex.Message, "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yardımcı: hücre değerini güvenli oku (arama/normal mod farketmez)
        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            try
            {
                if (dataGridView1.Columns.Contains(columnName))
                    return row.Cells[columnName]?.Value?.ToString() ?? "";
            }
            catch { }
            return "";
        }

        // Hücre seçildiğinde bilgileri TextBox'lara getir
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                try
                {
                    var row = dataGridView1.CurrentRow;
                    textBox1.Text = GetCellValue(row, "Barkod");
                    richTextBox1.Text = GetCellValue(row, "Ürün Adı");
                    richTextBox1.Text = GetCellValue(row, "Marka");
                    textBox2.Text = GetCellValue(row, "Ebat");
                    comboBoxMevsim.Text = GetCellValue(row, "Mevsim");
                    textBoxSeri.Text = GetCellValue(row, "Seri");
                    textBoxFiyat.Text = GetCellValue(row, "Fiyat");
                    
                    toolStripStatusLabel1.Text = $"Seçili Ürün: {row.Index + 1} | {GetCellValue(row, "Barkod")} {GetCellValue(row, "Ebat")}";
                    UpdateTotals();
                    ApplyRowColoring();
                }
                catch { }
            }
        }

        // Fare hücre üzerine geldiğinde yeşil kenarlık
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    // Hücreye yeşil kenarlık ekle (hover efekti)
                    cell.Style.SelectionBackColor = Color.FromArgb(46, 204, 113); // Yeşil hover
                    cell.Style.SelectionForeColor = Color.White;

                    // Tüm satırı vurgula
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 247, 236); // Açık yeşil
                }
                catch (Exception ex)
                {
                    // Hata mesajını logamak için aşağıdaki satırı ekleyebilirsiniz
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }
        }

        // Fare hücreden çıktığında kenarlığı kaldır
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    // Hover efektini kaldır
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

                    // Seçili satır değilse normal renge döndür
                    if (dataGridView1.CurrentRow?.Index != e.RowIndex)
                    {
                        var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        cell.Style.SelectionBackColor = Color.FromArgb(52, 152, 219); // Normal seçim rengi
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını logamak için aşağıdaki satırı ekleyebilirsiniz
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }
        }

        // Hücre formatlama (hover efektleri için)
        private void dataGridView1_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // Seçili satırı daha belirgin yap
                if (dataGridView1.CurrentRow != null && e.RowIndex == dataGridView1.CurrentRow.Index)
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 250, 238); // Çok açık yeşil
                    e.CellStyle.ForeColor = Color.FromArgb(27, 94, 32); // Koyu yeşil
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); // Kalın yazı
                }
            }
            catch (Exception ex)
            {
                // Hata mesajını logamak için aşağıdaki satırı ekleyebilirsiniz
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }

        // Güncelle - form alanlarından güncelleme (arama modunu destekler)
        private void UpdateSelectedRowFromForm()
        {
            try
            {
                if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
                {
                    MessageBox.Show("Lütfen güncellenecek ürünü grid'den seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Grid'den barkodu al
                var gridRow = dataGridView1.CurrentRow;
                string barcodeCode = GetCellValue(gridRow, "Barkod");

                if (string.IsNullOrWhiteSpace(barcodeCode))
                {
                    MessageBox.Show("Seçilen ürünün barkodu bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Barkoda göre ürünü bul
                var barcode = _barcodeService.GetAllBarcodes().FirstOrDefault(b => b.Code == barcodeCode);
                if (barcode == null)
                {
                    MessageBox.Show($"Barkod '{barcodeCode}' bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var product = _productService.GetAllProducts().FirstOrDefault(p => p.BarcodeId == barcode.Id);
                if (product == null)
                {
                    MessageBox.Show($"Bu barkoda ait ürün bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ürün özelliklerini güncelle
                product.Name = richTextBox1.Text.Trim() ?? "Ürün";
                product.Brand = GetCellValue(gridRow, "Marka"); // Grid'den marka al
                product.Size = textBox2.Text.Trim() ?? "";
                product.Season = comboBoxMevsim.SelectedItem?.ToString() ?? "Yaz";
                product.Series = textBoxSeri.Text.Trim() ?? "";

                if (!decimal.TryParse(textBoxFiyat.Text, out var price))
                    price = 0;
                product.Price = price;

                _productService.UpdateProduct(product);

                MessageBox.Show($"✓ Ürün başarıyla güncellendi!\n\n{product.Name}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshGrid();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HesaplaKalanStok()
        {
            int gelen = (int)numericGelen.Value;
            int giden = (int)numericGiden.Value;
            int kalan = gelen - giden;
            labelKalanStokValue.Text = kalan.ToString();
            labelKalanStokValue.ForeColor = kalan <= 0 ? Color.Red : Color.FromArgb(39, 174, 96);
        }

        private void numericGelen_ValueChanged(object sender, EventArgs e)
        {
            HesaplaKalanStok();
        }

        private void numericGiden_ValueChanged(object sender, EventArgs e)
        {
            HesaplaKalanStok();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // Designer event handlers referenced in Form1.Designer.cs
        private void labelKDV_Click(object sender, EventArgs e)
        {
            // Intentionally empty - label click not used
        }

        private void dateTimeGelis_ValueChanged(object sender, EventArgs e)
        {
            // Intentionally empty - handled elsewhere if needed
        }

        // Barcode sistemi ile entegrasyon - Mevcut satırı barkod sistemine ekle
        public void AddProductToBarcode()
        {
            try
            {
                var urunKodu = textBox1.Text?.Trim();
                if (string.IsNullOrEmpty(urunKodu))
                {
                    MessageBox.Show("Lütfen ürün kodunu girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Barkod ve ürün bilgilerini veritabanına kaydet
                var barcode = new Barcode { Code = urunKodu };
                _barcodeService.AddBarcode(barcode);

                var product = new Product
                {
                    Name = textBox2.Text?.Trim() ?? "Unnamed Product",
                    Description = richTextBox1.Text?.Trim() ?? "",
                    Price = decimal.TryParse(textBoxFiyat.Text, out var price) ? price : 0,
                    Stock = int.TryParse(labelKalanStokValue.Text, out var stock) ? stock : 0,
                    BarcodeId = barcode.Id,
                    CreatedDate = DateTime.Now
                };
                _productService.AddProduct(product);

                MessageBox.Show("Ürün barkod sistemine başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Barkod sistemi entegrasyonu hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tüm ürünleri barkod sistemine aktarma
        public void SyncAllProductsToBarcode()
        {
            try
            {
                var currentTable = GetSeciliTablo();
                int syncedCount = 0;

                foreach (DataRow row in currentTable.Rows)
                {
                    var urunKodu = row["Ürün Kodu"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(urunKodu))
                    {
                        // Zaten eklenmiş mi kontrol et
                        var existingBarcodes = _barcodeService.GetAllBarcodes();
                        if (existingBarcodes.Any(b => b.Code == urunKodu))
                            continue;

                        var barcode = new Barcode { Code = urunKodu };
                        _barcodeService.AddBarcode(barcode);

                        var productName = row["Marka"]?.ToString()?.Trim() ?? "Ürün";
                        if (!string.IsNullOrEmpty(row["Ebat"]?.ToString()))
                            productName += $" {row["Ebat"]}";

                        var product = new Product
                        {
                            Name = productName,
                            Description = $"Seri: {row["Seri"]?.ToString() ?? ""}, Mevsim: {row["Mevsim"]?.ToString() ?? ""}",
                            Price = decimal.TryParse(row["Fiyat (KDVli)"]?.ToString(), out var price) ? price : 0,
                            Stock = int.TryParse(row["Kalan Stok"]?.ToString(), out var stock) ? stock : 0,
                            BarcodeId = barcode.Id,
                            CreatedDate = DateTime.Now
                        };
                        _productService.AddProduct(product);
                        syncedCount++;
                    }
                }

                MessageBox.Show($"{syncedCount} ürün barkod sistemine başarıyla senkronize edildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Senkronizasyon hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Excel'e ürünleri aktar
        private void ExportToExcel()
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Aktaracak veri yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçili depoyu al
                var selectedDepot = comboBoxSayfaSec.SelectedItem as Depot;
                string depoAdi = selectedDepot?.Name ?? "Bilinmeyen Depo";

                // Desktop yolunu al
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"StokListesi_{depoAdi}_{DateTime.Now:dd-MM-yyyy_HHmmss}.xlsx";
                string filePath = Path.Combine(desktopPath, fileName);

                // Excel dosyası oluştur
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Stok Listesi");
                    
                    // Başlıkları ekle (Depo sütunu ilk sütun)
                    worksheet.Cell(1, 1).Value = "Depo";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray;

                    for (int col = 0; col < dataGridView1.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col + 2).Value = dataGridView1.Columns[col].HeaderText;
                        worksheet.Cell(1, col + 2).Style.Font.Bold = true;
                        worksheet.Cell(1, col + 2).Style.Fill.BackgroundColor = XLColor.LightGray;
                    }

                    // Verileri ekle (Depo adını her satırın başına ekle)
                    for (int row = 0; row < dataGridView1.Rows.Count; row++)
                    {
                        worksheet.Cell(row + 2, 1).Value = depoAdi;
                        worksheet.Cell(row + 2, 1).Style.Font.Italic = true;

                        for (int col = 0; col < dataGridView1.Columns.Count; col++)
                        {
                            var cellValue = dataGridView1.Rows[row].Cells[col].Value;
                            worksheet.Cell(row + 2, col + 2).Value = cellValue?.ToString() ?? "";
                        }
                    }

                    // Sütun genişliğini otomatik ayarla
                    worksheet.Columns().AdjustToContents();

                    // Dosyayı kaydet
                    workbook.SaveAs(filePath);
                }

                MessageBox.Show($"Dosya başarıyla kaydedildi:\n{filePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Dosyayı aç
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excel aktarma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
