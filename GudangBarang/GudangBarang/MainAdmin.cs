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
    public partial class MainAdmin : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? ambilId;
        public MainAdmin(int? UserId)
        {
            InitializeComponent();
            ambilId = UserId;
        }

        private void MainAdmin_Load(object sender, EventArgs e)
        {
            var data = db.Users.FirstOrDefault(x => x.Id == ambilId);
            label2.Text=data.Username.ToString();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == Alert.confirm("Yakin ingin logout?"))
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
            }
        }

        private void inputBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataBarang dataBarang = new DataBarang();
            dataBarang.ShowDialog();
        }

        private void stokBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataStokBarang data=new DataStokBarang();
            data.ShowDialog();
        }

        private void inputTokoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataToko dataToko = new DataToko();
            dataToko.ShowDialog();
        }

        private void inputMutasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataMutasi dataMutasi = new DataMutasi();
            dataMutasi.ShowDialog();
        }

        private void stokTokoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataStokTokoAdmin data=new DataStokTokoAdmin();
            data.ShowDialog();
        }

        private void viewTransaksiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTransaksi data=new DataTransaksi();
            data.ShowDialog();
        }
    }
}
