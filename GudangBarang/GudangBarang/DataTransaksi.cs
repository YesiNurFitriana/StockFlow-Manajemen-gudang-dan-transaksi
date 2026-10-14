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
    public partial class DataTransaksi : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        public DataTransaksi()
        {
            InitializeComponent();
        }
        private void showdata()
        {
            var data = (from n in db.DetailPenjualans
                        join b in db.Barang1 on n.BarangId equals b.Id
                        join p in db.Penjualans on n.PenjualanId equals p.Id
                        join t in db.Tokoes on p.TokoId equals t.Id
                        select new
                        {
                            n.Id,
                            t.Toko1,
                            b.Barang,
                            b.HargaJual,
                            n.Qty,
                            p.Total,
                            p.Tanggal
                        }).OrderBy(x => x.Toko1).ToList();
            dataGridView1.DataSource = data;
        }

        private void DataTransaksi_Load(object sender, EventArgs e)
        {
            showdata();
        }
    }
}
