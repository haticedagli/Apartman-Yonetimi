using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Collections;

namespace AptYonetimi
{
    public class DatabaseService
    {

        public OleDbConnection ac = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=site.mdb;Persist Security Info=False;");
        public string sorgu, sorgu1;
        public string dolu;
        public OleDbCommand komut;
        public OleDbDataReader oku;

        public DatabaseService()
        {

        }

        public ArrayList getValueList(String query)
        {
            try
            {
                ArrayList values = new ArrayList();
                ac.Open();
                sorgu = string.Format(query);
                komut = new OleDbCommand(sorgu, ac);
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    values.Add(oku[0]);
                }
                return values;
            }finally{
                ac.Close();
            }
        }

        public int getIntValue(String query)
        {
            try
            {
                ac.Open();
                sorgu = string.Format(query);
                komut = new OleDbCommand(sorgu, ac);
                oku = komut.ExecuteReader();
                oku.Read();
                return int.Parse(string.Format("{0}", oku[0]));
            }
            finally
            {
                ac.Close();
            }
        }

    }
}
