using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GudangBarang
{
    public partial class DataStokBarang : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? selectedId;
        public DataStokBarang()
        {
            InitializeComponent();
        }
        private void isiBarang()
        {
            var data=(from n in db.Barang1
                      select new
                      {
                          n.Id,
                          n.Barang
                      }).ToList();
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "Barang";
            comboBox1.ValueMember = "Id";
        }
        private void showData()
        {
            var data = (from n in db.StokBarangs
                        join b in db.Barang1 on n.BarangId equals b.Id
                        join m in db.Mereks on b.MerekId equals m.Id
                        join k in db.Kategories on b.KategoriId equals k.Id
                        select new
                        {
                            n.Id,
                            Barang=b.Barang,
                            Merek=m.Merek1,
                            Kategori=k.Kategori,
                            Jumlah=n.Jumlah
                        }).ToList();
           
            dataGridView1.DataSource = data;

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
        private void DataStokBarang_Load(object sender, EventArgs e)
        {
            isiBarang();
            showData();
        }
        private bool validasi()
        {
            if (textBox1.Text == string.Empty ||
                comboBox1.SelectedValue == null 
               )
            {
                Alert.error("Silahkan isi semua data terlebih dahulu");
                return false;
            }
            return true;
        }
        private void bersih()
        {
            textBox1.Text = string.Empty;
            isiBarang();
            showData();
            textBox1.Select();
        }
        private void showselectedItem(DataGridViewRow row)
        {
            selectedId = (int)row.Cells["Id"].Value;
            var data = db.StokBarangs.FirstOrDefault(x => x.Id == selectedId);

            textBox1.Text = data.Jumlah.ToString();
           
            comboBox1.SelectedValue = data.BarangId;

            
            button1.Text = "Edit";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                if (selectedId == null)
                {
                    var data = new StokBarang();
                    data.BarangId = (int)comboBox1.SelectedValue;
                    data.Jumlah = Convert.ToInt32(textBox1.Text);

                    db.StokBarangs.Add(data);
                    db.SaveChanges();
                    Alert.info("Data Berhasil Disimpan");
                    bersih();
                }
                else
                {
                    var data = db.StokBarangs.FirstOrDefault(x => x.Id == selectedId);
                    data.BarangId = (int)comboBox1.SelectedValue;
                    data.Jumlah = Convert.ToInt32(textBox1.Text);

                    db.SaveChanges();
                    Alert.info("Data Berhasil Disimpan");
                    bersih();
                    selectedId = null;
                    button1.Text = "Simpan";
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            selectedId = (int)row.Cells["Id"].Value;
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index)
            {
                showselectedItem(row);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index)
            {
                if (Alert.confirm("Yakin Ingin Menghapus data ini?") == DialogResult.Yes)
                {
                    var data = db.Barang1.FirstOrDefault(x => x.Id == selectedId);
                    db.Barang1.Remove(data);
                    db.SaveChanges();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != null)
            {
                var data = (from n in db.StokBarangs
                            join b in db.Barang1 on n.BarangId equals b.Id
                            join m in db.Mereks on b.MerekId equals m.Id
                            join k in db.Kategories on b.KategoriId equals k.Id
                            where b.Barang.Contains(textBox4.Text)
                            select new
                            {
                                n.Id,
                                Barang = b.Barang,
                                Merek = m.Merek1,
                                Kategori = k.Kategori,
                                Jumlah = n.Jumlah
                            }).ToList();

                dataGridView1.DataSource = data;

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
