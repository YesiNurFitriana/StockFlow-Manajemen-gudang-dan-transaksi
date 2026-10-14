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
    public partial class DataStokTokoAdmin : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();

        public DataStokTokoAdmin()
        {
            InitializeComponent();
        }
        private void Showdata()
        {
            var data = (from n in db.StokTokoes
                        join b in db.Barang1 on n.BarangId equals b.Id
                        join t in db.Tokoes on n.TokoId equals t.Id
                        join m in db.Mereks on b.MerekId equals m.Id
                        join k in db.Kategories on b.KategoriId equals k.Id
                        select new
                        {
                            IdStok=n.Id,
                            TOKO=t.Toko1,
                            BARANG=b.Barang,
                            Merek=m.Merek1,
                            KATEGORI=k.Kategori,
                            STOK=n.Jumlah
                        }).OrderBy(x=>x.TOKO).ToList();
            dataGridView1.DataSource = data;
            dataGridView1.ReadOnly = true;
            
        }

        private void DataStokTokoAdmin_Load(object sender, EventArgs e)
        {
            Showdata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != null)
            {
                var data = (from n in db.StokTokoes
                            join b in db.Barang1 on n.BarangId equals b.Id
                            join t in db.Tokoes on n.TokoId equals t.Id
                            join m in db.Mereks on b.MerekId equals m.Id
                            join k in db.Kategories on b.KategoriId equals k.Id
                            where b.Barang.Contains(textBox4.Text)
                            select new
                            {
                                IdStok = n.Id,
                                TOKO = t.Toko1,
                                BARANG = b.Barang,
                                Merek = m.Merek1,
                                KATEGORI = k.Kategori,
                                STOK = n.Jumlah
                            }).OrderBy(x => x.TOKO).ToList();
                dataGridView1.DataSource = data;
                dataGridView1.ReadOnly = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                Showdata();
            }
        }
    }
}
