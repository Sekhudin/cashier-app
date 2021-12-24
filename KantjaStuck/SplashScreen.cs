using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace KantjaStuck
{
    public partial class SplashScreen : Form
    {
        FungsiMethod fn = new FungsiMethod();
        Komponen kom = new Komponen();
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            progresBar.Width = 0;
            progresStatus.Text = "0%";
            progresStatus.ForeColor = kom.primary;
            progresBar.BackColor = kom.primary;
            boxLogo.BackColor = kom.primary;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progresBar.Width += 10;

            int nilaiMax = 450;
            int persen = (progresBar.Width + 50) / 5;
            string persenText = persen.ToString();
            progresStatus.Text = persenText + "%";

            if (progresBar.Width == nilaiMax)
            {
                timer1.Stop();
                progresStatus.Text = "99%";
                bool cekKOneksi = fn.Connect_DB();
                if (cekKOneksi==true)
                {

                    Login l = new Login();
                    l.Show();
                    this.Hide();
                }
                else
                {
                    progresStatus.Text = "Gagal!";
                    progresStatus.ForeColor = Color.Red;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
            if (Opacity == 1)
            {
                timer2.Stop();
            }
        }
    }
}
