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
    public partial class Form3 : Form
    {
        int tarih = int.Parse(DateTime.Now.Year.ToString());
        public OleDbConnection ac = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=site.mdb;Persist Security Info=False;");
        public string sorgu, sorgu1;
        public string dolu, deneme;
        public OleDbCommand komut, komut1;
        public OleDbDataReader oku,oku1,oku2;
        public DatabaseService service = new DatabaseService();
        public Form3()
        {
            InitializeComponent();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string badi = comboBox1.Text;
            comboBox2.Items.Clear();
            try
            {
                comboBox2.Items.AddRange(service.getValueList(string.Format("select (daire_sayisi) from blok where blok_adi='{0}'", badi)).ToArray());
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata oldu içeriği... : " + hata.Message);
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ac.Open();
                string sorgu = string.Format("insert into odeme (blok_adi,daire_no,odeme_adi,odeme_tutar,yil,ay) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    comboBox1.Text,comboBox2.Text,comboBox5.Text,deneme,comboBox3.Text,comboBox4.Text);
                komut = new OleDbCommand(sorgu, ac);
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
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ac.Open();
                sorgu1 = string.Format("select tutar from odeme_turu where odeme_adi ='" +comboBox5.Text+"'");
                komut1 = new OleDbCommand(sorgu1, ac);
                oku2 = komut1.ExecuteReader();
                oku2.Read();
                MessageBox.Show("deger" + oku2[0]);
                deneme = oku2[0].ToString();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Oldu içeriği.. : " + hata.Message);
            }
            finally
            {
                ac.Close();
            }
        }

        private void yeniÖdemeTürüEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Show();
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Text="";
            comboBox4.Text = "";
            comboBox5.Items.Clear();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            for (int i = tarih-10; i <= tarih+5; i++)
            {
                comboBox3.Items.Add(i);
            }

            try
            {
                ac.Open();
                sorgu = string.Format("select (blok_adi) from blok order by blok_adi asc");
                sorgu1 = string.Format("select (odeme_adi) from odeme_turu order by odeme_adi asc");
                komut = new OleDbCommand(sorgu, ac);
                komut1 = new OleDbCommand(sorgu1,ac);
                oku = komut.ExecuteReader();
                oku1 = komut1.ExecuteReader();

                while (oku.Read())
                {
                    comboBox1.Items.Add(oku[0]);
                }
                while (oku1.Read())
                {
                    comboBox5.Items.Add(oku1[0]);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata oldu içeriği... : " + hata.Message);
            }
            finally
            {
                ac.Close();
            }

        }
    }
}
