﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Veri_Tabanlı_Parti_Seçim_Grafik_İstatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-HLMQ9PKG;Initial Catalog=DBSECIMPROJE;Integrated Security=True");


        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            // İlçe adlarını comboboxa çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);

            }
            baglanti.Close();
            //Grafiğe toplam sonuçları getirme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT SUM(APARTI), SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) FROM TBLILCE", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr2[4]);
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT * FROM TBLILCE WHERE ILCEAD=@P1",baglanti);
            komut3.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr= komut3.ExecuteReader();   
            while (dr.Read())
            {
                progressBar1.Value =int.Parse( dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                LblA.Text = dr[2].ToString();
                LblB.Text = dr[3].ToString();
                LblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();
           



            }
            baglanti.Close();
                

        }
    }
}
