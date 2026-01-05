using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SSEML_OkulZili
{
    public partial class TakvimForm : Form
    {
        // ═══════════════════════════════════════════════════════════════
        // PUBLIC ÖZELLİKLER
        // ═══════════════════════════════════════════════════════════════
        public List<TakvimGunu> TakvimGunleri { get; set; }
        public List<TakvimKural> TakvimKurallari { get; set; }

        // ═══════════════════════════════════════════════════════════════
        // PRIVATE DEĞİŞKENLER
        // ═══════════════════════════════════════════════════════════════
        private Dictionary<string, List<ZamanItem>> profiller;
        private readonly string[] gunAdlari = { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };

        // ═══════════════════════════════════════════════════════════════
        // KONTROLLER - TAKVİM VE GİRİŞ ALANLARI
        // ═══════════════════════════════════════════════════════════════
        private MonthCalendar takvim;
        private ListBox lstGunler;
        private ComboBox cmbProfil;
        private TextBox txtAciklama;
        private CheckBox chkZilDevreDisi;
        private Button btnEkle;
        private Button btnSil;
        private Button btnKaydet;
        private Button btnIptal;
        private Label lblDurum;
        private Label lblSeciliTarih;
        private Panel pnlSol;
        private Panel pnlSagUst;
        private Panel pnlSagAlt;
        private Panel pnlKurallar;

        // ═══════════════════════════════════════════════════════════════
        // KONTROLLER - KURAL YÖNETİMİ
        // ═══════════════════════════════════════════════════════════════
        private ListBox lstKurallar;
        private ComboBox cmbKuralGun;
        private ComboBox cmbKuralProfil;
        private Button btnKuralEkle;
        private Button btnKuralSil;

        // ═══════════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════════
        public TakvimForm(List<TakvimGunu> mevcutGunler, Dictionary<string, List<ZamanItem>> profiller)
        {
            // Null kontrolleri
            this.profiller = profiller ?? new Dictionary<string, List<ZamanItem>>();

            // Derin kopya oluştur - TakvimGunleri
            this.TakvimGunleri = new List<TakvimGunu>();
            if (mevcutGunler != null)
            {
                foreach (var gun in mevcutGunler)
                {
                    if (gun != null)
                    {
                        this.TakvimGunleri.Add(new TakvimGunu
                        {
                            Tarih = gun.Tarih,
                            ProfilAdi = gun.ProfilAdi ?? "",
                            Aciklama = gun.Aciklama ?? "",
                            ZilDevreDisi = gun.ZilDevreDisi
                        });
                    }
                }
            }

            // Kuralları başlat
            this.TakvimKurallari = new List<TakvimKural>();

            // Formu oluştur
            InitializeComponent();

            // Listeleri güncelle
            ListeGuncelle();
            KuralListesiniGuncelle();
            IstatistikGuncelle();
        }

        // ═══════════════════════════════════════════════════════════════
        // FORM TASARIMI - GENİŞLETİLMİŞ VE FERAH
        // ═══════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            // ═══════════════════════════════════════════
            // FORM AYARLARI - BÜYÜK BOYUT
            // ═══════════════════════════════════════════
            this.Text = "📅 Takvim ve Özel Gün Yönetimi";
            this.Size = new Size(1280, 780);
            this.MinimumSize = new Size(1280, 780);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.Font = new Font("Segoe UI", 9F);

            // ═══════════════════════════════════════════
            // SOL PANEL - TAKVİM VE GİRİŞ FORMU (280px genişlik)
            // ═══════════════════════════════════════════
            pnlSol = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(300, 650),
                BackColor = Color.White
            };
            YuvarlakKoseUygula(pnlSol, 16);
            this.Controls.Add(pnlSol);

            // Panel başlığı
            Label lblTakvimBaslik = new Label
            {
                Text = "📅 Tarih Seçimi",
                Location = new Point(20, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };
            pnlSol.Controls.Add(lblTakvimBaslik);

            // Ayırıcı çizgi
            Panel ayirici1 = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(260, 2),
                BackColor = Color.FromArgb(226, 232, 240)
            };
            pnlSol.Controls.Add(ayirici1);

            // Takvim kontrolü
            takvim = new MonthCalendar
            {
                Location = new Point(20, 65),
                MaxSelectionCount = 1,
                ShowTodayCircle = true,
                Font = new Font("Segoe UI", 9F)
            };
            takvim.DateSelected += Takvim_DateSelected;
            pnlSol.Controls.Add(takvim);

            // Seçili tarih göstergesi
            lblSeciliTarih = new Label
            {
                Text = $"📌 Seçili: {DateTime.Today:dd MMMM yyyy, dddd}",
                Location = new Point(20, 235),
                Size = new Size(260, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlSol.Controls.Add(lblSeciliTarih);

            // Ayırıcı çizgi
            Panel ayirici2 = new Panel
            {
                Location = new Point(20, 270),
                Size = new Size(260, 2),
                BackColor = Color.FromArgb(226, 232, 240)
            };
            pnlSol.Controls.Add(ayirici2);

            // Form alanları başlığı
            Label lblFormBaslik = new Label
            {
                Text = "📝 Kayıt Bilgileri",
                Location = new Point(20, 285),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105)
            };
            pnlSol.Controls.Add(lblFormBaslik);

            // Profil seçimi
            Label lblProfil = new Label
            {
                Text = "Profil Seçimi:",
                Location = new Point(20, 320),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            pnlSol.Controls.Add(lblProfil);

            cmbProfil = new ComboBox
            {
                Location = new Point(20, 345),
                Size = new Size(260, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            cmbProfil.Items.Add("(Otomatik - Gün bazlı kural)");
            if (profiller != null)
            {
                foreach (var profil in profiller.Keys)
                {
                    cmbProfil.Items.Add(profil);
                }
            }
            cmbProfil.SelectedIndex = 0;
            pnlSol.Controls.Add(cmbProfil);

            // Açıklama alanı
            Label lblAciklama = new Label
            {
                Text = "Açıklama: *",
                Location = new Point(20, 390),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            pnlSol.Controls.Add(lblAciklama);

            txtAciklama = new TextBox
            {
                Location = new Point(20, 415),
                Size = new Size(260, 32),
                Font = new Font("Segoe UI", 11),
                MaxLength = 100,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtAciklama.TextChanged += TxtAciklama_TextChanged;
            pnlSol.Controls.Add(txtAciklama);

            // Açıklama ipucu
            Label lblAciklamaIpucu = new Label
            {
                Text = "Örn: Yarıyıl Tatili, 29 Ekim Bayramı, Sınav Haftası...",
                Location = new Point(20, 450),
                Size = new Size(260, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184)
            };
            pnlSol.Controls.Add(lblAciklamaIpucu);

            // Zil devre dışı checkbox
            chkZilDevreDisi = new CheckBox
            {
                Text = "🔇 Bu gün ziller çalmasın (Tatil)",
                Location = new Point(20, 480),
                Size = new Size(260, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38)
            };
            pnlSol.Controls.Add(chkZilDevreDisi);

            // Ekle ve Sil butonları
            btnEkle = CreateStyledButton("➕ KAYIT EKLE", Color.FromArgb(34, 197, 94));
            btnEkle.Location = new Point(20, 525);
            btnEkle.Size = new Size(125, 48);
            btnEkle.Click += BtnEkle_Click;
            pnlSol.Controls.Add(btnEkle);

            btnSil = CreateStyledButton("🗑️ SİL", Color.FromArgb(239, 68, 68));
            btnSil.Location = new Point(155, 525);
            btnSil.Size = new Size(125, 48);
            btnSil.Click += BtnSil_Click;
            pnlSol.Controls.Add(btnSil);

            // Durum mesajı
            lblDurum = new Label
            {
                Text = "💡 Takvimden tarih seçin ve bilgileri doldurun",
                Location = new Point(20, 585),
                Size = new Size(260, 45),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            pnlSol.Controls.Add(lblDurum);

            // Zorunlu alan notu
            Label lblZorunlu = new Label
            {
                Text = "* İşaretli alan zorunludur",
                Location = new Point(20, 625),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(239, 68, 68)
            };
            pnlSol.Controls.Add(lblZorunlu);

            // ═══════════════════════════════════════════
            // ORTA PANEL - KURAL YÖNETİMİ (280px genişlik)
            // ═══════════════════════════════════════════
            pnlKurallar = new Panel
            {
                Location = new Point(340, 20),
                Size = new Size(300, 340),
                BackColor = Color.White
            };
            YuvarlakKoseUygula(pnlKurallar, 16);
            this.Controls.Add(pnlKurallar);

            // Kural başlığı
            Label lblKuralBaslik = new Label
            {
                Text = "⚙️ Otomatik Kurallar",
                Location = new Point(20, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241)
            };
            pnlKurallar.Controls.Add(lblKuralBaslik);

            // Kural açıklaması
            Label lblKuralAciklama = new Label
            {
                Text = "Her hafta tekrarlayan günler için\notomatik profil atayın:",
                Location = new Point(20, 50),
                Size = new Size(260, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            pnlKurallar.Controls.Add(lblKuralAciklama);

            // Gün seçimi
            Label lblKuralGun = new Label
            {
                Text = "Gün:",
                Location = new Point(20, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(71, 85, 105)
            };
            pnlKurallar.Controls.Add(lblKuralGun);

            cmbKuralGun = new ComboBox
            {
                Location = new Point(20, 125),
                Size = new Size(125, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            cmbKuralGun.Items.AddRange(gunAdlari);
            cmbKuralGun.SelectedIndex = 0;
            pnlKurallar.Controls.Add(cmbKuralGun);

            // Profil seçimi (kural için)
            Label lblKuralProfil = new Label
            {
                Text = "Profil:",
                Location = new Point(155, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(71, 85, 105)
            };
            pnlKurallar.Controls.Add(lblKuralProfil);

            cmbKuralProfil = new ComboBox
            {
                Location = new Point(155, 125),
                Size = new Size(125, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            if (profiller != null)
            {
                foreach (var profil in profiller.Keys)
                {
                    cmbKuralProfil.Items.Add(profil);
                }
            }
            if (cmbKuralProfil.Items.Count > 0)
                cmbKuralProfil.SelectedIndex = 0;
            pnlKurallar.Controls.Add(cmbKuralProfil);

            // Kural butonları
            btnKuralEkle = CreateStyledButton("✓ Kural Ekle", Color.FromArgb(34, 197, 94));
            btnKuralEkle.Location = new Point(20, 170);
            btnKuralEkle.Size = new Size(125, 40);
            btnKuralEkle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnKuralEkle.Click += BtnKuralEkle_Click;
            pnlKurallar.Controls.Add(btnKuralEkle);

            btnKuralSil = CreateStyledButton("✕ Kuralı Sil", Color.FromArgb(239, 68, 68));
            btnKuralSil.Location = new Point(155, 170);
            btnKuralSil.Size = new Size(125, 40);
            btnKuralSil.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnKuralSil.Click += BtnKuralSil_Click;
            pnlKurallar.Controls.Add(btnKuralSil);

            // Kural listesi
            Label lblKuralListeBaslik = new Label
            {
                Text = "📋 Tanımlı Kurallar:",
                Location = new Point(20, 220),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105)
            };
            pnlKurallar.Controls.Add(lblKuralListeBaslik);

            lstKurallar = new ListBox
            {
                Location = new Point(20, 245),
                Size = new Size(260, 75),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(248, 250, 252)
            };
            lstKurallar.DoubleClick += LstKurallar_DoubleClick;
            pnlKurallar.Controls.Add(lstKurallar);

            // İpucu
            Label lblKuralIpucu = new Label
            {
                Text = "💡 Silmek için çift tıklayın",
                Location = new Point(20, 322),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184)
            };
            pnlKurallar.Controls.Add(lblKuralIpucu);

            // ═══════════════════════════════════════════
            // ORTA ALT PANEL - İSTATİSTİKLER
            // ═══════════════════════════════════════════
            Panel pnlIstatistik = new Panel
            {
                Location = new Point(340, 380),
                Size = new Size(300, 140),
                BackColor = Color.White
            };
            YuvarlakKoseUygula(pnlIstatistik, 16);
            this.Controls.Add(pnlIstatistik);

            Label lblIstatistikBaslik = new Label
            {
                Name = "lblIstatistikBaslik",
                Text = "📊 İstatistikler",
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };
            pnlIstatistik.Controls.Add(lblIstatistikBaslik);

            Label lblIstatistikDetay = new Label
            {
                Name = "lblIstatistikDetay",
                Text = "",
                Location = new Point(20, 50),
                Size = new Size(260, 80),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            pnlIstatistik.Controls.Add(lblIstatistikDetay);

            // ═══════════════════════════════════════════
            // SAĞ PANEL - ÖZEL GÜNLER LİSTESİ (560px genişlik)
            // ═══════════════════════════════════════════
            pnlSagUst = new Panel
            {
                Location = new Point(660, 20),
                Size = new Size(590, 500),
                BackColor = Color.White
            };
            YuvarlakKoseUygula(pnlSagUst, 16);
            this.Controls.Add(pnlSagUst);

            // Liste başlığı
            Label lblListeBaslik = new Label
            {
                Text = "📋 Tanımlı Özel Günler",
                Location = new Point(25, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };
            pnlSagUst.Controls.Add(lblListeBaslik);

            // Filtre/arama ipucu
            Label lblListeIpucu = new Label
            {
                Text = "🔵 Gelecek tarihler  |  ⚪ Geçmiş tarihler  |  🔇 Ziller kapalı",
                Location = new Point(25, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(148, 163, 184)
            };
            pnlSagUst.Controls.Add(lblListeIpucu);

            // Liste kutusu
            lstGunler = new ListBox
            {
                Location = new Point(25, 80),
                Size = new Size(540, 390),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(248, 250, 252),
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 40
            };
            lstGunler.DrawItem += LstGunler_DrawItem;
            lstGunler.SelectedIndexChanged += LstGunler_SelectedIndexChanged;
            pnlSagUst.Controls.Add(lstGunler);

            // Geçmiş temizle butonu
            Button btnGecmisSil = CreateStyledButton("🗑️ Geçmiş Kayıtları Temizle", Color.FromArgb(251, 191, 36));
            btnGecmisSil.Location = new Point(660, 540);
            btnGecmisSil.Size = new Size(280, 50);
            btnGecmisSil.ForeColor = Color.FromArgb(71, 85, 105);
            btnGecmisSil.Click += BtnGecmisSil_Click;
            this.Controls.Add(btnGecmisSil);

            // ═══════════════════════════════════════════
            // EN ALT - ANA BUTONLAR
            // ═══════════════════════════════════════════
            Panel pnlAlt = new Panel
            {
                Location = new Point(20, 690),
                Size = new Size(1230, 65),
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlAlt);

            // Sol alt bilgi
            Label lblAltBilgi = new Label
            {
                Text = "💡 Değişikliklerin kaydedilmesi için 'Kaydet ve Kapat' butonuna tıklayın",
                Location = new Point(0, 22),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184)
            };
            pnlAlt.Controls.Add(lblAltBilgi);

            // Kaydet butonu
            btnKaydet = CreateStyledButton("💾 KAYDET VE KAPAT", Color.FromArgb(99, 102, 241));
            btnKaydet.Location = new Point(800, 0);
            btnKaydet.Size = new Size(200, 55);
            btnKaydet.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnKaydet.Click += BtnKaydet_Click;
            pnlAlt.Controls.Add(btnKaydet);

            // İptal butonu
            btnIptal = CreateStyledButton("✕ İPTAL", Color.FromArgb(100, 116, 139));
            btnIptal.Location = new Point(1020, 0);
            btnIptal.Size = new Size(210, 55);
            btnIptal.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnIptal.Click += BtnIptal_Click;
            pnlAlt.Controls.Add(btnIptal);
        }

        // ═══════════════════════════════════════════════════════════════
        // KURAL YÖNETİMİ METODLARI
        // ═══════════════════════════════════════════════════════════════

        private void BtnKuralEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyon kontrolleri
                if (cmbKuralGun.SelectedIndex < 0)
                {
                    MessageBox.Show(
                        "Lütfen bir gün seçin!",
                        "⚠️ Eksik Seçim",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    cmbKuralGun.Focus();
                    return;
                }

                if (cmbKuralProfil.SelectedItem == null || cmbKuralProfil.Items.Count == 0)
                {
                    MessageBox.Show(
                        "Lütfen bir profil seçin!\n\n" +
                        "Henüz profil oluşturulmamış olabilir.\n" +
                        "Ana ekrandan yeni profil oluşturabilirsiniz.",
                        "⚠️ Profil Gerekli",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                int gunIndex = cmbKuralGun.SelectedIndex;
                string profilAdi = cmbKuralProfil.SelectedItem.ToString();
                string gunAdi = GunAdiniAl(gunIndex);

                // TakvimKurallari null kontrolü
                if (TakvimKurallari == null)
                    TakvimKurallari = new List<TakvimKural>();

                // Aynı gün için kural var mı kontrol et
                var mevcutKural = TakvimKurallari.FirstOrDefault(k => k.GunIndex == gunIndex);
                if (mevcutKural != null)
                {
                    DialogResult sonuc = MessageBox.Show(
                        $"'{gunAdi}' için zaten bir kural tanımlanmış:\n\n" +
                        $"📌 Mevcut Profil: {mevcutKural.ProfilAdi}\n" +
                        $"📌 Yeni Profil: {profilAdi}\n\n" +
                        $"Mevcut kuralı güncellemek istiyor musunuz?",
                        "⚠️ Kural Zaten Mevcut",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (sonuc != DialogResult.Yes) return;

                    TakvimKurallari.Remove(mevcutKural);
                }

                // Yeni kural ekle
                TakvimKurallari.Add(new TakvimKural
                {
                    GunIndex = gunIndex,
                    ProfilAdi = profilAdi
                });

                KuralListesiniGuncelle();
                IstatistikGuncelle();

                DurumGoster($"✓ Kural eklendi: Her {gunAdi} → {profilAdi}", Color.FromArgb(34, 197, 94));

                MessageBox.Show(
                    $"✓ Kural başarıyla eklendi!\n\n" +
                    $"Her {gunAdi} günü otomatik olarak\n" +
                    $"'{profilAdi}' profili kullanılacak.\n\n" +
                    $"Not: Özel gün tanımları bu kuralı geçersiz kılar.",
                    "✓ Kural Eklendi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kural eklenirken bir hata oluştu:\n\n{ex.Message}",
                    "❌ Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnKuralSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçim kontrolü
                if (lstKurallar.SelectedIndex < 0)
                {
                    MessageBox.Show(
                        "Lütfen silmek için listeden bir kural seçin!",
                        "ℹ️ Seçim Gerekli",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // TakvimKurallari null veya boş kontrolü
                if (TakvimKurallari == null || TakvimKurallari.Count == 0)
                {
                    MessageBox.Show(
                        "Silinecek kural bulunamadı.",
                        "ℹ️ Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    KuralListesiniGuncelle();
                    return;
                }

                // Index sınır kontrolü
                if (lstKurallar.SelectedIndex >= TakvimKurallari.Count)
                {
                    MessageBox.Show(
                        "Geçersiz seçim. Lütfen tekrar deneyin.",
                        "⚠️ Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    KuralListesiniGuncelle();
                    return;
                }

                var kural = TakvimKurallari[lstKurallar.SelectedIndex];
                string gunAdi = GunAdiniAl(kural.GunIndex);

                DialogResult sonuc = MessageBox.Show(
                    $"Bu kuralı silmek istediğinizden emin misiniz?\n\n" +
                    $"📌 {gunAdi} → {kural.ProfilAdi}\n\n" +
                    $"Bu işlem geri alınamaz!",
                    "🗑️ Kural Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    TakvimKurallari.RemoveAt(lstKurallar.SelectedIndex);
                    KuralListesiniGuncelle();
                    IstatistikGuncelle();

                    DurumGoster($"🗑️ Kural silindi: {gunAdi}", Color.FromArgb(239, 68, 68));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kural silinirken bir hata oluştu:\n\n{ex.Message}",
                    "❌ Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LstKurallar_DoubleClick(object sender, EventArgs e)
        {
            // Çift tıklayınca silme işlemi
            if (lstKurallar.SelectedIndex >= 0 &&
                TakvimKurallari != null &&
                lstKurallar.SelectedIndex < TakvimKurallari.Count)
            {
                BtnKuralSil_Click(sender, e);
            }
        }

        private void KuralListesiniGuncelle()
        {
            try
            {
                lstKurallar.Items.Clear();

                if (TakvimKurallari == null || TakvimKurallari.Count == 0)
                {
                    lstKurallar.Items.Add("(Henüz kural tanımlanmamış)");
                    lstKurallar.Enabled = false;
                    return;
                }

                lstKurallar.Enabled = true;

                foreach (var kural in TakvimKurallari.OrderBy(k => k.GunIndex))
                {
                    string gunAdi = GunAdiniAl(kural.GunIndex);
                    string profilAdi = kural.ProfilAdi ?? "Bilinmeyen";
                    lstKurallar.Items.Add($"📌 {gunAdi} → {profilAdi}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"KuralListesiniGuncelle Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // TAKVİM VE ÖZEL GÜN METODLARI
        // ═══════════════════════════════════════════════════════════════

        private void Takvim_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                DateTime secilenTarih = e.Start.Date;
                lblSeciliTarih.Text = $"📌 Seçili: {secilenTarih:dd MMMM yyyy, dddd}";

                var mevcutGun = TakvimGunleri?.FirstOrDefault(t => t.Tarih.Date == secilenTarih);

                if (mevcutGun != null)
                {
                    // Mevcut kayıt varsa formu doldur
                    txtAciklama.Text = mevcutGun.Aciklama ?? "";
                    chkZilDevreDisi.Checked = mevcutGun.ZilDevreDisi;

                    // Profil seçimini ayarla
                    if (string.IsNullOrEmpty(mevcutGun.ProfilAdi))
                    {
                        cmbProfil.SelectedIndex = 0;
                    }
                    else
                    {
                        bool bulundu = false;
                        for (int i = 0; i < cmbProfil.Items.Count; i++)
                        {
                            if (cmbProfil.Items[i].ToString() == mevcutGun.ProfilAdi)
                            {
                                cmbProfil.SelectedIndex = i;
                                bulundu = true;
                                break;
                            }
                        }
                        if (!bulundu) cmbProfil.SelectedIndex = 0;
                    }

                    DurumGoster($"📝 Bu tarih için kayıt mevcut. Düzenleyebilirsiniz.", Color.FromArgb(99, 102, 241));
                }
                else
                {
                    // Yeni kayıt için formu temizle
                    txtAciklama.Clear();
                    chkZilDevreDisi.Checked = false;
                    cmbProfil.SelectedIndex = 0;
                    DurumGoster($"💡 Bu tarih için henüz kayıt yok. Yeni kayıt ekleyebilirsiniz.", Color.FromArgb(100, 116, 139));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Takvim_DateSelected Hata: {ex.Message}");
            }
        }

        private void TxtAciklama_TextChanged(object sender, EventArgs e)
        {
            // Açıklama girildiğinde ekle butonunu vurgula
            if (!string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                btnEkle.BackColor = Color.FromArgb(22, 163, 74);
            }
            else
            {
                btnEkle.BackColor = Color.FromArgb(34, 197, 94);
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Açıklama zorunlu alan kontrolü
                if (string.IsNullOrWhiteSpace(txtAciklama.Text))
                {
                    MessageBox.Show(
                        "⚠️ AÇIKLAMA ZORUNLUDUR!\n\n" +
                        "Lütfen bu özel gün için bir açıklama girin.\n\n" +
                        "Örnekler:\n" +
                        "• Yarıyıl Tatili\n" +
                        "• 29 Ekim Cumhuriyet Bayramı\n" +
                        "• Sınav Haftası Başlangıcı\n" +
                        "• Öğretmenler Günü\n" +
                        "• Karne Günü",
                        "❌ Eksik Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtAciklama.Focus();
                    return;
                }

                DateTime secilenTarih = takvim.SelectionStart.Date;

                // Geçmiş tarih uyarısı
                if (secilenTarih < DateTime.Today)
                {
                    DialogResult sonuc = MessageBox.Show(
                        $"Seçilen tarih ({secilenTarih:dd.MM.yyyy}) geçmiş bir tarihtir.\n\n" +
                        $"Geçmiş tarihler için kayıt eklemeniz gerekli mi?\n" +
                        $"Devam etmek istiyor musunuz?",
                        "⚠️ Geçmiş Tarih Uyarısı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (sonuc != DialogResult.Yes) return;
                }

                // TakvimGunleri null kontrolü
                if (TakvimGunleri == null)
                    TakvimGunleri = new List<TakvimGunu>();

                // Aynı tarih varsa güncelleme onayı
                var mevcutGun = TakvimGunleri.FirstOrDefault(t => t.Tarih.Date == secilenTarih);
                if (mevcutGun != null)
                {
                    DialogResult sonuc = MessageBox.Show(
                        $"Bu tarih için zaten bir kayıt mevcut:\n\n" +
                        $"📅 Tarih: {secilenTarih:dd.MM.yyyy dddd}\n" +
                        $"📝 Mevcut: {mevcutGun.Aciklama}\n" +
                        $"📝 Yeni: {txtAciklama.Text.Trim()}\n\n" +
                        $"Mevcut kaydı güncellemek istiyor musunuz?",
                        "⚠️ Kayıt Mevcut",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (sonuc != DialogResult.Yes) return;

                    TakvimGunleri.Remove(mevcutGun);
                }

                // Yeni gün oluştur
                TakvimGunu yeniGun = new TakvimGunu
                {
                    Tarih = secilenTarih,
                    ProfilAdi = cmbProfil.SelectedIndex == 0 ? "" : cmbProfil.SelectedItem?.ToString() ?? "",
                    Aciklama = txtAciklama.Text.Trim(),
                    ZilDevreDisi = chkZilDevreDisi.Checked
                };

                TakvimGunleri.Add(yeniGun);
                ListeGuncelle();
                IstatistikGuncelle();

                // Formu temizle
                txtAciklama.Clear();
                chkZilDevreDisi.Checked = false;
                cmbProfil.SelectedIndex = 0;

                DurumGoster($"✓ {secilenTarih:dd.MM.yyyy} tarihi başarıyla eklendi!", Color.FromArgb(34, 197, 94));

                MessageBox.Show(
                    $"✓ Özel gün başarıyla eklendi!\n\n" +
                    $"📅 {secilenTarih:dd MMMM yyyy, dddd}\n" +
                    $"📝 {yeniGun.Aciklama}" +
                    (string.IsNullOrEmpty(yeniGun.ProfilAdi) ? "" : $"\n📁 Profil: {yeniGun.ProfilAdi}") +
                    (yeniGun.ZilDevreDisi ? "\n🔇 Bu gün ziller çalmayacak" : ""),
                    "✓ Kayıt Eklendi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kayıt eklenirken bir hata oluştu:\n\n{ex.Message}",
                    "❌ Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime secilenTarih = takvim.SelectionStart.Date;
                var mevcutGun = TakvimGunleri?.FirstOrDefault(t => t.Tarih.Date == secilenTarih);

                if (mevcutGun != null)
                {
                    DialogResult sonuc = MessageBox.Show(
                        $"Bu kaydı silmek istediğinizden emin misiniz?\n\n" +
                        $"📅 Tarih: {secilenTarih:dd.MM.yyyy dddd}\n" +
                        $"📝 Açıklama: {mevcutGun.Aciklama}\n\n" +
                        $"Bu işlem geri alınamaz!",
                        "🗑️ Silme Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (sonuc == DialogResult.Yes)
                    {
                        TakvimGunleri.Remove(mevcutGun);
                        ListeGuncelle();
                        IstatistikGuncelle();

                        // Formu temizle
                        txtAciklama.Clear();
                        chkZilDevreDisi.Checked = false;
                        cmbProfil.SelectedIndex = 0;

                        DurumGoster($"🗑️ {secilenTarih:dd.MM.yyyy} tarihi silindi", Color.FromArgb(239, 68, 68));
                    }
                }
                else
                {
                    MessageBox.Show(
                        $"{secilenTarih:dd.MM.yyyy} tarihinde kayıt bulunmuyor.\n\n" +
                        $"Silmek için önce sağ taraftaki listeden bir kayıt seçin\n" +
                        $"veya takvimden kayıtlı bir tarih seçin.",
                        "ℹ️ Kayıt Bulunamadı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kayıt silinirken bir hata oluştu:\n\n{ex.Message}",
                    "❌ Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnGecmisSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (TakvimGunleri == null || TakvimGunleri.Count == 0)
                {
                    MessageBox.Show(
                        "Silinecek kayıt bulunamadı.\n\nHenüz hiç özel gün tanımlanmamış.",
                        "ℹ️ Liste Boş",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                int gecmisKayitSayisi = TakvimGunleri.Count(g => g.Tarih.Date < DateTime.Today);

                if (gecmisKayitSayisi == 0)
                {
                    MessageBox.Show(
                        "Geçmiş tarihli kayıt bulunamadı.\n\nTüm kayıtlar bugün veya gelecek tarihlere ait.",
                        "ℹ️ Geçmiş Kayıt Yok",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DialogResult sonuc = MessageBox.Show(
                    $"Geçmiş tarihli {gecmisKayitSayisi} kayıt silinecek.\n\n" +
                    $"⚠️ DİKKAT: Bu işlem geri alınamaz!\n\n" +
                    $"Devam etmek istiyor musunuz?",
                    "🗑️ Geçmiş Kayıtları Temizle",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (sonuc == DialogResult.Yes)
                {
                    TakvimGunleri.RemoveAll(g => g.Tarih.Date < DateTime.Today);
                    ListeGuncelle();
                    IstatistikGuncelle();

                    DurumGoster($"🗑️ {gecmisKayitSayisi} geçmiş kayıt başarıyla silindi", Color.FromArgb(239, 68, 68));

                    MessageBox.Show(
                        $"✓ {gecmisKayitSayisi} geçmiş tarihli kayıt silindi.\n\n" +
                        $"Kalan kayıt sayısı: {TakvimGunleri.Count}",
                        "✓ Temizlik Tamamlandı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Geçmiş kayıtlar silinirken bir hata oluştu:\n\n{ex.Message}",
                    "❌ Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LstGunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstGunler.SelectedIndex < 0) return;
                if (TakvimGunleri == null || TakvimGunleri.Count == 0) return;

                var siraliListe = TakvimGunleri.OrderBy(t => t.Tarih).ToList();

                if (lstGunler.SelectedIndex >= siraliListe.Count) return;

                var gun = siraliListe[lstGunler.SelectedIndex];

                // Takvimde tarihi seç
                takvim.SetDate(gun.Tarih);
                lblSeciliTarih.Text = $"📌 Seçili: {gun.Tarih:dd MMMM yyyy, dddd}";

                // Form alanlarını doldur
                txtAciklama.Text = gun.Aciklama ?? "";
                chkZilDevreDisi.Checked = gun.ZilDevreDisi;

                // Profil seçimini ayarla
                if (string.IsNullOrEmpty(gun.ProfilAdi))
                {
                    cmbProfil.SelectedIndex = 0;
                }
                else
                {
                    bool bulundu = false;
                    for (int i = 0; i < cmbProfil.Items.Count; i++)
                    {
                        if (cmbProfil.Items[i].ToString() == gun.ProfilAdi)
                        {
                            cmbProfil.SelectedIndex = i;
                            bulundu = true;
                            break;
                        }
                    }
                    if (!bulundu) cmbProfil.SelectedIndex = 0;
                }

                DurumGoster($"📝 '{gun.Aciklama}' seçildi. Düzenleyebilir veya silebilirsiniz.", Color.FromArgb(99, 102, 241));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LstGunler_SelectedIndexChanged Hata: {ex.Message}");
            }
        }

        private void LstGunler_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;
                if (TakvimGunleri == null || TakvimGunleri.Count == 0) return;

                var siraliListe = TakvimGunleri.OrderBy(t => t.Tarih).ToList();
                if (e.Index >= siraliListe.Count) return;

                var gun = siraliListe[e.Index];
                bool secili = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                bool gecmis = gun.Tarih.Date < DateTime.Today;

                // Arka plan rengi
                Color arkaPlan;
                if (secili)
                    arkaPlan = Color.FromArgb(99, 102, 241);
                else if (gecmis)
                    arkaPlan = Color.FromArgb(241, 245, 249);
                else
                    arkaPlan = Color.White;

                using (SolidBrush brush = new SolidBrush(arkaPlan))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                // İkon ve metin hazırla
                string ikon = gecmis ? "⚪" : "🔵";
                string zilIkon = gun.ZilDevreDisi ? " 🔇" : "";
                string profilBilgi = string.IsNullOrEmpty(gun.ProfilAdi) ? "" : $" [{gun.ProfilAdi}]";
                string tarihStr = gun.Tarih.ToString("dd.MM.yyyy dddd");
                string aciklama = gun.Aciklama ?? "";

                // Metin satırları
                string ustSatir = $"{ikon} {tarihStr}{zilIkon}";
                string altSatir = $"     {aciklama}{profilBilgi}";

                // Yazı renkleri
                Color ustRenk = secili ? Color.White : (gecmis ? Color.FromArgb(148, 163, 184) : Color.FromArgb(51, 65, 85));
                Color altRenk = secili ? Color.FromArgb(220, 220, 255) : (gecmis ? Color.FromArgb(180, 180, 180) : Color.FromArgb(100, 116, 139));

                // Üst satır (tarih)
                using (Font ustFont = new Font("Segoe UI", 10, FontStyle.Bold))
                using (SolidBrush ustBrush = new SolidBrush(ustRenk))
                {
                    e.Graphics.DrawString(ustSatir, ustFont, ustBrush, e.Bounds.X + 8, e.Bounds.Y + 4);
                }

                // Alt satır (açıklama)
                using (Font altFont = new Font("Segoe UI", 9))
                using (SolidBrush altBrush = new SolidBrush(altRenk))
                {
                    e.Graphics.DrawString(altSatir, altFont, altBrush, e.Bounds.X + 8, e.Bounds.Y + 22);
                }

                // Seçim çerçevesi
                if (secili)
                {
                    using (Pen pen = new Pen(Color.FromArgb(79, 70, 229), 2))
                    {
                        e.Graphics.DrawRectangle(pen, e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 3, e.Bounds.Height - 3);
                    }
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LstGunler_DrawItem Hata: {ex.Message}");
            }
        }

        private void ListeGuncelle()
        {
            try
            {
                lstGunler.Items.Clear();

                if (TakvimGunleri == null || TakvimGunleri.Count == 0)
                {
                    return;
                }

                foreach (var gun in TakvimGunleri.OrderBy(t => t.Tarih))
                {
                    // DrawItem kullanıldığı için sadece placeholder ekliyoruz
                    lstGunler.Items.Add(gun.Aciklama ?? "");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ListeGuncelle Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // KAYDET VE İPTAL BUTONLARI
        // ═══════════════════════════════════════════════════════════════

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Kaydetme onayı (opsiyonel - isterseniz kaldırabilirsiniz)
                int kayitSayisi = TakvimGunleri?.Count ?? 0;
                int kuralSayisi = TakvimKurallari?.Count ?? 0;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kaydetme işlemi sırasında hata oluştu:\n\n{ex.Message}",
                    "❌ Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show(
                "Yaptığınız değişiklikler kaydedilmeyecek.\n\n" +
                "Çıkmak istediğinizden emin misiniz?",
                "⚠️ Değişiklikler Kaydedilmeyecek",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // YARDIMCI METODLAR
        // ═══════════════════════════════════════════════════════════════

        private string GunAdiniAl(int gunIndex)
        {
            if (gunIndex >= 0 && gunIndex < gunAdlari.Length)
                return gunAdlari[gunIndex];
            return "Bilinmeyen Gün";
        }

        private void DurumGoster(string mesaj, Color renk)
        {
            if (lblDurum != null)
            {
                lblDurum.Text = mesaj;
                lblDurum.ForeColor = renk;
            }
        }

        private void IstatistikGuncelle()
        {
            try
            {
                var lblDetay = this.Controls.Find("lblIstatistikDetay", true).FirstOrDefault() as Label;
                if (lblDetay == null) return;

                int toplamGun = TakvimGunleri?.Count ?? 0;
                int gelecekGun = TakvimGunleri?.Count(g => g.Tarih.Date >= DateTime.Today) ?? 0;
                int gecmisGun = toplamGun - gelecekGun;
                int tatilGunu = TakvimGunleri?.Count(g => g.ZilDevreDisi) ?? 0;
                int kuralSayisi = TakvimKurallari?.Count ?? 0;

                lblDetay.Text =
                    $"📅 Toplam Özel Gün: {toplamGun}\n" +
                    $"🔵 Gelecek: {gelecekGun}  |  ⚪ Geçmiş: {gecmisGun}\n" +
                    $"🔇 Tatil Günü: {tatilGunu}  |  ⚙️ Kural: {kuralSayisi}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"IstatistikGuncelle Hata: {ex.Message}");
            }
        }

        private Button CreateStyledButton(string text, Color backColor)
        {
            Button btn = new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            // Hover efekti
            Color originalColor = backColor;
            btn.MouseEnter += (s, e) =>
            {
                if (btn.Enabled)
                    btn.BackColor = ControlPaint.Dark(originalColor, 0.1f);
            };
            btn.MouseLeave += (s, e) =>
            {
                if (btn.Enabled)
                    btn.BackColor = originalColor;
            };

            return btn;
        } 

        private void YuvarlakKoseUygula(Panel panel, int radius)
        {
            try
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                panel.Region = new Region(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"YuvarlakKoseUygula Hata: {ex.Message}");
            }
        }
    }
}