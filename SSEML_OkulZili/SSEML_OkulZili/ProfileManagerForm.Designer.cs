namespace SSEML_OkulZili
{
    partial class ProfileManagerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtProfilAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSaat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnDuzenle = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnYukari = new System.Windows.Forms.Button();
            this.btnAsagi = new System.Windows.Forms.Button();
            this.labelDers = new System.Windows.Forms.Label();
            this.txtDers = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profil Adı:";
            // 
            // txtProfilAdi
            // 
            this.txtProfilAdi.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtProfilAdi.Location = new System.Drawing.Point(130, 18);
            this.txtProfilAdi.MaxLength = 50;
            this.txtProfilAdi.Name = "txtProfilAdi";
            this.txtProfilAdi.Size = new System.Drawing.Size(300, 32);
            this.txtProfilAdi.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.label2.Location = new System.Drawing.Point(18, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "📋 Zaman Listesi:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 105);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(530, 320);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label3.Location = new System.Drawing.Point(18, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Saat:";
            // 
            // txtSaat
            // 
            this.txtSaat.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSaat.Location = new System.Drawing.Point(75, 442);
            this.txtSaat.MaxLength = 5;
            this.txtSaat.Name = "txtSaat";
            this.txtSaat.Size = new System.Drawing.Size(90, 29);
            this.txtSaat.TabIndex = 5;
            this.txtSaat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label4.Location = new System.Drawing.Point(305, 445);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Açıklama:";
            // 
            // txtAciklama
            // 
            this.txtAciklama.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAciklama.Location = new System.Drawing.Point(395, 442);
            this.txtAciklama.MaxLength = 100;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(215, 29);
            this.txtAciklama.TabIndex = 7;
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEkle.FlatAppearance.BorderSize = 0;
            this.btnEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEkle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnEkle.ForeColor = System.Drawing.Color.White;
            this.btnEkle.Location = new System.Drawing.Point(18, 490);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(130, 50);
            this.btnEkle.TabIndex = 8;
            this.btnEkle.Text = "➕ EKLE";
            this.btnEkle.UseVisualStyleBackColor = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.FlatAppearance.BorderSize = 0;
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(158, 490);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(130, 50);
            this.btnSil.TabIndex = 9;
            this.btnSil.Text = "🗑️ SİL";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnDuzenle
            // 
            this.btnDuzenle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.btnDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuzenle.FlatAppearance.BorderSize = 0;
            this.btnDuzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuzenle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDuzenle.ForeColor = System.Drawing.Color.White;
            this.btnDuzenle.Location = new System.Drawing.Point(298, 490);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(130, 50);
            this.btnDuzenle.TabIndex = 10;
            this.btnDuzenle.Text = "✏️ DÜZENLE";
            this.btnDuzenle.UseVisualStyleBackColor = false;
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(18, 560);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(592, 55);
            this.btnKaydet.TabIndex = 12;
            this.btnKaydet.Text = "💾 PROFİLİ KAYDET";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnIptal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIptal.FlatAppearance.BorderSize = 0;
            this.btnIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnIptal.ForeColor = System.Drawing.Color.White;
            this.btnIptal.Location = new System.Drawing.Point(438, 490);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(110, 50);
            this.btnIptal.TabIndex = 11;
            this.btnIptal.Text = "✖️ İPTAL";
            this.btnIptal.UseVisualStyleBackColor = false;
            this.btnIptal.Visible = false;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnYukari
            // 
            this.btnYukari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnYukari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYukari.FlatAppearance.BorderSize = 0;
            this.btnYukari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYukari.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnYukari.ForeColor = System.Drawing.Color.White;
            this.btnYukari.Location = new System.Drawing.Point(560, 214);
            this.btnYukari.Name = "btnYukari";
            this.btnYukari.Size = new System.Drawing.Size(50, 50);
            this.btnYukari.TabIndex = 15;
            this.btnYukari.Text = "▲";
            this.btnYukari.UseVisualStyleBackColor = false;
            this.btnYukari.Click += new System.EventHandler(this.btnYukari_Click);
            // 
            // btnAsagi
            // 
            this.btnAsagi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnAsagi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsagi.FlatAppearance.BorderSize = 0;
            this.btnAsagi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsagi.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnAsagi.ForeColor = System.Drawing.Color.White;
            this.btnAsagi.Location = new System.Drawing.Point(560, 274);
            this.btnAsagi.Name = "btnAsagi";
            this.btnAsagi.Size = new System.Drawing.Size(50, 50);
            this.btnAsagi.TabIndex = 16;
            this.btnAsagi.Text = "▼";
            this.btnAsagi.UseVisualStyleBackColor = false;
            this.btnAsagi.Click += new System.EventHandler(this.btnAsagi_Click);
            // 
            // labelDers
            // 
            this.labelDers.AutoSize = true;
            this.labelDers.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelDers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.labelDers.Location = new System.Drawing.Point(180, 445);
            this.labelDers.Name = "labelDers";
            this.labelDers.Size = new System.Drawing.Size(45, 20);
            this.labelDers.TabIndex = 19;
            this.labelDers.Text = "Ders:";
            // 
            // txtDers
            // 
            this.txtDers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtDers.Location = new System.Drawing.Point(230, 442);
            this.txtDers.MaxLength = 2;
            this.txtDers.Name = "txtDers";
            this.txtDers.Size = new System.Drawing.Size(60, 29);
            this.txtDers.TabIndex = 6;
            this.txtDers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProfileManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(760, 635);
            this.Controls.Add(this.txtDers);
            this.Controls.Add(this.labelDers);
            this.Controls.Add(this.btnAsagi);
            this.Controls.Add(this.btnYukari);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.btnDuzenle);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSaat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProfilAdi);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📋 Profil Yönetimi";
            this.Load += new System.EventHandler(this.ProfileManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProfilAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSaat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnDuzenle;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnYukari;
        private System.Windows.Forms.Button btnAsagi;
        private System.Windows.Forms.Label labelDers;
        private System.Windows.Forms.TextBox txtDers;
    }
}