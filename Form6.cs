using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AptYonetimi
{
    public partial class Form6 : Form
    {
        public OleDbConnection ac = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=site.mdb;Persist Security Info=False;");
        public string sorgu;
        public OleDbCommand komut;
        public OleDbDataReader oku;
        public DatabaseService service = new DatabaseService();
        public Form6()
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
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            try
            { /*
                ac.Open();
                sorgu = string.Format("select (daire_sayisi) from blok where blok_adi='{0}'", badi);
                komut = new OleDbCommand(sorgu, ac);
                oku = komut.ExecuteReader();
                oku.Read();
                int dsayisi = Convert.ToInt32(oku[0]);
                for (int i = 1; i <= dsayisi; i++)
                {
                    comboBox2.Items.Add(i);
                }*/
                comboBox2.Items.AddRange(service.getValueList(string.Format("select (daire_sayisi) from blok where blok_adi='{0}'", badi)).ToArray());
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

        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.AddRange(service.getValueList("select (blok_adi) from blok order by blok_adi asc").ToArray());
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata oldu içeriği... : " + hata.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ac.Open();
                DataTable dt = new DataTable();
                sorgu = String.Format("select yil,ay,odeme_adi,odeme_tutar from odeme where blok_adi='" + comboBox1.Text + "'and daire_no='" + comboBox2.Text + "' order by yil,ay asc");
                OleDbDataAdapter da = new OleDbDataAdapter(sorgu,ac);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
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
