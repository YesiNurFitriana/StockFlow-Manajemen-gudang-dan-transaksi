namespace GudangBarang
{
    partial class MainAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stokTokoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stokBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transaksiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputTokoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stokTokoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mutasiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputMutasiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataTransaksiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTransaksiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stokTokoToolStripMenuItem,
            this.transaksiToolStripMenuItem,
            this.mutasiToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.dataTransaksiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1120, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stokTokoToolStripMenuItem
            // 
            this.stokTokoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputBarangToolStripMenuItem,
            this.stokBarangToolStripMenuItem});
            this.stokTokoToolStripMenuItem.Name = "stokTokoToolStripMenuItem";
            this.stokTokoToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.stokTokoToolStripMenuItem.Text = "Data Barang";
            // 
            // inputBarangToolStripMenuItem
            // 
            this.inputBarangToolStripMenuItem.Name = "inputBarangToolStripMenuItem";
            this.inputBarangToolStripMenuItem.Size = new System.Drawing.Size(214, 24);
            this.inputBarangToolStripMenuItem.Text = "Tambah barang";
            this.inputBarangToolStripMenuItem.Click += new System.EventHandler(this.inputBarangToolStripMenuItem_Click);
            // 
            // stokBarangToolStripMenuItem
            // 
            this.stokBarangToolStripMenuItem.Name = "stokBarangToolStripMenuItem";
            this.stokBarangToolStripMenuItem.Size = new System.Drawing.Size(214, 24);
            this.stokBarangToolStripMenuItem.Text = "Tambah Stok Barang";
            this.stokBarangToolStripMenuItem.Click += new System.EventHandler(this.stokBarangToolStripMenuItem_Click);
            // 
            // transaksiToolStripMenuItem
            // 
            this.transaksiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputTokoToolStripMenuItem,
            this.stokTokoToolStripMenuItem1});
            this.transaksiToolStripMenuItem.Name = "transaksiToolStripMenuItem";
            this.transaksiToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.transaksiToolStripMenuItem.Text = "Data Toko";
            // 
            // inputTokoToolStripMenuItem
            // 
            this.inputTokoToolStripMenuItem.Name = "inputTokoToolStripMenuItem";
            this.inputTokoToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.inputTokoToolStripMenuItem.Text = "Tambah Toko";
            this.inputTokoToolStripMenuItem.Click += new System.EventHandler(this.inputTokoToolStripMenuItem_Click);
            // 
            // stokTokoToolStripMenuItem1
            // 
            this.stokTokoToolStripMenuItem1.Name = "stokTokoToolStripMenuItem1";
            this.stokTokoToolStripMenuItem1.Size = new System.Drawing.Size(180, 24);
            this.stokTokoToolStripMenuItem1.Text = "Stok Toko";
            this.stokTokoToolStripMenuItem1.Click += new System.EventHandler(this.stokTokoToolStripMenuItem1_Click);
            // 
            // mutasiToolStripMenuItem
            // 
            this.mutasiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputMutasiToolStripMenuItem});
            this.mutasiToolStripMenuItem.Name = "mutasiToolStripMenuItem";
            this.mutasiToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.mutasiToolStripMenuItem.Text = "Mutasi";
            // 
            // inputMutasiToolStripMenuItem
            // 
            this.inputMutasiToolStripMenuItem.Name = "inputMutasiToolStripMenuItem";
            this.inputMutasiToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.inputMutasiToolStripMenuItem.Text = "Input Mutasi";
            this.inputMutasiToolStripMenuItem.Click += new System.EventHandler(this.inputMutasiToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(503, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 47);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(421, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 47);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selamat Datang ";
            // 
            // dataTransaksiToolStripMenuItem
            // 
            this.dataTransaksiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTransaksiToolStripMenuItem});
            this.dataTransaksiToolStripMenuItem.Name = "dataTransaksiToolStripMenuItem";
            this.dataTransaksiToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.dataTransaksiToolStripMenuItem.Text = "Histori Transaksi";
            // 
            // viewTransaksiToolStripMenuItem
            // 
            this.viewTransaksiToolStripMenuItem.Name = "viewTransaksiToolStripMenuItem";
            this.viewTransaksiToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.viewTransaksiToolStripMenuItem.Text = "View Transaksi";
            this.viewTransaksiToolStripMenuItem.Click += new System.EventHandler(this.viewTransaksiToolStripMenuItem_Click);
            // 
            // MainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 669);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainAdmin";
            this.Load += new System.EventHandler(this.MainAdmin_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stokTokoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputBarangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stokBarangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transaksiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputTokoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stokTokoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mutasiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputMutasiToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataTransaksiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTransaksiToolStripMenuItem;
    }
}