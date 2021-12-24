using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace KantjaStuck
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            cbPilihAvatar.Items.AddRange(avatar);

            txNama.Text = "Nama";
            txTelepon.Text = "Telepon";
            txUsername.Text = "Username";
            txPassword.Text = "Password";
            txCekPassword.Text = "Konfirmasi password";
            txPesanError.Text = "";

            btChangePicture.BackColor = Color.Transparent;
            btChangePicture.ForeColor = kom.primary;
            bt_Upload.BackColor = kom.primary;
            bt_Upload.ForeColor = Color.White;
            bt_PickAvatar.BackColor = kom.primary;
            bt_PickAvatar.ForeColor = Color.White;
            //wrapperButtonProfile.BackColor = kom.primary;
            wrapperButtonProfile.Visible = false;
            cbPilihAvatar.Visible = false;

            panelNama.BackColor = kom.primary;
            panelTelepon.BackColor = kom.primary;
            panelUsername.BackColor = kom.primary;
            panelPassword.BackColor = kom.primary;
            panelCekPassword.BackColor = kom.primary;
            panelHeader.BackColor = kom.primary;
            buttonRegister.BackColor = kom.primary;
            buttonLogin.FlatAppearance.BorderColor = kom.primary;
            buttonLogin.ForeColor = kom.primary;
            txAutoID.BackColor = kom.primary;
            txAutoID.ForeColor = Color.White;

            txPassword.UseSystemPasswordChar = false;
            txCekPassword.UseSystemPasswordChar = false;

        }
        Komponen kom = new Komponen();
        OperasiDB odb = new OperasiDB();
        FungsiMethod fn = new FungsiMethod();
        private void Register_Load(object sender, EventArgs e)
        {
            //====batas suci
            txAutoID.Text = fn.getAutoID("Kasir");

        }
        private void btClose_Click(object sender, EventArgs e)
        {
            kom.button_Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            kom.button_Minimize(this);
        }
        //=========================================================== GANTI FOTO PROFILE ===================================================
        private void btChangePicture_Click(object sender, EventArgs e)
        {
            btChangePicture.Visible = false;
            wrapperButtonProfile.Visible = true;
            cbPilihAvatar.Visible = false;
        }
        private void bt_Upload_Click(object sender, EventArgs e)
        {
            wrapperButtonProfile.Visible = false;
            btChangePicture.Visible = true;
            //string keyValue = txAutoID.Text.ToString();
            //string[] rincianTabel = { "kasir", "gambarKasir", "idKasir", keyValue };
            fn.uploadFoto(boxProfil,lbJenisProfile,"Profil");   
        }

        private void bt_PickAvatar_Click(object sender, EventArgs e)
        {
            wrapperButtonProfile.Visible = false;
            cbPilihAvatar.Visible = true;
            btChangePicture.Visible = true;

            cbPilihAvatar.BackColor = kom.primary;
            cbPilihAvatar.ForeColor = Color.White;
            cbPilihAvatar.Text = "-Avatar-";
        }
        private string[] avatar = { "beard", "child", "clown", "hacker", "hero", "malika", "old_Lady", "pirate", "teacher", "woman" };
        private void cbPilihAvatar_SelectedIndexChanged(object sender, EventArgs e)
        {
            fn.pickAvatar(cbPilihAvatar, boxProfil, lbJenisProfile);
        }
        //============================================================ KETIKA TEXTBOX DI EDIT =============================================
        private void txNama_Click(object sender, EventArgs e)
        {
            if (txNama.Text == "Nama")
            {
                txNama.Clear();
            }
        }

        private void txNama_TextChanged(object sender, EventArgs e)
        {
            if (txNama.Text=="")
            {
                txPesanError.Text = "Nama wajib diisi!";
                txPesanError.ForeColor = Color.Red;
                panelNama.BackColor = Color.Red;

            }
            else
            {
                txPesanError.Text = "";
                txPesanError.ForeColor = Color.Black;
                panelNama.BackColor = kom.primary;
            }
        }

        private void txNama_Leave(object sender, EventArgs e)
        {
            if(txNama.Text == "")
            {
                txNama.Text = "Nama";
                panelNama.BackColor = Color.Red;
            }
        }

        private void txTelepon_Click(object sender, EventArgs e)
        {
            if (txTelepon.Text == "Telepon")
            {
                txTelepon.Clear();
            }
        }

        private void txTelepon_TextChanged(object sender, EventArgs e)
        {
            bool inputAngka = Information.IsNumeric(txTelepon.Text);
            if (txTelepon.Text == "" || inputAngka==false)
            {
                if (txTelepon.Text == "")
                {
                    txPesanError.Text = "Telepon wajib diisi!";
                    txPesanError.ForeColor = Color.Red;
                    panelTelepon.BackColor = Color.Red;
                }
                else
                {
                    txPesanError.Text = "Telepon harus berupa angka!";
                    txPesanError.ForeColor = Color.Red;
                    panelTelepon.BackColor = Color.Red;
                }

            }
            else
            {
                txPesanError.Text = "";
                txPesanError.ForeColor = Color.Black;
                panelTelepon.BackColor = kom.primary;
            }
        }

        private void txTelepon_Leave(object sender, EventArgs e)
        {
            if (txTelepon.Text == "")
            {
                txTelepon.Text = "Telepon";
                panelTelepon.BackColor = Color.Red;
            }
        }

        private void txUsername_Click(object sender, EventArgs e)
        {
            if (txUsername.Text == "Username")
            {
                txUsername.Clear();
            }
        }

        private void txUsername_TextChanged(object sender, EventArgs e)
        {
            if (txUsername.Text=="")
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

        private void txUsername_Leave(object sender, EventArgs e)
        {
            if (txUsername.Text == "")
            {
                txUsername.Text = "Username";
                panelUsername.BackColor = Color.Red;
            }
        }

        private void txPassword_Click(object sender, EventArgs e)
        {
            if (txPassword.Text == "Password")
            {
                txPassword.Clear();
                txPassword.UseSystemPasswordChar = false;
            }
        }
        private void txPassword_TextChanged(object sender, EventArgs e)
        {
            txPassword.UseSystemPasswordChar = true;
            if (txPassword.Text != "")
            {
                txPesanError.ForeColor = Color.Black;
                panelPassword.BackColor = kom.primary;
                if (txPassword.Text == txCekPassword.Text)
                {
                    txPesanError.Text = "Password match!";
                    txPesanError.ForeColor = Color.Black;
                    panelPassword.BackColor = kom.primary;
                }
                else
                {
                    txPesanError.Text = "Password Anda tidak sama!";
                    txPesanError.ForeColor = Color.Red;
                    panelCekPassword.BackColor = Color.Red;
                }

            }
            else
            {
                txPesanError.Text = "Masukan Password Anda!";
                txPesanError.ForeColor = Color.Red;
                panelPassword.BackColor = Color.Red;
            }
        }

        private void txPassword_Leave(object sender, EventArgs e)
        {
            if (txPassword.Text == "")
            {
                txPassword.Text = "Password";
                panelPassword.BackColor = Color.Red;
                txPassword.UseSystemPasswordChar = false;
            }
        }

        private void txCekPassword_Click(object sender, EventArgs e)
        {
            if (txCekPassword.Text == "Konfirmasi password")
            {
                txCekPassword.Clear();
                txCekPassword.UseSystemPasswordChar = false;
            }
        }

        private void txCekPassword_TextChanged(object sender, EventArgs e)
        {
            if (txCekPassword.Text !="")
            {
                txCekPassword.UseSystemPasswordChar = true;
                if (txCekPassword.Text == txPassword.Text)
                {
                    txPesanError.Text = "Password match!";
                    txPesanError.ForeColor = Color.Black;
                    panelCekPassword.BackColor = kom.primary;
                }
                else
                {
                    txPesanError.Text = "Password Anda tidak sama!";
                    txPesanError.ForeColor = Color.Red;
                    panelCekPassword.BackColor = Color.Red;
                }
                
            }
            else
            {
                txPesanError.Text = "Konfirmasi password Anda!";
                txPesanError.ForeColor = Color.Red;
                panelCekPassword.BackColor = Color.Red;
                txCekPassword.UseSystemPasswordChar = true;
            }
        }

        private void txCekPassword_Leave(object sender, EventArgs e)
        {
            if (txCekPassword.Text == "")
            {
                txCekPassword.Text = "Konfirmasi password";
                panelCekPassword.BackColor = Color.Red;
                txCekPassword.UseSystemPasswordChar = false;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string namaProcedure = "DaftarKasir";
            SqlParameter id = new SqlParameter("@id", SqlDbType.VarChar);
            SqlParameter gambar = new SqlParameter("@gambar", SqlDbType.VarChar);
            SqlParameter nama = new SqlParameter("@nama", SqlDbType.VarChar);
            SqlParameter telepon = new SqlParameter("@telp", SqlDbType.VarChar);
            SqlParameter Username = new SqlParameter("@uname", SqlDbType.VarChar);
            SqlParameter Password = new SqlParameter("@pass", SqlDbType.VarChar);
            SqlParameter statusAdmin = new SqlParameter("@status", SqlDbType.VarChar);
            SqlParameter tipeProfile = new SqlParameter("@tipeGambar", SqlDbType.VarChar);

            SqlParameter[] parameters = { id, gambar, nama, telepon, Username, Password, statusAdmin, tipeProfile };

            if ((txNama.Text != "" && txNama.Text != "Nama") && (txTelepon.Text != "" && txTelepon.Text != "Telepon") &&
                (txUsername.Text != "" && txUsername.Text != "Username") && (txPassword.Text != "" && txPassword.Text != "Password") &&
                txCekPassword.Text == txPassword.Text)
            {
                if (panelNama.BackColor==kom.primary && panelTelepon.BackColor == kom.primary && panelUsername.BackColor == kom.primary &&
                    panelPassword.BackColor == kom.primary && panelCekPassword.BackColor == kom.primary)
                {
                    id.Value = txAutoID.Text;
                    gambar.Value = "";
                    nama.Value = txNama.Text;
                    telepon.Value = txTelepon.Text;
                    Username.Value = txUsername.Text;
                    Password.Value = fn.Crypt2(txCekPassword.Text);
                    statusAdmin.Value = "No";
                    tipeProfile.Value = lbJenisProfile.Text;

                    try
                    {
                        if (fn.saveUploaded("Profil") == true && cbPilihAvatar.Visible == false)
                        {
                            gambar.Value = fn.fileNameUpload;
                            odb.registerKasir(namaProcedure, parameters, this, txPassword, txCekPassword);
                        }
                        else if (fn.saveUploaded("Profil") == false && cbPilihAvatar.Visible == true && cbPilihAvatar.Text != "-Avatar-" && cbPilihAvatar.Text != "")
                        {
                            gambar.Value = cbPilihAvatar.SelectedItem.ToString() + ".PNG";
                            odb.registerKasir(namaProcedure, parameters, this, txPassword, txCekPassword);
                        }
                        else
                        {
                            gambar.Value = "NULL";
                            tipeProfile.Value = "NULL";
                            odb.registerKasir(namaProcedure, parameters, this, txPassword, txCekPassword);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal Registrasi Kasir\n" + ex.Message, "ERROR!");
                    }
                }
                else
                {
                    MessageBox.Show("Beberapa data salah!\n" + "Pastikan semua terisi dengan benar.", "Peringatan!");
                }
                
                

            }
            else
            {
                MessageBox.Show("Semua data wajib diisi!");
                panelNama.BackColor = Color.Red;
                txNama.Clear();
                txNama.Focus();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login L = new Login();
            L.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
            if (Opacity == 1)
            {
                timer1.Stop();
            }
        }
    }
}
