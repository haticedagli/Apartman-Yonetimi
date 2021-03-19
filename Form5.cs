using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AptYonetimi
{

    public partial class Form5 : Form
    {
        public OleDbConnection ac = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=site.mdb;Persist Security Info=False;");
        public string sorgu;
        public OleDbCommand komut;

        public Form5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ac.Open();
                sorgu = string.Format("insert into odeme_turu(odeme_adi,tutar) values ('{0}','{1}')", textBox1.Text,textBox2.Text );
                komut = new OleDbCommand(sorgu, ac);
                komut.ExecuteNonQuery();
                MessageBox.Show("kayıt başarılı bir şekilde eklenmiştir...");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata oluştu.. "+hata.Message);
            }
            finally
            {
                ac.Close();
            }
        }
    }
}
