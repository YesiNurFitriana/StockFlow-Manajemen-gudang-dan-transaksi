using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GudangBarang
{
    public partial class Form1 : Form
    {
        private readonly GudangEntities2 db = new GudangEntities2();
        int? selectedId;
        int? idUser;
        int? idtoko;
        public Form1()
        {
            InitializeComponent();
        }
        private bool validasi()
        {
            if(textBox1.Text==string.Empty||
                textBox2.Text == string.Empty)
            {
                Alert.error("Silahkan isi semua data terlebih dahulu");
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                string username=textBox1.Text;
                string password=textBox2.Text;
                var data = db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
                if (data != null)
                {
                    if (data.Role == "admin")
                    {
                        idUser = data.Id;
                        MainAdmin main =new MainAdmin(idUser);
                        main.ShowDialog();
                    }
                    else
                    {
                        idtoko = data.TokoId;
                        MainToko main=new MainToko(idtoko);   
                        main.ShowDialog();
                    }
                }
                else
                {
                    Alert.error("Maaf data tidak ditemukan!");
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bersih();
        }
        private void bersih()
        {
            textBox1.Text = string.Empty;
            
            textBox2.Text = string.Empty;
            
            textBox1.Select();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bersih();
            textBox2.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar=false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
