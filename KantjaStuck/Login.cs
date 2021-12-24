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
    public partial class Login : Form
    {
        public static string setValueUsername = "";
        public static string setValuePassword = "";

        Komponen kom = new Komponen();
        FungsiMethod fn = new FungsiMethod();
        public Login()
        {
            InitializeComponent();
            panel1.BackColor = kom.primary;
            panelUsername.BackColor = kom.primary;
            panelPassword.BackColor = kom.primary;
            buttonLogin.BackColor = kom.primary;
            buttonRegister.FlatAppearance.BorderColor = kom.primary;
            buttonRegister.ForeColor = kom.primary;
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            kom.button_Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            kom.button_Minimize(this);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txUsername.Text = "Username";
            txPassword.Text = "Password";

            txPesanError.Text = "";
            txPassword.UseSystemPasswordChar = false;
            
        }
        private void username_Clicked(object sender, EventArgs e)
        {
            if (txUsername.Text == "Username")
            {
                txUsername.Clear();
            }


        }

        private void password_Clicked(object sender, EventArgs e)
        {
            if (txPassword.Text == "Password")
            {
                txPassword.Clear();
                txPassword.UseSystemPasswordChar = true;
            }
        }

        private void txUsername_TextChanged(object sender, EventArgs e)
        {

            if (txUsername.TextLength < 1)
            {
                txPesanError.Text = "Username wajib diisi!";
                txPesanError.ForeColor = Color.Red;
                panelUsername.BackColor = Color.Red;
                
            }
            else
            {
                txPesanError.Text = "";
                txPesanError.ForeColor = Color.Black;
                panelUsername.BackColor = kom.primary;
            }
        }

        private void username_Leave(object sender, EventArgs e)
        {
            if(txUsername.Text == "")
            {
                txUsername.Text = "Username";
                panelUsername.BackColor = Color.Red;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (txPassword.Text == "")
            {
                txPassword.Text = "Password";
                panelPassword.BackColor = Color.Red;
                txPassword.UseSystemPasswordChar = false;
            }
        }

        private void txPassword_TextChanged(object sender, EventArgs e)
        {

            if (txPassword.TextLength < 1)
            {
                txPesanError.Text = "Password wajib diisi!";
                txPesanError.ForeColor = Color.Red;
                panelPassword.BackColor = Color.Red;
                txPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txPesanError.Text = "";
                txPesanError.ForeColor = Color.Black;
                panelPassword.BackColor = kom.primary;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.Show();
            this.Hide();
        }

        private void ckLihatPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (ckLihatPassword.Checked==true)
            {
                txPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txPassword.UseSystemPasswordChar = true;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            setValueUsername = txUsername.Text;
            setValuePassword = txPassword.Text;
            string namaProcedure = "KasirLogin";

            SqlParameter Username = new SqlParameter("@Username", SqlDbType.VarChar);
            SqlParameter Password = new SqlParameter("@Password", SqlDbType.VarChar);

            SqlParameter[] parameters = { Username, Password };

            if ((txUsername.Text != "" && txUsername.Text != "Username") && (txPassword.Text != "" && txPassword.Text !="Password"))
            {
                Username.Value = txUsername.Text;
                Password.Value = fn.Crypt2(txPassword.Text);

                fn.LOGIN(namaProcedure, parameters,this);
            }
            else
            {
                MessageBox.Show("Masukan username dan password anda!");
                panelUsername.BackColor = Color.Red;
                txUsername.Clear();
                txUsername.Focus();
            }

        }

        private void timer_fadeIn_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
            if (Opacity == 1)
            {
                timer_fadeIn.Stop();
            }
        }
    }
}
