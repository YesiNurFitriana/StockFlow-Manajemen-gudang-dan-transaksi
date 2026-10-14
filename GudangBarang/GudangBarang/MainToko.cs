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
    public partial class MainToko : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? ambilId;

        public string LabelId
        {
            get{ return label3.Text; }
        }
        public MainToko(int? iduser)
        {
            InitializeComponent();
            ambilId = iduser;
        }

        private void MainToko_Load(object sender, EventArgs e)
        {
            var data = db.Tokoes.FirstOrDefault(x => x.Id == ambilId);
            label2.Text=data.Toko1.ToString();
            label3.Text=ambilId.ToString();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes==Alert.confirm("Yakin ingin logout?"))
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
            }
        }

        private void stokTokoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataStokToko stokToko = new DataStokToko(this);
            stokToko.ShowDialog();
        }

        private void transaksiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Transaksi transaksi = new Transaksi(this);
            transaksi.ShowDialog();
        }
    }
}
