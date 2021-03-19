using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace AptYonetimi
{
    public partial class Form4 : Form
    {
        public OleDbConnection ac = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=site.mdb;Persist Security Info=False;");
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string badi = textBox1.Text;
            string dnum = textBox2.Text;
            try
            {
                ac.Open();
                //MessageBox.Show("Veritabanı bağlantısı sağlandı...");
                string sorgu = string.Format("insert into blok (blok_adi,daire_sayisi) values ('{0}','{1}')", badi, dnum);
                OleDbCommand komut = new OleDbCommand(sorgu, ac);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarılı bir şekilde gerçekleştirilmiştir...");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Oldu içeriği.. : " + hata.Message);
            }
            finally
            {
                ac.Close();
                this.Close();
            }
            
        }
    }
}
