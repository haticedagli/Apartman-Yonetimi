using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AptYonetimi
{
    public partial class Form2 : Form
    {
        public OleDbConnection ac = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=site.mdb;Persist Security Info=False;");
        public string sorgu,sorgu1;
        public string dolu;
        public OleDbCommand komut,komut1;
        public OleDbDataReader oku;
        public DatabaseService service = new DatabaseService();


        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string badi = comboBox1.Text;
            comboBox2.Items.Clear();
            try
            {
                int value = service.getIntValue(string.Format("select (daire_sayisi) from blok where blok_adi='{0}'", badi));
                for(int i=1;i <value+1; i++)
                {
                    comboBox2.Items.Add(i);
                }
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
                sorgu = string.Format("select * from kisi inner join Daire on Daire.daire_no=kisi.daire_no where kisi.blok_adi='{0}' and kisi.daire_no='{1}'",comboBox1.Text,comboBox2.Text);
                komut = new OleDbCommand(sorgu, ac);
                oku = komut.ExecuteReader();
                oku.Read();
              //MessageBox.Show(sorgu);
                if (oku.HasRows == false)
                {
                    MessageBox.Show("Bu dairede kimse oturmamakta...");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    checkBox1.Checked = false;
                    comboBox3.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Bu dairede bir kişi oturmakta güncelleyebilirsiniz...");
                    textBox1.Text = oku[1].ToString();
                    textBox2.Text = oku[2].ToString();
                    textBox3.Text = oku[3].ToString();
                    textBox4.Text = oku[4].ToString();
                    textBox5.Text = oku[5].ToString();
                    textBox6.Text = oku[9].ToString();
                    comboBox3.Text = oku[10].ToString();
                    if (Convert.ToString(oku[11]) == "Dolu")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
            } catch (Exception hata)
            {
                MessageBox.Show("Hata oldu İçeriği...: "+hata.Message);
            } finally
            {
                ac.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked == true)
            {
                dolu = "Dolu";
            }
            else
            {
                dolu = "Boş";
            }
            try
            {
                ac.Open();
                sorgu = string.Format("insert into kisi(daire_no,kimlikno,adi,soyadi,telno,IBAN,blok_adi) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') ", comboBox2.Text, textBox1.Text,
               textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Text);
                sorgu1=string.Format("insert into Daire (daire_no,blok_adi,kisi_say,durum,dolu_bos) values('{0}','{1}','{2}','{3}','{4}')", comboBox2.Text,  
                comboBox1.Text,textBox6.Text,comboBox3.Text,dolu);
                komut = new OleDbCommand(sorgu,ac);
                komut.ExecuteNonQuery();
                komut1 = new OleDbCommand(sorgu1, ac);
                komut1.ExecuteNonQuery();
                MessageBox.Show("kayıt başarılı bir şekilde eklenmiştir...");
            } catch (Exception hata)
            {
                MessageBox.Show("Bir hata oldu içeriği.. : " + hata.Message);
            } finally
            {
                ac.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dolu = "Dolu";
            }
            else
            {
                dolu = "Boş";
            }
            try
            {
                ac.Open();
                sorgu = string.Format("update kisi set kimlikno='" + textBox1.Text + "',adi='" + textBox2.Text + "',soyadi='" + textBox3.Text + "',telno='" + textBox4.Text + "',IBAN='" + textBox5.Text + "'where blok_adi='" + comboBox1.Text + "'and daire_no='" + comboBox2.Text + "'");
                komut = new OleDbCommand(sorgu, ac);
                komut.ExecuteNonQuery();
                sorgu1 = string.Format("update Daire set kisi_say='" + textBox6.Text + "',durum='" + comboBox3.Text + "',dolu_bos='" + dolu + "'where blok_adi='" + comboBox1.Text + "'and daire_no='" + comboBox2.Text + "'");
                komut1 = new OleDbCommand(sorgu1, ac);
                komut1.ExecuteNonQuery();

            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata oluştu..: " + hata.Message);
            }
            finally
            {
                ac.Close();
            }

        }
    }
}