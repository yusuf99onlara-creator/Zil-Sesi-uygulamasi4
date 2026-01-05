using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SSEML_OkulZili
{
    public partial class TeneffusMuzikForm : Form
    {
        public List<TeneffusMuzikProfili> Profiller { get; set; }
        private int duzenlenenProfilIndex = -1;

        // Kontroller
        private Panel pnlSol;
        private Panel pnlSag;
        private ListBox lstProfiller;
        private TextBox txtProfilAdi;
        private Label lblProfilAdiHata;
        private CheckBox chkAktif;
        private ComboBox cmbGun;
        private MaskedTextBox txtBaslangic;
        private MaskedTextBox txtBitis;
        private Label lblSaatHata;
        private TrackBar trackSes;
        private Label lblSesYuzde;
        private CheckedListBox lstMuzikler;
        private Label lblMuzikHata;
        private Button btnYeni;
        private Button btnKaydet;
        private Button btnSil;
        private Label lblDurum;
        private Panel pnlValidasyon;
        private Label lblValidasyonDurum;

        // Validasyon renkleri
        private Color hataBorderRengi = Color.FromArgb(239, 68, 68);
        private Color gecerliBorderRengi = Color.FromArgb(34, 197, 94);
        private Color normalBorderRengi = Color.FromArgb(203, 213, 225);
        private Color hataArkaplanRengi = Color.FromArgb(254, 242, 242);
        private Color gecerliArkaplanRengi = Color.FromArgb(240, 253, 244);

        public TeneffusMuzikForm(List<TeneffusMuzikProfili> profiller)
        {
            // Derin kopya oluştur
            this.Profiller = new List<TeneffusMuzikProfili>();
            foreach (var p in profiller ?? new List<TeneffusMuzikProfili>())
            {
                this.Profiller.Add(new TeneffusMuzikProfili
                {
                    ProfilAdi = p.ProfilAdi,
                    Aktif = p.Aktif,
                    Gun = p.Gun,
                    BaslangicSaat = p.BaslangicSaat,
                    BitisSaat = p.BitisSaat,
                    SesSeviyesi = p.SesSeviyesi,
                    MuzikDosyalari = new List<string>(p.MuzikDosyalari)
                });
            }
            InitializeComponent();
            this.Load += TeneffusMuzikForm_Load;
        }

        private void InitializeComponent()
        {
            this.Text = "🎵 Teneffüs Müzik Ayarları";
            this.Size = new Size(920, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Segoe UI", 9F);

            // ═══════════════════════════════════════════
            // SOL PANEL - Profil Listesi
            // ═══════════════════════════════════════════
            pnlSol = new Panel();
            pnlSol.Location = new Point(20, 20);
            pnlSol.Size = new Size(260, 580);
            pnlSol.BackColor = Color.White;
            YuvarlakKoseUygula(pnlSol, 12);
            this.Controls.Add(pnlSol);

            Label lblProfilBaslik = new Label();
            lblProfilBaslik.Text = "📋 Müzik Profilleri";
            lblProfilBaslik.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblProfilBaslik.ForeColor = Color.FromArgb(51, 65, 85);
            lblProfilBaslik.Location = new Point(15, 15);
            lblProfilBaslik.AutoSize = true;
            pnlSol.Controls.Add(lblProfilBaslik);

            lstProfiller = new ListBox();
            lstProfiller.Location = new Point(15, 50);
            lstProfiller.Size = new Size(230, 470);
            lstProfiller.Font = new Font("Segoe UI", 11);
            lstProfiller.BorderStyle = BorderStyle.None;
            lstProfiller.BackColor = Color.FromArgb(248, 250, 252);
            lstProfiller.DrawMode = DrawMode.OwnerDrawFixed;
            lstProfiller.ItemHeight = 50;
            lstProfiller.DrawItem += LstProfiller_DrawItem;
            lstProfiller.SelectedIndexChanged += LstProfiller_SelectedIndexChanged;
            pnlSol.Controls.Add(lstProfiller);

            Label lblProfilSayisi = new Label();
            lblProfilSayisi.Name = "lblProfilSayisi";
            lblProfilSayisi.Text = "";
            lblProfilSayisi.Font = new Font("Segoe UI", 9);
            lblProfilSayisi.ForeColor = Color.FromArgb(100, 116, 139);
            lblProfilSayisi.Location = new Point(15, 530);
            lblProfilSayisi.AutoSize = true;
            pnlSol.Controls.Add(lblProfilSayisi);

            // ═══════════════════════════════════════════
            // SAĞ PANEL - Profil Ayarları
            // ═══════════════════════════════════════════
            pnlSag = new Panel();
            pnlSag.Location = new Point(300, 20);
            pnlSag.Size = new Size(595, 580);
            pnlSag.BackColor = Color.White;
            YuvarlakKoseUygula(pnlSag, 12);
            this.Controls.Add(pnlSag);

            Label lblAyarlarBaslik = new Label();
            lblAyarlarBaslik.Text = "⚙️ Profil Ayarları";
            lblAyarlarBaslik.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblAyarlarBaslik.ForeColor = Color.FromArgb(51, 65, 85);
            lblAyarlarBaslik.Location = new Point(20, 15);
            lblAyarlarBaslik.AutoSize = true;
            pnlSag.Controls.Add(lblAyarlarBaslik);

            // Validasyon durumu paneli
            pnlValidasyon = new Panel();
            pnlValidasyon.Location = new Point(250, 10);
            pnlValidasyon.Size = new Size(330, 30);
            pnlValidasyon.BackColor = Color.Transparent;
            pnlSag.Controls.Add(pnlValidasyon);

            lblValidasyonDurum = new Label();
            lblValidasyonDurum.Text = "";
            lblValidasyonDurum.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblValidasyonDurum.Location = new Point(5, 7);
            lblValidasyonDurum.AutoSize = true;
            pnlValidasyon.Controls.Add(lblValidasyonDurum);

            // Ayırıcı çizgi
            Panel ayirici = new Panel();
            ayirici.Location = new Point(20, 50);
            ayirici.Size = new Size(555, 1);
            ayirici.BackColor = Color.FromArgb(226, 232, 240);
            pnlSag.Controls.Add(ayirici);

            int y = 70;
            int labelX = 20;
            int inputX = 150;
            int satirYuksekligi = 55;

            // ─────────────────────────────────────────
            // Profil Adı (ZORUNLU)
            // ─────────────────────────────────────────
            Label lblProfilAdi = new Label();
            lblProfilAdi.Text = "Profil Adı: *";
            lblProfilAdi.Location = new Point(labelX, y + 5);
            lblProfilAdi.Font = new Font("Segoe UI", 10);
            lblProfilAdi.ForeColor = Color.FromArgb(71, 85, 105);
            lblProfilAdi.AutoSize = true;
            pnlSag.Controls.Add(lblProfilAdi);

            txtProfilAdi = new TextBox();
            txtProfilAdi.Location = new Point(inputX, y);
            txtProfilAdi.Size = new Size(220, 30);
            txtProfilAdi.Font = new Font("Segoe UI", 11);
            txtProfilAdi.BorderStyle = BorderStyle.FixedSingle;
            txtProfilAdi.MaxLength = 50;
            txtProfilAdi.TextChanged += TxtProfilAdi_TextChanged;
            txtProfilAdi.Leave += TxtProfilAdi_Leave;
            pnlSag.Controls.Add(txtProfilAdi);

            // Hata mesajı etiketi
            lblProfilAdiHata = new Label();
            lblProfilAdiHata.Text = "";
            lblProfilAdiHata.Location = new Point(inputX, y + 28);
            lblProfilAdiHata.Font = new Font("Segoe UI", 8);
            lblProfilAdiHata.ForeColor = hataBorderRengi;
            lblProfilAdiHata.AutoSize = true;
            pnlSag.Controls.Add(lblProfilAdiHata);

            chkAktif = new CheckBox();
            chkAktif.Text = "✓ Aktif";
            chkAktif.Location = new Point(390, y + 2);
            chkAktif.Size = new Size(100, 25);
            chkAktif.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            chkAktif.ForeColor = Color.FromArgb(34, 197, 94);
            chkAktif.Checked = true;
            pnlSag.Controls.Add(chkAktif);

            y += satirYuksekligi;

            // ─────────────────────────────────────────
            // Gün Seçimi (ZORUNLU)
            // ─────────────────────────────────────────
            Label lblGun = new Label();
            lblGun.Text = "Gün: *";
            lblGun.Location = new Point(labelX, y + 5);
            lblGun.Font = new Font("Segoe UI", 10);
            lblGun.ForeColor = Color.FromArgb(71, 85, 105);
            lblGun.AutoSize = true;
            pnlSag.Controls.Add(lblGun);

            cmbGun = new ComboBox();
            cmbGun.Location = new Point(inputX, y);
            cmbGun.Size = new Size(180, 30);
            cmbGun.Font = new Font("Segoe UI", 10);
            cmbGun.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGun.BackColor = Color.White;
            cmbGun.SelectedIndexChanged += (s, e) => TumValidasyonlariKontrolEt();
            pnlSag.Controls.Add(cmbGun);

            y += satirYuksekligi - 10;

            // ─────────────────────────────────────────
            // Saat Aralığı (ZORUNLU - MaskedTextBox)
            // ─────────────────────────────────────────
            Label lblSaatAraligi = new Label();
            lblSaatAraligi.Text = "Saat Aralığı: *";
            lblSaatAraligi.Location = new Point(labelX, y + 5);
            lblSaatAraligi.Font = new Font("Segoe UI", 10);
            lblSaatAraligi.ForeColor = Color.FromArgb(71, 85, 105);
            lblSaatAraligi.AutoSize = true;
            pnlSag.Controls.Add(lblSaatAraligi);

            // Başlangıç saati - MaskedTextBox
            txtBaslangic = new MaskedTextBox();
            txtBaslangic.Location = new Point(inputX, y);
            txtBaslangic.Size = new Size(80, 30);
            txtBaslangic.Font = new Font("Segoe UI", 11);
            txtBaslangic.Mask = "00:00";
            txtBaslangic.ValidatingType = typeof(DateTime);
            txtBaslangic.Text = "0955";
            txtBaslangic.TextAlign = HorizontalAlignment.Center;
            txtBaslangic.BorderStyle = BorderStyle.FixedSingle;
            txtBaslangic.TextChanged += SaatKontrol_TextChanged;
            txtBaslangic.Leave += SaatKontrol_Leave;
            pnlSag.Controls.Add(txtBaslangic);

            Label lblTire = new Label();
            lblTire.Text = "→";
            lblTire.Location = new Point(inputX + 90, y + 5);
            lblTire.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTire.ForeColor = Color.FromArgb(99, 102, 241);
            lblTire.AutoSize = true;
            pnlSag.Controls.Add(lblTire);

            // Bitiş saati - MaskedTextBox
            txtBitis = new MaskedTextBox();
            txtBitis.Location = new Point(inputX + 120, y);
            txtBitis.Size = new Size(80, 30);
            txtBitis.Font = new Font("Segoe UI", 11);
            txtBitis.Mask = "00:00";
            txtBitis.ValidatingType = typeof(DateTime);
            txtBitis.Text = "1010";
            txtBitis.TextAlign = HorizontalAlignment.Center;
            txtBitis.BorderStyle = BorderStyle.FixedSingle;
            txtBitis.TextChanged += SaatKontrol_TextChanged;
            txtBitis.Leave += SaatKontrol_Leave;
            pnlSag.Controls.Add(txtBitis);

            // Saat hata mesajı
            lblSaatHata = new Label();
            lblSaatHata.Text = "";
            lblSaatHata.Location = new Point(inputX, y + 28);
            lblSaatHata.Font = new Font("Segoe UI", 8);
            lblSaatHata.ForeColor = hataBorderRengi;
            lblSaatHata.AutoSize = true;
            pnlSag.Controls.Add(lblSaatHata);

            // Saat formatı ipucu
            Label lblSaatFormat = new Label();
            lblSaatFormat.Text = "Örn: 09:55 → 10:10";
            lblSaatFormat.Location = new Point(inputX + 210, y + 7);
            lblSaatFormat.Font = new Font("Segoe UI", 8);
            lblSaatFormat.ForeColor = Color.FromArgb(148, 163, 184);
            lblSaatFormat.AutoSize = true;
            pnlSag.Controls.Add(lblSaatFormat);

            y += satirYuksekligi;

            // ─────────────────────────────────────────
            // Ses Seviyesi
            // ─────────────────────────────────────────
            Label lblSes = new Label();
            lblSes.Text = "Ses Seviyesi:";
            lblSes.Location = new Point(labelX, y + 5);
            lblSes.Font = new Font("Segoe UI", 10);
            lblSes.ForeColor = Color.FromArgb(71, 85, 105);
            lblSes.AutoSize = true;
            pnlSag.Controls.Add(lblSes);

            trackSes = new TrackBar();
            trackSes.Location = new Point(inputX - 10, y - 5);
            trackSes.Size = new Size(280, 45);
            trackSes.Minimum = 5; // Minimum 5% - tamamen kapatmaya izin verme
            trackSes.Maximum = 100;
            trackSes.Value = 30;
            trackSes.TickFrequency = 10;
            trackSes.BackColor = Color.White;
            trackSes.Scroll += TrackSes_Scroll;
            pnlSag.Controls.Add(trackSes);

            lblSesYuzde = new Label();
            lblSesYuzde.Text = "🔉 30%";
            lblSesYuzde.Location = new Point(inputX + 280, y + 5);
            lblSesYuzde.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSesYuzde.ForeColor = Color.FromArgb(99, 102, 241);
            lblSesYuzde.AutoSize = true;
            pnlSag.Controls.Add(lblSesYuzde);

            y += satirYuksekligi;

            // ─────────────────────────────────────────
            // Müzik Seçimi (EN AZ 1 ZORUNLU)
            // ─────────────────────────────────────────
            Panel pnlMuzikBaslik = new Panel();
            pnlMuzikBaslik.Location = new Point(labelX, y);
            pnlMuzikBaslik.Size = new Size(555, 35);
            pnlMuzikBaslik.BackColor = Color.FromArgb(248, 250, 252);
            pnlSag.Controls.Add(pnlMuzikBaslik);

            Label lblMuzikler = new Label();
            lblMuzikler.Text = "🎵 Çalınacak Müzikler (en az 1 seçin) *";
            lblMuzikler.Location = new Point(10, 8);
            lblMuzikler.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblMuzikler.ForeColor = Color.FromArgb(51, 65, 85);
            lblMuzikler.AutoSize = true;
            pnlMuzikBaslik.Controls.Add(lblMuzikler);

            // Tümünü seç/kaldır butonları
            Button btnTumunuSec = new Button();
            btnTumunuSec.Text = "Tümünü Seç";
            btnTumunuSec.Location = new Point(350, 4);
            btnTumunuSec.Size = new Size(90, 26);
            btnTumunuSec.FlatStyle = FlatStyle.Flat;
            btnTumunuSec.BackColor = Color.FromArgb(59, 130, 246);
            btnTumunuSec.ForeColor = Color.White;
            btnTumunuSec.Font = new Font("Segoe UI", 8);
            btnTumunuSec.FlatAppearance.BorderSize = 0;
            btnTumunuSec.Cursor = Cursors.Hand;
            btnTumunuSec.Click += (s, e) => {
                for (int i = 0; i < lstMuzikler.Items.Count; i++)
                    lstMuzikler.SetItemChecked(i, true);
                MuzikSecimKontrol();
            };
            pnlMuzikBaslik.Controls.Add(btnTumunuSec);

            Button btnTumunuKaldir = new Button();
            btnTumunuKaldir.Text = "Temizle";
            btnTumunuKaldir.Location = new Point(445, 4);
            btnTumunuKaldir.Size = new Size(60, 26);
            btnTumunuKaldir.FlatStyle = FlatStyle.Flat;
            btnTumunuKaldir.BackColor = Color.FromArgb(148, 163, 184);
            btnTumunuKaldir.ForeColor = Color.White;
            btnTumunuKaldir.Font = new Font("Segoe UI", 8);
            btnTumunuKaldir.FlatAppearance.BorderSize = 0;
            btnTumunuKaldir.Cursor = Cursors.Hand;
            btnTumunuKaldir.Click += (s, e) => {
                for (int i = 0; i < lstMuzikler.Items.Count; i++)
                    lstMuzikler.SetItemChecked(i, false);
                MuzikSecimKontrol();
            };
            pnlMuzikBaslik.Controls.Add(btnTumunuKaldir);

            Button btnYenile = new Button();
            btnYenile.Text = "🔄";
            btnYenile.Location = new Point(510, 4);
            btnYenile.Size = new Size(35, 26);
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.BackColor = Color.FromArgb(226, 232, 240);
            btnYenile.ForeColor = Color.FromArgb(71, 85, 105);
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.Cursor = Cursors.Hand;
            btnYenile.Click += (s, e) => MuzikleriYukle();
            pnlMuzikBaslik.Controls.Add(btnYenile);

            y += 40;

            lstMuzikler = new CheckedListBox();
            lstMuzikler.Location = new Point(labelX, y);
            lstMuzikler.Size = new Size(555, 170);
            lstMuzikler.Font = new Font("Segoe UI", 10);
            lstMuzikler.BorderStyle = BorderStyle.FixedSingle;
            lstMuzikler.BackColor = Color.FromArgb(250, 251, 252);
            lstMuzikler.CheckOnClick = true;
            lstMuzikler.ItemCheck += LstMuzikler_ItemCheck;
            pnlSag.Controls.Add(lstMuzikler);

            // Müzik hata mesajı
            lblMuzikHata = new Label();
            lblMuzikHata.Text = "";
            lblMuzikHata.Location = new Point(labelX, y + 175);
            lblMuzikHata.Font = new Font("Segoe UI", 8);
            lblMuzikHata.ForeColor = hataBorderRengi;
            lblMuzikHata.AutoSize = true;
            pnlSag.Controls.Add(lblMuzikHata);

            // Müzik klasörü ipucu
            Label lblMuzikIpucu = new Label();
            lblMuzikIpucu.Text = "📁 Müzikleri 'TeneffusMuzikleri' klasörüne ekleyin (.mp3, .wav, .wma)";
            lblMuzikIpucu.Location = new Point(labelX + 200, y + 175);
            lblMuzikIpucu.Font = new Font("Segoe UI", 8);
            lblMuzikIpucu.ForeColor = Color.FromArgb(148, 163, 184);
            lblMuzikIpucu.AutoSize = true;
            pnlSag.Controls.Add(lblMuzikIpucu);

            // Zorunlu alan açıklaması
            Label lblZorunlu = new Label();
            lblZorunlu.Text = "* Zorunlu alanlar";
            lblZorunlu.Location = new Point(labelX, y + 195);
            lblZorunlu.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblZorunlu.ForeColor = Color.FromArgb(239, 68, 68);
            lblZorunlu.AutoSize = true;
            pnlSag.Controls.Add(lblZorunlu);

            // ═══════════════════════════════════════════
            // ALT BUTONLAR
            // ═══════════════════════════════════════════
            Panel pnlAlt = new Panel();
            pnlAlt.Location = new Point(20, 610);
            pnlAlt.Size = new Size(875, 50);
            pnlAlt.BackColor = Color.Transparent;
            this.Controls.Add(pnlAlt);

            btnYeni = CreateButton("➕ Yeni Profil", Color.FromArgb(34, 197, 94));
            btnYeni.Location = new Point(0, 0);
            btnYeni.Size = new Size(130, 45);
            btnYeni.Click += BtnYeni_Click;
            pnlAlt.Controls.Add(btnYeni);

            btnKaydet = CreateButton("💾 Kaydet", Color.FromArgb(99, 102, 241));
            btnKaydet.Location = new Point(140, 0);
            btnKaydet.Size = new Size(120, 45);
            btnKaydet.Enabled = false; // Başta pasif
            btnKaydet.Click += BtnKaydet_Click;
            pnlAlt.Controls.Add(btnKaydet);

            btnSil = CreateButton("🗑️ Sil", Color.FromArgb(239, 68, 68));
            btnSil.Location = new Point(270, 0);
            btnSil.Size = new Size(100, 45);
            btnSil.Click += BtnSil_Click;
            pnlAlt.Controls.Add(btnSil);

            // Durum etiketi
            lblDurum = new Label();
            lblDurum.Text = "";
            lblDurum.Location = new Point(390, 12);
            lblDurum.Font = new Font("Segoe UI", 10);
            lblDurum.ForeColor = Color.FromArgb(100, 116, 139);
            lblDurum.Size = new Size(250, 25);
            pnlAlt.Controls.Add(lblDurum);

            Button btnTamam = CreateButton("✓ Tamam", Color.FromArgb(59, 130, 246));
            btnTamam.Location = new Point(645, 0);
            btnTamam.Size = new Size(110, 45);
            btnTamam.Click += (s, e) => {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            pnlAlt.Controls.Add(btnTamam);

            Button btnIptal = CreateButton("✕ İptal", Color.FromArgb(100, 116, 139));
            btnIptal.Location = new Point(765, 0);
            btnIptal.Size = new Size(110, 45);
            btnIptal.Click += (s, e) => {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            pnlAlt.Controls.Add(btnIptal);
        }

        // ═══════════════════════════════════════════
        // VALİDASYON METODLARI
        // ═══════════════════════════════════════════

        private void TxtProfilAdi_TextChanged(object sender, EventArgs e)
        {
            ProfilAdiValidasyonu();
            TumValidasyonlariKontrolEt();
        }

        private void TxtProfilAdi_Leave(object sender, EventArgs e)
        {
            // Boşlukları temizle
            txtProfilAdi.Text = txtProfilAdi.Text.Trim();
            ProfilAdiValidasyonu();
        }

        private bool ProfilAdiValidasyonu()
        {
            string ad = txtProfilAdi.Text.Trim();

            if (string.IsNullOrEmpty(ad))
            {
                SetValidasyonDurumu(txtProfilAdi, lblProfilAdiHata, "⚠️ Profil adı zorunludur", false);
                return false;
            }

            if (ad.Length < 2)
            {
                SetValidasyonDurumu(txtProfilAdi, lblProfilAdiHata, "⚠️ En az 2 karakter olmalı", false);
                return false;
            }

            // Geçersiz karakterler kontrolü
            if (Regex.IsMatch(ad, @"[<>:""/\\|?*]"))
            {
                SetValidasyonDurumu(txtProfilAdi, lblProfilAdiHata, "⚠️ Geçersiz karakter içeriyor", false);
                return false;
            }

            // Aynı isimde profil var mı kontrolü (tek döngü yeterli)
            bool ayniIsimVar = false;
            for (int i = 0; i < Profiller.Count; i++)
            {
                if (i != duzenlenenProfilIndex &&
                    Profiller[i].ProfilAdi.Equals(ad, StringComparison.OrdinalIgnoreCase))
                {
                    ayniIsimVar = true;
                    break;
                }
            }

            if (ayniIsimVar)
            {
                SetValidasyonDurumu(txtProfilAdi, lblProfilAdiHata, "⚠️ Bu isimde profil zaten var", false);
                return false;
            }

            SetValidasyonDurumu(txtProfilAdi, lblProfilAdiHata, "", true);
            return true;
        }

        private void SaatKontrol_TextChanged(object sender, EventArgs e)
        {
            SaatValidasyonu();
            TumValidasyonlariKontrolEt();
        }

        private void SaatKontrol_Leave(object sender, EventArgs e)
        {
            SaatValidasyonu();

            // Otomatik düzeltmeler
            MaskedTextBox mtb = sender as MaskedTextBox;
            if (mtb != null)
            {
                string saat = mtb.Text.Replace(":", "").Replace(" ", "");
                if (saat.Length == 4)
                {
                    int saatNum, dakikaNum;
                    if (int.TryParse(saat.Substring(0, 2), out saatNum) &&
                        int.TryParse(saat.Substring(2, 2), out dakikaNum))
                    {
                        // Saat 23'ten büyükse 23 yap
                        if (saatNum > 23) saatNum = 23;
                        // Dakika 59'dan büyükse 59 yap
                        if (dakikaNum > 59) dakikaNum = 59;

                        mtb.Text = $"{saatNum:D2}{dakikaNum:D2}";
                    }
                }
            }
        }

        private bool SaatValidasyonu()
        {
            string baslangic = txtBaslangic.Text.Replace(":", "").Replace(" ", "").Replace("_", "");
            string bitis = txtBitis.Text.Replace(":", "").Replace(" ", "").Replace("_", "");

            // Boşluk kontrolü
            if (baslangic.Length < 4 || bitis.Length < 4)
            {
                SetSaatValidasyonDurumu("⚠️ Saatleri tam girin (SS:DD)", false);
                return false;
            }

            // Geçerli saat kontrolü
            if (!SaatGecerliMi(txtBaslangic.Text))
            {
                SetSaatValidasyonDurumu("⚠️ Başlangıç saati geçersiz (00:00-23:59)", false);
                return false;
            }

            if (!SaatGecerliMi(txtBitis.Text))
            {
                SetSaatValidasyonDurumu("⚠️ Bitiş saati geçersiz (00:00-23:59)", false);
                return false;
            }

            // TRY-CATCH EKLE
            try
            {
                TimeSpan baslangicZaman = TimeSpan.Parse(txtBaslangic.Text);
                TimeSpan bitisZaman = TimeSpan.Parse(txtBitis.Text);

                if (baslangicZaman >= bitisZaman)
                {
                    SetSaatValidasyonDurumu("⚠️ Başlangıç, bitişten önce olmalı", false);
                    return false;
                }

                if ((bitisZaman - baslangicZaman).TotalMinutes < 5)
                {
                    SetSaatValidasyonDurumu("⚠️ Süre en az 5 dakika olmalı", false);
                    return false;
                }
            }
            catch
            {
                SetSaatValidasyonDurumu("⚠️ Geçersiz saat formatı", false);
                return false;
            }

            SetSaatValidasyonDurumu("", true);
            return true;
        }
        private void SetSaatValidasyonDurumu(string mesaj, bool gecerli)
        {
            lblSaatHata.Text = mesaj;

            Color borderRengi = gecerli ? gecerliBorderRengi : (string.IsNullOrEmpty(mesaj) ? normalBorderRengi : hataBorderRengi);
            Color arkaPlan = gecerli ? gecerliArkaplanRengi : (string.IsNullOrEmpty(mesaj) ? Color.White : hataArkaplanRengi);

            // Her iki saat kutusuna da uygula
            // Not: MaskedTextBox BorderStyle değiştirilemez, arka plan kullanıyoruz
            txtBaslangic.BackColor = arkaPlan;
            txtBitis.BackColor = arkaPlan;
        }

        private void LstMuzikler_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(() => {
                // ✅ Form disposed kontrolü
                if (!this.IsDisposed && !this.Disposing)
                {
                    MuzikSecimKontrol();
                    TumValidasyonlariKontrolEt();
                }
            }));
        }

        private bool MuzikSecimKontrol()
        {
            int secilenSayisi = lstMuzikler.CheckedItems.Count;

            if (lstMuzikler.Items.Count == 0)
            {
                lblMuzikHata.Text = "⚠️ TeneffusMuzikleri klasörüne müzik ekleyin";
                lblMuzikHata.ForeColor = Color.FromArgb(251, 191, 36); // Uyarı sarısı
                return false;
            }

            if (secilenSayisi == 0)
            {
                lblMuzikHata.Text = "⚠️ En az 1 müzik seçmelisiniz";
                lblMuzikHata.ForeColor = hataBorderRengi;
                return false;
            }

            lblMuzikHata.Text = $"✓ {secilenSayisi} müzik seçildi";
            lblMuzikHata.ForeColor = gecerliBorderRengi;
            return true;
        }

        private void TrackSes_Scroll(object sender, EventArgs e)
        {
            string icon = trackSes.Value < 30 ? "🔉" : trackSes.Value < 70 ? "🔊" : "🔊";
            lblSesYuzde.Text = $"{icon} {trackSes.Value}%";
        }

        private void TumValidasyonlariKontrolEt()
        {
            bool profilAdiGecerli = ProfilAdiValidasyonu();
            bool saatGecerli = SaatValidasyonu();
            bool muzikSecili = lstMuzikler.CheckedItems.Count > 0 || lstMuzikler.Items.Count == 0;
            bool gunSecili = cmbGun.SelectedIndex >= 0;

            bool tumGecerli = profilAdiGecerli && saatGecerli && muzikSecili && gunSecili;

            // Kaydet butonunu güncelle
            btnKaydet.Enabled = tumGecerli;
            btnKaydet.BackColor = tumGecerli ? Color.FromArgb(99, 102, 241) : Color.FromArgb(148, 163, 184);

            // Validasyon durumunu göster
            if (tumGecerli)
            {
                lblValidasyonDurum.Text = "✓ Tüm alanlar geçerli";
                lblValidasyonDurum.ForeColor = gecerliBorderRengi;
                pnlValidasyon.BackColor = Color.FromArgb(240, 253, 244);
            }
            else
            {
                int hataSayisi = 0;
                if (!profilAdiGecerli) hataSayisi++;
                if (!saatGecerli) hataSayisi++;
                if (!muzikSecili && lstMuzikler.Items.Count > 0) hataSayisi++;
                if (!gunSecili) hataSayisi++;

                lblValidasyonDurum.Text = $"⚠️ {hataSayisi} alan düzeltilmeli";
                lblValidasyonDurum.ForeColor = hataBorderRengi;
                pnlValidasyon.BackColor = Color.FromArgb(254, 242, 242);
            }
        }

        private void SetValidasyonDurumu(TextBox txt, Label lblHata, string mesaj, bool gecerli)
        {
            lblHata.Text = mesaj;

            if (gecerli)
            {
                txt.BackColor = gecerliArkaplanRengi;
            }
            else if (!string.IsNullOrEmpty(mesaj))
            {
                txt.BackColor = hataArkaplanRengi;
            }
            else
            {
                txt.BackColor = Color.White;
            }
        }


private bool SaatGecerliMi(string saat)
        {
            if (string.IsNullOrEmpty(saat)) return false;
            saat = saat.Replace(" ", "").Replace("_", "");

            // Alt çizgi varsa tam doldurulmamış demektir
            if (saat.Contains("_")) return false;

            try
            {
                string[] parcalar = saat.Split(':');
                if (parcalar.Length != 2) return false;

                int saatNum = int.Parse(parcalar[0]);
                int dakikaNum = int.Parse(parcalar[1]);

                return saatNum >= 0 && saatNum <= 23 && dakikaNum >= 0 && dakikaNum <= 59;
            }
            catch
            {
                return false;
            }
        }

        // ═══════════════════════════════════════════
        // YARDIMCI METODLAR
        // ═══════════════════════════════════════════

        private Button CreateButton(string text, Color backColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => {
                if (btn.Enabled)
                    btn.BackColor = ControlPaint.Dark(backColor, 0.1f);
            };
            btn.MouseLeave += (s, e) => {
                if (btn.Enabled)
                    btn.BackColor = backColor;
            };

            return btn;
        }

        private void YuvarlakKoseUygula(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }

        private void LstProfiller_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Profiller.Count) return;

            var profil = Profiller[e.Index];
            bool secili = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color arkaPlan = secili ? Color.FromArgb(99, 102, 241) : Color.FromArgb(248, 250, 252);
            Color yaziRengi = secili ? Color.White : Color.FromArgb(51, 65, 85);
            Color ikinciYazi = secili ? Color.FromArgb(220, 220, 255) : Color.FromArgb(100, 116, 139);

            using (SolidBrush brush = new SolidBrush(arkaPlan))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // Aktif ikonu
            string durum = profil.Aktif ? "✓" : "○";
            Color durumRengi = profil.Aktif ?
                (secili ? Color.FromArgb(134, 239, 172) : Color.FromArgb(34, 197, 94)) :
                ikinciYazi;

            using (Font durumFont = new Font("Segoe UI", 12, FontStyle.Bold))
            using (SolidBrush durumBrush = new SolidBrush(durumRengi))
            {
                e.Graphics.DrawString(durum, durumFont, durumBrush, e.Bounds.X + 10, e.Bounds.Y + 14);
            }

            // Profil adı
            using (Font adFont = new Font("Segoe UI", 11, FontStyle.Bold))
            using (SolidBrush adBrush = new SolidBrush(yaziRengi))
            {
                e.Graphics.DrawString(profil.ProfilAdi, adFont, adBrush, e.Bounds.X + 35, e.Bounds.Y + 6);
            }

            // Detay
            string[] gunler = { "Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi" };
            string gunAdi = profil.Gun >= 0 && profil.Gun < 7 ? gunler[profil.Gun] : "?";
            string detay = $"{gunAdi} • {profil.BaslangicSaat}-{profil.BitisSaat} • {profil.MuzikDosyalari.Count} müzik";

            using (Font detayFont = new Font("Segoe UI", 8))
            using (SolidBrush detayBrush = new SolidBrush(ikinciYazi))
            {
                e.Graphics.DrawString(detay, detayFont, detayBrush, e.Bounds.X + 35, e.Bounds.Y + 28);
            }
        }

        // ═══════════════════════════════════════════
        // FORM OLAYLARI
        // ═══════════════════════════════════════════

        private void TeneffusMuzikForm_Load(object sender, EventArgs e)
        {
            // Günleri yükle
            cmbGun.Items.Clear();
            cmbGun.Items.Add("Pazar");
            cmbGun.Items.Add("Pazartesi");
            cmbGun.Items.Add("Salı");
            cmbGun.Items.Add("Çarşamba");
            cmbGun.Items.Add("Perşembe");
            cmbGun.Items.Add("Cuma");
            cmbGun.Items.Add("Cumartesi");
            cmbGun.SelectedIndex = 1;

            // Müzikleri yükle
            MuzikleriYukle();

            // Profilleri listele
            ProfilListesiniGuncelle();

            // İlk profili seç
            if (lstProfiller.Items.Count > 0)
            {
                lstProfiller.SelectedIndex = 0;
            }
            else
            {
                FormuTemizle();
            }

            ProfilSayisiniGuncelle();
            TumValidasyonlariKontrolEt();
        }

        private void MuzikleriYukle()
        {
            lstMuzikler.Items.Clear();

            string muzikKlasoru = Path.Combine(Application.StartupPath, "TeneffusMuzikleri");
            if (!Directory.Exists(muzikKlasoru))
                Directory.CreateDirectory(muzikKlasoru);

            string[] desteklenenUzantilar = { ".mp3", ".wav", ".wma", ".m4a" };

            var dosyalar = Directory.GetFiles(muzikKlasoru)
                .Where(f => desteklenenUzantilar.Contains(Path.GetExtension(f).ToLower()))
                .OrderBy(f => Path.GetFileName(f))
                .ToList();

            foreach (string dosya in dosyalar)
            {
                lstMuzikler.Items.Add(Path.GetFileName(dosya));
            }

            if (lstMuzikler.Items.Count == 0)
            {
                lblDurum.Text = "⚠️ TeneffusMuzikleri klasörü boş";
                lblDurum.ForeColor = Color.FromArgb(251, 191, 36);
            }
            else
            {
                lblDurum.Text = $"📁 {lstMuzikler.Items.Count} müzik dosyası bulundu";
                lblDurum.ForeColor = Color.FromArgb(34, 197, 94);
            }

            MuzikSecimKontrol();
        }

        private void ProfilListesiniGuncelle()
        {
            lstProfiller.Items.Clear();
            for (int i = 0; i < Profiller.Count; i++)
            {
                lstProfiller.Items.Add(Profiller[i].ProfilAdi);
            }
            ProfilSayisiniGuncelle();
        }

        private void ProfilSayisiniGuncelle()
        {
            var lbl = pnlSol.Controls.Find("lblProfilSayisi", false).FirstOrDefault() as Label;
            if (lbl != null)
            {
                int aktifSayisi = Profiller.Count(p => p.Aktif);
                lbl.Text = $"📊 Toplam: {Profiller.Count} profil ({aktifSayisi} aktif)";
            }
        }

        private void LstProfiller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProfiller.SelectedIndex >= 0 && lstProfiller.SelectedIndex < Profiller.Count)
            {
                duzenlenenProfilIndex = lstProfiller.SelectedIndex;
                var profil = Profiller[duzenlenenProfilIndex];

                txtProfilAdi.Text = profil.ProfilAdi;
                chkAktif.Checked = profil.Aktif;

                if (profil.Gun >= 0 && profil.Gun < cmbGun.Items.Count)
                    cmbGun.SelectedIndex = profil.Gun;
                else
                    cmbGun.SelectedIndex = 1;

                // ✅ Direkt ata, Replace kullanma
                txtBaslangic.Text = profil.BaslangicSaat ?? "09:55";
                txtBitis.Text = profil.BitisSaat ?? "10:10";

                trackSes.Value = Math.Max(5, Math.Min(100, profil.SesSeviyesi));

                string icon = trackSes.Value < 30 ? "🔉" : "🔊";
                lblSesYuzde.Text = $"{icon} {trackSes.Value}%";

                // Müzik seçimlerini geri yükle
                for (int i = 0; i < lstMuzikler.Items.Count; i++)
                {
                    string muzikAdi = lstMuzikler.Items[i].ToString();
                    lstMuzikler.SetItemChecked(i, profil.MuzikDosyalari.Contains(muzikAdi));
                }

                lblDurum.Text = $"📝 '{profil.ProfilAdi}' düzenleniyor";
                lblDurum.ForeColor = Color.FromArgb(99, 102, 241);

                TumValidasyonlariKontrolEt();
            }
        }

        private void FormuTemizle()
        {
            txtProfilAdi.Text = "";
            txtProfilAdi.BackColor = Color.White;
            lblProfilAdiHata.Text = "";

            chkAktif.Checked = true;
            cmbGun.SelectedIndex = 1;

            // ✅ Mask uyumlu format kullan
            txtBaslangic.Text = "09:55";
            txtBitis.Text = "10:10";
            txtBaslangic.BackColor = Color.White;
            txtBitis.BackColor = Color.White;
            lblSaatHata.Text = "";

            trackSes.Value = 30;
            lblSesYuzde.Text = "🔉 30%";

            for (int i = 0; i < lstMuzikler.Items.Count; i++)
                lstMuzikler.SetItemChecked(i, false);

            lblMuzikHata.Text = "";
            duzenlenenProfilIndex = -1;

            TumValidasyonlariKontrolEt();
        }

        private void BtnYeni_Click(object sender, EventArgs e)
        {
            lstProfiller.ClearSelected();
            FormuTemizle();
            txtProfilAdi.Focus();
            lblDurum.Text = "➕ Yeni profil oluşturuluyor...";
            lblDurum.ForeColor = Color.FromArgb(34, 197, 94);
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // Son kontroller (buton aktifse zaten geçerli ama yine de kontrol edelim)
            if (!ProfilAdiValidasyonu() || !SaatValidasyonu())
            {
                MessageBox.Show("Lütfen tüm alanları doğru şekilde doldurun.",
                    "⚠️ Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lstMuzikler.Items.Count == 0)
            {
                MessageBox.Show(
                    "TeneffusMuzikleri klasöründe hiç müzik dosyası yok!\n\n" +
                    "Lütfen önce müzik dosyalarını klasöre ekleyin.",
                    "❌ Müzik Yok", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Müzik uyarısı (zorunlu değil ama uyaralım)
            if (lstMuzikler.CheckedItems.Count == 0 && lstMuzikler.Items.Count > 0)
            {
                DialogResult sonuc = MessageBox.Show(
                    "Hiç müzik seçilmedi!\n\nMüzik seçmeden devam etmek istiyor musunuz?",
                    "⚠️ Müzik Seçilmedi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (sonuc != DialogResult.Yes)
                {
                    lstMuzikler.Focus();
                    return;
                }
            }

            TeneffusMuzikProfili profil;

            if (duzenlenenProfilIndex >= 0 && duzenlenenProfilIndex < Profiller.Count)
            {
                profil = Profiller[duzenlenenProfilIndex];
            }
            else
            {
                profil = new TeneffusMuzikProfili();
                Profiller.Add(profil);
                duzenlenenProfilIndex = Profiller.Count - 1;
            }

            // Değerleri ata
            profil.ProfilAdi = txtProfilAdi.Text.Trim();
            profil.Aktif = chkAktif.Checked;
            profil.Gun = cmbGun.SelectedIndex;
            profil.BaslangicSaat = txtBaslangic.Text;
            profil.BitisSaat = txtBitis.Text;
            profil.SesSeviyesi = trackSes.Value;

            // Seçili müzikleri ekle
            profil.MuzikDosyalari.Clear();
            foreach (var item in lstMuzikler.CheckedItems)
            {
                profil.MuzikDosyalari.Add(item.ToString());
            }

            ProfilListesiniGuncelle();

            if (duzenlenenProfilIndex >= 0 && duzenlenenProfilIndex < lstProfiller.Items.Count)
                lstProfiller.SelectedIndex = duzenlenenProfilIndex;

            lblDurum.Text = $"✓ '{profil.ProfilAdi}' kaydedildi!";
            lblDurum.ForeColor = Color.FromArgb(34, 197, 94);

            // Başarı animasyonu
            btnKaydet.BackColor = Color.FromArgb(34, 197, 94);
            btnKaydet.Text = "✓ Kaydedildi!";

            Timer resetTimer = new Timer();
            resetTimer.Interval = 1500;
            resetTimer.Tick += (s, ev) => {
                btnKaydet.BackColor = Color.FromArgb(99, 102, 241);
                btnKaydet.Text = "💾 Kaydet";
                resetTimer.Stop();
                resetTimer.Dispose();
            };
            resetTimer.Start();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (lstProfiller.SelectedIndex < 0 || lstProfiller.SelectedIndex >= Profiller.Count)
            {
                MessageBox.Show("Lütfen silmek için bir profil seçin.",
                    "ℹ️ Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string profilAdi = Profiller[lstProfiller.SelectedIndex].ProfilAdi;

            DialogResult result = MessageBox.Show(
                $"'{profilAdi}' profilini silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!",
                "🗑️ Profil Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Profiller.RemoveAt(lstProfiller.SelectedIndex);
                ProfilListesiniGuncelle();
                FormuTemizle();

                if (lstProfiller.Items.Count > 0)
                    lstProfiller.SelectedIndex = 0;

                lblDurum.Text = $"🗑️ '{profilAdi}' silindi";
                lblDurum.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }
    }
}