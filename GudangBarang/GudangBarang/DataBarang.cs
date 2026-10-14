using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GudangBarang
{
    public partial class DataBarang : Form
    {
        private readonly GudangEntities2 db= new GudangEntities2();
        int? selectedId;
        public DataBarang()
        {
            InitializeComponent();
        }
        private void isiMerek()
        {
            var data=(from n in db.Mereks 
                      select new
                      {
                          n.Id,
                          n.Merek1
                      }).ToList();
            comboBox1.DataSource = data;
            comboBox1.ValueMember="Id";
            comboBox1.DisplayMember="Merek1";
        }
        private void tampil()
        {

        }
        private void isiKategori()
        {
            var data = (from n in db.Kategories
                        select new
                        {
                            n.Id,
                            n.Kategori
                        }).ToList();
            comboBox2.DataSource = data;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "Kategori";
        }
        private void showData()
        {
            var data=(from n in db.Barang1 
                      join m in db.Mereks on n.MerekId equals m.Id
                      join k in db.Kategories on n.KategoriId equals k.Id
                      select new
                      {
                          IdBarang=n.Id,
                          n.Barang,
                          m.Merek1,
                          k.Kategori,
                          n.HargaBeli,
                          n.HargaJual,
                          n.BarangFoto,
                      }).ToList();
            dataGridView1.RowTemplate.Height = 170;
            dataGridView1.DataSource = data;

            DataGridViewImageColumn img=new DataGridViewImageColumn();
            img=(DataGridViewImageColumn)dataGridView1.Columns["BarangFoto"];
            img.ImageLayout=DataGridViewImageCellLayout.Stretch;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string location = "";
            openFileDialog1.Filter = "select image(*.jpg;*.png)|*.JPG;*.PNG";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                location = openFileDialog1.FileName;
                pictureBox1.ImageLocation = location;
            }
        }
        private void showselectedItem(DataGridViewRow row)
        {
            selectedId =(int)row.Cells["IdBarang"].Value;
            var data=db.Barang1.FirstOrDefault(x => x.Id==selectedId);
            
            textBox1.Text=data.Barang.ToString();
            textBox2.Text=data.HargaBeli.ToString();
            textBox3.Text=data.HargaJual.ToString();
            comboBox1.SelectedValue = data.MerekId;
           
            comboBox2.SelectedValue = data.KategoriId;

            byte[] image = (byte[])data.BarangFoto;
            MemoryStream ms=new MemoryStream(image);

            pictureBox1.Image=Image.FromStream(ms);

            button2.Text = "Edit";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                if (selectedId == null)
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms,pictureBox1.Image.RawFormat);
                    byte[] image = ms.ToArray();

                    var simpan = new Barang1();
                    simpan.Barang = textBox1.Text;
                    simpan.MerekId=(int)comboBox1.SelectedValue;
                    simpan.KategoriId=(int)comboBox2.SelectedValue;
                    simpan.HargaBeli=Convert.ToInt32(textBox2.Text);
                    simpan.HargaJual=Convert.ToInt32(textBox3.Text);
                    simpan.BarangFoto = image;

                    db.Barang1.Add(simpan);
                    db.SaveChanges();
                    Alert.info("Data berhasil disimpan");
                    bersih();

                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] image = ms.ToArray();

                    var simpan = db.Barang1.FirstOrDefault(x => x.Id == selectedId);
                    simpan.Barang = textBox1.Text;
                    simpan.MerekId = (int)comboBox1.SelectedValue;
                    simpan.KategoriId = (int)comboBox2.SelectedValue;
                    simpan.HargaBeli = Convert.ToInt32(textBox2.Text);
                    simpan.HargaJual = Convert.ToInt32(textBox3.Text);
                    simpan.BarangFoto = image;

                    db.SaveChanges();
                    Alert.info("Data berhasil diubah");
                    bersih();

                    selectedId = 0;
                    button2.Text = "Simpan";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bersih();
        }
        private bool validasi()
        {
            if (textBox1.Text == string.Empty ||
                textBox3.Text == string.Empty ||
                textBox2.Text == string.Empty ||
                comboBox1.SelectedValue==null ||
                comboBox2.SelectedValue==null)
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
            pictureBox1.Image = null;
            isiKategori();
            isiMerek();
            showData();
            textBox1.Select();
        }

        private void DataBarang_Load(object sender, EventArgs e)
        {
            isiKategori();
            isiMerek();
            showData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            selectedId=(int)row.Cells["IdBarang"].Value;
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index)
            {
                showselectedItem(row);
            }
            else if(e.ColumnIndex == dataGridView1.Columns["delete"].Index)
            {
                if(Alert.confirm("Yakin Ingin Menghapus data ini?")==DialogResult.Yes)
                {
                    var data = db.Barang1.FirstOrDefault(x => x.Id == selectedId);
                    db.Barang1.Remove(data);
                    db.SaveChanges();
                    showData();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (textBox4.Text.ToLower() !=null)
            {
                //List<Barang1> list = new List<Barang1>();
                
                //list = db.Barang1.Where(x => x.Barang.Contains.FirstOrDefault();
               
                var data = (from n in db.Barang1
                            join m in db.Mereks on n.MerekId equals m.Id
                            join k in db.Kategories on n.KategoriId equals k.Id
                            where n.Barang.Contains(textBox4.Text)
                            select new
                            {
                                IdBarang = n.Id,
                                n.Barang,
                                m.Merek1,
                                k.Kategori,
                                n.HargaBeli,
                                n.HargaJual,
                                n.BarangFoto,
                            }).ToList();
                dataGridView1.RowTemplate.Height = 170;
                dataGridView1.DataSource = data;

                DataGridViewImageColumn img = new DataGridViewImageColumn();
                img = (DataGridViewImageColumn)dataGridView1.Columns["BarangFoto"];
                img.ImageLayout = DataGridViewImageCellLayout.Stretch;

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
