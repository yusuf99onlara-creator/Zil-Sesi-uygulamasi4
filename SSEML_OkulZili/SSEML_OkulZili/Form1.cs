using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace SSEML_OkulZili
{
    public partial class Form1 : Form
    {
        // ═══════════════════════════════════════════════════════════════
        // ANA DEĞİŞKENLER
        // ═══════════════════════════════════════════════════════════════
        private Dictionary<string, List<ZamanItem>> profiller = new Dictionary<string, List<ZamanItem>>();
        private string aktifProfil = "";
        private bool marsCaliniyor = false;
        private bool alarmCaliniyor = false;
        private List<TakvimKural> takvimKurallari = new List<TakvimKural>();
        private string devreDisiIkenGelenZil = "";

        // ═══════════════════════════════════════════════════════════════
        // MEDIA PLAYER DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private WindowsMediaPlayer wmp = new WindowsMediaPlayer();
        private WindowsMediaPlayer marsWmp = new WindowsMediaPlayer();
        private WindowsMediaPlayer alarmWmp = new WindowsMediaPlayer();
        private WindowsMediaPlayer testWmp = new WindowsMediaPlayer();
        private WindowsMediaPlayer onizlemeWmp = new WindowsMediaPlayer();
        private WindowsMediaPlayer teneffusPlayer = new WindowsMediaPlayer();

        // ═══════════════════════════════════════════════════════════════
        // TIMER DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private Timer marsBitisTimer = new Timer();
        private Timer alarmBitisTimer = new Timer();
        private Timer testBitisTimer = new Timer();
        private Timer zilBitisTimer = new Timer();
        private Timer onizlemeBitisTimer = new Timer();
        private Timer geriSayimTimer = new Timer();
        private Timer teneffusKontrolTimer = new Timer();
        private DateTime marsBitisZamani = DateTime.MinValue;

        // ═══════════════════════════════════════════════════════════════
        // SES DOSYALARI DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private string seciliZilSesi = "";
        private string seciliMarsSesi = "";
        private string seciliAlarmSesi = "";
        private List<string> zilSesleri = new List<string>();
        private List<string> marsSesleri = new List<string>();
        private List<string> alarmSesleri = new List<string>();
        private Dictionary<string, string> sesGorunenAdlari = new Dictionary<string, string>();

        // ═══════════════════════════════════════════════════════════════
        // DURUM DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private bool zilCaliniyor = false;
        private string sonCalinanZaman = "";
        private bool karanlikMod = false;
        private bool onizlemeCaliniyor = false;
        private List<TakvimGunu> takvimGunleri = new List<TakvimGunu>();
        private List<string> calinanZiller = new List<string>();
        private int sesSeviyes = 80;
        private bool testZilCaliniyor = false;
        private bool testAlarmCaliniyor = false;
        private bool testMarsCaliniyor = false;

        // ═══════════════════════════════════════════════════════════════
        // TRAY ICON DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        // ═══════════════════════════════════════════════════════════════
        // TENEFFÜS MÜZİĞİ DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private List<TeneffusMuzikProfili> teneffusProfilleri = new List<TeneffusMuzikProfili>();
        private bool teneffusMuzikCaliniyor = false;
        private bool teneffusDuraklatildi = false;
        private double teneffusDuraklatmaKonumu = 0;
        private int teneffusMuzikIndex = 0;
        private TeneffusMuzikProfili aktifTeneffusProfil = null;
        private string aktifTeneffusMuzikDosyasi = "";
        private DateTime teneffusBitisSaati = DateTime.MinValue;

        // ═══════════════════════════════════════════════════════════════
        // ⭐ YENİ: FLİCKER ÖNLEME DEĞİŞKENLERİ
        // ═══════════════════════════════════════════════════════════════
        private string sonGuncellenenDakika = "";
        private DateTime sonListeGuncelleme = DateTime.MinValue;
        private string sonListeIcerigi = "";

        // ═══════════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════════
        public Form1()
        {
            InitializeComponent();
            SureTimerlariAyarla();
            TrayIconOlustur();
        }

        // ═══════════════════════════════════════════════════════════════
        // TIMER AYARLARI - ⭐ GÜNCELLENDİ
        // ═══════════════════════════════════════════════════════════════
        private void SureTimerlariAyarla()
        {
            // Marş bitiş kontrolü
            marsBitisTimer.Interval = 500;
            marsBitisTimer.Tick += MarsBitisTimer_Tick;

            // Alarm bitiş kontrolü
            alarmBitisTimer.Interval = 500;
            alarmBitisTimer.Tick += AlarmBitisTimer_Tick;

            // Test bitiş kontrolü
            testBitisTimer.Interval = 500;
            testBitisTimer.Tick += TestBitisTimer_Tick;

            // Zil bitiş kontrolü
            zilBitisTimer.Interval = 500;
            zilBitisTimer.Tick += ZilBitisTimer_Tick;

            // Önizleme bitiş kontrolü
            onizlemeBitisTimer.Interval = 500;
            onizlemeBitisTimer.Tick += OnizlemeBitisTimer_Tick;

            // ⭐ GERİ SAYIM TIMER - 5 SANİYE (Göz yorgunluğu önleme)
            geriSayimTimer.Interval = 1000;
            geriSayimTimer.Tick += GeriSayimTimer_Tick;

            // ⭐ TENEFFÜS TIMER - 2 SANİYE (CPU tasarrufu)
            teneffusKontrolTimer.Interval = 2000;
            teneffusKontrolTimer.Tick += TeneffusTimer_Tick;
        }

        // ═══════════════════════════════════════════════════════════════
        // TRAY ICON OLUŞTURMA
        // ═══════════════════════════════════════════════════════════════
        private void TrayIconOlustur()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("🔔 Göster", null, TrayGoster_Click);
            trayMenu.Items.Add("🚨 Acil Alarm", null, TrayAlarm_Click);
            trayMenu.Items.Add("-");
            trayMenu.Items.Add("❌ Çıkış", null, TrayCikis_Click);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "SSEML Okul Zili";
            trayIcon.Icon = SystemIcons.Application;
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.DoubleClick += TrayIcon_DoubleClick;
        }

        private void TrayGoster_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            trayIcon.Visible = false;
        }

        private void TrayAlarm_Click(object sender, EventArgs e)
        {
            btnAlarm_Click(null, null);
        }

        private void TrayCikis_Click(object sender, EventArgs e)
        {
            TumTimerlariDurdur();
            TumSesleriDurdur();
            AyarlariKaydet();
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            trayIcon.Visible = false;
        }

        // ═══════════════════════════════════════════════════════════════
        // ⭐ YENİ: TÜM TİMERLARI DURDUR (Memory Leak Önleme)
        // ═══════════════════════════════════════════════════════════════
        private void TumTimerlariDurdur()
        {
            try
            {
                tmrSaat.Stop();
                tmrZilCheck.Stop();
                geriSayimTimer.Stop();
                teneffusKontrolTimer.Stop();
                marsBitisTimer.Stop();
                alarmBitisTimer.Stop();
                testBitisTimer.Stop();
                zilBitisTimer.Stop();
                onizlemeBitisTimer.Stop();
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════
        // ⭐ YENİ: TÜM SESLERİ DURDUR
        // ═══════════════════════════════════════════════════════════════
        private void TumSesleriDurdur()
        {
            try
            {
                wmp.controls.stop();
                marsWmp.controls.stop();
                alarmWmp.controls.stop();
                testWmp.controls.stop();
                onizlemeWmp.controls.stop();
                teneffusPlayer.controls.stop();
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════
        // TIMER TICK OLAYLARI
        // ═══════════════════════════════════════════════════════════════
        private void MarsBitisTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (marsWmp.playState != WMPPlayState.wmppsPlaying)
                {
                    marsBitisTimer.Stop();
                    marsCaliniyor = false;
                    marsBitisZamani = DateTime.Now;
                    btnMarsBaslat.Text = "🎵 MARŞ";
                    btnMarsBaslat.BackColor = Color.FromArgb(34, 197, 94);
                    lblDurum.Text = "✓ Marş tamamlandı";

                    // Marş bitti - teneffüs müziği duraklatılmışsa devam et
                    if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                    {
                        TeneffusMuzikDevamEtGecikme();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"MarsBitisTimer Hata: {ex.Message}");
            }
        }

        private void AlarmBitisTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (alarmWmp.playState != WMPPlayState.wmppsPlaying)
                {
                    alarmBitisTimer.Stop();
                    alarmCaliniyor = false;
                    btnAlarm.Text = "🚨 ACİL ALARM";
                    btnAlarm.BackColor = Color.FromArgb(239, 68, 68);
                    lblDurum.Text = "✓ Alarm durduruldu";

                    // Alarm bitti - teneffüs müziği duraklatılmışsa devam et
                    if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                    {
                        TeneffusMuzikDevamEtGecikme();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AlarmBitisTimer Hata: {ex.Message}");
            }
        }

        private void TestBitisTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (testWmp.playState != WMPPlayState.wmppsPlaying)
                {
                    testBitisTimer.Stop();
                    testZilCaliniyor = false;
                    testAlarmCaliniyor = false;
                    testMarsCaliniyor = false;

                    btnZilTest.Text = "▶ Test";
                    btnAlarmTest.Text = "▶ Test";
                    btnMarsTest.Text = "▶ Test";

                    btnZilTest.BackColor = Color.FromArgb(59, 130, 246);
                    btnAlarmTest.BackColor = Color.FromArgb(239, 68, 68);
                    btnMarsTest.BackColor = Color.FromArgb(34, 197, 94);

                    lblDurum.Text = "✓ Test tamamlandı";

                    // Test bitti - teneffüs müziği duraklatılmışsa devam et
                    if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                    {
                        TeneffusMuzikDevamEtGecikme();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TestBitisTimer Hata: {ex.Message}");
            }
        }

        private void ZilBitisTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (wmp.playState != WMPPlayState.wmppsPlaying)
                {
                    zilBitisTimer.Stop();
                    zilCaliniyor = false;

                    // Zil bitti - teneffüs müziği duraklatılmışsa devam et
                    if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                    {
                        TeneffusMuzikDevamEtGecikme();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ZilBitisTimer Hata: {ex.Message}");
            }
        }

        private void OnizlemeBitisTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (onizlemeWmp.playState != WMPPlayState.wmppsPlaying)
                {
                    onizlemeBitisTimer.Stop();
                    onizlemeCaliniyor = false;
                    lblDurum.Text = "✓ Önizleme tamamlandı";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnizlemeBitisTimer Hata: {ex.Message}");
            }
        }

        private void GeriSayimTimer_Tick(object sender, EventArgs e)
        {
            GeriSayimGuncelle();
            DersBilgisiniGuncelle();
        }

        // ═══════════════════════════════════════════════════════════════
        // GERİ SAYIM VE DERS BİLGİSİ - ⭐ GÜNCELLENDİ (NULL KONTROL)
        // ═══════════════════════════════════════════════════════════════
        private void GeriSayimGuncelle()
        {
            try
            {
                // ⭐ Kapsamlı null kontrol
                if (string.IsNullOrEmpty(aktifProfil) ||
                    !profiller.ContainsKey(aktifProfil) ||
                    profiller[aktifProfil] == null ||
                    profiller[aktifProfil].Count == 0)
                {
                    lblGeriSayim.Text = "";
                    return;
                }

                string simdikiSaat = DateTime.Now.ToString("HH:mm");

                // ⭐ Null kontrolü ile sıralama
                var siraliZamanlar = profiller[aktifProfil]
                    .Where(z => z != null && !string.IsNullOrEmpty(z.Saat))
                    .OrderBy(z => z.Saat)
                    .ToList();

                var siradakiZil = siraliZamanlar.FirstOrDefault(z => string.Compare(z.Saat, simdikiSaat) > 0);

                if (siradakiZil != null)
                {
                    DateTime simdi = DateTime.Now;
                    DateTime hedef = DateTime.Today.Add(TimeSpan.Parse(siradakiZil.Saat));
                    TimeSpan fark = hedef - simdi;

                    if (fark.TotalSeconds > 0)
                    {
                        // ⭐ 1 saatten fazlaysa saati de göster
                        if (fark.TotalHours >= 1)
                        {
                            lblGeriSayim.Text = $"⏱️ Sıradaki zile {(int)fark.TotalHours} sa {fark.Minutes} dk";
                        }
                        else
                        {
                            lblGeriSayim.Text = $"⏱️ Sıradaki zile {fark.Minutes} dk {fark.Seconds} sn";
                        }

                        // Renk ayarla
                        if (fark.TotalSeconds <= 60)
                            lblGeriSayim.ForeColor = Color.FromArgb(239, 68, 68);
                        else if (fark.TotalSeconds <= 300)
                            lblGeriSayim.ForeColor = Color.FromArgb(251, 191, 36);
                        else
                            lblGeriSayim.ForeColor = karanlikMod ? Color.FromArgb(134, 239, 172) : Color.FromArgb(34, 197, 94);
                    }
                }
                else
                {
                    lblGeriSayim.Text = "✓ Bugünün zilleri tamamlandı";
                    lblGeriSayim.ForeColor = karanlikMod ? Color.FromArgb(148, 163, 184) : Color.FromArgb(100, 116, 139);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GeriSayimGuncelle Hata: {ex.Message}");
                lblGeriSayim.Text = "";
            }
        }

        private void DersBilgisiniGuncelle()
        {
            try
            {
                if (string.IsNullOrEmpty(aktifProfil) ||
                    !profiller.ContainsKey(aktifProfil) ||
                    profiller[aktifProfil] == null ||
                    profiller[aktifProfil].Count == 0)
                {
                    lblSiradakiZil.Text = "";
                    return;
                }

                string simdikiSaat = DateTime.Now.ToString("HH:mm");

                var siraliZamanlar = profiller[aktifProfil]
                    .Where(z => z != null && !string.IsNullOrEmpty(z.Saat))
                    .OrderBy(z => z.Saat)
                    .ToList();

                var siradakiZil = siraliZamanlar.FirstOrDefault(z => string.Compare(z.Saat, simdikiSaat) > 0);

                if (siradakiZil != null)
                {
                    string dersInfo = string.IsNullOrEmpty(siradakiZil.Ders) ? "" : $"[{siradakiZil.Ders}. Ders] ";
                    string aciklama = siradakiZil.Aciklama ?? "";
                    lblSiradakiZil.Text = $"📍 Sıradaki: {siradakiZil.Saat} - {dersInfo}{aciklama}";
                }
                else
                {
                    lblSiradakiZil.Text = "";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DersBilgisiniGuncelle Hata: {ex.Message}");
                lblSiradakiZil.Text = "";
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // TEMA AYARLARI
        // ═══════════════════════════════════════════════════════════════
        private void TasarimiUygula()
        {
            Color arkaPlan, kartArkaPlan, yaziRengi, vurguRengi, ikinciYazi, kartBorder;

            if (karanlikMod)
            {
                arkaPlan = Color.FromArgb(26, 32, 44);
                kartArkaPlan = Color.FromArgb(45, 55, 72);
                yaziRengi = Color.FromArgb(237, 242, 247);
                vurguRengi = Color.FromArgb(139, 92, 246);
                ikinciYazi = Color.FromArgb(160, 174, 192);
                kartBorder = Color.FromArgb(74, 85, 104);
            }
            else
            {
                arkaPlan = Color.FromArgb(247, 250, 252);
                kartArkaPlan = Color.White;
                yaziRengi = Color.FromArgb(45, 55, 72);
                vurguRengi = Color.FromArgb(99, 102, 241);
                ikinciYazi = Color.FromArgb(113, 128, 150);
                kartBorder = Color.FromArgb(226, 232, 240);
            }

            this.BackColor = arkaPlan;

            // Label renkleri
            lblSaat.ForeColor = vurguRengi;
            lblTarih.ForeColor = ikinciYazi;
            lblGeriSayim.ForeColor = karanlikMod ? Color.FromArgb(134, 239, 172) : Color.FromArgb(34, 197, 94);
            lblIstatistik.ForeColor = ikinciYazi;
            lblSiradakiZil.ForeColor = yaziRengi;
            lblDurum.ForeColor = ikinciYazi;

            // Panel renkleri
            pnlSaatKutusu.BackColor = kartArkaPlan;
            pnlProfilListesi.BackColor = kartArkaPlan;
            pnlGununProgrami.BackColor = kartArkaPlan;
            pnlSesKontrol.BackColor = kartArkaPlan;
            pnlAltButonlar.BackColor = arkaPlan;

            // Liste renkleri
            lstGununProgrami.BackColor = kartArkaPlan;
            lstGununProgrami.ForeColor = yaziRengi;

            // Arama kutusu renkleri
            txtSesArama.BackColor = karanlikMod ? Color.FromArgb(26, 32, 44) : Color.FromArgb(241, 245, 249);
            txtSesArama.ForeColor = txtSesArama.Text == "🔍 Ses dosyası ara..." ? Color.Gray : yaziRengi;

            // ComboBox renkleri
            cmbZilSesi.BackColor = kartArkaPlan;
            cmbZilSesi.ForeColor = yaziRengi;
            cmbAlarmSesi.BackColor = kartArkaPlan;
            cmbAlarmSesi.ForeColor = yaziRengi;
            cmbMarsSesi.BackColor = kartArkaPlan;
            cmbMarsSesi.ForeColor = yaziRengi;

            // Başlık label renkleri
            lblProfilBaslik.ForeColor = yaziRengi;
            lblProgramBaslik.ForeColor = yaziRengi;
            lblSesBaslik.ForeColor = yaziRengi;
            lblZilSesi.ForeColor = ikinciYazi;
            lblAlarmSesi.ForeColor = ikinciYazi;
            lblMarsSesi.ForeColor = ikinciYazi;
            lblVolume.ForeColor = ikinciYazi;

            ProfilKartlariniGuncelle();
        }

        private void chkKaranlikMod_CheckedChanged(object sender, EventArgs e)
        {
            karanlikMod = chkKaranlikMod.Checked;
            TasarimiUygula();
            AyarlariKaydet();
        }

        // ═══════════════════════════════════════════════════════════════
        // FORM LOAD - ⭐ GÜNCELLENDİ
        // ═══════════════════════════════════════════════════════════════
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Klasörleri oluştur
                KlasorleriOlustur();

                // Ses dosyalarını yükle
                SesDosyalariniYukle();

                // Varsayılan profilleri oluştur
                VarsayilanProfilleriOlustur();

                // Ayarları yükle
                AyarlariYukle();

                // Tasarımı uygula
                TasarimiUygula();

                // Günlük profil seç
                GunlukProfilSec();

                // Takvim kontrol
                TakvimKontrol();

                // Profil kartlarını güncelle
                ProfilKartlariniGuncelle();

                // Günün programını göster
                GununPrograminiGoster();

                // Ses listelerini güncelle
                SesListeleriniGuncelle();
                SesSecimleriniYap();

                // İstatistik güncelle
                IstatistikGuncelle();

                // Arama kutusu placeholder
                txtSesArama.Text = "🔍 Ses dosyası ara...";
                txtSesArama.ForeColor = Color.Gray;

                // ⭐ SAAT TIMER - Her saniye (saat gösterimi için gerekli)
                tmrSaat.Interval = 1000;
                tmrSaat.Tick += tmrSaat_Tick;
                tmrSaat.Start();

                // ⭐ ZİL KONTROL TIMER - Her saniye (zil çalması için gerekli)
                tmrZilCheck.Interval = 1000;
                tmrZilCheck.Tick += tmrZilCheck_Tick;
                tmrZilCheck.Start();

                // ⭐ GERİ SAYIM TIMER - 5 saniye (göz yorgunluğu önleme)
                geriSayimTimer.Interval = 1000;
                geriSayimTimer.Start();

                // ⭐ TENEFFÜS TIMER - 2 saniye (CPU tasarrufu)
                teneffusKontrolTimer.Interval = 2000;
                teneffusKontrolTimer.Start();

                // Ses seviyesi ayarla
                trackVolume.Value = sesSeviyes;
                SesSeviyesiAyarla();

                // Saat ve tarih göster
                lblSaat.Text = DateTime.Now.ToString("HH:mm:ss");
                lblTarih.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

                lblDurum.Text = "✓ Program başarıyla yüklendi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                trayIcon.Visible = true;
                trayIcon.ShowBalloonTip(2000, "SSEML Okul Zili", "Program arka planda çalışmaya devam ediyor.", ToolTipIcon.Info);
            }
            else
            {
                // ⭐ Kapatırken tüm kaynakları temizle
                TumTimerlariDurdur();
                TumSesleriDurdur();
                AyarlariKaydet();
                trayIcon.Visible = false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
     
        }

        // ═══════════════════════════════════════════════════════════════
        // KLASÖR VE SES DOSYALARI
        // ═══════════════════════════════════════════════════════════════
        private void KlasorleriOlustur()
        {
            string[] klasorler = { "ZilSesleri", "AlarmSesleri", "Marslar", "TeneffusMuzikleri" };
            foreach (string klasor in klasorler)
            {
                try
                {
                    string yol = Path.Combine(Application.StartupPath, klasor);
                    if (!Directory.Exists(yol))
                        Directory.CreateDirectory(yol);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Klasör oluşturma hatası: {ex.Message}");
                }
            }
        }

        private void SesDosyalariniYukle()
        {
            try
            {
                // Zil sesleri
                zilSesleri.Clear();
                string zilKlasor = Path.Combine(Application.StartupPath, "ZilSesleri");
                if (Directory.Exists(zilKlasor))
                {
                    foreach (string dosya in Directory.GetFiles(zilKlasor, "*.*"))
                    {
                        string uzanti = Path.GetExtension(dosya).ToLower();
                        if (uzanti == ".mp3" || uzanti == ".wav" || uzanti == ".wma")
                            zilSesleri.Add(Path.GetFileName(dosya));
                    }
                }

                // Alarm sesleri
                alarmSesleri.Clear();
                string alarmKlasor = Path.Combine(Application.StartupPath, "AlarmSesleri");
                if (Directory.Exists(alarmKlasor))
                {
                    foreach (string dosya in Directory.GetFiles(alarmKlasor, "*.*"))
                    {
                        string uzanti = Path.GetExtension(dosya).ToLower();
                        if (uzanti == ".mp3" || uzanti == ".wav" || uzanti == ".wma")
                            alarmSesleri.Add(Path.GetFileName(dosya));
                    }
                }

                // Marş sesleri
                marsSesleri.Clear();
                string marsKlasor = Path.Combine(Application.StartupPath, "Marslar");
                if (Directory.Exists(marsKlasor))
                {
                    foreach (string dosya in Directory.GetFiles(marsKlasor, "*.*"))
                    {
                        string uzanti = Path.GetExtension(dosya).ToLower();
                        if (uzanti == ".mp3" || uzanti == ".wav" || uzanti == ".wma")
                            marsSesleri.Add(Path.GetFileName(dosya));
                    }
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Ses yükleme hatası: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // ARAMA KUTUSU
        // ═══════════════════════════════════════════════════════════════
        private void txtSesArama_Enter(object sender, EventArgs e)
        {
            if (txtSesArama.Text == "🔍 Ses dosyası ara...")
            {
                txtSesArama.Text = "";
                txtSesArama.ForeColor = karanlikMod ? Color.FromArgb(237, 242, 247) : Color.FromArgb(45, 55, 72);
            }
        }

        private void txtSesArama_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSesArama.Text))
            {
                txtSesArama.Text = "🔍 Ses dosyası ara...";
                txtSesArama.ForeColor = Color.Gray;
            }
        }

        private void txtSesArama_TextChanged(object sender, EventArgs e)
        {
            if (txtSesArama.Text != "🔍 Ses dosyası ara...")
            {
                SesListeleriniGuncelle();
                SesSecimleriniYap();
            }
        }

        private void SesListeleriniGuncelle()
        {
            string filtre = "";

            if (txtSesArama.Text != "🔍 Ses dosyası ara...")
            {
                filtre = txtSesArama.Text.ToLower().Trim();
            }

            cmbZilSesi.Items.Clear();
            cmbAlarmSesi.Items.Clear();
            cmbMarsSesi.Items.Clear();

            foreach (string ses in zilSesleri)
            {
                string gorunenAd = SesGorunenAdiniAl(ses);
                if (string.IsNullOrEmpty(filtre) || gorunenAd.ToLower().Contains(filtre) || ses.ToLower().Contains(filtre))
                    cmbZilSesi.Items.Add(new SesItem(ses, gorunenAd));
            }

            foreach (string ses in alarmSesleri)
            {
                string gorunenAd = SesGorunenAdiniAl(ses);
                if (string.IsNullOrEmpty(filtre) || gorunenAd.ToLower().Contains(filtre) || ses.ToLower().Contains(filtre))
                    cmbAlarmSesi.Items.Add(new SesItem(ses, gorunenAd));
            }

            foreach (string ses in marsSesleri)
            {
                string gorunenAd = SesGorunenAdiniAl(ses);
                if (string.IsNullOrEmpty(filtre) || gorunenAd.ToLower().Contains(filtre) || ses.ToLower().Contains(filtre))
                    cmbMarsSesi.Items.Add(new SesItem(ses, gorunenAd));
            }
        }

        private string SesGorunenAdiniAl(string dosyaAdi)
        {
            if (string.IsNullOrEmpty(dosyaAdi))
                return "";

            if (sesGorunenAdlari.ContainsKey(dosyaAdi))
                return sesGorunenAdlari[dosyaAdi];

            string ad = Path.GetFileNameWithoutExtension(dosyaAdi);
            ad = ad.Replace("_", " ").Replace("-", " ");

            if (!string.IsNullOrEmpty(ad))
            {
                var kelimeler = ad.Split(' ');
                for (int i = 0; i < kelimeler.Length; i++)
                {
                    if (kelimeler[i].Length > 0)
                        kelimeler[i] = char.ToUpper(kelimeler[i][0]) + kelimeler[i].Substring(1).ToLower();
                }
                ad = string.Join(" ", kelimeler);
            }

            return ad;
        }

        private void SesSecimleriniYap()
        {
            // Zil sesi seçimi
            if (!string.IsNullOrEmpty(seciliZilSesi))
            {
                string seciliDosya = Path.GetFileName(seciliZilSesi);
                for (int i = 0; i < cmbZilSesi.Items.Count; i++)
                {
                    if (((SesItem)cmbZilSesi.Items[i]).DosyaAdi == seciliDosya)
                    {
                        cmbZilSesi.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (cmbZilSesi.Items.Count > 0)
                cmbZilSesi.SelectedIndex = 0;

            // Alarm sesi seçimi
            if (!string.IsNullOrEmpty(seciliAlarmSesi))
            {
                string seciliDosya = Path.GetFileName(seciliAlarmSesi);
                for (int i = 0; i < cmbAlarmSesi.Items.Count; i++)
                {
                    if (((SesItem)cmbAlarmSesi.Items[i]).DosyaAdi == seciliDosya)
                    {
                        cmbAlarmSesi.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (cmbAlarmSesi.Items.Count > 0)
                cmbAlarmSesi.SelectedIndex = 0;

            // Marş sesi seçimi
            if (!string.IsNullOrEmpty(seciliMarsSesi))
            {
                string seciliDosya = Path.GetFileName(seciliMarsSesi);
                for (int i = 0; i < cmbMarsSesi.Items.Count; i++)
                {
                    if (((SesItem)cmbMarsSesi.Items[i]).DosyaAdi == seciliDosya)
                    {
                        cmbMarsSesi.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (cmbMarsSesi.Items.Count > 0)
                cmbMarsSesi.SelectedIndex = 0;
        }

        // ═══════════════════════════════════════════════════════════════
        // SAAT VE ZİL KONTROL - ⭐ GÜNCELLENDİ
        // ═══════════════════════════════════════════════════════════════
        private void tmrSaat_Tick(object sender, EventArgs e)
        {
            // Saat güncelle
            lblSaat.Text = DateTime.Now.ToString("HH:mm:ss");

            // Gece yarısı kontrolü
            if (DateTime.Now.ToString("HH:mm:ss") == "00:00:00")
            {
                lblTarih.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                calinanZiller.Clear();
                sonGuncellenenDakika = "";
                IstatistikGuncelle();
                TakvimKontrol();
                GununPrograminiGoster();
            }
        }

        private void tmrZilCheck_Tick(object sender, EventArgs e)
        {
            // Zil kontrolü
            ZilKontrol();

            // ⭐ LİSTEYİ SADECE DAKİKA DEĞİŞTİĞİNDE GÜNCELLE (Flicker önleme)
            string simdikiDakika = DateTime.Now.ToString("HH:mm");
            if (simdikiDakika != sonGuncellenenDakika)
            {
                sonGuncellenenDakika = simdikiDakika;
                GununPrograminiGoster();
                IstatistikGuncelle();
            }
        }

        private void ZilKontrol()
        {
            try
            {
                if (string.IsNullOrEmpty(aktifProfil)) return;
                if (marsCaliniyor) return;
                if (!profiller.ContainsKey(aktifProfil)) return;

                string simdikiSaat = DateTime.Now.ToString("HH:mm");

                // Zil devre dışıysa
                if (chkZilDevreDisi.Checked)
                {
                    foreach (ZamanItem item in profiller[aktifProfil])
                    {
                        if (item != null && item.Saat == simdikiSaat)
                        {
                            devreDisiIkenGelenZil = simdikiSaat;
                            return;
                        }
                    }
                    return;
                }

                if (devreDisiIkenGelenZil == simdikiSaat) return;
                if (sonCalinanZaman == simdikiSaat) return;

                foreach (ZamanItem item in profiller[aktifProfil])
                {
                    if (item == null) continue;

                    if (item.Saat == simdikiSaat)
                    {
                        // Marş bittikten sonra 5 saniye bekle
                        if (marsBitisZamani != DateTime.MinValue)
                        {
                            TimeSpan fark = DateTime.Now - marsBitisZamani;
                            if (fark.TotalSeconds < 5)
                            {
                                sonCalinanZaman = simdikiSaat;
                                return;
                            }
                        }

                        if (!zilCaliniyor)
                        {
                            // Teneffüs müziğini duraklat
                            if (teneffusMuzikCaliniyor)
                            {
                                TeneffusMuzikDuraklat();
                            }

                            DigerSesleriDurdur("zil");

                            if (!string.IsNullOrEmpty(seciliZilSesi) && File.Exists(seciliZilSesi))
                            {
                                try
                                {
                                    wmp.URL = seciliZilSesi;
                                    wmp.controls.play();
                                    zilCaliniyor = true;
                                    zilBitisTimer.Start();
                                    sonCalinanZaman = simdikiSaat;
                                    devreDisiIkenGelenZil = "";

                                    if (!calinanZiller.Contains(simdikiSaat))
                                        calinanZiller.Add(simdikiSaat);

                                    string aciklama = item.Aciklama ?? "Zil çaldı";
                                    lblDurum.Text = $"🔔 {aciklama}";

                                    if (trayIcon != null && trayIcon.Visible)
                                        trayIcon.ShowBalloonTip(3000, "🔔 Zil Çaldı", aciklama, ToolTipIcon.Info);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Zil çalma hatası: {ex.Message}");
                                }
                            }
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ZilKontrol Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // İSTATİSTİK
        // ═══════════════════════════════════════════════════════════════
        private void IstatistikGuncelle()
        {
            try
            {
                if (string.IsNullOrEmpty(aktifProfil) ||
                    !profiller.ContainsKey(aktifProfil) ||
                    profiller[aktifProfil] == null)
                {
                    lblIstatistik.Text = "📊 0/0 zil";
                    return;
                }

                int toplamZil = profiller[aktifProfil].Count;
                int calan = 0;

                string simdikiSaat = DateTime.Now.ToString("HH:mm");

                foreach (var zaman in profiller[aktifProfil])
                {
                    if (zaman != null && !string.IsNullOrEmpty(zaman.Saat))
                    {
                        if (string.Compare(zaman.Saat, simdikiSaat) <= 0)
                        {
                            calan++;
                        }
                    }
                }

                lblIstatistik.Text = $"📊 {calan}/{toplamZil} zil çaldı";

                if (calan == toplamZil && toplamZil > 0)
                    lblIstatistik.ForeColor = Color.FromArgb(34, 197, 94);
                else if (calan > 0)
                    lblIstatistik.ForeColor = Color.FromArgb(251, 191, 36);
                else
                    lblIstatistik.ForeColor = karanlikMod ? Color.FromArgb(160, 174, 192) : Color.FromArgb(113, 128, 150);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"IstatistikGuncelle Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // GÜNÜN PROGRAMI - ⭐ GÜNCELLENDİ (FLİCKER ÖNLEME)
        // ═══════════════════════════════════════════════════════════════
        private void GununPrograminiGoster()
        {
            try
            {
                if (!profiller.ContainsKey(aktifProfil) || profiller[aktifProfil] == null)
                {
                    if (lstGununProgrami.Items.Count > 0)
                        lstGununProgrami.Items.Clear();
                    return;
                }

                string simdikiSaat = DateTime.Now.ToString("HH:mm");
                var siraliListe = profiller[aktifProfil]
                    .Where(z => z != null && !string.IsNullOrEmpty(z.Saat))
                    .OrderBy(z => z.Saat)
                    .ToList();

                // ⭐ Yeni içeriği oluştur
                List<string> yeniIcerik = new List<string>();
                foreach (ZamanItem item in siraliListe)
                {
                    string durum;
                    if (calinanZiller.Contains(item.Saat) || string.Compare(item.Saat, simdikiSaat) < 0)
                        durum = "✓";
                    else if (item.Saat == simdikiSaat)
                        durum = "▶";
                    else
                        durum = "○";

                    string dersInfo = string.IsNullOrEmpty(item.Ders) ? "" : $"[{item.Ders}. Ders]";
                    string aciklama = item.Aciklama ?? "";
                    yeniIcerik.Add($"  {durum}  {item.Saat}    {dersInfo}  {aciklama}");
                }

                // ⭐ İçerik değişti mi kontrol et
                string yeniIcerikStr = string.Join("|", yeniIcerik);
                if (yeniIcerikStr == sonListeIcerigi)
                {
                    // Sadece seçimi güncelle
                    for (int i = 0; i < siraliListe.Count; i++)
                    {
                        if (string.Compare(siraliListe[i].Saat, simdikiSaat) >= 0)
                        {
                            if (i < lstGununProgrami.Items.Count && lstGununProgrami.SelectedIndex != i)
                            {
                                lstGununProgrami.SelectedIndex = i;
                                lstGununProgrami.TopIndex = Math.Max(0, i - 2);
                            }
                            break;
                        }
                    }
                    return;
                }

                // ⭐ FLICKER ÖNLEME - BeginUpdate/EndUpdate
                lstGununProgrami.BeginUpdate();

                try
                {
                    lstGununProgrami.Items.Clear();

                    foreach (string satir in yeniIcerik)
                    {
                        lstGununProgrami.Items.Add(satir);
                    }

                    sonListeIcerigi = yeniIcerikStr;

                    // Sıradaki zile scroll
                    for (int i = 0; i < siraliListe.Count; i++)
                    {
                        if (string.Compare(siraliListe[i].Saat, simdikiSaat) >= 0)
                        {
                            if (i < lstGununProgrami.Items.Count)
                            {
                                lstGununProgrami.SelectedIndex = i;
                                lstGununProgrami.TopIndex = Math.Max(0, i - 2);
                            }
                            break;
                        }
                    }
                }
                finally
                {
                    lstGununProgrami.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GununPrograminiGoster Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // PROFİL KARTLARI
        // ═══════════════════════════════════════════════════════════════
        private void ProfilKartlariniGuncelle()
        {
            try
            {
                pnlProfilKartlari.Controls.Clear();

                int kartYukseklik = 75;
                int kartAraligi = 12;
                int y = 10;

                Color kartArkaplan = karanlikMod ? Color.FromArgb(45, 55, 72) : Color.FromArgb(241, 245, 249);
                Color aktifKartArkaplan = karanlikMod ? Color.FromArgb(139, 92, 246) : Color.FromArgb(99, 102, 241);
                Color kartYazi = karanlikMod ? Color.FromArgb(237, 242, 247) : Color.FromArgb(45, 55, 72);
                Color aktifKartYazi = Color.White;

                foreach (var profil in profiller)
                {
                    Panel kart = new Panel();
                    kart.Size = new Size(pnlProfilKartlari.Width - 30, kartYukseklik);
                    kart.Location = new Point(10, y);
                    kart.Cursor = Cursors.Hand;
                    kart.Tag = profil.Key;
                    kart.BackColor = profil.Key == aktifProfil ? aktifKartArkaplan : kartArkaplan;

                    // Yuvarlak köşeler
                    try
                    {
                        GraphicsPath path = new GraphicsPath();
                        int radius = 16;
                        path.AddArc(0, 0, radius, radius, 180, 90);
                        path.AddArc(kart.Width - radius, 0, radius, radius, 270, 90);
                        path.AddArc(kart.Width - radius, kart.Height - radius, radius, radius, 0, 90);
                        path.AddArc(0, kart.Height - radius, radius, radius, 90, 90);
                        path.CloseFigure();
                        kart.Region = new Region(path);
                    }
                    catch { }

                    // İkon panel
                    Panel ikonPanel = new Panel();
                    ikonPanel.Size = new Size(50, 50);
                    ikonPanel.Location = new Point(10, 12);
                    ikonPanel.BackColor = Color.Transparent;

                    Label lblIkon = new Label();
                    string ikon = "📁";
                    string profilLower = profil.Key.ToLower();
                    if (profilLower.Contains("normal")) ikon = "☀️";
                    else if (profilLower.Contains("öğle") || profilLower.Contains("ogle")) ikon = "🌙";
                    else if (profilLower.Contains("sınav") || profilLower.Contains("sinav")) ikon = "📝";
                    else if (profilLower.Contains("cuma")) ikon = "🕌";
                    else if (profilLower.Contains("hafta")) ikon = "🏠";

                    lblIkon.Text = ikon;
                    lblIkon.Font = new Font("Segoe UI Emoji", 20);
                    lblIkon.Location = new Point(5, 5);
                    lblIkon.Size = new Size(40, 40);
                    lblIkon.BackColor = Color.Transparent;
                    lblIkon.TextAlign = ContentAlignment.MiddleCenter;

                    ikonPanel.Controls.Add(lblIkon);
                    kart.Controls.Add(ikonPanel);

                    // Profil adı
                    Label lblAd = new Label();
                    lblAd.Text = profil.Key;
                    lblAd.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    lblAd.ForeColor = profil.Key == aktifProfil ? aktifKartYazi : kartYazi;
                    lblAd.Location = new Point(70, 15);
                    lblAd.AutoSize = true;
                    lblAd.BackColor = Color.Transparent;
                    kart.Controls.Add(lblAd);

                    // Detay
                    Label lblDetay = new Label();
                    int zilSayisi = profil.Value != null ? profil.Value.Count : 0;
                    lblDetay.Text = $"{zilSayisi} zil zamanı";
                    lblDetay.Font = new Font("Segoe UI", 10);
                    lblDetay.ForeColor = profil.Key == aktifProfil ?
                        Color.FromArgb(220, 220, 255) :
                        (karanlikMod ? Color.FromArgb(160, 174, 192) : Color.FromArgb(113, 128, 150));
                    lblDetay.Location = new Point(70, 40);
                    lblDetay.AutoSize = true;
                    lblDetay.BackColor = Color.Transparent;
                    kart.Controls.Add(lblDetay);

                    // Aktif etiketi
                    if (profil.Key == aktifProfil)
                    {
                        Label lblAktif = new Label();
                        lblAktif.Text = "✓ AKTİF";
                        lblAktif.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        lblAktif.ForeColor = Color.FromArgb(134, 239, 172);
                        lblAktif.Location = new Point(kart.Width - 70, 28);
                        lblAktif.AutoSize = true;
                        lblAktif.BackColor = Color.Transparent;
                        kart.Controls.Add(lblAktif);
                    }

                    // Click olayları
                    kart.Click += ProfilKart_Click;
                    ikonPanel.Click += (s, ev) => ProfilKart_Click(kart, ev);
                    lblIkon.Click += (s, ev) => ProfilKart_Click(kart, ev);
                    lblAd.Click += (s, ev) => ProfilKart_Click(kart, ev);
                    lblDetay.Click += (s, ev) => ProfilKart_Click(kart, ev);

                    pnlProfilKartlari.Controls.Add(kart);
                    y += kartYukseklik + kartAraligi;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProfilKartlariniGuncelle Hata: {ex.Message}");
            }
        }

        private void ProfilKart_Click(object sender, EventArgs e)
        {
            try
            {
                Panel kart = sender as Panel;
                if (kart != null && kart.Tag != null)
                {
                    aktifProfil = kart.Tag.ToString();
                    calinanZiller.Clear();
                    sonListeIcerigi = "";
                    sonGuncellenenDakika = "";
                    ProfilKartlariniGuncelle();
                    GununPrograminiGoster();
                    IstatistikGuncelle();
                    AyarlariKaydet();
                    lblDurum.Text = $"📁 {aktifProfil} profili seçildi";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProfilKart_Click Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // ALARM BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(seciliAlarmSesi) && File.Exists(seciliAlarmSesi))
                {
                    if (!alarmCaliniyor)
                    {
                        DialogResult onay = MessageBox.Show(
                            "⚠️ DİKKAT!\n\n" +
                            "ACİL ALARM ÇALINACAK!\n\n" +
                            "Bu işlem tüm okula alarm sesi yayacaktır.\n" +
                            "Emin misiniz?",
                            "⚠️ ACİL ALARM ONAYI",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2);

                        if (onay != DialogResult.Yes)
                        {
                            lblDurum.Text = "✗ Alarm iptal edildi";
                            return;
                        }

                        // Teneffüs müziğini duraklat
                        if (teneffusMuzikCaliniyor)
                        {
                            TeneffusMuzikDuraklat();
                        }

                        DigerSesleriDurdur("alarm");

                        alarmWmp.URL = seciliAlarmSesi;
                        alarmWmp.controls.play();
                        alarmCaliniyor = true;
                        alarmBitisTimer.Start();
                        btnAlarm.Text = "⏹️ DURDUR";
                        btnAlarm.BackColor = Color.FromArgb(185, 28, 28);
                        lblDurum.Text = "⚠️ ACİL ALARM ÇALIYOR!";
                    }
                    else
                    {
                        alarmWmp.controls.stop();
                        alarmBitisTimer.Stop();
                        alarmCaliniyor = false;
                        btnAlarm.Text = "🚨 ACİL ALARM";
                        btnAlarm.BackColor = Color.FromArgb(239, 68, 68);
                        lblDurum.Text = "✓ Alarm durduruldu";

                        // Teneffüs devam
                        if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                        {
                            TeneffusMuzikDevamEtGecikme();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Alarm sesi dosyası bulunamadı!\nLütfen bir alarm sesi seçin.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // MARŞ BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnMarsBaslat_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(seciliMarsSesi) && File.Exists(seciliMarsSesi))
                {
                    if (!marsCaliniyor)
                    {
                        DialogResult onay = MessageBox.Show(
                            "İstiklal Marşı'nı çalmak istiyor musunuz?\n\n" +
                            "Bu işlem tüm okula marş sesi yayacaktır.",
                            "🎵 İstiklal Marşı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (onay != DialogResult.Yes)
                        {
                            lblDurum.Text = "✗ Marş iptal edildi";
                            return;
                        }

                        // Teneffüs müziğini duraklat
                        if (teneffusMuzikCaliniyor)
                        {
                            TeneffusMuzikDuraklat();
                        }

                        DigerSesleriDurdur("mars");

                        marsWmp.URL = seciliMarsSesi;
                        marsWmp.controls.play();
                        marsCaliniyor = true;
                        marsBitisTimer.Start();
                        btnMarsBaslat.Text = "⏹️ DURDUR";
                        btnMarsBaslat.BackColor = Color.FromArgb(21, 128, 61);
                        lblDurum.Text = "🎵 Marş çalıyor...";
                    }
                    else
                    {
                        marsWmp.controls.stop();
                        marsBitisTimer.Stop();
                        marsCaliniyor = false;
                        btnMarsBaslat.Text = "🎵 MARŞ";
                        btnMarsBaslat.BackColor = Color.FromArgb(34, 197, 94);
                        lblDurum.Text = "✓ Marş durduruldu";

                        // Teneffüs devam
                        if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                        {
                            TeneffusMuzikDevamEtGecikme();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Marş dosyası bulunamadı!\n\nLütfen:\n1. Ses Ayarları sekmesinden bir marş seçin\n2. 'Marslar' klasörüne mp3 dosyası ekleyin",
                        "⚠️ Marş Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // TAKVİM BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnTakvim_Click(object sender, EventArgs e)
        {
            try
            {
                TakvimForm frm = new TakvimForm(takvimGunleri, profiller);
                frm.TakvimKurallari = takvimKurallari;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    takvimGunleri = frm.TakvimGunleri;
                    takvimKurallari = frm.TakvimKurallari;
                    AyarlariKaydet();
                    TakvimKontrol();
                    lblDurum.Text = "📅 Takvim güncellendi";
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // AYARLAR BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            try
            {
                if (profiller.ContainsKey(aktifProfil))
                {
                    string zilSesiDosyaAdi = !string.IsNullOrEmpty(seciliZilSesi) ? Path.GetFileName(seciliZilSesi) : "";
                    ProfileManagerForm frm = new ProfileManagerForm(aktifProfil, profiller, zilSesiDosyaAdi);

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        profiller[frm.ProfilAdi] = frm.ZamanListesi;
                        aktifProfil = frm.ProfilAdi;
                        sonListeIcerigi = "";
                        AyarlariKaydet();
                        ProfilKartlariniGuncelle();
                        GununPrograminiGoster();
                        lblDurum.Text = "✓ Profil güncellendi";
                    }
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // YENİ PROFİL BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnYeniProfil_Click(object sender, EventArgs e)
        {
            try
            {
                string yeniProfilAdi = Prompt.ShowDialog("Yeni profil adını girin:", "➕ Yeni Profil");

                if (!string.IsNullOrWhiteSpace(yeniProfilAdi))
                {
                    if (!profiller.ContainsKey(yeniProfilAdi))
                    {
                        profiller[yeniProfilAdi] = new List<ZamanItem>();
                        aktifProfil = yeniProfilAdi;
                        sonListeIcerigi = "";
                        ProfilKartlariniGuncelle();
                        GununPrograminiGoster();
                        AyarlariKaydet();
                        lblDurum.Text = $"✓ '{yeniProfilAdi}' oluşturuldu";

                        btnAyarlar_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Bu profil adı zaten mevcut!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // PROFİL SİL BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnProfilSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (profiller.ContainsKey(aktifProfil) && aktifProfil != "NormalGun" && aktifProfil != "OgleOkulu")
                {
                    DialogResult result = MessageBox.Show(
                        $"'{aktifProfil}' profilini silmek istediğinizden emin misiniz?",
                        "🗑️ Profil Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        profiller.Remove(aktifProfil);
                        aktifProfil = "NormalGun";
                        calinanZiller.Clear();
                        sonListeIcerigi = "";
                        ProfilKartlariniGuncelle();
                        GununPrograminiGoster();
                        AyarlariKaydet();
                        lblDurum.Text = "🗑️ Profil silindi";
                    }
                }
                else
                {
                    MessageBox.Show("Varsayılan profiller silinemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // SES TEST BUTONLARI
        // ═══════════════════════════════════════════════════════════════
        private void btnZilTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbZilSesi.SelectedItem != null)
                {
                    if (!testZilCaliniyor)
                    {
                        DialogResult onay = MessageBox.Show(
                            $"Seçili zil sesini test etmek istiyor musunuz?\n\n" +
                            $"Ses: {((SesItem)cmbZilSesi.SelectedItem).GorunenAd}",
                            "🔔 Zil Testi",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (onay != DialogResult.Yes) return;

                        if (teneffusMuzikCaliniyor)
                        {
                            TeneffusMuzikDuraklat();
                        }

                        string dosya = Path.Combine(Application.StartupPath, "ZilSesleri", ((SesItem)cmbZilSesi.SelectedItem).DosyaAdi);
                        if (File.Exists(dosya))
                        {
                            DigerSesleriDurdur("test");
                            testWmp.URL = dosya;
                            testWmp.controls.play();
                            testZilCaliniyor = true;
                            testBitisTimer.Start();
                            btnZilTest.Text = "⏹️ Durdur";
                            btnZilTest.BackColor = Color.FromArgb(30, 64, 175);
                            lblDurum.Text = $"🎵 Zil test ediliyor...";
                        }
                    }
                    else
                    {
                        testWmp.controls.stop();
                        testBitisTimer.Stop();
                        testZilCaliniyor = false;
                        btnZilTest.Text = "▶ Test";
                        btnZilTest.BackColor = Color.FromArgb(59, 130, 246);
                        lblDurum.Text = "✓ Test durduruldu";

                        if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                        {
                            TeneffusMuzikDevamEtGecikme();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        private void btnAlarmTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAlarmSesi.SelectedItem != null)
                {
                    if (!testAlarmCaliniyor)
                    {
                        DialogResult onay = MessageBox.Show(
                            $"Seçili alarm sesini test etmek istiyor musunuz?\n\n" +
                            $"Ses: {((SesItem)cmbAlarmSesi.SelectedItem).GorunenAd}",
                            "🚨 Alarm Testi",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (onay != DialogResult.Yes) return;

                        if (teneffusMuzikCaliniyor)
                        {
                            TeneffusMuzikDuraklat();
                        }

                        string dosya = Path.Combine(Application.StartupPath, "AlarmSesleri", ((SesItem)cmbAlarmSesi.SelectedItem).DosyaAdi);
                        if (File.Exists(dosya))
                        {
                            DigerSesleriDurdur("test");
                            testWmp.URL = dosya;
                            testWmp.controls.play();
                            testAlarmCaliniyor = true;
                            testBitisTimer.Start();
                            btnAlarmTest.Text = "⏹️ Durdur";
                            btnAlarmTest.BackColor = Color.FromArgb(185, 28, 28);
                            lblDurum.Text = $"🎵 Alarm test ediliyor...";
                        }
                    }
                    else
                    {
                        testWmp.controls.stop();
                        testBitisTimer.Stop();
                        testAlarmCaliniyor = false;
                        btnAlarmTest.Text = "▶ Test";
                        btnAlarmTest.BackColor = Color.FromArgb(239, 68, 68);
                        lblDurum.Text = "✓ Test durduruldu";

                        if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                        {
                            TeneffusMuzikDevamEtGecikme();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        private void btnMarsTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMarsSesi.SelectedItem != null)
                {
                    if (!testMarsCaliniyor)
                    {
                        DialogResult onay = MessageBox.Show(
                            $"Seçili marşı test etmek istiyor musunuz?\n\n" +
                            $"Ses: {((SesItem)cmbMarsSesi.SelectedItem).GorunenAd}",
                            "🎺 Marş Testi",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (onay != DialogResult.Yes) return;

                        if (teneffusMuzikCaliniyor)
                        {
                            TeneffusMuzikDuraklat();
                        }

                        string dosya = Path.Combine(Application.StartupPath, "Marslar", ((SesItem)cmbMarsSesi.SelectedItem).DosyaAdi);
                        if (File.Exists(dosya))
                        {
                            DigerSesleriDurdur("test");
                            testWmp.URL = dosya;
                            testWmp.controls.play();
                            testMarsCaliniyor = true;
                            testBitisTimer.Start();
                            btnMarsTest.Text = "⏹️ Durdur";
                            btnMarsTest.BackColor = Color.FromArgb(21, 128, 61);
                            lblDurum.Text = $"🎵 Marş test ediliyor...";
                        }
                    }
                    else
                    {
                        testWmp.controls.stop();
                        testBitisTimer.Stop();
                        testMarsCaliniyor = false;
                        btnMarsTest.Text = "▶ Test";
                        btnMarsTest.BackColor = Color.FromArgb(34, 197, 94);
                        lblDurum.Text = "✓ Test durduruldu";

                        if (teneffusDuraklatildi && aktifTeneffusProfil != null)
                        {
                            TeneffusMuzikDevamEtGecikme();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // SES SEÇİM DEĞİŞİKLİKLERİ
        // ═══════════════════════════════════════════════════════════════
        private void cmbZilSesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZilSesi.SelectedItem != null)
            {
                var item = (SesItem)cmbZilSesi.SelectedItem;
                seciliZilSesi = Path.Combine(Application.StartupPath, "ZilSesleri", item.DosyaAdi);
                AyarlariKaydet();
            }
        }

        private void cmbAlarmSesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlarmSesi.SelectedItem != null)
            {
                var item = (SesItem)cmbAlarmSesi.SelectedItem;
                seciliAlarmSesi = Path.Combine(Application.StartupPath, "AlarmSesleri", item.DosyaAdi);
                AyarlariKaydet();
            }
        }

        private void cmbMarsSesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarsSesi.SelectedItem != null)
            {
                var item = (SesItem)cmbMarsSesi.SelectedItem;
                seciliMarsSesi = Path.Combine(Application.StartupPath, "Marslar", item.DosyaAdi);
                AyarlariKaydet();
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // SES SEVİYESİ
        // ═══════════════════════════════════════════════════════════════
        private void trackVolume_Scroll(object sender, EventArgs e)
        {
            sesSeviyes = trackVolume.Value;
            SesSeviyesiAyarla();
            lblVolume.Text = $"🔊 {sesSeviyes}%";
            AyarlariKaydet();
        }

        private void SesSeviyesiAyarla()
        {
            try
            {
                wmp.settings.volume = sesSeviyes;
                marsWmp.settings.volume = sesSeviyes;
                alarmWmp.settings.volume = sesSeviyes;
                testWmp.settings.volume = sesSeviyes;
                onizlemeWmp.settings.volume = sesSeviyes;
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════
        // ZİL DEVRE DIŞI
        // ═══════════════════════════════════════════════════════════════
        private void chkZilDevreDisi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZilDevreDisi.Checked)
            {
                lblDurum.Text = "🔇 Ziller devre dışı";
                chkZilDevreDisi.ForeColor = Color.FromArgb(239, 68, 68);
                chkZilDevreDisi.BackColor = Color.FromArgb(254, 226, 226);
            }
            else
            {
                lblDurum.Text = "🔔 Ziller aktif";
                chkZilDevreDisi.ForeColor = karanlikMod ? Color.FromArgb(237, 242, 247) : Color.FromArgb(45, 55, 72);
                chkZilDevreDisi.BackColor = karanlikMod ? Color.FromArgb(45, 55, 72) : Color.FromArgb(241, 245, 249);
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // TAKVİM KONTROL
        // ═══════════════════════════════════════════════════════════════
        private void TakvimKontrol()
        {
            try
            {
                DateTime bugun = DateTime.Today;
                var bugunKaydi = takvimGunleri.FirstOrDefault(t => t.Tarih.Date == bugun);

                if (bugunKaydi != null)
                {
                    if (bugunKaydi.ZilDevreDisi)
                        chkZilDevreDisi.Checked = true;

                    if (!string.IsNullOrEmpty(bugunKaydi.ProfilAdi) && profiller.ContainsKey(bugunKaydi.ProfilAdi))
                    {
                        aktifProfil = bugunKaydi.ProfilAdi;
                        sonListeIcerigi = "";
                        ProfilKartlariniGuncelle();
                        GununPrograminiGoster();
                    }

                    string aciklama = bugunKaydi.Aciklama ?? "Özel gün";
                    lblDurum.Text = $"📅 {aciklama}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TakvimKontrol Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // GÜNLÜK PROFİL SEÇİMİ
        // ═══════════════════════════════════════════════════════════════
        private void GunlukProfilSec()
        {
            try
            {
                // Önce takvim kayıtlarına bak
                var bugunKaydi = takvimGunleri.FirstOrDefault(t => t.Tarih.Date == DateTime.Today);
                if (bugunKaydi != null && !string.IsNullOrEmpty(bugunKaydi.ProfilAdi) && profiller.ContainsKey(bugunKaydi.ProfilAdi))
                {
                    aktifProfil = bugunKaydi.ProfilAdi;
                    return;
                }

                // Kurallara bak
                DayOfWeek gun = DateTime.Now.DayOfWeek;
                int gunIndex = -1;

                switch (gun)
                {
                    case DayOfWeek.Monday: gunIndex = 0; break;
                    case DayOfWeek.Tuesday: gunIndex = 1; break;
                    case DayOfWeek.Wednesday: gunIndex = 2; break;
                    case DayOfWeek.Thursday: gunIndex = 3; break;
                    case DayOfWeek.Friday: gunIndex = 4; break;
                    case DayOfWeek.Saturday: gunIndex = 5; break;
                    case DayOfWeek.Sunday: gunIndex = 6; break;
                }

                var kural = takvimKurallari.FirstOrDefault(k => k.GunIndex == gunIndex);
                if (kural != null && profiller.ContainsKey(kural.ProfilAdi))
                {
                    aktifProfil = kural.ProfilAdi;
                    return;
                }

                // Varsayılan mantık
                if (gun == DayOfWeek.Saturday || gun == DayOfWeek.Sunday)
                {
                    if (profiller.ContainsKey("HaftaSonu"))
                        aktifProfil = "HaftaSonu";
                    else
                        aktifProfil = "NormalGun";
                }
                else if (gun == DayOfWeek.Monday || gun == DayOfWeek.Thursday)
                {
                    if (profiller.ContainsKey("OgleOkulu"))
                        aktifProfil = "OgleOkulu";
                    else
                        aktifProfil = "NormalGun";
                }
                else
                {
                    aktifProfil = "NormalGun";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GunlukProfilSec Hata: {ex.Message}");
                aktifProfil = "NormalGun";
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // DİĞER SESLERİ DURDUR
        // ═══════════════════════════════════════════════════════════════
        private void DigerSesleriDurdur(string aktifSes)
        {
            try
            {
                if (aktifSes != "alarm" && alarmCaliniyor)
                {
                    alarmWmp.controls.stop();
                    alarmBitisTimer.Stop();
                    alarmCaliniyor = false;
                    btnAlarm.Text = "🚨 ACİL ALARM";
                    btnAlarm.BackColor = Color.FromArgb(239, 68, 68);
                }

                if (aktifSes != "mars" && marsCaliniyor)
                {
                    marsWmp.controls.stop();
                    marsBitisTimer.Stop();
                    marsCaliniyor = false;
                    btnMarsBaslat.Text = "🎵 MARŞ";
                    btnMarsBaslat.BackColor = Color.FromArgb(34, 197, 94);
                }

                if (aktifSes != "test" && (testZilCaliniyor || testAlarmCaliniyor || testMarsCaliniyor))
                {
                    testWmp.controls.stop();
                    testBitisTimer.Stop();
                    testZilCaliniyor = false;
                    testAlarmCaliniyor = false;
                    testMarsCaliniyor = false;
                    btnZilTest.Text = "▶ Test";
                    btnAlarmTest.Text = "▶ Test";
                    btnMarsTest.Text = "▶ Test";
                    btnZilTest.BackColor = Color.FromArgb(59, 130, 246);
                    btnAlarmTest.BackColor = Color.FromArgb(239, 68, 68);
                    btnMarsTest.BackColor = Color.FromArgb(34, 197, 94);
                }

                if (aktifSes != "zil" && zilCaliniyor)
                {
                    wmp.controls.stop();
                    zilBitisTimer.Stop();
                    zilCaliniyor = false;
                }

                if (aktifSes != "onizleme" && onizlemeCaliniyor)
                {
                    onizlemeWmp.controls.stop();
                    onizlemeBitisTimer.Stop();
                    onizlemeCaliniyor = false;
                }

                if (aktifSes != "teneffus" && teneffusMuzikCaliniyor)
                {
                    TeneffusMuzikDuraklat();
                }
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════
        // VARSAYILAN PROFİLLER
        // ═══════════════════════════════════════════════════════════════
        private void VarsayilanProfilleriOlustur()
        {
            if (!profiller.ContainsKey("NormalGun"))
            {
                profiller["NormalGun"] = new List<ZamanItem>
                {
                    new ZamanItem { Saat = "07:50", Ders = "", Aciklama = "Okul Açılışı" },
                    new ZamanItem { Saat = "07:55", Ders = "1", Aciklama = "1. Ders Başlangıç" },
                    new ZamanItem { Saat = "08:35", Ders = "1", Aciklama = "1. Ders Bitiş" },
                    new ZamanItem { Saat = "08:45", Ders = "2", Aciklama = "2. Ders Başlangıç" },
                    new ZamanItem { Saat = "09:25", Ders = "2", Aciklama = "2. Ders Bitiş" },
                    new ZamanItem { Saat = "09:40", Ders = "3", Aciklama = "3. Ders Başlangıç" },
                    new ZamanItem { Saat = "10:20", Ders = "3", Aciklama = "3. Ders Bitiş" },
                    new ZamanItem { Saat = "10:30", Ders = "4", Aciklama = "4. Ders Başlangıç" },
                    new ZamanItem { Saat = "11:10", Ders = "4", Aciklama = "4. Ders Bitiş" },
                    new ZamanItem { Saat = "11:20", Ders = "5", Aciklama = "5. Ders Başlangıç" },
                    new ZamanItem { Saat = "12:00", Ders = "5", Aciklama = "5. Ders Bitiş" },
                    new ZamanItem { Saat = "12:10", Ders = "6", Aciklama = "6. Ders Başlangıç" },
                    new ZamanItem { Saat = "12:50", Ders = "6", Aciklama = "6. Ders Bitiş" },
                    new ZamanItem { Saat = "13:00", Ders = "", Aciklama = "Okul Kapanışı" }
                };
            }

            if (!profiller.ContainsKey("OgleOkulu"))
            {
                profiller["OgleOkulu"] = new List<ZamanItem>
                {
                    new ZamanItem { Saat = "13:30", Ders = "", Aciklama = "Öğle Okulu Açılış" },
                    new ZamanItem { Saat = "13:35", Ders = "1", Aciklama = "1. Ders Başlangıç" },
                    new ZamanItem { Saat = "14:15", Ders = "1", Aciklama = "1. Ders Bitiş" },
                    new ZamanItem { Saat = "14:25", Ders = "2", Aciklama = "2. Ders Başlangıç" },
                    new ZamanItem { Saat = "15:05", Ders = "2", Aciklama = "2. Ders Bitiş" },
                    new ZamanItem { Saat = "15:15", Ders = "3", Aciklama = "3. Ders Başlangıç" },
                    new ZamanItem { Saat = "15:55", Ders = "3", Aciklama = "3. Ders Bitiş" },
                    new ZamanItem { Saat = "16:00", Ders = "", Aciklama = "Öğle Okulu Kapanış" }
                };
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // TENEFFÜS MÜZİK SİSTEMİ
        // ═══════════════════════════════════════════════════════════════
        private void TeneffusTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (teneffusProfilleri == null || teneffusProfilleri.Count == 0)
                    return;

                TeneffusSaatKontrol();
                TeneffusMuzikDurumKontrol();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Teneffüs Timer Hata: {ex.Message}");
            }
        }

        private void TeneffusSaatKontrol()
        {
            try
            {
                string simdikiSaat = DateTime.Now.ToString("HH:mm");
                DayOfWeek bugun = DateTime.Now.DayOfWeek;
                int gunNumarasi = (int)bugun;

                TeneffusMuzikProfili uygunProfil = null;

                foreach (var profil in teneffusProfilleri)
                {
                    if (!profil.Aktif) continue;
                    if (profil.Gun != gunNumarasi) continue;
                    if (profil.MuzikDosyalari == null || profil.MuzikDosyalari.Count == 0) continue;

                    if (string.Compare(profil.BaslangicSaat, simdikiSaat) <= 0 &&
                        string.Compare(simdikiSaat, profil.BitisSaat) < 0)
                    {
                        uygunProfil = profil;
                        break;
                    }
                }

                if (uygunProfil == null && aktifTeneffusProfil != null)
                {
                    TeneffusMuzikDurdur(true);
                    aktifTeneffusProfil = null;
                    lblDurum.Text = "✓ Teneffüs müziği tamamlandı";
                }
                else if (uygunProfil != null && aktifTeneffusProfil != uygunProfil)
                {
                    aktifTeneffusProfil = uygunProfil;
                    teneffusMuzikIndex = 0;
                    teneffusDuraklatildi = false;
                    teneffusDuraklatmaKonumu = 0;

                    teneffusBitisSaati = DateTime.Today.Add(TimeSpan.Parse(uygunProfil.BitisSaat));

                    lblDurum.Text = $"🎵 Teneffüs müziği başlıyor ({uygunProfil.ProfilAdi})";

                    if (!ZilCaliyor())
                    {
                        SiradakiTeneffusMuzikCal();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TeneffusSaatKontrol Hata: {ex.Message}");
            }
        }

        private void TeneffusMuzikDurumKontrol()
        {
            try
            {
                if (aktifTeneffusProfil == null) return;

                bool zilAktif = ZilCaliyor();

                if (zilAktif)
                {
                    if (teneffusMuzikCaliniyor && !teneffusDuraklatildi)
                    {
                        TeneffusMuzikDuraklat();
                    }
                }
                else
                {
                    if (teneffusDuraklatildi)
                    {
                        TeneffusMuzikDevamEt();
                    }
                    else if (!teneffusMuzikCaliniyor)
                    {
                        SiradakiTeneffusMuzikCal();
                    }
                    else
                    {
                        // Müzik bitiş kontrolü
                        bool muzikBitti = false;

                        try
                        {
                            if (teneffusPlayer.playState == WMPPlayState.wmppsStopped ||
                                teneffusPlayer.playState == WMPPlayState.wmppsMediaEnded)
                            {
                                muzikBitti = true;
                            }
                            else if (teneffusPlayer.currentMedia != null)
                            {
                                double currentPosition = teneffusPlayer.controls.currentPosition;
                                double duration = teneffusPlayer.currentMedia.duration;

                                if (duration > 0 && currentPosition >= (duration * 0.98))
                                {
                                    muzikBitti = true;
                                }
                            }
                        }
                        catch
                        {
                            if (teneffusPlayer.playState == WMPPlayState.wmppsStopped ||
                                teneffusPlayer.playState == WMPPlayState.wmppsMediaEnded)
                            {
                                muzikBitti = true;
                            }
                        }

                        if (muzikBitti)
                        {
                            teneffusMuzikIndex++;
                            if (teneffusMuzikIndex >= aktifTeneffusProfil.MuzikDosyalari.Count)
                            {
                                teneffusMuzikIndex = 0;
                            }
                            SiradakiTeneffusMuzikCal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TeneffusMuzikDurumKontrol Hata: {ex.Message}");
            }
        }

        private bool ZilCaliyor()
        {
            return zilCaliniyor || marsCaliniyor || alarmCaliniyor ||
                   testZilCaliniyor || testAlarmCaliniyor || testMarsCaliniyor;
        }

        private void SiradakiTeneffusMuzikCal()
        {
            try
            {
                // Profil kontrolü
                if (aktifTeneffusProfil == null)
                {
                    System.Diagnostics.Debug.WriteLine("[Teneffüs] Profil yok!");
                    return;
                }

                // Liste boş kontrolü
                if (aktifTeneffusProfil.MuzikDosyalari == null ||
                    aktifTeneffusProfil.MuzikDosyalari.Count == 0)
                {
                    lblDurum.Text = "⚠️ Teneffüs müzik listesi boş!";
                    TeneffusMuzikDurdur(true);
                    return;
                }

                // Index sınır kontrolü
                if (teneffusMuzikIndex < 0 || teneffusMuzikIndex >= aktifTeneffusProfil.MuzikDosyalari.Count)
                {
                    teneffusMuzikIndex = 0;
                }

                // Bitiş saati kontrolü
                if (DateTime.Now >= teneffusBitisSaati)
                {
                    TeneffusMuzikDurdur(true);
                    aktifTeneffusProfil = null;
                    lblDurum.Text = "✓ Teneffüs müziği tamamlandı";
                    return;
                }

                // Zil çalıyorsa bekle
                if (ZilCaliyor())
                {
                    return;
                }

                string muzikAdi = aktifTeneffusProfil.MuzikDosyalari[teneffusMuzikIndex];

                string muzikYolu = Path.Combine(
                    Application.StartupPath,
                    "TeneffusMuzikleri",
                    Path.GetFileName(muzikAdi)
                );

                // Dosya var mı kontrolü
                if (!File.Exists(muzikYolu))
                {
                    lblDurum.Text = $"⚠️ Müzik bulunamadı: {muzikAdi}";

                    teneffusMuzikIndex++;
                    if (teneffusMuzikIndex >= aktifTeneffusProfil.MuzikDosyalari.Count)
                    {
                        teneffusMuzikIndex = 0;
                    }

                    Timer retryTimer = new Timer { Interval = 200 };
                    retryTimer.Tick += (s, ev) =>
                    {
                        retryTimer.Stop();
                        retryTimer.Dispose();
                        SiradakiTeneffusMuzikCal();
                    };
                    retryTimer.Start();
                    return;
                }

                // Müziği çal
                teneffusPlayer.URL = muzikYolu;
                teneffusPlayer.settings.volume = aktifTeneffusProfil.SesSeviyesi;
                teneffusPlayer.controls.play();

                teneffusMuzikCaliniyor = true;
                teneffusDuraklatildi = false;
                aktifTeneffusMuzikDosyasi = muzikAdi;

                int siradakiIndex = teneffusMuzikIndex + 1;
                int toplam = aktifTeneffusProfil.MuzikDosyalari.Count;
                string muzikBaslik = Path.GetFileNameWithoutExtension(muzikAdi);

                lblDurum.Text = $"🎵 [{siradakiIndex}/{toplam}] {muzikBaslik}";

                System.Diagnostics.Debug.WriteLine($"[Teneffüs] Çalıyor: {muzikBaslik} (Index: {teneffusMuzikIndex})");
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                string dosyaAdi = aktifTeneffusProfil.MuzikDosyalari[teneffusMuzikIndex];
                lblDurum.Text = $"⚠️ Codec hatası: {Path.GetFileNameWithoutExtension(dosyaAdi)}";

                System.Diagnostics.Debug.WriteLine($"[Teneffüs] COM Hatası: {comEx.Message}");

                teneffusMuzikIndex++;
                if (teneffusMuzikIndex >= aktifTeneffusProfil.MuzikDosyalari.Count)
                {
                    teneffusMuzikIndex = 0;
                }

                Timer retryTimer = new Timer { Interval = 300 };
                retryTimer.Tick += (s, ev) =>
                {
                    retryTimer.Stop();
                    retryTimer.Dispose();
                    SiradakiTeneffusMuzikCal();
                };
                retryTimer.Start();
            }
            catch (Exception ex)
            {
                lblDurum.Text = $"❌ Teneffüs müzik hatası: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"[Teneffüs] Genel Hata: {ex}");

                TeneffusMuzikDurdur(true);
                teneffusMuzikCaliniyor = false;
            }
        }

        private void TeneffusMuzikDuraklat()
        {
            if (!teneffusMuzikCaliniyor) return;

            try
            {
                teneffusDuraklatmaKonumu = teneffusPlayer.controls.currentPosition;
                teneffusPlayer.controls.pause();
                teneffusDuraklatildi = true;
                teneffusMuzikCaliniyor = false;

                lblDurum.Text = "⏸️ Müzik duraklatıldı (Zil çalıyor)";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TeneffusMuzikDuraklat Hata: {ex.Message}");
            }
        }

        private void TeneffusMuzikDevamEt()
        {
            if (!teneffusDuraklatildi) return;
            if (aktifTeneffusProfil == null) return;

            try
            {
                if (DateTime.Now >= teneffusBitisSaati)
                {
                    TeneffusMuzikDurdur(true);
                    return;
                }

                teneffusPlayer.controls.currentPosition = teneffusDuraklatmaKonumu;
                teneffusPlayer.controls.play();

                teneffusDuraklatildi = false;
                teneffusMuzikCaliniyor = true;

                string muzikBaslik = Path.GetFileNameWithoutExtension(aktifTeneffusMuzikDosyasi);
                lblDurum.Text = $"▶️ Müzik devam ediyor: {muzikBaslik}";
            }
            catch
            {
                teneffusDuraklatildi = false;
                SiradakiTeneffusMuzikCal();
            }
        }

        private void TeneffusMuzikDevamEtGecikme()
        {
            Timer devamTimer = new Timer();
            devamTimer.Interval = 500;
            devamTimer.Tick += (s, ev) =>
            {
                devamTimer.Stop();
                devamTimer.Dispose();

                if (teneffusDuraklatildi && !ZilCaliyor() && aktifTeneffusProfil != null)
                {
                    TeneffusMuzikDevamEt();
                }
            };
            devamTimer.Start();
        }

        private void TeneffusMuzikDurdur(bool tamamenDurdur)
        {
            try
            {
                teneffusPlayer.controls.stop();
                teneffusMuzikCaliniyor = false;

                if (tamamenDurdur)
                {
                    teneffusDuraklatildi = false;
                    teneffusDuraklatmaKonumu = 0;
                    teneffusMuzikIndex = 0;
                    aktifTeneffusMuzikDosyasi = "";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TeneffusMuzikDurdur Hata: {ex.Message}");
            }
        }

        private void btnTeneffusMuzik_Click(object sender, EventArgs e)
        {
            try
            {
                TeneffusMuzikForm frm = new TeneffusMuzikForm(teneffusProfilleri);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    teneffusProfilleri = frm.Profiller;
                    AyarlariKaydet();
                    lblDurum.Text = "🎵 Teneffüs müzik ayarları güncellendi";
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // HAKKINDA BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnHakkinda_Click(object sender, EventArgs e)
        {
            try
            {
                using (HakkindaForm hakkinda = new HakkindaForm())
                {
                    hakkinda.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                lblDurum.Text = "⚠️ Hata: " + ex.Message;
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // SES YENİLE BUTONU
        // ═══════════════════════════════════════════════════════════════
        private void btnSesYenile_Click(object sender, EventArgs e)
        {
            try
            {
                btnSesYenile.Text = "🔄 Yenileniyor...";
                btnSesYenile.Enabled = false;
                lblDurum.Text = "⏳ Dosyalar taranıyor...";
                Application.DoEvents();

                SesDosyalariniYukle();

                txtSesArama.Text = "🔍 Ses dosyası ara...";
                txtSesArama.ForeColor = Color.Gray;

                SesListeleriniGuncelle();
                SesSecimleriniYap();

                lblDurum.Text = $"✅ {zilSesleri.Count + alarmSesleri.Count + marsSesleri.Count} dosya yüklendi";

                btnSesYenile.Text = "🔄 Yenile";
                btnSesYenile.Enabled = true;
            }
            catch (Exception ex)
            {
                lblDurum.Text = "❌ Yenileme hatası!";
                btnSesYenile.Text = "🔄 Yenile";
                btnSesYenile.Enabled = true;

                MessageBox.Show($"Hata: {ex.Message}", "Yenileme Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // LİSTE ÇİZİM METODU (CUSTOM DRAW)
        // ═══════════════════════════════════════════════════════════════
        private void lstGununProgrami_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;

                ListBox lb = (ListBox)sender;
                if (lb.Items.Count == 0 || e.Index >= lb.Items.Count) return;

                string itemText = lb.Items[e.Index].ToString();

                e.DrawBackground();

                Color bgColor = karanlikMod ? Color.FromArgb(45, 55, 72) : Color.White;
                Color textColor;
                FontStyle fontStyle = FontStyle.Regular;

                if (itemText.Contains("▶"))
                {
                    bgColor = karanlikMod ?
                        Color.FromArgb(88, 80, 236) :
                        Color.FromArgb(237, 233, 254);
                    textColor = karanlikMod ?
                        Color.White :
                        Color.FromArgb(99, 102, 241);
                    fontStyle = FontStyle.Bold;
                }
                else if (itemText.Contains("✓"))
                {
                    textColor = karanlikMod ?
                        Color.FromArgb(148, 163, 184) :
                        Color.FromArgb(156, 163, 175);
                }
                else
                {
                    textColor = karanlikMod ?
                        Color.FromArgb(226, 232, 240) :
                        Color.FromArgb(45, 55, 72);
                }

                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                }

                using (Font font = new Font("Segoe UI", 14F, fontStyle))
                using (SolidBrush textBrush = new SolidBrush(textColor))
                using (StringFormat sf = new StringFormat())
                {
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;

                    Rectangle textRect = new Rectangle(
                        e.Bounds.X + 10,
                        e.Bounds.Y,
                        e.Bounds.Width - 20,
                        e.Bounds.Height
                    );

                    e.Graphics.DrawString(itemText, font, textBrush, textRect, sf);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    using (Pen pen = new Pen(Color.FromArgb(99, 102, 241), 2))
                    {
                        e.Graphics.DrawRectangle(pen, e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 3, e.Bounds.Height - 3);
                    }
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"lstGununProgrami_DrawItem Hata: {ex.Message}");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // AYARLARI YÜKLE
        // ═══════════════════════════════════════════════════════════════
        private void AyarlariYukle()
        {
            string ayarDosya = Path.Combine(Application.StartupPath, "Ayarlar.txt");
            string profilDosya = Path.Combine(Application.StartupPath, "Profiller.txt");
            string takvimDosya = Path.Combine(Application.StartupPath, "Takvim.txt");
            string teneffusDosya = Path.Combine(Application.StartupPath, "TeneffusMuzik.txt");
            string kuralDosya = Path.Combine(Application.StartupPath, "TakvimKurallari.txt");

            // Ayarlar dosyasını yükle
            if (File.Exists(ayarDosya))
            {
                try
                {
                    foreach (string satir in File.ReadAllLines(ayarDosya))
                    {
                        if (string.IsNullOrEmpty(satir)) continue;

                        if (satir.StartsWith("AktifProfil="))
                        {
                            string profil = satir.Substring(12);
                            if (profiller.ContainsKey(profil)) aktifProfil = profil;
                        }
                        else if (satir.StartsWith("ZilSesi="))
                        {
                            string ses = satir.Substring(8);
                            if (zilSesleri.Contains(ses))
                                seciliZilSesi = Path.Combine(Application.StartupPath, "ZilSesleri", ses);
                        }
                        else if (satir.StartsWith("AlarmSesi="))
                        {
                            string ses = satir.Substring(10);
                            if (alarmSesleri.Contains(ses))
                                seciliAlarmSesi = Path.Combine(Application.StartupPath, "AlarmSesleri", ses);
                        }
                        else if (satir.StartsWith("MarsSesi="))
                        {
                            string ses = satir.Substring(9);
                            if (marsSesleri.Contains(ses))
                                seciliMarsSesi = Path.Combine(Application.StartupPath, "Marslar", ses);
                        }
                        else if (satir.StartsWith("KaranlikMod="))
                        {
                            karanlikMod = satir.Substring(12) == "1";
                            chkKaranlikMod.Checked = karanlikMod;
                        }
                        else if (satir.StartsWith("SesSeviyes="))
                        {
                            int.TryParse(satir.Substring(11), out sesSeviyes);
                            if (sesSeviyes < 0) sesSeviyes = 0;
                            if (sesSeviyes > 100) sesSeviyes = 100;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ayar yükleme hatası: {ex.Message}");
                }
            }

            // Profil dosyasını yükle
            if (File.Exists(profilDosya))
            {
                try
                {
                    string[] satirlar = File.ReadAllLines(profilDosya);
                    string mevcutProfil = "";
                    List<ZamanItem> mevcutZamanlar = null;

                    foreach (string satir in satirlar)
                    {
                        if (string.IsNullOrEmpty(satir)) continue;

                        if (satir.StartsWith("[PROFIL:"))
                        {
                            if (!string.IsNullOrEmpty(mevcutProfil) && mevcutZamanlar != null)
                                profiller[mevcutProfil] = mevcutZamanlar;

                            mevcutProfil = satir.Substring(8, satir.Length - 9);
                            mevcutZamanlar = new List<ZamanItem>();
                        }
                        else if (satir.Contains("=") && mevcutZamanlar != null)
                        {
                            string[] parcalar = satir.Split(new char[] { '=' }, 3);
                            if (parcalar.Length >= 3)
                            {
                                mevcutZamanlar.Add(new ZamanItem
                                {
                                    Saat = parcalar[0],
                                    Ders = parcalar[1],
                                    Aciklama = parcalar[2]
                                });
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(mevcutProfil) && mevcutZamanlar != null)
                        profiller[mevcutProfil] = mevcutZamanlar;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Profil yükleme hatası: {ex.Message}");
                }
            }

            // Takvim dosyasını yükle
            if (File.Exists(takvimDosya))
            {
                try
                {
                    takvimGunleri.Clear();
                    foreach (string satir in File.ReadAllLines(takvimDosya))
                    {
                        if (string.IsNullOrEmpty(satir)) continue;

                        string[] parcalar = satir.Split('|');
                        if (parcalar.Length >= 4)
                        {
                            DateTime tarih;
                            if (DateTime.TryParse(parcalar[0], out tarih))
                            {
                                takvimGunleri.Add(new TakvimGunu
                                {
                                    Tarih = tarih,
                                    ProfilAdi = parcalar[1],
                                    Aciklama = parcalar[2],
                                    ZilDevreDisi = parcalar[3] == "1"
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Takvim yükleme hatası: {ex.Message}");
                }
            }

            // Teneffüs müzik dosyasını yükle
            if (File.Exists(teneffusDosya))
            {
                try
                {
                    teneffusProfilleri.Clear();
                    string[] satirlar = File.ReadAllLines(teneffusDosya);

                    TeneffusMuzikProfili mevcutProfil = null;

                    foreach (string satir in satirlar)
                    {
                        if (string.IsNullOrEmpty(satir)) continue;

                        if (satir.StartsWith("[PROFIL:"))
                        {
                            if (mevcutProfil != null)
                                teneffusProfilleri.Add(mevcutProfil);

                            string profilAdi = satir.Substring(8, satir.Length - 9);
                            mevcutProfil = new TeneffusMuzikProfili { ProfilAdi = profilAdi };
                        }
                        else if (satir.StartsWith("Aktif=") && mevcutProfil != null)
                        {
                            mevcutProfil.Aktif = satir.Substring(6) == "1";
                        }
                        else if (satir.StartsWith("Gun=") && mevcutProfil != null)
                        {
                            int gun;
                            if (int.TryParse(satir.Substring(4), out gun))
                                mevcutProfil.Gun = gun;
                        }
                        else if (satir.StartsWith("Baslangic=") && mevcutProfil != null)
                        {
                            mevcutProfil.BaslangicSaat = satir.Substring(10);
                        }
                        else if (satir.StartsWith("Bitis=") && mevcutProfil != null)
                        {
                            mevcutProfil.BitisSaat = satir.Substring(6);
                        }
                        else if (satir.StartsWith("Ses=") && mevcutProfil != null)
                        {
                            int ses;
                            if (int.TryParse(satir.Substring(4), out ses))
                                mevcutProfil.SesSeviyesi = ses;
                        }
                        else if (satir.StartsWith("Muzik=") && mevcutProfil != null)
                        {
                            mevcutProfil.MuzikDosyalari.Add(satir.Substring(6));
                        }
                    }

                    if (mevcutProfil != null)
                        teneffusProfilleri.Add(mevcutProfil);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Teneffüs yükleme hatası: {ex.Message}");
                }
            }

            // Takvim kurallarını yükle
            if (File.Exists(kuralDosya))
            {
                try
                {
                    takvimKurallari.Clear();
                    foreach (string satir in File.ReadAllLines(kuralDosya))
                    {
                        if (string.IsNullOrEmpty(satir)) continue;

                        string[] parcalar = satir.Split('|');
                        if (parcalar.Length >= 2)
                        {
                            int gunIndex;
                            if (int.TryParse(parcalar[0], out gunIndex))
                            {
                                takvimKurallari.Add(new TakvimKural
                                {
                                    GunIndex = gunIndex,
                                    ProfilAdi = parcalar[1]
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Kural yükleme hatası: {ex.Message}");
                }
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // AYARLARI KAYDET
        // ═══════════════════════════════════════════════════════════════
        private void AyarlariKaydet()
        {
            try
            {
                // Ana ayarları kaydet
                string ayarDosya = Path.Combine(Application.StartupPath, "Ayarlar.txt");
                List<string> satirlar = new List<string>
                {
                    $"AktifProfil={aktifProfil}",
                    $"KaranlikMod={(karanlikMod ? "1" : "0")}",
                    $"SesSeviyes={sesSeviyes}"
                };

                if (cmbZilSesi.SelectedItem != null)
                    satirlar.Add($"ZilSesi={((SesItem)cmbZilSesi.SelectedItem).DosyaAdi}");
                if (cmbAlarmSesi.SelectedItem != null)
                    satirlar.Add($"AlarmSesi={((SesItem)cmbAlarmSesi.SelectedItem).DosyaAdi}");
                if (cmbMarsSesi.SelectedItem != null)
                    satirlar.Add($"MarsSesi={((SesItem)cmbMarsSesi.SelectedItem).DosyaAdi}");

                File.WriteAllLines(ayarDosya, satirlar);

                // Profilleri kaydet
                string profilDosya = Path.Combine(Application.StartupPath, "Profiller.txt");
                List<string> profilSatirlari = new List<string>();
                foreach (var kvp in profiller)
                {
                    profilSatirlari.Add($"[PROFIL:{kvp.Key}]");
                    if (kvp.Value != null)
                    {
                        foreach (var zaman in kvp.Value)
                        {
                            if (zaman != null)
                            {
                                string saat = zaman.Saat ?? "";
                                string ders = zaman.Ders ?? "";
                                string aciklama = zaman.Aciklama ?? "";
                                profilSatirlari.Add($"{saat}={ders}={aciklama}");
                            }
                        }
                    }
                }
                File.WriteAllLines(profilDosya, profilSatirlari);

                // Takvimi kaydet
                string takvimDosya = Path.Combine(Application.StartupPath, "Takvim.txt");
                List<string> takvimSatirlari = new List<string>();
                foreach (var gun in takvimGunleri)
                {
                    if (gun != null)
                    {
                        string profilAdi = gun.ProfilAdi ?? "";
                        string aciklama = gun.Aciklama ?? "";
                        takvimSatirlari.Add($"{gun.Tarih:yyyy-MM-dd}|{profilAdi}|{aciklama}|{(gun.ZilDevreDisi ? "1" : "0")}");
                    }
                }
                File.WriteAllLines(takvimDosya, takvimSatirlari);

                // Teneffüs müzik ayarlarını kaydet
                string teneffusDosya = Path.Combine(Application.StartupPath, "TeneffusMuzik.txt");
                List<string> teneffusSatirlari = new List<string>();
                foreach (var profil in teneffusProfilleri)
                {
                    if (profil != null)
                    {
                        teneffusSatirlari.Add($"[PROFIL:{profil.ProfilAdi ?? ""}]");
                        teneffusSatirlari.Add($"Aktif={(profil.Aktif ? "1" : "0")}");
                        teneffusSatirlari.Add($"Gun={profil.Gun}");
                        teneffusSatirlari.Add($"Baslangic={profil.BaslangicSaat ?? ""}");
                        teneffusSatirlari.Add($"Bitis={profil.BitisSaat ?? ""}");
                        teneffusSatirlari.Add($"Ses={profil.SesSeviyesi}");

                        if (profil.MuzikDosyalari != null)
                        {
                            foreach (var muzik in profil.MuzikDosyalari)
                            {
                                if (!string.IsNullOrEmpty(muzik))
                                    teneffusSatirlari.Add($"Muzik={muzik}");
                            }
                        }
                    }
                }
                File.WriteAllLines(teneffusDosya, teneffusSatirlari);

                // Takvim kurallarını kaydet
                string kuralDosya = Path.Combine(Application.StartupPath, "TakvimKurallari.txt");
                List<string> kuralSatirlari = new List<string>();
                foreach (var kural in takvimKurallari)
                {
                    if (kural != null)
                    {
                        string profilAdi = kural.ProfilAdi ?? "";
                        kuralSatirlari.Add($"{kural.GunIndex}|{profilAdi}");
                    }
                }
                File.WriteAllLines(kuralDosya, kuralSatirlari);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Kaydetme hatası: {ex.Message}");
            }
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // YARDIMCI SINIFLAR
    // ═══════════════════════════════════════════════════════════════

    public class ZamanItem
    {
        public string Saat { get; set; }
        public string Ders { get; set; }
        public string Aciklama { get; set; }
    }

    public class SesItem
    {
        public string DosyaAdi { get; set; }
        public string GorunenAd { get; set; }

        public SesItem(string dosyaAdi, string gorunenAd)
        {
            DosyaAdi = dosyaAdi ?? "";
            GorunenAd = gorunenAd ?? "";
        }

        public override string ToString()
        {
            return GorunenAd;
        }
    }

    public class TakvimGunu
    {
        public DateTime Tarih { get; set; }
        public string ProfilAdi { get; set; }
        public string Aciklama { get; set; }
        public bool ZilDevreDisi { get; set; }
    }

    public class TeneffusMuzikProfili
    {
        public string ProfilAdi { get; set; } = "";
        public bool Aktif { get; set; } = true;
        public int Gun { get; set; } = 1;
        public string BaslangicSaat { get; set; } = "09:55";
        public string BitisSaat { get; set; } = "10:10";
        public int SesSeviyesi { get; set; } = 30;
        public List<string> MuzikDosyalari { get; set; } = new List<string>();
    }

    public class TakvimKural
    {
        public int GunIndex { get; set; }
        public string ProfilAdi { get; set; }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(247, 250, 252),
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label()
            {
                Left = 20,
                Top = 20,
                Text = text,
                Width = 350,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            TextBox textBox = new TextBox()
            {
                Left = 20,
                Top = 50,
                Width = 350,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.FixedSingle
            };

            Button confirmation = new Button()
            {
                Text = "✓ Tamam",
                Left = 180,
                Width = 90,
                Height = 35,
                Top = 90,
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            confirmation.FlatAppearance.BorderSize = 0;

            Button cancel = new Button()
            {
                Text = "✗ İptal",
                Left = 280,
                Width = 90,
                Height = 35,
                Top = 90,
                DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(148, 163, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            cancel.FlatAppearance.BorderSize = 0;

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}