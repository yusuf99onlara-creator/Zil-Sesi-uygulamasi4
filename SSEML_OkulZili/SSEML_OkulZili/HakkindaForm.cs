using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SSEML_OkulZili
{
    public partial class HakkindaForm : Form
    {
        public HakkindaForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Özellikleri
            this.ClientSize = new Size(650, 780);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "ℹ️ Hakkında - SSEML Okul Zili";
            this.BackColor = Color.FromArgb(247, 250, 252);

            // Logo Panel
            Panel pnlLogo = new Panel
            {
                Size = new Size(610, 120),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(99, 102, 241)
            };

            Label lblLogo = new Label
            {
                Text = "🔔",
                Font = new Font("Segoe UI Emoji", 48F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(100, 80),
                Location = new Point(255, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlLogo.Controls.Add(lblLogo);

            // Başlık
            Label lblBaslik = new Label
            {
                Text = "SSEML Okul Zili Sistemi",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Size = new Size(610, 40),
                Location = new Point(20, 160),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Versiyon
            Label lblVersiyon = new Label
            {
                Text = "Versiyon 2.5.0 - Teneffüs Müzik Edition",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(113, 128, 150),
                Size = new Size(610, 25),
                Location = new Point(20, 205),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Bilgi Paneli
            Panel pnlBilgi = new Panel
            {
                Size = new Size(610, 300),
                Location = new Point(20, 245),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblOzellikler = new Label
            {
                Text = "✨ Özellikler",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Size = new Size(570, 30),
                Location = new Point(20, 15)
            };
            pnlBilgi.Controls.Add(lblOzellikler);

            string[] ozellikler = new string[]
            {
                "• Otomatik zil sistemi (Takvim desteği)",
                "• Haftalık profil kuralları",
                "• Çoklu zil profilleri",
                "• Teneffüs müzik sistemi",
                "• İstiklal Marşı çalma",
                "• Acil alarm sistemi",
                "• Karanlık mod desteği",
                "• Ses dosyası yönetimi",
                "• Sistem tray ikonu",
                "• Otomatik güncelleme"
            };

            int yPos = 55;
            foreach (string ozellik in ozellikler)
            {
                Label lblOz = new Label
                {
                    Text = ozellik,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(45, 55, 72),
                    Size = new Size(570, 22),
                    Location = new Point(20, yPos)
                };
                pnlBilgi.Controls.Add(lblOz);
                yPos += 25;
            }

            // ═══════════════════════════════════════════════════
            // ⭐ YENİ: YAPIMCI BİLGİLERİ PANELİ
            // ═══════════════════════════════════════════════════
            Panel pnlYapimci = new Panel
            {
                Size = new Size(610, 100),
                Location = new Point(20, 560),
                BackColor = Color.FromArgb(237, 233, 254),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblYapimciBaslik = new Label
            {
                Text = "👨‍💻 Geliştirici",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Size = new Size(570, 25),
                Location = new Point(20, 10)
            };
            pnlYapimci.Controls.Add(lblYapimciBaslik);

            Label lblYapimci = new Label
            {
                Text = "Yusuf Gençel",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Size = new Size(570, 25),
                Location = new Point(20, 35)
            };
            pnlYapimci.Controls.Add(lblYapimci);

            // Email LinkLabel (Tıklanabilir)
            LinkLabel lnkEmail = new LinkLabel
            {
                Text = "📧 workssyusuf@gmail.com",
                Font = new Font("Segoe UI", 11F),
                LinkColor = Color.FromArgb(99, 102, 241),
                Size = new Size(570, 25),
                Location = new Point(20, 65),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lnkEmail.LinkClicked += (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "mailto:workssyusuf@gmail.com?subject=SSEML Okul Zili - İletişim",
                        UseShellExecute = true
                    });
                }
                catch { }
            };
            pnlYapimci.Controls.Add(lnkEmail);

            // Telif Hakkı (Güncellendi)
            Label lblTelif = new Label
            {
                Text = "© 2025 Yusuf Gençel - Tüm hakları saklıdır",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Size = new Size(610, 25),
                Location = new Point(20, 675),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // GitHub Link
            LinkLabel lnkGitHub = new LinkLabel
            {
                Text = "🌐 GitHub Profili",
                Font = new Font("Segoe UI", 11F),
                LinkColor = Color.FromArgb(59, 130, 246),
                Size = new Size(200, 25),
                Location = new Point(225, 705),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lnkGitHub.LinkClicked += (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://github.com/yusuf99onlara-creator",
                        UseShellExecute = true
                    });
                }
                catch { }
            };

            // Kapat Butonu
            Button btnKapat = new Button
            {
                Text = "✓ Kapat",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(150, 45),
                Location = new Point(250, 720),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnKapat.FlatAppearance.BorderSize = 0;
            btnKapat.Click += (s, e) => this.Close();

            // Kontrolleri Ekle
            this.Controls.Add(pnlLogo);
            this.Controls.Add(lblBaslik);
            this.Controls.Add(lblVersiyon);
            this.Controls.Add(pnlBilgi);
            this.Controls.Add(pnlYapimci);
            this.Controls.Add(lblTelif);
            this.Controls.Add(lnkGitHub);
            this.Controls.Add(btnKapat);

            this.ResumeLayout(false);
        }
    }
}