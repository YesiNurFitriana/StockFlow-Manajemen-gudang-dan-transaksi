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
    public partial class DataMutasi : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? selectedId;
        int? BarangId;
        int? TokoId;
        int? StokBarangId;
        int? StokTokoId;
        
        public DataMutasi()
        {
            InitializeComponent();
        }
        private void isitoko()
        {
            //var data = db.Tokoes
            //    .Select(n => new
            //    {
            //        NamaToko = n.Toko1,
            //        TokoID = n.Id,
            //        StokTokoID = n.StokTokoes.Any() ? n.StokTokoes.FirstOrDefault().Id : 0
            //    }).ToList();
            //comboBox1.DataSource = data;
            //comboBox1.ValueMember = "TokoID";
            //comboBox1.DisplayMember = "NamaToko";
            var data = db.Tokoes
                    .Select(n => new
                    {
                        NamaToko = n.Toko1,
                        TokoID = n.Id
                    }).ToList();

            comboBox1.DataSource = data;
            comboBox1.ValueMember = "TokoID";
            comboBox1.DisplayMember = "NamaToko";
        }
        private void isibarang()
        {
            var data=(from n in db.StokBarangs
                      join b in db.Barang1 on n.BarangId equals b.Id
                      join m in db.Mereks on b.MerekId equals m.Id
                      join k in db.Kategories on b.KategoriId equals k.Id
                      select new
                      {
                          StokBarangID= n.Id,   
                          BarangID= n.BarangId,
                          NamaBarang=b.Barang
                      }).ToList();
            comboBox2.DataSource = data;
            comboBox2.DisplayMember = "NamaBarang";
            comboBox2.ValueMember = "BarangId";
        }

        private void Showdata()
        {
            var data = (from n in db.Mutasis
                        join t in db.Tokoes on n.TokoId equals t.Id
                        join b in db.Barang1 on n.BarangId equals b.Id
                        select new
                        {
                            n.Id,
                            BarangId= n.BarangId,
                            TokoId= n.TokoId,
                            Barang=b.Barang,
                            Toko=t.Toko1,
                            HargaBarang=b.HargaJual,
                            Jumlah=n.Jumlah,
                            Tanggal=n.Tanggal
                        }).ToList();
            dataGridView1.DataSource = data;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns["BarangId"].Visible = false;
            dataGridView1.Columns["TokoId"].Visible = false;

            if (!dataGridView1.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editcol = new DataGridViewButtonColumn()
                {
                    Name = "edit",
                    Text = "Edit",
                    HeaderText = "",
                    UseColumnTextForButtonValue = true,
                };
                dataGridView1.Columns.Add(editcol);
            }
        }
        private void bersih()
        {
            textBox1.Text = string.Empty;
            isibarang();
            isitoko();
            textBox1.Select();
        }
        private bool validasi()
        {
            if (textBox1.Text == string.Empty ||
                comboBox1.SelectedValue == null ||
                comboBox2.SelectedValue == null)
            {
                Alert.error("Silahkan isi semua data terlebih dahulu");
                return false;
            }
            return true;
        }
        private void showselectedItem(DataGridViewRow row)
        {
            //selectedId = (int)row.Cells["IdBarang"].Value;
            selectedId = (int)row.Cells["Id"].Value;
            var data = db.Mutasis.FirstOrDefault(x => x.Id == selectedId);
            comboBox1.SelectedValue = data.TokoId;
            comboBox2.SelectedValue = data.BarangId;
            textBox1.Text= data.Jumlah.ToString();
            button1.Text = "Edit";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                if (selectedId == null)
                {
                    var mutasi=new Mutasi();
                    mutasi.TokoId = TokoId;
                    mutasi.BarangId = BarangId;
                    mutasi.Jumlah=Convert.ToInt32(textBox1.Text);
                    mutasi.Tanggal=DateTime.Now;

                    int jumlah = Convert.ToInt32(textBox1.Text);
                    var stokbarang = db.StokBarangs.FirstOrDefault(x => x.Id == StokBarangId);
                    if (stokbarang != null)
                    {
                        if (jumlah > stokbarang.Jumlah)
                        {
                            Alert.info("Maaf stok tidak mencukupi!");
                            return;
                        }
                        else
                        {
                            stokbarang.Jumlah -= jumlah;
                            db.SaveChanges();
                        }
                    }

                    //var stoktoko=db.StokTokoes.FirstOrDefault(x => x.Id==StokTokoId && x.BarangId==BarangId && x.TokoId==TokoId);
                    var stoktoko = db.StokTokoes.FirstOrDefault(x => x.BarangId == BarangId && x.TokoId == TokoId
);
                    if (stoktoko != null)
                    {
                        stoktoko.Jumlah += jumlah;
                        db.SaveChanges();
                    }
                    else
                    {
                        var databaru = new StokToko();
                        databaru.TokoId = TokoId;
                        databaru.BarangId = BarangId;
                        databaru.Jumlah = jumlah;
                        db.StokTokoes.Add(databaru);
                        db.SaveChanges() ;
                    }
                    
                    db.Mutasis.Add(mutasi);
                    db.SaveChanges();
                    Alert.info("Mutasi Sukses");
                    Showdata();
                    bersih();
                }
                else
                {
                    var mutasi = db.Mutasis.FirstOrDefault(x => x.Id == selectedId);
                    mutasi.TokoId = TokoId;
                    mutasi.BarangId = BarangId;
                    mutasi.Jumlah = Convert.ToInt32(textBox1.Text);
                    mutasi.Tanggal = DateTime.Now;

                    int jumlah = Convert.ToInt32(textBox1.Text);
                    var stokbarang = db.StokBarangs.FirstOrDefault(x => x.Id == StokBarangId);
                    if (stokbarang != null)
                    {
                        if (jumlah > stokbarang.Jumlah)
                        {
                            Alert.info("Maaf stok tidak mencukupi!");
                            return;
                        }
                        else
                        {
                            stokbarang.Jumlah -= jumlah;
                            db.SaveChanges();
                        }
                    }

                    var stoktoko = db.StokTokoes.FirstOrDefault(x => x.Id == StokTokoId && x.BarangId == BarangId && x.TokoId == TokoId);
                    if (stoktoko != null)
                    {
                        stoktoko.Jumlah += jumlah;
                        db.SaveChanges();
                    }
                    else
                    {
                        var databaru = new StokToko();
                        databaru.TokoId = TokoId;
                        databaru.BarangId = BarangId;
                        databaru.Jumlah = jumlah;
                        db.StokTokoes.Add(databaru);
                        db.SaveChanges();
                    }

                    db.SaveChanges();
                    Alert.info("Mutasi Sukses");
                    Showdata();
                    bersih();
                }
            }
        }
        
        private void DataMutasi_Load(object sender, EventArgs e)
        {
            isibarang();
            isitoko();
            Showdata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            //selectedId = (int)row.Cells["IdBarang"].Value;
            selectedId = (int)row.Cells["Id"].Value;
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index)
            {
                showselectedItem(row);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index)
            {
                if (Alert.confirm("Yakin Ingin Menghapus data ini?") == DialogResult.Yes)
                {
                    var data = db.Mutasis.FirstOrDefault(x => x.Id == selectedId);
                    db.Mutasis.Remove(data);
                    db.SaveChanges();
                    Showdata();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var item=(dynamic)comboBox1.SelectedItem;
            //TokoId=item.TokoID;
            //StokTokoId=item.StokTokoID;
            if (comboBox1.SelectedItem != null)
            {
                var item = (dynamic)comboBox1.SelectedItem;
                TokoId = item.TokoID;
                StokTokoId = null;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (dynamic)comboBox2.SelectedItem;
            BarangId = item.BarangID;
            StokBarangId = item.StokBarangID;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox4.Text != null)
            {
                var data = (from n in db.Mutasis
                            join t in db.Tokoes on n.TokoId equals t.Id
                            join b in db.Barang1 on n.BarangId equals b.Id
                            where b.Barang.Contains(textBox4.Text)
                            select new
                            {
                                n.Id,
                                BarangId = n.BarangId,
                                TokoId = n.TokoId,
                                Barang = b.Barang,
                                Toko = t.Toko1,
                                HargaBarang = b.HargaJual,
                                Jumlah = n.Jumlah,
                                Tanggal = n.Tanggal
                            }).ToList();
                dataGridView1.DataSource = data;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns["BarangId"].Visible = false;
                dataGridView1.Columns["TokoId"].Visible = false;

                if (!dataGridView1.Columns.Contains("Edit"))
                {
                    DataGridViewButtonColumn editcol = new DataGridViewButtonColumn()
                    {
                        Name = "edit",
                        Text = "Edit",
                        HeaderText = "",
                        UseColumnTextForButtonValue = true,
                    };
                    dataGridView1.Columns.Add(editcol);
                }
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
