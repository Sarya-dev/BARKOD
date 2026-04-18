namespace StokTakipSistemi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            dateTimeGelis = new DateTimePicker();
            button1 = new Button();
            button_sil = new Button();
            button_kaydet = new Button();
            richTextBox1 = new RichTextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            textBoxTelefon = new TextBox();
            comboBoxMevsim = new ComboBox();
            labelMevsim = new Label();
            textBoxSeri = new TextBox();
            textBoxFiyat = new TextBox();
            comboBoxKDV = new ComboBox();
            labelKDV = new Label();
            textBoxTarih = new TextBox();
            numericGelen = new NumericUpDown();
            numericGiden = new NumericUpDown();
            labelKalanStokValue = new Label();
            numericSiparis = new NumericUpDown();
            labelGelen = new Label();
            labelGiden = new Label();
            labelKalanStok = new Label();
            labelSeri = new Label();
            labelFiyat = new Label();
            labelTarih = new Label();
            labelSiparis = new Label();
            labelGelis = new Label();
            labelTelefon = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            panel3 = new Panel();
            labelKayitSayisi = new Label();
            txtAra = new TextBox();
            lblAra = new Label();
            comboBoxMevsimFiltre = new ComboBox();
            labelMevsimFiltre = new Label();
            comboBoxSayfaSec = new ComboBox();
            labelSayfa = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericGelen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericGiden).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericSiparis).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dateTimeGelis);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button_sil);
            panel1.Controls.Add(button_kaydet);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBoxTelefon);
            panel1.Controls.Add(comboBoxMevsim);
            panel1.Controls.Add(labelMevsim);
            panel1.Controls.Add(textBoxSeri);
            panel1.Controls.Add(textBoxFiyat);
            panel1.Controls.Add(comboBoxKDV);
            panel1.Controls.Add(labelKDV);
            panel1.Controls.Add(textBoxTarih);
            panel1.Controls.Add(numericGelen);
            panel1.Controls.Add(numericGiden);
            panel1.Controls.Add(labelKalanStokValue);
            panel1.Controls.Add(numericSiparis);
            panel1.Controls.Add(labelSeri);
            panel1.Controls.Add(labelFiyat);
            panel1.Controls.Add(labelTarih);
            panel1.Controls.Add(labelGelen);
            panel1.Controls.Add(labelGiden);
            panel1.Controls.Add(labelKalanStok);
            panel1.Controls.Add(labelSiparis);
            panel1.Controls.Add(labelGelis);
            panel1.Controls.Add(labelTelefon);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(15, 15);
            panel1.Name = "panel1";
            panel1.Size = new Size(320, 760);
            panel1.TabIndex = 0;
            panel1.Paint += Panel_Paint;
            // 
            // dateTimeGelis
            // 
            dateTimeGelis.Font = new Font("Segoe UI", 14F);
            dateTimeGelis.Format = DateTimePickerFormat.Short;
            dateTimeGelis.Location = new Point(15, 577);
            dateTimeGelis.Name = "dateTimeGelis";
            dateTimeGelis.Size = new Size(285, 32);
            dateTimeGelis.TabIndex = 11;
            dateTimeGelis.ValueChanged += dateTimeGelis_ValueChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(52, 152, 219);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(182, 663);
            button1.Name = "button1";
            button1.Size = new Size(130, 35);
            button1.TabIndex = 9;
            button1.Text = "✏️ Güncelle";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += Button_MouseEnter;
            button1.MouseLeave += Button_MouseLeave;
            // 
            // button_sil
            // 
            button_sil.BackColor = Color.FromArgb(231, 76, 60);
            button_sil.FlatAppearance.BorderSize = 0;
            button_sil.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            button_sil.FlatStyle = FlatStyle.Flat;
            button_sil.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button_sil.ForeColor = Color.White;
            button_sil.Location = new Point(105, 615);
            button_sil.Name = "button_sil";
            button_sil.Size = new Size(115, 35);
            button_sil.TabIndex = 8;
            button_sil.Text = "🗑️ Sil";
            button_sil.UseVisualStyleBackColor = false;
            button_sil.Click += button_sil_Click;
            button_sil.MouseEnter += Button_MouseEnter;
            button_sil.MouseLeave += Button_MouseLeave;
            // 
            // button_kaydet
            // 
            button_kaydet.BackColor = Color.FromArgb(46, 204, 113);
            button_kaydet.FlatAppearance.BorderSize = 0;
            button_kaydet.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
            button_kaydet.FlatStyle = FlatStyle.Flat;
            button_kaydet.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button_kaydet.ForeColor = Color.White;
            button_kaydet.Location = new Point(14, 661);
            button_kaydet.Name = "button_kaydet";
            button_kaydet.Size = new Size(120, 35);
            button_kaydet.TabIndex = 7;
            button_kaydet.Text = "💾 Kaydet";
            button_kaydet.UseVisualStyleBackColor = false;
            button_kaydet.Click += button_kaydet_Click;
            button_kaydet.MouseEnter += Button_MouseEnter;
            button_kaydet.MouseLeave += Button_MouseLeave;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(248, 249, 250);
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Font = new Font("Segoe UI", 14F);
            richTextBox1.Location = new Point(120, 196);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(180, 40);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            richTextBox1.Enter += TextBox_Enter;
            richTextBox1.Leave += TextBox_Leave;
            // 
            // textBox2 (Ebat)
            // 
            textBox2.BackColor = Color.FromArgb(248, 249, 250);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Segoe UI", 14F);
            textBox2.Location = new Point(120, 148);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 32);
            textBox2.TabIndex = 4;
            textBox2.Enter += TextBox_Enter;
            textBox2.Leave += TextBox_Leave;
            // 
            // comboBoxMevsim
            // 
            comboBoxMevsim.BackColor = Color.FromArgb(248, 249, 250);
            comboBoxMevsim.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMevsim.FlatStyle = FlatStyle.Flat;
            comboBoxMevsim.Font = new Font("Segoe UI", 11F);
            comboBoxMevsim.Items.AddRange(new object[] { "Yaz", "Kış", "4 Mevsim" });
            comboBoxMevsim.Location = new Point(225, 148);
            comboBoxMevsim.Name = "comboBoxMevsim";
            comboBoxMevsim.Size = new Size(90, 30);
            comboBoxMevsim.TabIndex = 30;
            // 
            // labelMevsim
            // 
            labelMevsim.AutoSize = true;
            labelMevsim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelMevsim.ForeColor = Color.FromArgb(52, 73, 94);
            labelMevsim.Location = new Point(225, 130);
            labelMevsim.Name = "labelMevsim";
            labelMevsim.Size = new Size(72, 19);
            labelMevsim.TabIndex = 31;
            labelMevsim.Text = "Mevsim";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(248, 249, 250);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Segoe UI", 14F);
            textBox1.Location = new Point(120, 15);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(180, 32);
            textBox1.TabIndex = 3;
            textBox1.Enter += TextBox_Enter;
            textBox1.Leave += TextBox_Leave;
            // 
            // textBoxTelefon
            // 
            textBoxTelefon.BackColor = Color.FromArgb(248, 249, 250);
            textBoxTelefon.BorderStyle = BorderStyle.FixedSingle;
            textBoxTelefon.Font = new Font("Segoe UI", 14F);
            textBoxTelefon.Location = new Point(120, 64);
            textBoxTelefon.Name = "textBoxTelefon";
            textBoxTelefon.Size = new Size(180, 32);
            textBoxTelefon.TabIndex = 3;
            textBoxTelefon.Enter += TextBox_Enter;
            textBoxTelefon.Leave += TextBox_Leave;
            // 
            // textBoxSeri
            // 
            textBoxSeri.BackColor = Color.FromArgb(248, 249, 250);
            textBoxSeri.BorderStyle = BorderStyle.FixedSingle;
            textBoxSeri.Font = new Font("Segoe UI", 14F);
            textBoxSeri.Location = new Point(120, 252);
            textBoxSeri.Name = "textBoxSeri";
            textBoxSeri.Size = new Size(180, 32);
            textBoxSeri.TabIndex = 6;
            textBoxSeri.Enter += TextBox_Enter;
            textBoxSeri.Leave += TextBox_Leave;
            // 
            // textBoxFiyat
            // 
            textBoxFiyat.BackColor = Color.FromArgb(248, 249, 250);
            textBoxFiyat.BorderStyle = BorderStyle.FixedSingle;
            textBoxFiyat.Font = new Font("Segoe UI", 14F);
            textBoxFiyat.Location = new Point(120, 306);
            textBoxFiyat.Name = "textBoxFiyat";
            textBoxFiyat.Size = new Size(100, 32);
            textBoxFiyat.TabIndex = 7;
            textBoxFiyat.TextChanged += textBoxFiyat_TextChanged;
            textBoxFiyat.Enter += TextBox_Enter;
            textBoxFiyat.Leave += TextBox_Leave;
            // 
            // comboBoxKDV
            // 
            comboBoxKDV.BackColor = Color.FromArgb(248, 249, 250);
            comboBoxKDV.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxKDV.FlatStyle = FlatStyle.Flat;
            comboBoxKDV.Font = new Font("Segoe UI", 12F);
            comboBoxKDV.Items.AddRange(new object[] { "18%", "20%" });
            comboBoxKDV.Location = new Point(230, 309);
            comboBoxKDV.Name = "comboBoxKDV";
            comboBoxKDV.Size = new Size(70, 29);
            comboBoxKDV.TabIndex = 17;
            comboBoxKDV.SelectedIndexChanged += comboBoxKDV_SelectedIndexChanged;
            // 
            // labelKDV
            // 
            labelKDV.AutoSize = true;
            labelKDV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelKDV.ForeColor = Color.FromArgb(52, 73, 94);
            labelKDV.Location = new Point(230, 287);
            labelKDV.Name = "labelKDV";
            labelKDV.Size = new Size(37, 19);
            labelKDV.TabIndex = 18;
            labelKDV.Text = "KDV";
            labelKDV.Click += labelKDV_Click;
            // 
            // textBoxTarih
            // 
            textBoxTarih.BackColor = Color.FromArgb(248, 249, 250);
            textBoxTarih.BorderStyle = BorderStyle.FixedSingle;
            textBoxTarih.Font = new Font("Segoe UI", 14F);
            textBoxTarih.Location = new Point(120, 355);
            textBoxTarih.Name = "textBoxTarih";
            textBoxTarih.Size = new Size(180, 32);
            textBoxTarih.TabIndex = 8;
            textBoxTarih.Enter += TextBox_Enter;
            textBoxTarih.Leave += TextBox_Leave;
            // 
            // numericGelen
            // 
            numericGelen.Font = new Font("Segoe UI", 14F);
            numericGelen.Location = new Point(120, 403);
            numericGelen.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericGelen.Name = "numericGelen";
            numericGelen.Size = new Size(180, 32);
            numericGelen.TabIndex = 9;
            numericGelen.ValueChanged += numericGelen_ValueChanged;
            // 
            // numericGiden
            // 
            numericGiden.Font = new Font("Segoe UI", 14F);
            numericGiden.Location = new Point(120, 442);
            numericGiden.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericGiden.Name = "numericGiden";
            numericGiden.Size = new Size(180, 32);
            numericGiden.TabIndex = 32;
            numericGiden.ValueChanged += numericGiden_ValueChanged;
            // 
            // labelKalanStokValue
            // 
            labelKalanStokValue.AutoSize = true;
            labelKalanStokValue.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelKalanStokValue.ForeColor = Color.FromArgb(39, 174, 96);
            labelKalanStokValue.Location = new Point(120, 480);
            labelKalanStokValue.Name = "labelKalanStokValue";
            labelKalanStokValue.Size = new Size(28, 30);
            labelKalanStokValue.TabIndex = 33;
            labelKalanStokValue.Text = "0";
            // 
            // numericSiparis
            // 
            numericSiparis.Font = new Font("Segoe UI", 14F);
            numericSiparis.Location = new Point(120, 522);
            numericSiparis.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericSiparis.Name = "numericSiparis";
            numericSiparis.Size = new Size(180, 32);
            numericSiparis.TabIndex = 10;
            // 
            // labelSeri
            // 
            labelSeri.AutoSize = true;
            labelSeri.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelSeri.ForeColor = Color.FromArgb(52, 73, 94);
            labelSeri.Location = new Point(61, 257);
            labelSeri.Name = "labelSeri";
            labelSeri.Size = new Size(43, 21);
            labelSeri.TabIndex = 20;
            labelSeri.Text = "Seri:";
            // 
            // labelFiyat
            // 
            labelFiyat.AutoSize = true;
            labelFiyat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelFiyat.ForeColor = Color.FromArgb(52, 73, 94);
            labelFiyat.Location = new Point(61, 312);
            labelFiyat.Name = "labelFiyat";
            labelFiyat.Size = new Size(51, 21);
            labelFiyat.TabIndex = 21;
            labelFiyat.Text = "Fiyat:";
            // 
            // labelTarih
            // 
            labelTarih.AutoSize = true;
            labelTarih.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTarih.ForeColor = Color.FromArgb(52, 73, 94);
            labelTarih.Location = new Point(59, 360);
            labelTarih.Name = "labelTarih";
            labelTarih.Size = new Size(52, 21);
            labelTarih.TabIndex = 22;
            labelTarih.Text = "Tarih:";
            // 
            // labelGelen
            // 

            labelGelen.AutoSize = true;
            labelGelen.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelGelen.ForeColor = Color.FromArgb(52, 73, 94);
            labelGelen.Location = new Point(51, 408);
            labelGelen.Name = "labelGelen";
            labelGelen.Size = new Size(57, 21);
            labelGelen.TabIndex = 34;
            labelGelen.Text = "Gelen:";
            // 
            // labelGiden
            // 

            labelGiden.AutoSize = true;
            labelGiden.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelGiden.ForeColor = Color.FromArgb(52, 73, 94);
            labelGiden.Location = new Point(51, 447);
            labelGiden.Name = "labelGiden";
            labelGiden.Size = new Size(57, 21);
            labelGiden.TabIndex = 35;
            labelGiden.Text = "Giden:";
            // 
            // labelKalanStok
            // 

            labelKalanStok.AutoSize = true;
            labelKalanStok.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelKalanStok.ForeColor = Color.FromArgb(52, 73, 94);
            labelKalanStok.Location = new Point(21, 484);
            labelKalanStok.Name = "labelKalanStok";
            labelKalanStok.Size = new Size(90, 21);
            labelKalanStok.TabIndex = 36;
            labelKalanStok.Text = "Kalan:";
            // 
            // labelSiparis
            // 
            labelSiparis.AutoSize = true;
            labelSiparis.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelSiparis.ForeColor = Color.FromArgb(52, 73, 94);
            labelSiparis.Location = new Point(4, 527);
            labelSiparis.Name = "labelSiparis";
            labelSiparis.Size = new Size(110, 21);
            labelSiparis.TabIndex = 24;
            labelSiparis.Text = "Sipariş Adeti:";
            // 
            // labelGelis
            // 
            labelGelis.AutoSize = true;
            labelGelis.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelGelis.ForeColor = Color.FromArgb(52, 73, 94);
            labelGelis.Location = new Point(4, 560);
            labelGelis.Name = "labelGelis";
            labelGelis.Size = new Size(98, 21);
            labelGelis.TabIndex = 25;
            labelGelis.Text = "Geliş Tarihi:";
            // 
            // labelTelefon
            // 
            labelTelefon.AutoSize = true;
            labelTelefon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTelefon.ForeColor = Color.FromArgb(52, 73, 94);
            labelTelefon.Location = new Point(6, 64);
            labelTelefon.Name = "labelTelefon";
            labelTelefon.Size = new Size(109, 21);
            labelTelefon.TabIndex = 2;
            labelTelefon.Text = "TF Numarasi:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(52, 73, 94);
            label3.Location = new Point(49, 201);
            label3.Name = "label3";
            label3.Size = new Size(62, 21);
            label3.TabIndex = 2;
            label3.Text = "Marka:";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(52, 73, 94);
            label2.Location = new Point(60, 153);
            label2.Name = "label2";
            label2.Size = new Size(48, 21);
            label2.TabIndex = 1;
            label2.Text = "Ebat:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(52, 73, 94);
            label1.Location = new Point(8, 20);
            label1.Name = "label1";
            label1.Size = new Size(96, 21);
            label1.TabIndex = 0;
            label1.Text = "Ürün Kodu:";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(dataGridView1);
            panel2.Location = new Point(321, 15);
            panel2.Name = "panel2";
            panel2.Size = new Size(1049, 554);
            panel2.TabIndex = 1;
            panel2.Paint += Panel_Paint;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 60;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(46, 204, 113);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(189, 195, 199);
            dataGridView1.Location = new Point(12, 32);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 28;
            dataGridView1.Size = new Size(1032, 503);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(labelKayitSayisi);
            panel3.Controls.Add(comboBoxMevsimFiltre);
            panel3.Controls.Add(labelMevsimFiltre);
            panel3.Controls.Add(txtAra);
            panel3.Controls.Add(lblAra);
            panel3.Controls.Add(comboBoxSayfaSec);
            panel3.Controls.Add(labelSayfa);
            panel3.Location = new Point(333, 558);
            panel3.Name = "panel3";
            panel3.Size = new Size(1351, 107);
            panel3.TabIndex = 2;
            panel3.Paint += Panel_Paint;
            // 
            // labelSayfa
            // 
            labelSayfa.AutoSize = true;
            labelSayfa.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelSayfa.ForeColor = Color.FromArgb(52, 73, 94);
            labelSayfa.Location = new Point(15, 25);
            labelSayfa.Name = "labelSayfa";
            labelSayfa.Size = new Size(52, 21);
            labelSayfa.TabIndex = 23;
            labelSayfa.Text = "Sayfa:";
            // 
            // comboBoxSayfaSec
            // 
            comboBoxSayfaSec.BackColor = Color.FromArgb(248, 249, 250);
            comboBoxSayfaSec.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSayfaSec.FlatStyle = FlatStyle.Flat;
            comboBoxSayfaSec.Font = new Font("Segoe UI", 11F);
            comboBoxSayfaSec.Location = new Point(75, 22);
            comboBoxSayfaSec.Name = "comboBoxSayfaSec";
            comboBoxSayfaSec.Size = new Size(110, 28);
            comboBoxSayfaSec.TabIndex = 22;
            // 
            // lblAra
            // 
            lblAra.AutoSize = true;
            lblAra.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAra.ForeColor = Color.FromArgb(52, 73, 94);
            lblAra.Location = new Point(210, 25);
            lblAra.Name = "lblAra";
            lblAra.Size = new Size(38, 21);
            lblAra.TabIndex = 21;
            lblAra.Text = "Ara:";
            // 
            // txtAra
            // 
            txtAra.BackColor = Color.FromArgb(248, 249, 250);
            txtAra.BorderStyle = BorderStyle.FixedSingle;
            txtAra.Font = new Font("Segoe UI", 12F);
            txtAra.Location = new Point(255, 22);
            txtAra.Name = "txtAra";
            txtAra.Size = new Size(160, 29);
            txtAra.TabIndex = 20;
            txtAra.TextChanged += txtAra_TextChanged;
            // 
            // labelMevsimFiltre
            // 
            labelMevsimFiltre.AutoSize = true;
            labelMevsimFiltre.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelMevsimFiltre.ForeColor = Color.FromArgb(52, 73, 94);
            labelMevsimFiltre.Location = new Point(430, 25);
            labelMevsimFiltre.Name = "labelMevsimFiltre";
            labelMevsimFiltre.Size = new Size(60, 21);
            labelMevsimFiltre.TabIndex = 24;
            labelMevsimFiltre.Text = "Mevsim:";
            // 
            // comboBoxMevsimFiltre
            // 
            comboBoxMevsimFiltre.BackColor = Color.FromArgb(248, 249, 250);
            comboBoxMevsimFiltre.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMevsimFiltre.FlatStyle = FlatStyle.Flat;
            comboBoxMevsimFiltre.Font = new Font("Segoe UI", 11F);
            comboBoxMevsimFiltre.Items.AddRange(new object[] { "Tümü", "Yaz", "Kış", "4 Mevsim" });
            comboBoxMevsimFiltre.Location = new Point(505, 22);
            comboBoxMevsimFiltre.Name = "comboBoxMevsimFiltre";
            comboBoxMevsimFiltre.Size = new Size(100, 28);
            comboBoxMevsimFiltre.TabIndex = 25;
            comboBoxMevsimFiltre.SelectedIndex = 0;
            comboBoxMevsimFiltre.SelectedIndexChanged += comboBoxMevsimFiltre_SelectedIndexChanged;
            // 
            // labelKayitSayisi
            // 
            labelKayitSayisi.AutoSize = true;
            labelKayitSayisi.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelKayitSayisi.ForeColor = Color.FromArgb(52, 152, 219);
            labelKayitSayisi.Location = new Point(625, 25);
            labelKayitSayisi.Name = "labelKayitSayisi";
            labelKayitSayisi.Size = new Size(126, 21);
            labelKayitSayisi.TabIndex = 19;
            labelKayitSayisi.Text = "Toplam: 0 ürün";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(52, 73, 94);
            statusStrip1.Font = new Font("Segoe UI", 12F);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 723);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1360, 26);
            statusStrip1.TabIndex = 100;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.White;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(248, 21);
            toolStripStatusLabel1.Text = "Hazır | Modern Dosya Sistemi v2.0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1360, 749);
            Controls.Add(statusStrip1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 12F);
            MaximizeBox = false;
            MaximumSize = new Size(2000, 1200);
            MinimumSize = new Size(1200, 726);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LastikPark - Depolar Arası Stok Takip Sistemi";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericGelen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericGiden).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericSiparis).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private RichTextBox richTextBox1;
        private Button button_kaydet;
        private Button button_sil;
        private Button button1;

        private Label lblAra;
        private TextBox txtAra;
        private ComboBox comboBoxSayfaSec;
        private Label labelSayfa;
        private ComboBox comboBoxMevsimFiltre;
        private Label labelMevsimFiltre;
        private DataGridView dataGridView1;
        private Label labelKayitSayisi;
        private TextBox textBoxSeri;
        private TextBox textBoxFiyat;
        private ComboBox comboBoxKDV;
        private Label labelKDV;
        private TextBox textBoxTarih;
        private TextBox textBoxTelefon;
        private NumericUpDown numericGelen;
        private NumericUpDown numericGiden;
        private Label labelKalanStokValue;
        private NumericUpDown numericSiparis;
        private DateTimePicker dateTimeGelis;
        private Label labelSeri;
        private Label labelFiyat;
        private Label labelTarih;
        private Label labelGelen;
        private Label labelGiden;
        private Label labelKalanStok;
        private Label labelSiparis;
        private Label labelGelis;
        private Label labelTelefon;
        private ComboBox comboBoxMevsim;
        private Label labelMevsim;

        // Modern tasarım events
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(236, 240, 241), 2), 0, 0, panel.Width - 1, panel.Height - 1);
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
                tb.BackColor = Color.White;
            else if (sender is RichTextBox rtb)
                rtb.BackColor = Color.White;
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
                tb.BackColor = Color.FromArgb(248, 249, 250);
            else if (sender is RichTextBox rtb)
                rtb.BackColor = Color.FromArgb(248, 249, 250);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Cursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Cursor = Cursors.Default;
        }
    }
}
