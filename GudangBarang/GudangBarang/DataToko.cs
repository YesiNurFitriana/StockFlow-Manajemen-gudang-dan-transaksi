using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GudangBarang
{
    public partial class DataToko : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? selectedId;
        int? idToko;
        public DataToko()
        {
            InitializeComponent();
        }
        private void Showdata()
        {
            var data = (from n in db.Users
                        join t in db.Tokoes on n.TokoId equals t.Id
                        select new
                        {
                            IdUser=n.Id,
                            TokoId = t.Id,
                            NamaToko = t.Toko1,
                            Alamat = t.Alamat,
                            Username = n.Username,
                            Password = n.Password
                        }).ToList();
            dataGridView1.DataSource = data;
            dataGridView1.ReadOnly = true;
            if (!dataGridView1.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editcol = new DataGridViewButtonColumn()
                {
                    Name = "edit",
                    Text = "Edit",
                    HeaderText = "",
                    UseColumnTextForButtonValue = true,
                };
                DataGridViewButtonColumn deletecol = new DataGridViewButtonColumn()
                {
                    Name = "delete",
                    Text = "Delete",
                    HeaderText = "",
                    UseColumnTextForButtonValue = true,
                };
                dataGridView1.Columns.Add(editcol);
                dataGridView1.Columns.Add(deletecol);
            }
        }

        private void DataToko_Load(object sender, EventArgs e)
        {
            Showdata();
        }
        private bool validasi()
        {
            if (textBox1.Text == string.Empty ||
                textBox3.Text == string.Empty ||
                textBox2.Text == string.Empty ||
                textBox4.Text==string.Empty)
            {
                Alert.error("Silahkan isi semua data terlebih dahulu");
                return false;
            }
            return true;
        }
        private void bersih()
        {
            textBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox1.Select();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                if (selectedId == null && idToko==null)
                {
                    var toko=new Toko();
                    toko.Toko1 = textBox1.Text;
                    toko.Alamat= textBox2.Text;
                    db.Tokoes.Add(toko);
                    db.SaveChanges();

                    var user=new User();
                    user.Username = textBox3.Text;
                    user.Password = textBox4.Text;
                    user.Role=textBox1.Text.ToLower();
                    user.TokoId = toko.Id;
                    db.Users.Add(user);
                    db.SaveChanges();
                    
                    Alert.info("data Berhasil disimpan!");
                    bersih();
                    Showdata();
                }
                else
                {
                    var toko = db.Tokoes.FirstOrDefault(x => x.Id == idToko);
                    toko.Toko1 = textBox1.Text;
                    toko.Alamat = textBox2.Text;
                   
                    db.SaveChanges();

                    var user = db.Users.FirstOrDefault(x => x.Id== selectedId);
                    user.Username = textBox3.Text;
                    user.Password = textBox4.Text;
                    user.Role = textBox1.Text.ToLower();
                    user.TokoId = toko.Id;
                    
                    db.SaveChanges();
                    
                    Alert.info("data Berhasil diedit!");
                    bersih();
                    Showdata();
                    button1.Text = "Simpan";
                    selectedId = null;
                    idToko = null;
                }
            }
        }
        private void showselectedItem(DataGridViewRow row)
        {
            //selectedId = (int)row.Cells["Id"].Value;
            //var data = db.Tokoes.FirstOrDefault(x => x.Id == selectedId);

            textBox1.Text = row.Cells["NamaToko"].Value.ToString();
            textBox2.Text= row.Cells["Alamat"].Value.ToString();
            textBox3.Text = row.Cells["Username"].Value.ToString();
            textBox4.Text = row.Cells["Password"].Value.ToString();
            button1.Text = "Edit";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            selectedId = (int)row.Cells["IdUser"].Value;
            idToko = (int)row.Cells["TokoId"].Value;
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index)
            {
                showselectedItem(row);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index)
            {
                if (Alert.confirm("Yakin Ingin Menghapus data ini?") == DialogResult.Yes)
                {
                    selectedId = (int)row.Cells["IdUser"].Value;
                    idToko = (int)row.Cells["TokoId"].Value;

                    var data = db.Users.FirstOrDefault(x => x.Id == selectedId);
                    db.Users.Remove(data);
                    db.SaveChanges();

                    var data2 = db.Tokoes.FirstOrDefault(x => x.Id == idToko);
                    db.Tokoes.Remove(data2);
                    db.SaveChanges();

                    Showdata();
                }
            }
        }
    }
    
}
