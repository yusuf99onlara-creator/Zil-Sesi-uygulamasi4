using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SSEML_OkulZili
{
    public partial class ProfileManagerForm : Form
    {
        public string ProfilAdi { get; set; }
        public List<ZamanItem> ZamanListesi { get; set; }
        private bool duzenlemeModu = false;
        private string duzenlenecekSaat = ""; // Index yerine saat kullan
        private Dictionary<string, List<ZamanItem>> tumProfiller;
        private TextBox txtArama;
        private Label lblAramaSonuc;

        public ProfileManagerForm(string profilAdi, Dictionary<string, List<ZamanItem>> profiller, string zilDosyasi)
        {
            InitializeComponent();

            tumProfiller = profiller;

            if (!string.IsNullOrEmpty(profilAdi))
            {
                txtProfilAdi.Text = profilAdi;
                txtProfilAdi.ReadOnly = true;
                ProfilAdi = profilAdi;

                if (profiller.ContainsKey(profilAdi))
                {
                    // Derin kopya oluştur
                    ZamanListesi = new List<ZamanItem>();
                    foreach (var item in profiller[profilAdi])
                    {
                        ZamanListesi.Add(new ZamanItem
                        {
                            Saat = item.Saat,
                            Ders = item.Ders,
                            Aciklama = item.Aciklama
                        });
                    }
                }
                else
                {
                    ZamanListesi = new List<ZamanItem>();
                }
            }
            else
            {
                ZamanListesi = new List<ZamanItem>();
            }
        }

        private void ProfileManagerForm_Load(object sender, EventArgs e)
        {
            // btnIptal başlangıçta gizli
            btnIptal.Visible = false;

            // ARAMA KUTUSU EKLEME
            Label lblAramaBaslik = new Label();
            lblAramaBaslik.Text = "🔍 Zaman Ara:";
            lblAramaBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAramaBaslik.ForeColor = Color.FromArgb(100, 116, 139);
            lblAramaBaslik.Location = new Point(570, 82);
            lblAramaBaslik.AutoSize = true;
            this.Controls.Add(lblAramaBaslik);

            txtArama = new TextBox();
            txtArama.Font = new Font("Segoe UI", 11F);
            txtArama.Location = new Point(570, 107);
            txtArama.Size = new Size(160, 27);
            txtArama.TextChanged += TxtArama_TextChanged;
            this.Controls.Add(txtArama);

            lblAramaSonuc = new Label();
            lblAramaSonuc.Font = new Font("Segoe UI", 9F);
            lblAramaSonuc.ForeColor = Color.FromArgb(34, 197, 94);
            lblAramaSonuc.Location = new Point(570, 138);
            lblAramaSonuc.Size = new Size(160, 20);
            lblAramaSonuc.Text = "";
            this.Controls.Add(lblAramaSonuc);

            DataGridViewAyarla();
            ListeGuncelle();
            txtSaat.Text = "08:30";
        }

        private void DataGridViewAyarla()
        {
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                headerStyle.BackColor = Color.FromArgb(241, 245, 249);

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                cellStyle.Font = new Font("Segoe UI", 10F);

                DataGridViewTextBoxColumn saatColumn = new DataGridViewTextBoxColumn();
                saatColumn.Name = "Saat";
                saatColumn.HeaderText = "Saat";
                saatColumn.Width = 100;
                saatColumn.HeaderCell.Style = headerStyle;
                saatColumn.DefaultCellStyle = cellStyle;
                saatColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(saatColumn);

                DataGridViewTextBoxColumn dersColumn = new DataGridViewTextBoxColumn();
                dersColumn.Name = "Ders";
                dersColumn.HeaderText = "Ders";
                dersColumn.Width = 80;
                dersColumn.HeaderCell.Style = headerStyle;
                dersColumn.DefaultCellStyle = cellStyle;
                dersColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(dersColumn);

                DataGridViewTextBoxColumn aciklamaColumn = new DataGridViewTextBoxColumn();
                aciklamaColumn.Name = "Aciklama";
                aciklamaColumn.HeaderText = "Açıklama";
                aciklamaColumn.Width = 250;
                aciklamaColumn.HeaderCell.Style = headerStyle;
                aciklamaColumn.DefaultCellStyle = cellStyle;
                aciklamaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(aciklamaColumn);
            }

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.GridColor = Color.FromArgb(226, 232, 240);
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            AramaYap();
        }

        private void AramaYap()
        {
            string aramaMetni = txtArama.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(aramaMetni))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Visible = true;
                }
                lblAramaSonuc.Text = "";
                return;
            }

            int bulunanSayisi = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string saat = row.Cells[0].Value?.ToString().ToLower() ?? "";
                string ders = row.Cells[1].Value?.ToString().ToLower() ?? "";
                string aciklama = row.Cells[2].Value?.ToString().ToLower() ?? "";

                if (saat.Contains(aramaMetni) || ders.Contains(aramaMetni) || aciklama.Contains(aramaMetni))
                {
                    row.Visible = true;
                    bulunanSayisi++;
                }
                else
                {
                    row.Visible = false;
                }
            }

            lblAramaSonuc.Text = bulunanSayisi > 0 ?
                $"✓ {bulunanSayisi} sonuç" :
                "❌ Sonuç yok";
            lblAramaSonuc.ForeColor = bulunanSayisi > 0 ?
                Color.FromArgb(34, 197, 94) :
                Color.FromArgb(239, 68, 68);
        }

        private void ListeGuncelle()
        {
            dataGridView1.Rows.Clear();

            var siraliListe = ZamanListesi.OrderBy(x => x.Saat.Replace(":", "")).ToList();

            foreach (ZamanItem item in siraliListe)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.Saat;
                dataGridView1.Rows[index].Cells[1].Value = item.Ders;
                dataGridView1.Rows[index].Cells[2].Value = item.Aciklama;
            }

            // Aramayı tekrar uygula
            if (txtArama != null && !string.IsNullOrWhiteSpace(txtArama.Text))
            {
                AramaYap();
            }
        }

        // ⭐ Saat ile eşleştirme - daha güvenilir
        private ZamanItem SaatIleBul(string saat)
        {
            return ZamanListesi.FirstOrDefault(x => x.Saat == saat);
        }

        private int SaatIndexBul(string saat)
        {
            for (int i = 0; i < ZamanListesi.Count; i++)
            {
                if (ZamanListesi[i].Saat == saat)
                    return i;
            }
            return -1;
        }

        private bool SaatZatenMevcut(string saat, string haricSaat = "")
        {
            foreach (var item in ZamanListesi)
            {
                if (item.Saat == saat && item.Saat != haricSaat)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // ========== HATA KONTROLÜ 1: BOŞ ALAN ==========
            if (string.IsNullOrWhiteSpace(txtSaat.Text))
            {
                MessageBox.Show(
                    "⚠️ SAAT ALANI BOŞ!\n\n" +
                    "Lütfen bir saat girin.\n\n" +
                    "Örnek: 08:30",
                    "❌ Eksik Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtSaat.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                MessageBox.Show(
                    "⚠️ AÇIKLAMA ALANI BOŞ!\n\n" +
                    "Lütfen bir açıklama girin.\n\n" +
                    "Örnek: 1. Ders Başlangıç",
                    "❌ Eksik Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtAciklama.Focus();
                return;
            }

            // ========== HATA KONTROLÜ 2: SAAT FORMATI ==========
            string saatText = txtSaat.Text.Trim();

            // Otomatik düzeltme: 8:30 -> 08:30
            if (saatText.Length == 4 && saatText[1] == ':')
            {
                saatText = "0" + saatText;
                txtSaat.Text = saatText;
            }

            DateTime gecerliSaat;
            try
            {
                gecerliSaat = DateTime.ParseExact(saatText, "HH:mm", null);
            }
            catch
            {
                MessageBox.Show(
                    "❌ GEÇERSİZ SAAT FORMATI!\n\n" +
                    "Saat formatı şu şekilde olmalıdır:\n" +
                    "• HH:mm (Örnek: 08:30)\n" +
                    "• 24 saat formatında\n" +
                    "• İki nokta üst üste ile ayrılmış\n\n" +
                    "Yazdığınız: " + txtSaat.Text + "\n\n" +
                    "Doğru örnekler:\n" +
                    "✓ 08:30\n" +
                    "✓ 13:45\n" +
                    "✓ 15:20",
                    "⚠️ Saat Formatı Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtSaat.Focus();
                txtSaat.SelectAll();
                return;
            }

            // ========== HATA KONTROLÜ 3: AYNI SAAT ==========
            string haricSaat = duzenlemeModu ? duzenlenecekSaat : "";

            if (SaatZatenMevcut(saatText, haricSaat))
            {
                MessageBox.Show(
                    "⚠️ BU SAAT ZATEN MEVCUT!\n\n" +
                    "'" + saatText + "' saati listede zaten var.\n\n" +
                    "Aynı saat birden fazla eklenemez.",
                    "❌ Tekrar Eden Saat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtSaat.Focus();
                txtSaat.SelectAll();
                return;
            }

            // ========== HATA KONTROLÜ 4: DERS NUMARASI ==========
            if (!string.IsNullOrWhiteSpace(txtDers.Text))
            {
                int dersNo;
                if (!int.TryParse(txtDers.Text.Trim(), out dersNo) || dersNo < 1 || dersNo > 12)
                {
                    MessageBox.Show(
                        "⚠️ GEÇERSİZ DERS NUMARASI!\n\n" +
                        "Ders numarası 1 ile 12 arasında olmalı.\n\n" +
                        "Not: Ders numarası zorunlu değil, boş bırakabilirsiniz.",
                        "⚠️ Ders Numarası Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtDers.Focus();
                    txtDers.SelectAll();
                    return;
                }
            }

            // ========== GÜNCELLEME VEYA EKLEME ==========
            if (duzenlemeModu && !string.IsNullOrEmpty(duzenlenecekSaat))
            {
                // Mevcut öğeyi bul ve güncelle
                int index = SaatIndexBul(duzenlenecekSaat);
                if (index >= 0)
                {
                    ZamanListesi[index].Saat = saatText;
                    ZamanListesi[index].Ders = txtDers.Text.Trim();
                    ZamanListesi[index].Aciklama = txtAciklama.Text.Trim();
                }

                DuzenlemeMOduKapat();
            }
            else
            {
                ZamanItem yeniItem = new ZamanItem
                {
                    Saat = saatText,
                    Ders = txtDers.Text.Trim(),
                    Aciklama = txtAciklama.Text.Trim()
                };

                ZamanListesi.Add(yeniItem);
            }

            SiraliListeGuncelle();
            FormuTemizle();
        }

        private void DuzenlemeMOduKapat()
        {
            duzenlemeModu = false;
            duzenlenecekSaat = "";
            btnEkle.Text = "➕ EKLE";
            btnIptal.Visible = false;
        }

        private void FormuTemizle()
        {
            txtSaat.Text = "08:30";
            txtDers.Clear();
            txtAciklama.Clear();
            txtSaat.Focus();
        }

        private void SiraliListeGuncelle()
        {
            // Listeyi sırala
            ZamanListesi = ZamanListesi.OrderBy(x => x.Saat.Replace(":", "")).ToList();

            // DataGridView'i güncelle
            dataGridView1.Rows.Clear();

            foreach (ZamanItem item in ZamanListesi)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.Saat;
                dataGridView1.Rows[index].Cells[1].Value = item.Ders;
                dataGridView1.Rows[index].Cells[2].Value = item.Aciklama;
            }

            // Aramayı tekrar uygula
            if (txtArama != null && !string.IsNullOrWhiteSpace(txtArama.Text))
            {
                AramaYap();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string silinecekSaat = selectedRow.Cells[0].Value?.ToString();
                string silinecekAciklama = selectedRow.Cells[2].Value?.ToString();

                if (string.IsNullOrEmpty(silinecekSaat))
                {
                    MessageBox.Show("Geçersiz seçim!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"⚠️ SİLME ONAYI\n\n" +
                    $"Bu zamanı silmek istediğinizden emin misiniz?\n\n" +
                    $"Saat: {silinecekSaat}\n" +
                    $"Açıklama: {silinecekAciklama}\n\n" +
                    $"Bu işlem geri alınamaz!",
                    "🗑️ Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    // ⭐ Saat ile bul ve sil (index yerine)
                    int index = SaatIndexBul(silinecekSaat);
                    if (index >= 0)
                    {
                        ZamanListesi.RemoveAt(index);
                        SiraliListeGuncelle();

                        MessageBox.Show(
                            "✓ Zaman başarıyla silindi.",
                            "✓ Başarılı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "⚠️ SEÇİM YAPILMADI!\n\n" +
                    "Lütfen silmek istediğiniz zamanı seçin.\n\n" +
                    "Nasıl seçilir?\n" +
                    "• Listeden bir satıra tıklayın",
                    "❌ Seçim Gerekli",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string secilenSaat = selectedRow.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(secilenSaat))
                {
                    MessageBox.Show("Geçersiz seçim!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ⭐ Saat ile bul
                ZamanItem item = SaatIleBul(secilenSaat);
                if (item != null)
                {
                    duzenlemeModu = true;
                    duzenlenecekSaat = secilenSaat;

                    txtSaat.Text = item.Saat;
                    txtDers.Text = item.Ders;
                    txtAciklama.Text = item.Aciklama;

                    btnEkle.Text = "✓ GÜNCELLE";
                    btnIptal.Visible = true;

                    txtSaat.Focus();
                }
            }
            else
            {
                MessageBox.Show(
                    "⚠️ SEÇİM YAPILMADI!\n\n" +
                    "Lütfen düzenlemek istediğiniz zamanı seçin.\n\n" +
                    "İpucu:\n" +
                    "Bir satıra çift tıklayarak da düzenleyebilirsiniz!",
                    "❌ Seçim Gerekli",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DuzenlemeMOduKapat();
            FormuTemizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // ========== HATA KONTROLÜ 1: PROFİL ADI ==========
            if (string.IsNullOrWhiteSpace(txtProfilAdi.Text))
            {
                MessageBox.Show(
                    "❌ PROFİL ADI BOŞ!\n\n" +
                    "Lütfen profil adı girin.\n\n" +
                    "Örnek profil adları:\n" +
                    "• Normal Gün\n" +
                    "• Sınav Haftası\n" +
                    "• Kısa Günler",
                    "⚠️ Profil Adı Gerekli",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtProfilAdi.Focus();
                return;
            }

            // ========== HATA KONTROLÜ 2: PROFİL ADI BENZERSİZ ==========
            string yeniProfilAdi = txtProfilAdi.Text.Trim();

            if (tumProfiller.ContainsKey(yeniProfilAdi) && yeniProfilAdi != ProfilAdi)
            {
                MessageBox.Show(
                    "❌ BU PROFİL ADI ZATEN MEVCUT!\n\n" +
                    "'" + yeniProfilAdi + "' isimli bir profil zaten var.\n\n" +
                    "Lütfen farklı bir profil adı girin.",
                    "⚠️ Tekrar Eden Profil Adı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtProfilAdi.Focus();
                txtProfilAdi.SelectAll();
                return;
            }

            // ========== HATA KONTROLÜ 3: BOŞ LİSTE ==========
            if (ZamanListesi.Count == 0)
            {
                DialogResult result = MessageBox.Show(
                    "⚠️ ZAMAN LİSTESİ BOŞ!\n\n" +
                    "Hiç zaman eklemediniz.\n\n" +
                    "Boş profil kaydetmek istediğinizden emin misiniz?",
                    "⚠️ Boş Liste Uyarısı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    txtSaat.Focus();
                    return;
                }
            }

            ProfilAdi = yeniProfilAdi;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnDuzenle_Click(null, null);
            }
        }

        private void btnYukari_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string secilenSaat = selectedRow.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(secilenSaat)) return;

                int index = SaatIndexBul(secilenSaat);
                if (index > 0)
                {
                    // Swap
                    var temp = ZamanListesi[index];
                    ZamanListesi[index] = ZamanListesi[index - 1];
                    ZamanListesi[index - 1] = temp;

                    // NOT: Sıralama yapma, manuel sıra değiştiriliyor
                    ListeGuncelleSiralamasiz();

                    // Yeni pozisyonu seç
                    if (index - 1 < dataGridView1.Rows.Count)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[index - 1].Selected = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Lütfen taşımak için bir satır seçin!",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnAsagi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string secilenSaat = selectedRow.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(secilenSaat)) return;

                int index = SaatIndexBul(secilenSaat);
                if (index >= 0 && index < ZamanListesi.Count - 1)
                {
                    // Swap
                    var temp = ZamanListesi[index];
                    ZamanListesi[index] = ZamanListesi[index + 1];
                    ZamanListesi[index + 1] = temp;

                    ListeGuncelleSiralamasiz();

                    // Yeni pozisyonu seç
                    if (index + 1 < dataGridView1.Rows.Count)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[index + 1].Selected = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Lütfen taşımak için bir satır seçin!",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ListeGuncelleSiralamasiz()
        {
            dataGridView1.Rows.Clear();

            foreach (ZamanItem item in ZamanListesi)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.Saat;
                dataGridView1.Rows[index].Cells[1].Value = item.Ders;
                dataGridView1.Rows[index].Cells[2].Value = item.Aciklama;
            }

            // Aramayı tekrar uygula
            if (txtArama != null && !string.IsNullOrWhiteSpace(txtArama.Text))
            {
                AramaYap();
            }
        }
    }
}