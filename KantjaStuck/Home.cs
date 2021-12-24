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
using System.IO;
using System.Drawing.Imaging;
using Microsoft.VisualBasic;

namespace KantjaStuck
{
    public partial class Home : Form
    {
        //CATATAN: untuk mengubah form pertama yang ditampilkan saat pertama kali di running ada pada Program.cs
        OperasiDB odb = new OperasiDB();
        Komponen kom = new Komponen();
        FungsiMethod fn = new FungsiMethod();
        Login l = new Login();

        //Variabel untuk menyimpan Detail Akun
        private static string[] InfoAkun;
        public Home()
        {
            InitializeComponent();
            wrapperHeader.BackColor = kom.primary;
            WrapperMenuNavigation.BackColor = kom.secondary;
            toolStrip1.BackColor = kom.primary;

            //Warna Awal dari komponen Navigasi
            Button[] DAFTAR_BT_NAVIGASI =
            {
                BT_DASHBOARD,BT_KASIR,BT_ADMIN,BT_DATABASE,BT_PROFIL,BT_LAPORAN,
            };
            Panel[] DAFTAR_PANEL =
            {
                wrapperDashBoard, wrapperKasir,wrapperAdmin,wrapperDatabase,wrapperProfile,wrapperLaporan
            };
            kom.colorBase_ButtonPanel(DAFTAR_BT_NAVIGASI, DAFTAR_PANEL);

            //======================= PROFILE ===========================
            profile_cardProfile.BackColor = SystemColors.ControlLight;
            Button[] profile_grupButton =
            {
                profile_btUploadProfile, profile_btPickAvatar, profile_btUbah, profile_btSimpanProfile
            };
            TextBox[] profile_grupTextBox =
            {
                profile_txNamaKasir,profile_txTeleponKasir,profile_txUsernameKasir,profile_txPasswordKasir
            };

            kom.button_colorPrimary(profile_grupButton, profile_btGantiProfile);
            kom.textBx_ReadOnly(profile_grupTextBox, true,null);
            profile_cbAvatar.Items.AddRange(avatar);

            profile_wrapperBtGantiProfile.Visible = false;
            profile_cbAvatar.Visible = false;
            profile_btSimpanProfile.Visible = false;
            profile_btGantiProfile.Visible = false;
            profile_txIdKasir.BackColor = kom.primary;
            profile_txIdKasir.ForeColor = Color.White;
            profile_txStatusAdmin.BackColor = kom.secondary;
            profile_txStatusAdmin.ForeColor = Color.White;

            // ===================================== ADMIN ==============================
            Button[] admin_GrupButton =
            {
                admin_cardBtLogin, admin_cardBtTambahData, admin_cardBtSimpan, admin_cardBtUbah, admin_cardBtHapus, admin_cardBtUpload, admin_cardBtTambahkan
            };
            TextBox[] admin_GrupTextBox =
            {
                admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin
            };
            kom.textBx_ReadOnly(admin_GrupTextBox, true,null);
            kom.button_colorPrimary(admin_GrupButton, null);
            admin_cardCB.BackColor = kom.primary;
            admin_cardLabelCB.ForeColor = Color.White;
            admin_cardDetail.BackColor = SystemColors.ControlLight;
            admin_cardLogin.BackColor = SystemColors.ControlLight;
            admin_cardAfterLogin.BackColor = kom.primary;
            admin_txNamaAfterLogin.BackColor = kom.primary;
            admin_txNamaAfterLogin.ForeColor = Color.White;
            admin_txIdAfterLogin.ForeColor = kom.primary;
            admin_cardLabelLabel.ForeColor = Color.White;
            admin_cardTxID.BackColor = kom.primary;
            admin_cbJenisTabel.Items.AddRange(CB_JenisTabel);
            admin_cardBtHapus.FlatAppearance.MouseDownBackColor=Color.Red;
            admin_cardBtHapus.BackColor = Color.FromArgb(192, 0, 0);
            //Visibilitas dan readonly
            admin_cardCB.Visible = false;
            admin_cardDetail.Visible = false;
            admin_cardGridView.Visible = false;
            admin_cardBtUpload.Visible = false;
            admin_cardBtUbahFoto.Visible = false;
            admin_cardBtTambahkan.Visible = false;
            // ======================================================== KASIR ==================================
            TextBox[] kasir_grupTxHeader =
            {
                textBox17,textBox23,textBox20,textBox22,textBox16,textBox3,textBox15,textBox13,textBox18,textBox21,textBox19,textBox9
            };
            Button[] kasir_grupButton =
            {
                kasir_cardBtPilih, kasir_cardBtTambahkan, kasir_card_BtCheckOut
            };
            kom.button_colorPrimary(kasir_grupButton, null);
            kom.textBoxHeader(kasir_grupTxHeader);
            //
            kaisr_cardBtBatal.BackColor = SystemColors.ControlLight;
            kaisr_cardBtBatal.FlatAppearance.BorderSize = 2;
            kaisr_cardBtBatal.FlatAppearance.BorderColor = kom.primary;
            kaisr_cardBtBatal.ForeColor = kom.primary;
            //
            kasir_cardPilihMenu.BackColor = SystemColors.ControlLight;
            kasir_cardHitungCheckOut.BackColor = SystemColors.ControlLight;
            kasir_cardKontainerMakanan.BackColor = SystemColors.ControlLight;
            kasir_cardKontainerMinuman.BackColor = SystemColors.ControlLight;

        }


        private void Home_Load(object sender, EventArgs e)
        {
            wrapperDashBoard.Visible = true;
            wrapperKasir.Visible = false;
            wrapperAdmin.Visible = false;
            wrapperDatabase.Visible = false;
            wrapperProfile.Visible = false;
            wrapperLaporan.Visible = false;

            //login admin
            admin_cardLogin.Visible = true;
            admin_cardAfterLogin.Visible = false;

            BT_DASHBOARD.BackColor = SystemColors.ControlLight;
            BT_DASHBOARD.ForeColor = kom.primary;

            //untuk menyimpan data Akun pada variabel --Array InfoAkun--
            //string uname = Login.setValueUsername;
            //string pass = fn.Crypt2(Login.setValuePassword);
            string uname = "Maya";
            string pass = fn.Crypt2("Maya");
            InfoAkun = odb.getInfoKasir(uname, pass);
            //Text Hallo pada Dashboard
            dashboard_txGreeting.Text = "Hallo, " + InfoAkun[2].ToString();
        }
        //================================================= Method saat Button Navigasi diclick
        private void Navigasi_diCLICK(Button BT_diCLICK, Panel PanelNavigasi)
        {
            Button[] DAFTAR_BT_NAVIGASI =
            {
                BT_DASHBOARD,BT_KASIR,BT_ADMIN,BT_DATABASE,BT_PROFIL,BT_LAPORAN,
            };
            Panel[] DAFTAR_PANEL =
            {
                wrapperDashBoard, wrapperKasir,wrapperAdmin,wrapperDatabase,wrapperProfile,wrapperLaporan
            };
            foreach (Button BT in DAFTAR_BT_NAVIGASI)
            {
                BT.BackColor = kom.secondary;
                BT.ForeColor = kom.wrapper;
            }
            foreach(Panel panel in DAFTAR_PANEL)
            {
                panel.Visible = false;
            }
            BT_diCLICK.BackColor = SystemColors.ControlLight;
            BT_diCLICK.ForeColor = kom.primary;
            PanelNavigasi.Visible = true;
        }
        // ============== box Pencarian
        private void textBox_Cari_Click(object sender, EventArgs e)
        {
            textBox_Cari.Clear();
        }

        private void textBox_Cari_TextCanged(object sender, EventArgs e)
        {
            string key = textBox_Cari.Text;
            string query = "";
            if (textBox_Cari.Text == "")
            {
                query = "";
            }
            else
            {
                query = "SELECT * FROM admin WHERE userId LIKE '%" + key + "%' OR password LIKE '%" + key + "%'";
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
        //============= Tombol Close dan Minimize
        private void btClose_Click(object sender, EventArgs e)
        {
            kom.button_Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            kom.button_Minimize(this);
        }
        //======================================================================== NAVIGASI ==========================================================
        private void BT_DASHBOARD_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_DASHBOARD, wrapperDashBoard);
            dashboard_txGreeting.Text = "Hallo, " + InfoAkun[2].ToString();
        }

        private void BT_KASIR_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_KASIR, wrapperKasir);
        }
        // ======================================================================= ADMIN =============================================================
        private void BT_ADMIN_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_ADMIN, wrapperAdmin);
            admin_cbJenisTabel.Text = "-Pilih Tabel-";

        }
        private void admin_cardBtLogin_Click(object sender, EventArgs e)
        {
            string namaKasir = InfoAkun[2].ToString();
            string idKasir = InfoAkun[0].ToString();

            SqlParameter id = new SqlParameter("@id", SqlDbType.VarChar);
            SqlParameter Username = new SqlParameter("@Username", SqlDbType.VarChar);
            SqlParameter Password = new SqlParameter("@Password", SqlDbType.VarChar);

            SqlParameter[] parameters = { id, Username, Password };
            if(admin_cardTxUsername.Text != "" && admin_cardTxPassword.Text != "")
            {
                id.Value = idKasir;
                Username.Value = admin_cardTxUsername.Text;
                Password.Value = fn.Crypt2(admin_cardTxPassword.Text);
                if(odb.commandLoginAdmin("LoginAdmin", parameters) == true)
                {
                    admin_cardAfterLogin.Visible = true;
                    admin_cardLogin.Visible = false;
                    admin_cardCB.Visible = true;
                    admin_txNamaAfterLogin.Text = namaKasir;
                    admin_txIdAfterLogin.Text = idKasir;
                }
                else
                {
                    admin_cardAfterLogin.Visible = false;
                    admin_cardLogin.Visible = true;
                    admin_cardCB.Visible = false;
                    MessageBox.Show(namaKasir + ", Anda Bukanlah Admin!", "Gagal");
                }
            }
            else
            {
                MessageBox.Show("Kolom tidak boleh kosong!", "Peringatan");
            }

            
        }
        private string[] CB_JenisTabel = { "Kasir", "Makanan", "Minuman", "Transaksi"};
        private void admin_cbJenisTabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox[] admin_GrupTextBox =
            {
                admin_cardTxID,admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin
            };
            string query = "";
            kom.textBx_ReadOnly(admin_GrupTextBox, true, null);
            admin_cardTxID.BackColor = kom.primary;
            admin_cardBtUbahFoto.Visible = false;
            //tombolTambahkan
            admin_cardBtTambahkan.Visible = false;
            if (admin_cbJenisTabel.SelectedItem == "Kasir")
            {
                admin_cardGridView.Visible = true;
                admin_cardWrapperSubGV.Visible = false;
                admin_cardDetail.Visible = true;
                //label harga
                admin_cardLbHarga_orTelp.Text = "Telp :";
                //txStatus admin
                admin_cardLbStatusAdmin.Visible = true;
                admin_cardTxStatusAdmin.Visible = true;
                //nable kotak detail
                admin_cardDetail.Enabled = true;
                admin_cardLabelJenisTabel.Text = "Kasir";
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = true;
                //button tambah
                admin_cardBtTambahData.Visible = false;
                pictureBox2.Visible = true;
                //=====
                query = "SELECT idKasir, gambarKasir, namaKasir, noTelepon, statusAdmin, tipeProfil FROM kasir";
            }
            else if(admin_cbJenisTabel.SelectedItem == "Makanan")
            {
                admin_cardGridView.Visible = true;
                admin_cardWrapperSubGV.Visible = false;
                admin_cardDetail.Visible = true;
                //label harga
                admin_cardLbHarga_orTelp.Text = "Harga :";
                //txStatus admin
                admin_cardLbStatusAdmin.Visible = false;
                admin_cardTxStatusAdmin.Visible = false;
                //nable kotak detail
                admin_cardDetail.Enabled = true;
                admin_cardLabelJenisTabel.Text = "Makanan";
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = true;
                //button tambah
                admin_cardBtTambahData.Visible = true;
                pictureBox2.Visible = true;
                //===
                query = "SELECT * FROM makanan";
            }
            else if (admin_cbJenisTabel.SelectedItem == "Minuman")
            {
                admin_cardGridView.Visible = true;
                admin_cardWrapperSubGV.Visible = false;
                admin_cardDetail.Visible = true;
                //label harga
                admin_cardLbHarga_orTelp.Text = "Harga :";
                //txStatus admin
                admin_cardLbStatusAdmin.Visible = false;
                admin_cardTxStatusAdmin.Visible = false;
                //nable kotak detail
                admin_cardDetail.Enabled = true;
                admin_cardLabelJenisTabel.Text = "Minuman";
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = true;
                //button tambah
                admin_cardBtTambahData.Visible = true;
                pictureBox2.Visible = true;

                //===
                query = "SELECT * FROM minuman";
            }
            else if (admin_cbJenisTabel.SelectedItem == "Transaksi")
            {
                admin_cardGridView.Visible = true;
                admin_cardWrapperSubGV.Visible = true;
                admin_cardDetail.Visible = true;
                //nable kotak detail
                admin_cardDetail.Enabled = false;
                kom.textBx_Reset(admin_GrupTextBox, null);
                //label text
                admin_cardLabelJenisTabel.Text = "";
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = true;
                //button tambah
                admin_cardBtTambahData.Visible = true;
                pictureBox2.Visible = false;
                //===
                query = "SELECT * FROM transaksi";
            }
            else
            {
                MessageBox.Show("Jenis tabel belum dipilih!", "Peringatan");
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = true;
                //button tambah
                admin_cardBtTambahData.Visible = true;
                pictureBox2.Visible = true;
                query = "NULL";
            }
            odb.showDataGV(query, admin_gvTabel);
        }
        private void admin_cardBtUbah_Click(object sender, EventArgs e)
        {
            TextBox[] admin_GrupTextBox =
            {
                admin_cardTxID,admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin
            };
            admin_cardBtUbahFoto.Visible = true;
            if(admin_cardLabelJenisTabel.Text=="Kasir" || admin_cardLabelJenisTabel.Text == "kasir")
            {
                kom.textBx_ReadOnly(admin_GrupTextBox, false, admin_cardTxID);
                admin_cardTxNama.ReadOnly = true;
                admin_cardTxHarga_orTelp.ReadOnly = true;
                admin_cardTxNama.BackColor = SystemColors.ControlLight;
                admin_cardTxHarga_orTelp.BackColor = SystemColors.ControlLight;
                admin_cardBtUbahFoto.Visible = false;
            }
            else
            {
                kom.textBx_ReadOnly(admin_GrupTextBox, false, admin_cardTxID);
            }
            admin_cardBtSimpan.Visible = true;
            admin_cardBtUbah.Visible = false;
        }
        private void admin_cardBtSimpan_Click(object sender, EventArgs e)
        {
            TextBox[] admin_GrupTextBox =
            {
                admin_cardTxID,admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin
            };
            kom.textBx_ReadOnly(admin_GrupTextBox, true, null);
            admin_cardTxID.BackColor = kom.primary;
            admin_cardBtUbahFoto.Visible = false;
            //button ubah dan simpan dan upload gambar
            admin_cardBtSimpan.Visible = false;
            admin_cardBtUbah.Visible = true;
            admin_cardBtUpload.Visible = false;
        }
        private void admin_cardBtUbahFoto_Click(object sender, EventArgs e)
        {
            admin_cardBtUpload.Visible = true;
        }
        private void admin_cardBtUpload_Click(object sender, EventArgs e) //ButtonUploadGambar
        {
            admin_cardBtUpload.Visible = false;
        }
        private void admin_cardBtTambahData_Click(object sender, EventArgs e)
        {
            TextBox[] admin_GrupTextBox =
            {
                admin_cardTxID,admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin
            };
            kom.textBx_Reset(admin_GrupTextBox, null);
            kom.textBx_ReadOnly(admin_GrupTextBox, false, admin_cardTxID);
            admin_cardBtTambahkan.Visible = true;
            admin_cardBtSimpan.Visible = false;
            admin_cardBtUbah.Visible = false;
            admin_cardBtUbahFoto.Visible = true;
        }
        private void admin_cardBtTambahkan_Click(object sender, EventArgs e)
        {
            TextBox[] admin_GrupTextBox =
            {
                admin_cardTxID,admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin
            };
            kom.textBx_Reset(admin_GrupTextBox, null);
            kom.textBx_ReadOnly(admin_GrupTextBox, true, admin_cardTxID);
            admin_cardTxID.BackColor = kom.primary;
            admin_cardBtTambahkan.Visible = false;
            admin_cardBtSimpan.Visible = false;
            admin_cardBtUbah.Visible = true;
            admin_cardBtUbahFoto.Visible = false;
        }
        private void admin_gvTabel_cellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = admin_gvTabel.CurrentRow.Index;
            string key_id = admin_gvTabel.Rows[indexRow].Cells[0].Value.ToString();
            string query = "";

            string id = admin_gvTabel.Rows[indexRow].Cells[0].Value.ToString();
            string gambar = admin_gvTabel.Rows[indexRow].Cells[1].Value.ToString();
            string nama = admin_gvTabel.Rows[indexRow].Cells[2].Value.ToString();
            string harga = "";
            string statusAdmin = "";
            string jenisGambar = "";
            string jenisFile = "";

            if (admin_cardLabelJenisTabel.Text == "Kasir")
            {
                harga = admin_gvTabel.Rows[indexRow].Cells[3].Value.ToString();
                statusAdmin = admin_gvTabel.Rows[indexRow].Cells[4].Value.ToString();
                query = "SELECT idKasir, gambarKasir, namaKasir, noTelepon, statusAdmin, tipeProfil FROM kasir WHERE idKasir = '" + key_id + "'";
                jenisGambar = "Profil";
                jenisFile = admin_gvTabel.Rows[indexRow].Cells[5].Value.ToString();
            }
            else if (admin_cardLabelJenisTabel.Text == "Makanan")
            {
                harga = admin_gvTabel.Rows[indexRow].Cells[3].Value.ToString();
                statusAdmin = "";
                query= "SELECT * FROM makanan WHERE idMakanan = '" + key_id + "'";
                jenisGambar = "Makanan";
                jenisFile = "NULL";
            }
            else if (admin_cardLabelJenisTabel.Text == "Minuman")
            {
                harga = admin_gvTabel.Rows[indexRow].Cells[3].Value.ToString();
                statusAdmin = "";
                query = "SELECT * FROM minuman WHERE idMinuman = '" + key_id + "'";
                jenisGambar = "Minuman";
                jenisFile = "NULL";
            }
            else //ketika transaksi
            {

            }
            admin_cardTxID.Text = id;
            admin_cardTxNama.Text = nama;
            admin_cardTxHarga_orTelp.Text = harga;
            admin_cardTxStatusAdmin.Text = statusAdmin;
            odb.showDataGV(query, admin_gvTabel);
            string[] NamajenisFile = { gambar, jenisFile };
            fn.tampilGambar(pictureBox2, NamajenisFile, jenisGambar);
        }
        /*tx_idCustomer.Text = admin_gvTabel.Rows[indexRow].Cells[0].Value.ToString();
            tx_idCustomer.ReadOnly = true;
            tx_namaCustomer.Text = admin_gvTabel.Rows[indexRow].Cells[1].Value.ToString();
            tx_isiVoucher.Text = admin_gvTabel.Rows[indexRow].Cells[2].Value.ToString();

            showData(query, gridView_Customer);*/
        //======================================================================= DATABASE ===========================================================
        private void BT_DATABASE_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_DATABASE, wrapperDatabase);
        }
        // ============================================================= NAVIGASI PROFILE ===========================================================
        private void BT_PROFIL_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_PROFIL, wrapperProfile);
            string id = InfoAkun[0].ToString();
            string gambar = InfoAkun[1].ToString();
            string nama = InfoAkun[2].ToString();
            string telepon = InfoAkun[3].ToString();
            string username = InfoAkun[4].ToString();
            string password = InfoAkun[5].ToString();
            string statusAdmin = InfoAkun[6].ToString();
            string jenisprofile = InfoAkun[7].ToString();
            string[] namaJenisFile = { gambar, jenisprofile };

            profile_txIdKasir.Text = id;
            profile_txStatusAdmin.Text = statusAdmin;
            profile_txNamaKasir.Text = nama;
            profile_txTeleponKasir.Text = telepon;
            profile_txUsernameKasir.Text = username;
            profile_txPasswordKasir.Text = fn.Decrypt2(password);
            profile_lbJenisProfile.Text = jenisprofile;

            fn.tampilGambar(profile_pictureBox,namaJenisFile,"Profil");
        }
        //===== Ganti Foto Profile
        private void profile_btGantiProfile_Click(object sender, EventArgs e)
        {
            profile_wrapperBtGantiProfile.Visible = true;
            profile_btGantiProfile.Visible = false;
        }

        private void profile_btUploadProfile_Click(object sender, EventArgs e)
        {
            profile_wrapperBtGantiProfile.Visible = false;
            profile_btGantiProfile.Visible = true;
            profile_cbAvatar.Visible = false;

            //string keyValue = InfoAkun[0].ToString(); // idKasir
            //string[] rincianTabel = { "kasir", "gambarKasir", "idKasir", keyValue };
            fn.uploadFoto(profile_pictureBox, profile_lbJenisProfile, "Profil");
        }

        private void profile_btPickAvatar_Click(object sender, EventArgs e)
        {
            profile_wrapperBtGantiProfile.Visible = false;
            profile_cbAvatar.Visible = true;
            profile_btGantiProfile.Visible = true;

            profile_cbAvatar.BackColor = kom.primary;
            profile_cbAvatar.ForeColor = Color.White;
            profile_cbAvatar.Text = "-Avatar-";

        }
        private string[] avatar = { "beard", "child", "clown", "hacker", "hero", "malika", "old_Lady", "pirate", "teacher", "woman" };

        private void profile_btUbah_Click(object sender, EventArgs e)
        {
            TextBox[] profile_grupTextBox =
            {
                profile_txNamaKasir,profile_txTeleponKasir,profile_txUsernameKasir,profile_txPasswordKasir
            };
            kom.textBx_ReadOnly(profile_grupTextBox, false,null);
            profile_btSimpanProfile.Visible = true;
            profile_btGantiProfile.Visible = true;
        }

        private void profile_btSimpanProfile_Click(object sender, EventArgs e)
        {
            TextBox[] profile_grupTextBox =
            {
                profile_txNamaKasir,profile_txTeleponKasir,profile_txUsernameKasir,profile_txPasswordKasir
            };
            kom.textBx_ReadOnly(profile_grupTextBox, true,null);
            

            string namaProcedure = "UpdateKasir";
            SqlParameter id = new SqlParameter("@id", SqlDbType.VarChar);
            SqlParameter gambar = new SqlParameter("@gambar", SqlDbType.VarChar);
            SqlParameter nama = new SqlParameter("@nama", SqlDbType.VarChar);
            SqlParameter telepon = new SqlParameter("@telp", SqlDbType.VarChar);
            SqlParameter Username = new SqlParameter("@uname", SqlDbType.VarChar);
            SqlParameter Password = new SqlParameter("@pass", SqlDbType.VarChar);
            SqlParameter tipeProfile = new SqlParameter("@tipeGambar", SqlDbType.VarChar);

            SqlParameter[] parameters = { id,gambar, nama, telepon, Username, Password, tipeProfile };
            string uname = profile_txUsernameKasir.Text;
            string pass = fn.Crypt2(profile_txPasswordKasir.Text);

            gambar.Value = InfoAkun[1].ToString();
            if (profile_txNamaKasir.Text != "" && profile_txTeleponKasir.Text != "" && profile_txUsernameKasir.Text != "" && profile_txPasswordKasir.Text != "")
            {
                if (Information.IsNumeric(profile_txTeleponKasir.Text)==true)
                {
                    id.Value = profile_txIdKasir.Text;
                    gambar.Value = InfoAkun[1].ToString();
                    nama.Value = profile_txNamaKasir.Text;
                    telepon.Value = profile_txTeleponKasir.Text;
                    Username.Value = profile_txUsernameKasir.Text;
                    Password.Value = fn.Crypt2(profile_txPasswordKasir.Text);
                    tipeProfile.Value = profile_lbJenisProfile.Text;
                    try
                    {
                        
                        if (fn.saveUploaded("Profil") == true && profile_cbAvatar.Visible == false)
                        {
                            gambar.Value = fn.fileNameUpload;
                            odb.procedureIUD(namaProcedure, parameters,"Upadte");
                        }
                        else if (fn.saveUploaded("Profil") == false && profile_cbAvatar.Visible == true && profile_cbAvatar.Text != "-Avatar-" && profile_cbAvatar.Text != "")
                        {
                            gambar.Value = profile_cbAvatar.SelectedItem.ToString() + ".PNG";
                            odb.procedureIUD(namaProcedure, parameters, "Upadte");
                        }
                        else if (fn.saveUploaded("Profil") == false && profile_cbAvatar.Visible == false)
                        {
                            gambar.Value = InfoAkun[1].ToString();
                            odb.procedureIUD(namaProcedure, parameters, "Upadte");
                        }
                        else
                        {
                            gambar.Value = "NULL";
                            tipeProfile.Value = "NULL";
                            odb.procedureIUD(namaProcedure, parameters, "Upadte");
                            
                        }
                        InfoAkun = odb.getInfoKasir(uname, pass);
                        MessageBox.Show("Berhasil di update!", "Succes!");
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal Registrasi Kasir\n" + ex.Message, "ERROR!");
                    }
                }
                else
                {
                    MessageBox.Show("Telepon harus diisi dengan benar!", "Peringatan");
                }
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong!", "Peringatan");
            }
            
            profile_btSimpanProfile.Visible = false;
            profile_btGantiProfile.Visible = false;
            profile_wrapperBtGantiProfile.Visible = false;
            profile_cbAvatar.Visible = false;

        }
        private void profile_cbAvatar_SelectedIndexChanged(object sender, EventArgs e)
        {
            fn.pickAvatar(profile_cbAvatar, profile_pictureBox, profile_lbJenisProfile);
        }
        // ============================================================= NAVIGASI LAPORAN ===========================================================
        private void BT_LAPORAN_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_LAPORAN, wrapperLaporan);
        }
        //====================================================================================================
        int thisUnit = 1;
        int jarakLeft = 10;
        int urut = 1;
        public List<string[]> PesananMakanan = new List<string[]>();
        public List<Component[]> listKomponen = new List<Component[]>();
        private void button11_Click(object sender, EventArgs e)
        {
            int top = thisUnit * 100;

            TextBox txId = new TextBox();
            TextBox txNamaMenu = new TextBox();
            TextBox txQty = new TextBox();
            TextBox txTotal = new TextBox();
            Button btDelete = new Button();
            //untuk memberi warna textBox dan posisi top
            TextBox[] txBoxes = { txId, txNamaMenu, txQty, txTotal };
            Component[] komps = { txId, txNamaMenu, txQty, txTotal, btDelete };
            string[] kompName = { txId.Name, txNamaMenu.Name, txQty.Name, txTotal.Name, btDelete.Name };
           
            // untuk memberi warna pada textbox dan jarak atas textbox
            foreach (TextBox txB in txBoxes)
            {
                txB.BackColor = SystemColors.ControlLight;
                txB.ForeColor = kom.primary;
                txB.Top = thisUnit * 30;
                txB.BorderStyle = BorderStyle.None;
                txB.Font = new Font("Poppins", 9, FontStyle.Regular);
                txB.Height = 20;
            }
            txId.Left = jarakLeft;
            txId.Name = "tbNama" + urut.ToString();
            txId.TextAlign = HorizontalAlignment.Center;
            txId.Width = 59;
            //============
            txNamaMenu.Left = txId.Width + 20;
            txNamaMenu.Width = 138;
            txNamaMenu.Name = "tbMenu" + urut.ToString();
            //===========
            txQty.Left = txId.Width + txNamaMenu.Width + 30;
            txQty.Width = 32;
            txQty.Name = "tbQty" + urut.ToString();
            //===========
            txTotal.Left = txId.Width + txNamaMenu.Width + txQty.Width + 40;
            txTotal.Width = 74;
            txTotal.Name = "tbTotal" + urut.ToString();
            //============
            btDelete.Top = thisUnit * 30;
            btDelete.Left = txId.Width + txNamaMenu.Width + txQty.Width + txTotal.Width + 50;
            btDelete.Width = 20;
            btDelete.Height = 20;
            btDelete.Text = "X";
            btDelete.Name = "btDelete" + urut.ToString();
            btDelete.FlatStyle = FlatStyle.Flat;
            btDelete.BackColor = Color.Red;
            btDelete.ForeColor = Color.White;
            btDelete.Font = new Font("Poppins", 6, FontStyle.Bold);
            btDelete.FlatAppearance.BorderSize = 0;
            btDelete.Click += new EventHandler(buttonHapusData_click);

            if (textBox25.Text != "" && textBox26.Text !="" && textBox27.Text != "" && textBox28.Text != "")
            {
                txId.Text = textBox25.Text;
                txNamaMenu.Text = textBox26.Text;
                txQty.Text = textBox27.Text;
                txTotal.Text = textBox28.Text;
                string[] pesanan = { txId.Text, txNamaMenu.Text, txQty.Text, txTotal.Text };
                try
                {
                    //menampung komponen pada list komponen
                    foreach (Component komp in komps)
                    {
                        flowLayoutPanel1.Controls.Add((Control)komp);
                    }
                    //Menampung komponen ke dalam list komponen
                    listKomponen.Add(komps);
                    // menampung pesanan pada list pesanan
                    PesananMakanan.Add(pesanan);
                    // menampilkan item dari lis pesan makanan
                    listBox1.Items.Add(pesanan[0]);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Gagal Menambah pesanan! \n"+ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Semua wajig di isi!", "Perimgatan");
            }
            thisUnit = thisUnit + 1;
            urut = urut + 1;
            //menghitung total list
            textBox24.Text = listKomponen.Count().ToString();
            textBox29.Text = PesananMakanan.Count().ToString();

            //reset teks pesanan
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
            textBox28.Clear();


        }
        //create user define func
        void buttonHapusData_click(object sender, EventArgs e)
        {
            //get Button click
            Button btn = sender as Button;
            for(int i=0; i<listKomponen.Count; i++)
            {
                if (listKomponen[i][4] == btn)
                {
                    //menghapus komponen secara visual
                    foreach (Component ko in listKomponen[i])
                    {
                        flowLayoutPanel1.Controls.Remove((Control)ko);
                    }
                    //menghapus listkomponen ke-i
                    listKomponen.Remove(listKomponen[i]);
                    //menghapus list pesanan
                    PesananMakanan.Remove(PesananMakanan[i]);
                    //menghapus pada tampilan
                    try
                    {
                        listBox1.Items.Remove(PesananMakanan[i]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //hitung list
                    textBox24.Text = listKomponen.Count().ToString();
                    textBox29.Text = PesananMakanan.Count().ToString();
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

    }
}

