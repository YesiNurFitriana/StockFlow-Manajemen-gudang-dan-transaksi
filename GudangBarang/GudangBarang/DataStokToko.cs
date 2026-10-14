using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GudangBarang
{
    public partial class DataStokToko : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? TokoID;

        MainToko IdnyaToko;
        public DataStokToko(MainToko ambil)
        {
            InitializeComponent();
            IdnyaToko = ambil;
        }
        private void showData()
        {
            TokoID = Convert.ToInt32(IdnyaToko.LabelId);
            var data = (from n in db.StokTokoes
                        join t in db.Tokoes on n.TokoId equals t.Id
                        join b in db.Barang1 on n.BarangId equals b.Id
                        join m in db.Mereks on b.MerekId equals m.Id
                        join k in db.Kategories on b.KategoriId equals k.Id
                        where n.TokoId== TokoID
                      select new
                      {
                          n.Id,
                          Toko = t.Toko1,
                          Barang = b.Barang,
                          Merek = m.Merek1,
                          Kategori = k.Kategori,
                          Stok = n.Jumlah
                      }).ToList();
            dataGridView1.DataSource = data;
        }

        private void DataStokToko_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != null)
            {
                TokoID = Convert.ToInt32(IdnyaToko.LabelId);
                var data = (from n in db.StokTokoes
                            join t in db.Tokoes on n.TokoId equals t.Id
                            join b in db.Barang1 on n.BarangId equals b.Id
                            join m in db.Mereks on b.MerekId equals m.Id
                            join k in db.Kategories on b.KategoriId equals k.Id
                            where n.TokoId == TokoID && b.Barang.Contains(textBox4.Text)
                            select new
                            {
                                n.Id,
                                Toko = t.Toko1,
                                Barang = b.Barang,
                                Merek = m.Merek1,
                                Kategori = k.Kategori,
                                Stok = n.Jumlah
                            }).ToList();
                dataGridView1.DataSource = data;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                showData();
            }
        }
    }
}
