using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GudangBarang
{
    public partial class Transaksi : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? selectedId;
        //int? StokBarangId;
        int? StokTokoId;
        int? BarangId;
        int? TokoId;
        int? StokBarang;
        int? totalBarang;
        int? HargaSatuan;

        MainToko ambil;
        public Transaksi(MainToko id)
        {
            InitializeComponent();
            ambil = id;
        }
        private void isiToko()
        {
            TokoId = Convert.ToInt32(ambil.LabelId);
            var data = db.Tokoes.FirstOrDefault(x => x.Id == TokoId);
            textBox1.Text = data.Toko1;
            
        }
        private void isiBarang()
        {
            TokoId=Convert.ToInt32(ambil.LabelId);
            var data=(from n in db.StokTokoes
                      join b in db.Barang1 on n.BarangId equals b.Id
                      join t in db.Tokoes on n.TokoId equals t.Id
                      where n.TokoId == TokoId
                      select new
                      {
                          StokTokoId=n.Id,
                          BarangID=n.BarangId,
                          NamaBarang=b.Barang,
                          Stok=n.Jumlah,
                          Harga=b.HargaJual
                      }).ToList();
            comboBox1.DataSource = data;
            comboBox1.ValueMember = "BarangID";
            comboBox1.DisplayMember = "NamaBarang";
        }

        private void Transaksi_Load(object sender, EventArgs e)
        {
            isiToko();
            isiBarang();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item=(dynamic)comboBox1.SelectedItem;
            StokTokoId=item.StokTokoId;
            BarangId=item.BarangID;
            StokBarang=item.Stok;
            HargaSatuan=item.Harga;

            textBox2.Text=HargaSatuan.ToString();


            if(StokBarang > 0)
            {
                label9.Text=StokBarang.ToString();

            }
            else
            {
                label9.Visible = false;
                label10.Visible = false;
                label8.Text = "Maaf Stok Saat Ini Kosong";
                textBox3.ReadOnly = true;
            }

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            int qty, harga, subtotal,stok;
            qty=Convert.ToInt32(textBox3.Text);
            harga=Convert.ToInt32(textBox2.Text);

            stok=Convert.ToInt32(label9.Text);
            if (qty > stok)
            {
                int kosong = 0;
                Alert.error("Qty melebihi stok ayang ada");
                textBox3.Text=kosong.ToString();
                textBox3.Select();
            }
            else
            {
                subtotal=qty*harga;
                textBox4.Text=subtotal.ToString();
                button1.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int qty, harga, subtotal, stok;
            qty = Convert.ToInt32(textBox3.Text);
            harga = Convert.ToInt32(textBox2.Text);
            subtotal=Convert.ToInt32(textBox4.Text);

            bool data = false;

            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value !=null && row.Cells[5].Value != null)
                {
                    int adabarang = (int)row.Cells[5].Value;
                    int adatoko = (int)row.Cells[2].Value;
                    if(adatoko==TokoId && adabarang == BarangId)
                    {
                        int subtotallama=(int)row.Cells[8].Value;
                        int qtylama=(int)row.Cells [7].Value;

                        int qtybaru=qty+qtylama;
                        int subtotalbaru=subtotal+subtotallama;

                        row.Cells[7].Value = qtybaru;
                        row.Cells[8].Value = subtotalbaru;

                        data= true;

                        int total = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            total += Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
                        }
                        richTextBox1.Text=total.ToString();

                        isiBarang();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        comboBox1.Select();
                    }
                }
            }
            if (!data)
            {
                dataGridView1.Rows.Add(StokTokoId, textBox1.Text, TokoId, DateTime.Now, comboBox1.Text, BarangId, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));

                int total = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    total += Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
                }
                richTextBox1.Text = total.ToString();

                isiBarang();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox1.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            int totalBeli=Convert.ToInt32(richTextBox1.Text);
            StokTokoId=(int)row.Cells[0].Value;
            totalBarang = (int)row.Cells[7].Value;
            HargaSatuan = (int)row.Cells[6].Value;

            var stoktoko = db.StokTokoes.FirstOrDefault(x => x.Id == StokTokoId);
            if (stoktoko != null)
            {
                stoktoko.Jumlah -=totalBarang;
                db.SaveChanges();
            }

            var penjualan=new Penjualan();
            penjualan.TokoId = TokoId;
            penjualan.Total=totalBeli;
            penjualan.Tanggal=DateTime.Now;
            db.Penjualans.Add(penjualan);
            db.SaveChanges();

            var detail=new DetailPenjualan();
            detail.PenjualanId = penjualan.Id;
            detail.BarangId = BarangId;
            detail.Qty = totalBarang;
            detail.HargaSatuan = HargaSatuan;
            db.DetailPenjualans.Add(detail);
            db.SaveChanges();
            Alert.info("Transaksi Sukses");

            isiBarang();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Select();
            richTextBox1.Text = string.Empty;
            dataGridView1.Rows.Clear();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =! char.IsDigit(e.KeyChar) && e.KeyChar !=(char)Keys.Back;
        }
    }
}
