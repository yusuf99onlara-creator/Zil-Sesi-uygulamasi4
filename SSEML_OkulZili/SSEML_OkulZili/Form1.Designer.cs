namespace SSEML_OkulZili
{
    partial class Form1
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrSaat = new System.Windows.Forms.Timer(this.components);
            this.tmrZilCheck = new System.Windows.Forms.Timer(this.components);
            this.pnlSaatKutusu = new System.Windows.Forms.Panel();
            this.lblSaat = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.lblGeriSayim = new System.Windows.Forms.Label();
            this.lblSiradakiZil = new System.Windows.Forms.Label();
            this.lblIstatistik = new System.Windows.Forms.Label();
            this.chkKaranlikMod = new System.Windows.Forms.CheckBox();
            this.pnlProfilListesi = new System.Windows.Forms.Panel();
            this.lblProfilBaslik = new System.Windows.Forms.Label();
            this.pnlProfilKartlari = new System.Windows.Forms.Panel();
            this.btnYeniProfil = new System.Windows.Forms.Button();
            this.btnProfilSil = new System.Windows.Forms.Button();
            this.pnlGununProgrami = new System.Windows.Forms.Panel();
            this.lblProgramBaslik = new System.Windows.Forms.Label();
            this.lstGununProgrami = new System.Windows.Forms.ListBox();
            this.pnlSesKontrol = new System.Windows.Forms.Panel();
            this.lblSesBaslik = new System.Windows.Forms.Label();
            this.txtSesArama = new System.Windows.Forms.TextBox();
            this.lblZilSesi = new System.Windows.Forms.Label();
            this.cmbZilSesi = new System.Windows.Forms.ComboBox();
            this.btnZilTest = new System.Windows.Forms.Button();
            this.lblAlarmSesi = new System.Windows.Forms.Label();
            this.cmbAlarmSesi = new System.Windows.Forms.ComboBox();
            this.btnAlarmTest = new System.Windows.Forms.Button();
            this.lblMarsSesi = new System.Windows.Forms.Label();
            this.cmbMarsSesi = new System.Windows.Forms.ComboBox();
            this.btnMarsTest = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.trackVolume = new System.Windows.Forms.TrackBar();
            this.btnSesYenile = new System.Windows.Forms.Button();
            this.pnlAltButonlar = new System.Windows.Forms.Panel();
            this.btnHakkinda = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.btnMarsBaslat = new System.Windows.Forms.Button();
            this.btnTakvim = new System.Windows.Forms.Button();
            this.btnAyarlar = new System.Windows.Forms.Button();
            this.chkZilDevreDisi = new System.Windows.Forms.CheckBox();
            this.btnTeneffusMuzik = new System.Windows.Forms.Button();
            this.lblDurum = new System.Windows.Forms.Label();
            this.pnlSaatKutusu.SuspendLayout();
            this.pnlProfilListesi.SuspendLayout();
            this.pnlGununProgrami.SuspendLayout();
            this.pnlSesKontrol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackVolume)).BeginInit();
            this.pnlAltButonlar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSaatKutusu
            // 
            this.pnlSaatKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSaatKutusu.BackColor = System.Drawing.Color.White;
            this.pnlSaatKutusu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSaatKutusu.Controls.Add(this.lblSaat);
            this.pnlSaatKutusu.Controls.Add(this.lblTarih);
            this.pnlSaatKutusu.Controls.Add(this.lblGeriSayim);
            this.pnlSaatKutusu.Controls.Add(this.lblSiradakiZil);
            this.pnlSaatKutusu.Controls.Add(this.lblIstatistik);
            this.pnlSaatKutusu.Controls.Add(this.chkKaranlikMod);
            this.pnlSaatKutusu.Location = new System.Drawing.Point(30, 25);
            this.pnlSaatKutusu.Name = "pnlSaatKutusu";
            this.pnlSaatKutusu.Size = new System.Drawing.Size(1740, 180);
            this.pnlSaatKutusu.TabIndex = 0;
            // 
            // lblSaat
            // 
            this.lblSaat.Font = new System.Drawing.Font("Segoe UI", 64F, System.Drawing.FontStyle.Bold);
            this.lblSaat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.lblSaat.Location = new System.Drawing.Point(30, 20);
            this.lblSaat.Name = "lblSaat";
            this.lblSaat.Size = new System.Drawing.Size(450, 110);
            this.lblSaat.TabIndex = 0;
            this.lblSaat.Text = "00:00:00";
            this.lblSaat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTarih
            // 
            this.lblTarih.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblTarih.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblTarih.Location = new System.Drawing.Point(35, 135);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(450, 35);
            this.lblTarih.TabIndex = 1;
            this.lblTarih.Text = "Pazartesi, 01 Ocak 2025";
            // 
            // lblGeriSayim
            // 
            this.lblGeriSayim.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblGeriSayim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblGeriSayim.Location = new System.Drawing.Point(550, 30);
            this.lblGeriSayim.Name = "lblGeriSayim";
            this.lblGeriSayim.Size = new System.Drawing.Size(400, 50);
            this.lblGeriSayim.TabIndex = 2;
            this.lblGeriSayim.Text = "⏱️ Sıradaki zile 5 dk 30 sn";
            this.lblGeriSayim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSiradakiZil
            // 
            this.lblSiradakiZil.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblSiradakiZil.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.lblSiradakiZil.Location = new System.Drawing.Point(550, 85);
            this.lblSiradakiZil.Name = "lblSiradakiZil";
            this.lblSiradakiZil.Size = new System.Drawing.Size(400, 30);
            this.lblSiradakiZil.TabIndex = 3;
            this.lblSiradakiZil.Text = "📍 Sıradaki: 09:15 - [2. Ders] 2. Ders Bitiş";
            this.lblSiradakiZil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIstatistik
            // 
            this.lblIstatistik.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblIstatistik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblIstatistik.Location = new System.Drawing.Point(550, 125);
            this.lblIstatistik.Name = "lblIstatistik";
            this.lblIstatistik.Size = new System.Drawing.Size(400, 35);
            this.lblIstatistik.TabIndex = 4;
            this.lblIstatistik.Text = "📊 0/14 zil çaldı";
            this.lblIstatistik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkKaranlikMod
            // 
            this.chkKaranlikMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkKaranlikMod.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkKaranlikMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.chkKaranlikMod.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkKaranlikMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkKaranlikMod.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.chkKaranlikMod.Location = new System.Drawing.Point(1630, 60);
            this.chkKaranlikMod.Name = "chkKaranlikMod";
            this.chkKaranlikMod.Size = new System.Drawing.Size(80, 60);
            this.chkKaranlikMod.TabIndex = 5;
            this.chkKaranlikMod.Text = "🌙";
            this.chkKaranlikMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkKaranlikMod.UseVisualStyleBackColor = false;
            this.chkKaranlikMod.CheckedChanged += new System.EventHandler(this.chkKaranlikMod_CheckedChanged);
            // 
            // pnlProfilListesi
            // 
            this.pnlProfilListesi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlProfilListesi.BackColor = System.Drawing.Color.White;
            this.pnlProfilListesi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProfilListesi.Controls.Add(this.lblProfilBaslik);
            this.pnlProfilListesi.Controls.Add(this.pnlProfilKartlari);
            this.pnlProfilListesi.Controls.Add(this.btnYeniProfil);
            this.pnlProfilListesi.Controls.Add(this.btnProfilSil);
            this.pnlProfilListesi.Location = new System.Drawing.Point(30, 225);
            this.pnlProfilListesi.Name = "pnlProfilListesi";
            this.pnlProfilListesi.Size = new System.Drawing.Size(380, 550);
            this.pnlProfilListesi.TabIndex = 1;
            // 
            // lblProfilBaslik
            // 
            this.lblProfilBaslik.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblProfilBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.lblProfilBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblProfilBaslik.Name = "lblProfilBaslik";
            this.lblProfilBaslik.Size = new System.Drawing.Size(340, 35);
            this.lblProfilBaslik.TabIndex = 0;
            this.lblProfilBaslik.Text = "📁 Profiller";
            // 
            // pnlProfilKartlari
            // 
            this.pnlProfilKartlari.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProfilKartlari.AutoScroll = true;
            this.pnlProfilKartlari.BackColor = System.Drawing.Color.Transparent;
            this.pnlProfilKartlari.Location = new System.Drawing.Point(15, 65);
            this.pnlProfilKartlari.Name = "pnlProfilKartlari";
            this.pnlProfilKartlari.Size = new System.Drawing.Size(350, 410);
            this.pnlProfilKartlari.TabIndex = 1;
            // 
            // btnYeniProfil
            // 
            this.btnYeniProfil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYeniProfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnYeniProfil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYeniProfil.FlatAppearance.BorderSize = 0;
            this.btnYeniProfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYeniProfil.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnYeniProfil.ForeColor = System.Drawing.Color.White;
            this.btnYeniProfil.Location = new System.Drawing.Point(20, 485);
            this.btnYeniProfil.Name = "btnYeniProfil";
            this.btnYeniProfil.Size = new System.Drawing.Size(170, 50);
            this.btnYeniProfil.TabIndex = 2;
            this.btnYeniProfil.Text = "➕ Yeni Profil";
            this.btnYeniProfil.UseVisualStyleBackColor = false;
            this.btnYeniProfil.Click += new System.EventHandler(this.btnYeniProfil_Click);
            // 
            // btnProfilSil
            // 
            this.btnProfilSil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProfilSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnProfilSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProfilSil.FlatAppearance.BorderSize = 0;
            this.btnProfilSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfilSil.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnProfilSil.ForeColor = System.Drawing.Color.White;
            this.btnProfilSil.Location = new System.Drawing.Point(195, 485);
            this.btnProfilSil.Name = "btnProfilSil";
            this.btnProfilSil.Size = new System.Drawing.Size(170, 50);
            this.btnProfilSil.TabIndex = 3;
            this.btnProfilSil.Text = "🗑️ Sil";
            this.btnProfilSil.UseVisualStyleBackColor = false;
            this.btnProfilSil.Click += new System.EventHandler(this.btnProfilSil_Click);
            // 
            // pnlGununProgrami
            // 
            this.pnlGununProgrami.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlGununProgrami.BackColor = System.Drawing.Color.White;
            this.pnlGununProgrami.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGununProgrami.Controls.Add(this.lblProgramBaslik);
            this.pnlGununProgrami.Controls.Add(this.lstGununProgrami);
            this.pnlGununProgrami.Location = new System.Drawing.Point(430, 225);
            this.pnlGununProgrami.Name = "pnlGununProgrami";
            this.pnlGununProgrami.Size = new System.Drawing.Size(620, 550);
            this.pnlGununProgrami.TabIndex = 2;
            // 
            // lblProgramBaslik
            // 
            this.lblProgramBaslik.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblProgramBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.lblProgramBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblProgramBaslik.Name = "lblProgramBaslik";
            this.lblProgramBaslik.Size = new System.Drawing.Size(580, 35);
            this.lblProgramBaslik.TabIndex = 0;
            this.lblProgramBaslik.Text = "📋 Günün Programı";
            // 
            // lstGununProgrami
            // 
            this.lstGununProgrami.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGununProgrami.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstGununProgrami.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstGununProgrami.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lstGununProgrami.ItemHeight = 38;
            this.lstGununProgrami.Location = new System.Drawing.Point(20, 65);
            this.lstGununProgrami.Name = "lstGununProgrami";
            this.lstGununProgrami.Size = new System.Drawing.Size(580, 456);
            this.lstGununProgrami.TabIndex = 1;
            this.lstGununProgrami.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstGununProgrami_DrawItem);
            // 
            // pnlSesKontrol
            // 
            this.pnlSesKontrol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSesKontrol.BackColor = System.Drawing.Color.White;
            this.pnlSesKontrol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSesKontrol.Controls.Add(this.lblSesBaslik);
            this.pnlSesKontrol.Controls.Add(this.txtSesArama);
            this.pnlSesKontrol.Controls.Add(this.lblZilSesi);
            this.pnlSesKontrol.Controls.Add(this.cmbZilSesi);
            this.pnlSesKontrol.Controls.Add(this.btnZilTest);
            this.pnlSesKontrol.Controls.Add(this.lblAlarmSesi);
            this.pnlSesKontrol.Controls.Add(this.cmbAlarmSesi);
            this.pnlSesKontrol.Controls.Add(this.btnAlarmTest);
            this.pnlSesKontrol.Controls.Add(this.lblMarsSesi);
            this.pnlSesKontrol.Controls.Add(this.cmbMarsSesi);
            this.pnlSesKontrol.Controls.Add(this.btnMarsTest);
            this.pnlSesKontrol.Controls.Add(this.lblVolume);
            this.pnlSesKontrol.Controls.Add(this.trackVolume);
            this.pnlSesKontrol.Controls.Add(this.btnSesYenile);
            this.pnlSesKontrol.Location = new System.Drawing.Point(1070, 225);
            this.pnlSesKontrol.Name = "pnlSesKontrol";
            this.pnlSesKontrol.Size = new System.Drawing.Size(700, 550);
            this.pnlSesKontrol.TabIndex = 3;
            // 
            // lblSesBaslik
            // 
            this.lblSesBaslik.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSesBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.lblSesBaslik.Location = new System.Drawing.Point(20, 20);
            this.lblSesBaslik.Name = "lblSesBaslik";
            this.lblSesBaslik.Size = new System.Drawing.Size(250, 35);
            this.lblSesBaslik.TabIndex = 0;
            this.lblSesBaslik.Text = "🎵 Ses Ayarları";
            // 
            // txtSesArama
            // 
            this.txtSesArama.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSesArama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSesArama.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtSesArama.ForeColor = System.Drawing.Color.Gray;
            this.txtSesArama.Location = new System.Drawing.Point(20, 65);
            this.txtSesArama.Name = "txtSesArama";
            this.txtSesArama.Size = new System.Drawing.Size(660, 31);
            this.txtSesArama.TabIndex = 1;
            this.txtSesArama.Text = "🔍 Ses dosyası ara...";
            this.txtSesArama.TextChanged += new System.EventHandler(this.txtSesArama_TextChanged);
            this.txtSesArama.Enter += new System.EventHandler(this.txtSesArama_Enter);
            this.txtSesArama.Leave += new System.EventHandler(this.txtSesArama_Leave);
            // 
            // lblZilSesi
            // 
            this.lblZilSesi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblZilSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblZilSesi.Location = new System.Drawing.Point(20, 120);
            this.lblZilSesi.Name = "lblZilSesi";
            this.lblZilSesi.Size = new System.Drawing.Size(120, 28);
            this.lblZilSesi.TabIndex = 2;
            this.lblZilSesi.Text = "🔔 Zil Sesi:";
            // 
            // cmbZilSesi
            // 
            this.cmbZilSesi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbZilSesi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZilSesi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbZilSesi.Location = new System.Drawing.Point(20, 150);
            this.cmbZilSesi.Name = "cmbZilSesi";
            this.cmbZilSesi.Size = new System.Drawing.Size(520, 29);
            this.cmbZilSesi.TabIndex = 3;
            this.cmbZilSesi.SelectedIndexChanged += new System.EventHandler(this.cmbZilSesi_SelectedIndexChanged);
            // 
            // btnZilTest
            // 
            this.btnZilTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZilTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnZilTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZilTest.FlatAppearance.BorderSize = 0;
            this.btnZilTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZilTest.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnZilTest.ForeColor = System.Drawing.Color.White;
            this.btnZilTest.Location = new System.Drawing.Point(550, 148);
            this.btnZilTest.Name = "btnZilTest";
            this.btnZilTest.Size = new System.Drawing.Size(130, 40);
            this.btnZilTest.TabIndex = 4;
            this.btnZilTest.Text = "▶ Test";
            this.btnZilTest.UseVisualStyleBackColor = false;
            this.btnZilTest.Click += new System.EventHandler(this.btnZilTest_Click);
            // 
            // lblAlarmSesi
            // 
            this.lblAlarmSesi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAlarmSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblAlarmSesi.Location = new System.Drawing.Point(20, 210);
            this.lblAlarmSesi.Name = "lblAlarmSesi";
            this.lblAlarmSesi.Size = new System.Drawing.Size(150, 28);
            this.lblAlarmSesi.TabIndex = 5;
            this.lblAlarmSesi.Text = "🚨 Alarm Sesi:";
            // 
            // cmbAlarmSesi
            // 
            this.cmbAlarmSesi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAlarmSesi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlarmSesi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbAlarmSesi.Location = new System.Drawing.Point(20, 240);
            this.cmbAlarmSesi.Name = "cmbAlarmSesi";
            this.cmbAlarmSesi.Size = new System.Drawing.Size(520, 29);
            this.cmbAlarmSesi.TabIndex = 6;
            this.cmbAlarmSesi.SelectedIndexChanged += new System.EventHandler(this.cmbAlarmSesi_SelectedIndexChanged);
            // 
            // btnAlarmTest
            // 
            this.btnAlarmTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlarmTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnAlarmTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlarmTest.FlatAppearance.BorderSize = 0;
            this.btnAlarmTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarmTest.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAlarmTest.ForeColor = System.Drawing.Color.White;
            this.btnAlarmTest.Location = new System.Drawing.Point(550, 238);
            this.btnAlarmTest.Name = "btnAlarmTest";
            this.btnAlarmTest.Size = new System.Drawing.Size(130, 40);
            this.btnAlarmTest.TabIndex = 7;
            this.btnAlarmTest.Text = "▶ Test";
            this.btnAlarmTest.UseVisualStyleBackColor = false;
            this.btnAlarmTest.Click += new System.EventHandler(this.btnAlarmTest_Click);
            // 
            // lblMarsSesi
            // 
            this.lblMarsSesi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMarsSesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblMarsSesi.Location = new System.Drawing.Point(20, 300);
            this.lblMarsSesi.Name = "lblMarsSesi";
            this.lblMarsSesi.Size = new System.Drawing.Size(150, 28);
            this.lblMarsSesi.TabIndex = 8;
            this.lblMarsSesi.Text = "🎺 Marş Sesi:";
            // 
            // cmbMarsSesi
            // 
            this.cmbMarsSesi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMarsSesi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarsSesi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbMarsSesi.Location = new System.Drawing.Point(20, 330);
            this.cmbMarsSesi.Name = "cmbMarsSesi";
            this.cmbMarsSesi.Size = new System.Drawing.Size(520, 29);
            this.cmbMarsSesi.TabIndex = 9;
            this.cmbMarsSesi.SelectedIndexChanged += new System.EventHandler(this.cmbMarsSesi_SelectedIndexChanged);
            // 
            // btnMarsTest
            // 
            this.btnMarsTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarsTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnMarsTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarsTest.FlatAppearance.BorderSize = 0;
            this.btnMarsTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarsTest.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMarsTest.ForeColor = System.Drawing.Color.White;
            this.btnMarsTest.Location = new System.Drawing.Point(550, 328);
            this.btnMarsTest.Name = "btnMarsTest";
            this.btnMarsTest.Size = new System.Drawing.Size(130, 40);
            this.btnMarsTest.TabIndex = 10;
            this.btnMarsTest.Text = "▶ Test";
            this.btnMarsTest.UseVisualStyleBackColor = false;
            this.btnMarsTest.Click += new System.EventHandler(this.btnMarsTest_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblVolume.Location = new System.Drawing.Point(20, 410);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(150, 35);
            this.lblVolume.TabIndex = 11;
            this.lblVolume.Text = "🔊 100%";
            // 
            // trackVolume
            // 
            this.trackVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackVolume.Location = new System.Drawing.Point(20, 450);
            this.trackVolume.Maximum = 100;
            this.trackVolume.Name = "trackVolume";
            this.trackVolume.Size = new System.Drawing.Size(660, 45);
            this.trackVolume.TabIndex = 12;
            this.trackVolume.TickFrequency = 10;
            this.trackVolume.Value = 80;
            this.trackVolume.Scroll += new System.EventHandler(this.trackVolume_Scroll);
            // 
            // btnSesYenile
            // 
            this.btnSesYenile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnSesYenile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSesYenile.FlatAppearance.BorderSize = 0;
            this.btnSesYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSesYenile.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSesYenile.ForeColor = System.Drawing.Color.White;
            this.btnSesYenile.Location = new System.Drawing.Point(560, 505);
            this.btnSesYenile.Name = "btnSesYenile";
            this.btnSesYenile.Size = new System.Drawing.Size(120, 40);
            this.btnSesYenile.TabIndex = 13;
            this.btnSesYenile.Text = "🔄 Yenile";
            this.btnSesYenile.UseVisualStyleBackColor = false;
            this.btnSesYenile.Click += new System.EventHandler(this.btnSesYenile_Click);
            // 
            // pnlAltButonlar
            // 
            this.pnlAltButonlar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAltButonlar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlAltButonlar.Controls.Add(this.btnHakkinda);
            this.pnlAltButonlar.Controls.Add(this.btnAlarm);
            this.pnlAltButonlar.Controls.Add(this.btnMarsBaslat);
            this.pnlAltButonlar.Controls.Add(this.btnTakvim);
            this.pnlAltButonlar.Controls.Add(this.btnAyarlar);
            this.pnlAltButonlar.Controls.Add(this.chkZilDevreDisi);
            this.pnlAltButonlar.Controls.Add(this.btnTeneffusMuzik);
            this.pnlAltButonlar.Controls.Add(this.lblDurum);
            this.pnlAltButonlar.Location = new System.Drawing.Point(30, 795);
            this.pnlAltButonlar.Name = "pnlAltButonlar";
            this.pnlAltButonlar.Size = new System.Drawing.Size(1740, 75);
            this.pnlAltButonlar.TabIndex = 4;
            // 
            // btnHakkinda
            // 
            this.btnHakkinda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnHakkinda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHakkinda.FlatAppearance.BorderSize = 0;
            this.btnHakkinda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHakkinda.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHakkinda.ForeColor = System.Drawing.Color.White;
            this.btnHakkinda.Location = new System.Drawing.Point(1330, 10);
            this.btnHakkinda.Name = "btnHakkinda";
            this.btnHakkinda.Size = new System.Drawing.Size(130, 55);
            this.btnHakkinda.TabIndex = 7;
            this.btnHakkinda.Text = "ℹ️ Hakkında";
            this.btnHakkinda.UseVisualStyleBackColor = false;
            this.btnHakkinda.Click += new System.EventHandler(this.btnHakkinda_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlarm.FlatAppearance.BorderSize = 0;
            this.btnAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarm.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnAlarm.ForeColor = System.Drawing.Color.White;
            this.btnAlarm.Location = new System.Drawing.Point(10, 10);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(200, 55);
            this.btnAlarm.TabIndex = 0;
            this.btnAlarm.Text = "🚨 ACİL ALARM";
            this.btnAlarm.UseVisualStyleBackColor = false;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // btnMarsBaslat
            // 
            this.btnMarsBaslat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnMarsBaslat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarsBaslat.FlatAppearance.BorderSize = 0;
            this.btnMarsBaslat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarsBaslat.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMarsBaslat.ForeColor = System.Drawing.Color.White;
            this.btnMarsBaslat.Location = new System.Drawing.Point(220, 10);
            this.btnMarsBaslat.Name = "btnMarsBaslat";
            this.btnMarsBaslat.Size = new System.Drawing.Size(160, 55);
            this.btnMarsBaslat.TabIndex = 1;
            this.btnMarsBaslat.Text = "🎵 MARŞ";
            this.btnMarsBaslat.UseVisualStyleBackColor = false;
            this.btnMarsBaslat.Click += new System.EventHandler(this.btnMarsBaslat_Click);
            // 
            // btnTakvim
            // 
            this.btnTakvim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnTakvim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTakvim.FlatAppearance.BorderSize = 0;
            this.btnTakvim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakvim.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnTakvim.ForeColor = System.Drawing.Color.White;
            this.btnTakvim.Location = new System.Drawing.Point(390, 10);
            this.btnTakvim.Name = "btnTakvim";
            this.btnTakvim.Size = new System.Drawing.Size(180, 55);
            this.btnTakvim.TabIndex = 2;
            this.btnTakvim.Text = "📅 TAKVİM";
            this.btnTakvim.UseVisualStyleBackColor = false;
            this.btnTakvim.Click += new System.EventHandler(this.btnTakvim_Click);
            // 
            // btnAyarlar
            // 
            this.btnAyarlar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.btnAyarlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAyarlar.FlatAppearance.BorderSize = 0;
            this.btnAyarlar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAyarlar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnAyarlar.ForeColor = System.Drawing.Color.White;
            this.btnAyarlar.Location = new System.Drawing.Point(580, 10);
            this.btnAyarlar.Name = "btnAyarlar";
            this.btnAyarlar.Size = new System.Drawing.Size(250, 55);
            this.btnAyarlar.TabIndex = 3;
            this.btnAyarlar.Text = "✏️ PROFİL DÜZENLE";
            this.btnAyarlar.UseVisualStyleBackColor = false;
            this.btnAyarlar.Click += new System.EventHandler(this.btnAyarlar_Click);
            // 
            // chkZilDevreDisi
            // 
            this.chkZilDevreDisi.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkZilDevreDisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.chkZilDevreDisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkZilDevreDisi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkZilDevreDisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.chkZilDevreDisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkZilDevreDisi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.chkZilDevreDisi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.chkZilDevreDisi.Location = new System.Drawing.Point(850, 10);
            this.chkZilDevreDisi.Name = "chkZilDevreDisi";
            this.chkZilDevreDisi.Size = new System.Drawing.Size(200, 55);
            this.chkZilDevreDisi.TabIndex = 4;
            this.chkZilDevreDisi.Text = "🔇 Zilleri Kapat";
            this.chkZilDevreDisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkZilDevreDisi.UseVisualStyleBackColor = false;
            this.chkZilDevreDisi.CheckedChanged += new System.EventHandler(this.chkZilDevreDisi_CheckedChanged);
            // 
            // btnTeneffusMuzik
            // 
            this.btnTeneffusMuzik.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnTeneffusMuzik.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTeneffusMuzik.FlatAppearance.BorderSize = 0;
            this.btnTeneffusMuzik.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeneffusMuzik.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnTeneffusMuzik.ForeColor = System.Drawing.Color.White;
            this.btnTeneffusMuzik.Location = new System.Drawing.Point(1070, 10);
            this.btnTeneffusMuzik.Name = "btnTeneffusMuzik";
            this.btnTeneffusMuzik.Size = new System.Drawing.Size(250, 55);
            this.btnTeneffusMuzik.TabIndex = 5;
            this.btnTeneffusMuzik.Text = "🎵 TENEFFÜS MÜZİĞİ";
            this.btnTeneffusMuzik.UseVisualStyleBackColor = false;
            this.btnTeneffusMuzik.Click += new System.EventHandler(this.btnTeneffusMuzik_Click);
            // 
            // lblDurum
            // 
            this.lblDurum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDurum.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblDurum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(128)))), ((int)(((byte)(150)))));
            this.lblDurum.Location = new System.Drawing.Point(1470, 20);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(260, 35);
            this.lblDurum.TabIndex = 6;
            this.lblDurum.Text = "📍 Sistem hazır";
            this.lblDurum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1800, 900);
            this.Controls.Add(this.pnlSaatKutusu);
            this.Controls.Add(this.pnlProfilListesi);
            this.Controls.Add(this.pnlGununProgrami);
            this.Controls.Add(this.pnlSesKontrol);
            this.Controls.Add(this.pnlAltButonlar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1400, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🔔 SSEML Okul Zili Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pnlSaatKutusu.ResumeLayout(false);
            this.pnlProfilListesi.ResumeLayout(false);
            this.pnlGununProgrami.ResumeLayout(false);
            this.pnlSesKontrol.ResumeLayout(false);
            this.pnlSesKontrol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackVolume)).EndInit();
            this.pnlAltButonlar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Liste özel çizim - GÜNÜN PROGRAMI (Memory Leak Düzeltildi)
       

        #endregion

        // Timer'lar
        private System.Windows.Forms.Timer tmrSaat;
        private System.Windows.Forms.Timer tmrZilCheck;

        // Paneller
        private System.Windows.Forms.Panel pnlSaatKutusu;
        private System.Windows.Forms.Panel pnlProfilListesi;
        private System.Windows.Forms.Panel pnlProfilKartlari;
        private System.Windows.Forms.Panel pnlGununProgrami;
        private System.Windows.Forms.Panel pnlSesKontrol;
        private System.Windows.Forms.Panel pnlAltButonlar;

        // Saat Kutusu
        private System.Windows.Forms.Label lblSaat;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Label lblGeriSayim;
        private System.Windows.Forms.Label lblSiradakiZil;
        private System.Windows.Forms.Label lblIstatistik;
        private System.Windows.Forms.CheckBox chkKaranlikMod;

        // Profil Listesi
        private System.Windows.Forms.Label lblProfilBaslik;
        private System.Windows.Forms.Button btnYeniProfil;
        private System.Windows.Forms.Button btnProfilSil;

        // Günün Programı
        private System.Windows.Forms.Label lblProgramBaslik;
        private System.Windows.Forms.ListBox lstGununProgrami;

        // Ses Kontrol
        private System.Windows.Forms.Label lblSesBaslik;
        private System.Windows.Forms.TextBox txtSesArama;
        private System.Windows.Forms.Label lblZilSesi;
        private System.Windows.Forms.ComboBox cmbZilSesi;
        private System.Windows.Forms.Button btnZilTest;
        private System.Windows.Forms.Label lblAlarmSesi;
        private System.Windows.Forms.ComboBox cmbAlarmSesi;
        private System.Windows.Forms.Button btnAlarmTest;
        private System.Windows.Forms.Label lblMarsSesi;
        private System.Windows.Forms.Button btnHakkinda;
        private System.Windows.Forms.ComboBox cmbMarsSesi;
        private System.Windows.Forms.Button btnSesYenile;
        private System.Windows.Forms.Button btnMarsTest;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar trackVolume;

        // Alt Butonlar
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Button btnMarsBaslat;
        private System.Windows.Forms.Button btnTakvim;
        private System.Windows.Forms.Button btnAyarlar;
        private System.Windows.Forms.CheckBox chkZilDevreDisi;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.Button btnTeneffusMuzik;
    }
}