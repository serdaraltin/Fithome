using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
namespace Fithome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Application.StartupPath+@"\data\data.mdb");
        public void antrenman_gunlugu()
        {
            try
            {
            baglan.Open();
            listView1.Items.Clear();
            OleDbCommand komut = new OleDbCommand("select *from antrenman", baglan);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem itemler = new ListViewItem(oku["gun"].ToString());
                itemler.SubItems.Add(oku["omuz"].ToString());
                itemler.SubItems.Add(oku["gogus"].ToString());
                itemler.SubItems.Add(oku["sirt"].ToString());
                itemler.SubItems.Add(oku["biceps"].ToString());
                itemler.SubItems.Add(oku["triceps"].ToString());
                itemler.SubItems.Add(oku["karin"].ToString());
                itemler.SubItems.Add(oku["bacak"].ToString());
                itemler.SubItems.Add(oku["protein"].ToString());
                itemler.SubItems.Add(oku["kosu"].ToString());
                itemler.SubItems.Add(oku["sorun"].ToString());
                listView1.Items.Add(itemler);
                ListView lw = this.listView1;
                foreach (ListViewItem item in lw.Items)
                {
                    for(int i=0; i<=item.SubItems.Count-1;i++)
                    {
                           item.UseItemStyleForSubItems = false;
                           if (item.SubItems[i].Text.IndexOf("✗") > -1 && i <= 7 && item.SubItems[i].Text != "")
                               item.SubItems[i].BackColor = Color.MistyRose;
                           else if (item.SubItems[i].Text.IndexOf("✓") == -1 && i <= 7 && item.SubItems[i].Text != "")
                               item.SubItems[i].BackColor = Color.LightGreen;
                           else if (i == 8 && item.SubItems[i].Text != "")
                               item.SubItems[i].BackColor = Color.FromArgb(255, 255, 160);
                           else if (i == 9 && item.SubItems[i].Text != "")
                               item.SubItems[i].BackColor = Color.LightBlue;
                           else if (i >= 10 && item.SubItems[i].Text != "")
                               item.UseItemStyleForSubItems = true;                         
                    }
                    string gun = item.Text.Remove(0, item.Text.IndexOf("-") + 1);

                    if (gun == "Pazartesi")
                        item.BackColor = Color.FromArgb(80, 90, 90);
                    if (item.Text.IndexOf("Salı") > 0)
                        item.BackColor = Color.FromArgb(90, 100, 110);
                    if (item.Text.IndexOf("Çarşamba") > 0)
                        item.BackColor = Color.FromArgb(120, 130, 140);
                    if (item.Text.IndexOf("Perşembe") > 0)
                        item.BackColor = Color.FromArgb(150, 160, 170);
                    if (item.Text.IndexOf("Cuma") > 0)
                        item.BackColor = Color.FromArgb(180, 190, 200);
                    if (item.Text.IndexOf("Cumartesi") > 0)
                        item.BackColor = Color.FromArgb(210, 220, 230);
                    if (gun=="Pazar")
                        item.BackColor = Color.FromArgb(240, 250, 250);
                }
            }
            gun_label.Text = "ANTRENMAN " + (listView1.Items.Count + 1).ToString();
            baglan.Close();
        }
         catch(Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Fithome", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            antrenman_gunlugu();
            gun_label.Text = "ANTRENMAN " + (listView1.Items.Count + 1).ToString();
        }

        private void yenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kaydet_button_Click(object sender, EventArgs e)
        {
            try
            {
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("insert into antrenman (gun,omuz,gogus,sirt,biceps,triceps,karin,bacak,protein,kosu) values(@gun,@omuz,@gogus,@sirt,@biceps,@triceps,@karin,@bacak,@protein,@kosu)", baglan);
            string gun = DateTime.Now.ToLongDateString();
            for (int i = 0; i <= gun.Length - 1; i++)
            {
                gun = gun.Remove(0, gun.IndexOf(" ") + 1);
            }
                if(listView1.Items.Count==-1)
                   komut.Parameters.AddWithValue("@gun","1-" + gun);
                else
                    komut.Parameters.AddWithValue("@gun", (listView1.Items.Count+1).ToString() + "-" + gun);
            if(omuz_ok.Checked==true)
                komut.Parameters.AddWithValue("@omuz", omuz_tekrar.Value.ToString() + "x" + omuz_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@omuz", "✗");

            if (gogus_ok.Checked == true)
                komut.Parameters.AddWithValue("@gogus", gogus_tekrar.Value.ToString() + "x" + gogus_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@gogus", "✗");

            if (sirt_ok.Checked == true)
                komut.Parameters.AddWithValue("@sirt", sirt_tekrar.Value.ToString() + "x" + sirt_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@sirt", "✗");

            if (biceps_ok.Checked == true)
                komut.Parameters.AddWithValue("@biceps", biceps_tekrar.Value.ToString() + "x" + biceps_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@biceps", "✗");

            if (triceps_ok.Checked == true)
                komut.Parameters.AddWithValue("@triceps", triceps_tekrar.Value.ToString() + "x" + triceps_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@omuz", "✗");

            if (karin_ok.Checked == true)
                komut.Parameters.AddWithValue("@karin", karin_tekrar.Value.ToString() + "x" + karin_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@karin", "✗");

            if (bacak_ok.Checked == true)
                komut.Parameters.AddWithValue("@bacak", bacak_tekrar.Value.ToString() + "x" + bacak_set.Value.ToString());
            else
                komut.Parameters.AddWithValue("@bacak", "✗");

            komut.Parameters.AddWithValue("@protein", protein_takviyesi.Text);

            if (kosu_ok.Checked == true)
                komut.Parameters.AddWithValue("@kosu", kosu_km.Value.ToString()+ "," + kosu_mt.Value.ToString()+" km");
            else
                komut.Parameters.AddWithValue("@kosu", "✗");

            komut.ExecuteNonQuery();
            baglan.Close();
            antrenman_gunlugu();
        }
             catch(Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Fithome", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                OleDbCommand komut = new OleDbCommand("Delete from antrenman where gun='" + listView1.SelectedItems[0].SubItems[0].Text + "'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                antrenman_gunlugu();
            }
            catch(Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Fithome", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void sorun_button_Click(object sender, EventArgs e)
        {
            if (sorun_text.Text != "")
            {
                try
                {
                baglan.Open();
                OleDbCommand komut = new OleDbCommand("insert into antrenman (gun,sorun) values(@gun,@sorun)", baglan);
                string gun = DateTime.Now.ToLongDateString();
                for (int i = 0; i <= gun.Length - 1; i++)
                {
                    gun = gun.Remove(0, gun.IndexOf(" ") + 1);
                }
                komut.Parameters.AddWithValue("@gun", (listView1.Items.Count - 1).ToString() + "-" + gun);
                komut.Parameters.AddWithValue("@sorun", sorun_text.Text);
                komut.ExecuteNonQuery();
                baglan.Close();
                antrenman_gunlugu();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message.ToString(), "Fithome", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                baglan.Open();
                OleDbCommand komut = new OleDbCommand("insert into antrenman (gun,sorun) values(@gun,@sorun)", baglan);
                string gun = DateTime.Now.ToLongDateString();
                for (int i = 0; i <= gun.Length - 1; i++)
                {
                    gun = gun.Remove(0, gun.IndexOf(" ") + 1);
                }
                komut.Parameters.AddWithValue("@gun", (listView1.Items.Count - 1).ToString() + "-" + gun);
                komut.Parameters.AddWithValue("@sorun", "Belirtilmemiş");
                komut.ExecuteNonQuery();
                baglan.Close();
                antrenman_gunlugu();
                 }
         catch(Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Fithome", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0].SubItems[0].Text != null && listView1.SelectedIndices[0] > -1)
                    contextMenuStrip1.Enabled = true;
                else
                    contextMenuStrip1.Enabled = false;
            }
            catch
            {
                contextMenuStrip1.Enabled = false;
            }
        }

        private void temizle_button_Click(object sender, EventArgs e)
        {
            
        }


    }
}
