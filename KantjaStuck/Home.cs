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
            kom.textBx_ReadOnly(profile_grupTextBox, true, null);
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
            kom.textBx_ReadOnly(admin_GrupTextBox, true, null);
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
            admin_cardBtHapus.FlatAppearance.MouseDownBackColor = Color.Red;
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
                textBox17,textBox23,textBox20,textBox22,textBox16,textBox3,textBox15,textBox13,textBox18,textBox21,textBox19,textBox9,
                kasir_cardIdKasir, kasir_cardNoTransaksi, kasir_sumTotalMakanan, kasir_sumTotalMinuman
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
            //============================================================ DATABASE============================
            TextBox[] tb_grupDB =
            {
                db_cardTxNama, db_cardTxHarga
            };
            TextBox[] tbDB = { db_cardTxId };
            kom.textBx_ReadOnly(tb_grupDB, true, null);
            kom.textBoxHeader(tbDB);
            db_cardPilihTB.BackColor = kom.primary;
            db_CBTabel.Items.AddRange(tabelinDB);
            //========================================================== DASHBOARD ===============================
            panel2.BackColor = kom.primary;
            panel3.BackColor = kom.primary;
            panel4.BackColor = kom.primary;
            textBox7.BackColor = kom.primary;
            //============================================================= LAPORAN ==============================
            lap_btLoad.BackColor = kom.primary;
            lap_btLoad.ForeColor = Color.White;
        }

        // ============================================================ HOME LOAD ================================================
        // bagian card pilih menu
        private string[] daftarMakanan = { };
        private string[] daftarMinuman = { };
        private string[] nominalTunai = { "5000", "10000", "15000", "20000", "50000", "100000" };
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
            string uname = Login.setValueUsername;
            string pass = fn.Crypt2(Login.setValuePassword);
            //string uname = "Maya";
            //string pass = fn.Crypt2("Maya");
            InfoAkun = odb.getInfoKasir(uname, pass);
            //Text Hallo pada Dashboard
            dashboard_txGreeting.Text = "Hallo, " + InfoAkun[2].ToString();

            //==========Kasir
            daftarMakanan = odb.getDaftarMenu("Makanan");
            daftarMinuman = odb.getDaftarMenu("Minuman");
            kasir_chardCBMenuMinuman.Items.AddRange(daftarMinuman);
            kasir_chardCBMenuMakanan.Items.AddRange(daftarMakanan);

            kasir_chardCBMenuMinuman.Visible = false;
            kasir_chardCBMenuMakanan.Visible = false;
            kasir_cardCBPilihNominal.Items.AddRange(nominalTunai);
            kasir_cardCBPilihNominal.Visible = false;

            //============= DashBoard
            string bulanIni = DateTime.Now.ToString("Y");
            int penghasilanBruto = odb.getPenhasilanBruto();
            textBox7.Text = bulanIni;
            textBox1.Text = "Rp " + penghasilanBruto.ToString("N0");
            odb.getMenuTerlasris(textBox8, textBox2, textBox4, pictureBox2, "Makanan");
            odb.getMenuTerlasris(textBox10, textBox6, textBox5, pictureBox1, "Minuman");
            //====================================
            this.reportViewer1.RefreshReport();
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
            foreach (Panel panel in DAFTAR_PANEL)
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
        // ======================================================================== DASHBOARD =======================================================
        private void BT_DASHBOARD_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_DASHBOARD, wrapperDashBoard);
            dashboard_txGreeting.Text = "Hallo, " + InfoAkun[2].ToString();
            toolStripLabel1.Text = "Dashboard";

            string bulanIni = DateTime.Now.ToString("Y");
            int penghasilanBruto = odb.getPenhasilanBruto();

            textBox7.Text = bulanIni;
            textBox1.Text = "Rp " + penghasilanBruto.ToString("N0");
            //
            odb.getMenuTerlasris(textBox8, textBox2, textBox4, pictureBox2, "Makanan");
            odb.getMenuTerlasris(textBox10, textBox6, textBox5, pictureBox1, "Minuman");
        }
        // ======================================================================== KASIR =======================================================
        private void BT_KASIR_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_KASIR, wrapperKasir);
            // get Nama dan ID kasir
            kasir_cardNamaKasir.Text = InfoAkun[2].ToString();
            kasir_cardIdKasir.Text = InfoAkun[0].ToString();
            // get ID pelanggan
            kasir_cardIdPelanggan.Text = fn.getAutoID("Pelanggan");
            toolStripLabel1.Text = "Kasir";

            // refresh daftar Menu
            kasir_chardCBMenuMakanan.Items.Clear();
            kasir_chardCBMenuMinuman.Items.Clear();
            daftarMakanan = odb.getDaftarMenu("Makanan");
            daftarMinuman = odb.getDaftarMenu("Minuman");
            kasir_chardCBMenuMinuman.Items.AddRange(daftarMinuman);
            kasir_chardCBMenuMakanan.Items.AddRange(daftarMakanan);
        }
        // TextBox Dinamis=========
        int thisUnit = 1;
        int jarakLeft = 10;
        int urut = 1;
        //List komponen dan list pesanan
        private List<string[]> PesananMakanan = new List<string[]>();
        private List<string[]> PesananMinuman = new List<string[]>();
        private List<Component[]> listKomponenMakanan = new List<Component[]>();
        private List<Component[]> listKomponenMinuman = new List<Component[]>();
        //bagian pengkasiran
        //Bagian pengkasiran
        private static int sumPesananMakanan = 0;
        private static int sumPesananMinuman = 0;
        private static int totalHargaSeluruhPesanan = sumPesananMakanan + sumPesananMinuman;
        private void kasir_cardBtPilih_Click(object sender, EventArgs e)
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
                txB.BackColor = Color.White;
                txB.ForeColor = kom.primary;
                txB.Top = thisUnit * 30;
                txB.BorderStyle = BorderStyle.None;
                txB.Font = new Font("Poppins", 9, FontStyle.Regular);
                txB.Height = 20;
            }
            txId.Left = jarakLeft;
            txId.Width = 59;
            txId.Name = "tbNama" + urut.ToString();
            txId.TextAlign = HorizontalAlignment.Center;
            txId.ReadOnly = true;
            //============
            txNamaMenu.Left = txId.Width + 40;
            txNamaMenu.Width = 138;
            txNamaMenu.Name = "tbMenu" + urut.ToString();
            txNamaMenu.TextAlign = HorizontalAlignment.Center;
            txNamaMenu.ReadOnly = true;
            //===========
            txQty.Left = txId.Width + txNamaMenu.Width + 50;
            txQty.Width = 32;
            txQty.Name = "tbQty" + urut.ToString();
            txQty.TextAlign = HorizontalAlignment.Center;
            txQty.ReadOnly = true;
            //===========
            txTotal.Left = txId.Width + txNamaMenu.Width + txQty.Width + 50;
            txTotal.Width = 74;
            txTotal.Name = "tbTotal" + urut.ToString();
            txTotal.TextAlign = HorizontalAlignment.Center;
            txTotal.ReadOnly = true;
            //============
            btDelete.Top = thisUnit * 30;
            btDelete.Left = txId.Width + txNamaMenu.Width + txQty.Width + txTotal.Width + 65;
            btDelete.Width = 20;
            btDelete.Height = 20;
            btDelete.Text = "X";
            btDelete.Name = "btDelete" + urut.ToString();
            btDelete.FlatStyle = FlatStyle.Flat;
            btDelete.BackColor = Color.Red;
            btDelete.ForeColor = Color.White;
            btDelete.Font = new Font("Poppins", 6, FontStyle.Bold);
            btDelete.FlatAppearance.BorderSize = 0;

            if (kasir_cardIdMenu.Text != "" && kasir_cardNamaMenu.Text != "" && kasir_cardHargaMenu.Text != "" && kasir_cardQtyMenu.Text != "" && Information.IsNumeric(kasir_cardQtyMenu.Text))
            {
                txId.Text = kasir_cardIdMenu.Text;
                txNamaMenu.Text = kasir_cardNamaMenu.Text;
                txQty.Text = kasir_cardQtyMenu.Text;
                int totalHargaItem = (int.Parse(kasir_cardQtyMenu.Text) * int.Parse(kasir_cardHargaMenu.Text));
                txTotal.Text = totalHargaItem.ToString("N0");
                string[] pesanan = { txId.Text, txNamaMenu.Text, txQty.Text, totalHargaItem.ToString() };
                try
                {
                    if (kasir_cardRBMakanan.Checked)
                    {
                        btDelete.Click += new EventHandler(buttonHapusDataMakanan_click);
                        //menampung komponen pada list komponen
                        foreach (Component komp in komps)
                        {
                            kasir_wrapperPesananMakanan.Controls.Add((Control)komp);
                        }
                        //Menampung komponen ke dalam list komponen
                        listKomponenMakanan.Add(komps);
                        // menampung pesanan pada list pesanan
                        PesananMakanan.Add(pesanan);
                        kasir_totalPesananMakanan.Text = PesananMakanan.Count().ToString();
                        // untuk menghitung sum total pesan
                        int sum = 0;
                        foreach (string[] detailPesan in PesananMakanan)
                        {
                            sum = sum + int.Parse(detailPesan[3]);
                        }
                        sumPesananMakanan = sum;
                        kasir_sumTotalMakanan.Text = sumPesananMakanan.ToString("N0");
                    }
                    else if (kasir_cardRBMinuman.Checked)
                    {
                        btDelete.Click += new EventHandler(buttonHapusDataMinuman_click);
                        //menampung komponen pada list komponen
                        foreach (Component komp in komps)
                        {
                            kasir_wrapperPesananMinuman.Controls.Add((Control)komp);
                        }
                        //Menampung komponen ke dalam list komponen
                        listKomponenMinuman.Add(komps);
                        // menampung pesanan pada list pesanan
                        PesananMinuman.Add(pesanan);
                        kasir_totalPesananMinuman.Text = PesananMinuman.Count().ToString();
                        // untuk menghitung sum total pesan
                        int sum = 0;
                        foreach (string[] detailPesan in PesananMinuman)
                        {
                            sum = sum + int.Parse(detailPesan[3]);
                        }
                        sumPesananMinuman = sum;
                        kasir_sumTotalMinuman.Text = sumPesananMinuman.ToString("N0");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Menambah pesanan! \n" + ex.Message, "Error");
                }
                //reset teks pesanan
                kasir_cardIdMenu.Clear();
                kasir_cardNamaMenu.Clear();
                kasir_cardHargaMenu.Clear();
                kasir_cardQtyMenu.Clear();
            }
            else
            {
                MessageBox.Show("Semua wajig di isi!", "Perimgatan");
            }
            thisUnit = thisUnit + 1;
            urut = urut + 1;
            //menghitung total list
            //textBox24.Text = listKomponen.Count().ToString();
        }
        void buttonHapusDataMakanan_click(object sender, EventArgs e)
        {
            //get Button click
            Button btn = sender as Button;
            //Untuk Makanan
            for (int i = 0; i < listKomponenMakanan.Count; i++)
            {
                if (listKomponenMakanan[i][4] == btn)
                {
                    try
                    {
                        //menghapus komponen secara visual
                        foreach (Component ko in listKomponenMakanan[i])
                        {
                            kasir_wrapperPesananMakanan.Controls.Remove((Control)ko);
                        }
                        //menghapus listkomponen ke-i
                        listKomponenMakanan.Remove(listKomponenMakanan[i]);
                        //menghapus list pesanan
                        PesananMakanan.Remove(PesananMakanan[i]);
                        MessageBox.Show("Makanan dihapus!");
                        // untuk menghitung sum total pesan
                        int sum = 0;
                        foreach (string[] detailPesan in PesananMakanan)
                        {
                            sum = sum + int.Parse(detailPesan[3]);
                        }
                        sumPesananMakanan = sum;
                        kasir_sumTotalMakanan.Text = sumPesananMakanan.ToString("N0");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus makanan! \n" + ex.Message, "Error");
                    }
                    //hitung list
                    kasir_totalPesananMakanan.Text = PesananMakanan.Count().ToString();
                }
            }
        }
        void buttonHapusDataMinuman_click(object sender, EventArgs e)
        {
            //get Button click
            Button btn = sender as Button;
            //Untuk Minuman
            for (int i = 0; i < listKomponenMinuman.Count; i++)
            {
                if (listKomponenMinuman[i][4] == btn)
                {
                    try
                    {
                        //menghapus komponen secara visual
                        foreach (Component ko in listKomponenMinuman[i])
                        {
                            kasir_wrapperPesananMinuman.Controls.Remove((Control)ko);
                        }
                        //menghapus listkomponen ke-i
                        listKomponenMinuman.Remove(listKomponenMinuman[i]);
                        //menghapus list pesanan
                        PesananMinuman.Remove(PesananMinuman[i]);
                        MessageBox.Show("Minuman dihapus!");
                        // untuk menghitung sum total pesan
                        int sum = 0;
                        foreach (string[] detailPesan in PesananMinuman)
                        {
                            sum = sum + int.Parse(detailPesan[3]);
                        }
                        sumPesananMinuman = sum;
                        kasir_sumTotalMinuman.Text = sumPesananMinuman.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus minuman! \n" + ex.Message, "Error");
                    }
                    //hitung list
                    kasir_totalPesananMinuman.Text = PesananMinuman.Count().ToString("N0");
                }
            }
        }


        private void kasir_cardRBMakanan_CheckedChanged(object sender, EventArgs e)
        {
            kasir_cardIdMenu.Clear();
            kasir_cardNamaMenu.Clear();
            kasir_cardHargaMenu.Clear();
            kasir_cardQtyMenu.Clear();
            if (kasir_cardRBMakanan.Checked)
            {
                kasir_chardCBMenuMinuman.Visible = false;
                kasir_chardCBMenuMakanan.Visible = true;
            }
            else
            {
                kasir_chardCBMenuMinuman.Visible = true;
                kasir_chardCBMenuMakanan.Visible = false;
            }

        }

        private void kasir_cardRBMinuman_CheckedChanged(object sender, EventArgs e)
        {
            kasir_cardIdMenu.Clear();
            kasir_cardNamaMenu.Clear();
            kasir_cardHargaMenu.Clear();
            kasir_cardQtyMenu.Clear();
            if (kasir_cardRBMinuman.Checked)
            {
                kasir_chardCBMenuMinuman.Visible = true;
                kasir_chardCBMenuMakanan.Visible = false;
            }
            else
            {
                kasir_chardCBMenuMinuman.Visible = false;
                kasir_chardCBMenuMakanan.Visible = true;
            }

        }

        private void kasir_chardCBMenuMakanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            string[] detailMenu = { };
            kasir_cardNamaMenu.Text = kasir_chardCBMenuMakanan.Text;
            if (kasir_cardNamaMenu.Text != "")
            {
                query = "SELECT * FROM makanan WHERE namaMakanan='" + kasir_cardNamaMenu.Text + "'";
                detailMenu = odb.getDetailMenu(query);
            }
            kasir_cardIdMenu.Text = detailMenu[0].ToString();
            kasir_cardHargaMenu.Text = detailMenu[3].ToString();
            string[] namaJenisFile = { detailMenu[1].ToString(), "NULL" };
            fn.tampilGambar(kasir_cardPBMenu, namaJenisFile, "Makanan");
            kasir_cardQtyMenu.Clear();
        }

        private void kasir_chardCBMenuMinuman_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            string[] detailMenu = { };
            kasir_cardNamaMenu.Text = kasir_chardCBMenuMinuman.Text;
            if (kasir_cardNamaMenu.Text != "")
            {
                query = "SELECT * FROM minuman WHERE namaMinuman='" + kasir_cardNamaMenu.Text + "'";
                detailMenu = odb.getDetailMenu(query);
            }
            kasir_cardIdMenu.Text = detailMenu[0].ToString();
            kasir_cardHargaMenu.Text = detailMenu[3].ToString();
            string[] namaJenisFile = { detailMenu[1].ToString(), "NULL" };
            fn.tampilGambar(kasir_cardPBMenu, namaJenisFile, "Minuman");
            kasir_cardQtyMenu.Clear();
        }

        private void kasir_cardBtTambahkan_Click(object sender, EventArgs e)
        {
            string noTransaksi = fn.getAutoID("Transaksi");
            string getTanggal = DateTime.Now.ToString("dd/MM/yyyy");
            string kasir = kasir_cardIdKasir.Text;
            int subMakanan = sumPesananMakanan;
            int subMinuman = sumPesananMinuman;
            int totalPesanan = sumPesananMakanan + sumPesananMinuman;
            totalHargaSeluruhPesanan = totalPesanan;

            if (kasir_cardQtyMenu.Text == "")
            {
                if (PesananMakanan.Count > 0 || PesananMinuman.Count > 0)
                {
                    kasir_cardCBPilihNominal.Visible = true;
                    kasir_cardNoTransaksi.Text = noTransaksi;
                    kasir_cardTanggalTransaksi.Text = getTanggal; //Tanggal transaksi
                    kassir_cardKasirTransaksi.Text = kasir; // kasir yang menangani
                    kasir_cardSubMakanan.Text = subMakanan.ToString("N0"); //jumlah pesanan makanan
                    kasir_cardSubMinuman.Text = subMinuman.ToString("N0"); //jumlah pesanan minuman
                    kasir_cardTotalPesananans.Text = totalPesanan.ToString("N0"); // jumlah pesanan makanan dan minuman
                    kasir_cardTotalDiskon.Text = "0"; //jumlah total diskon yang diperolwh

                    //set enabe
                    kasir_cardPilihMenu.Enabled = false;
                    kasir_cardKontainerMakanan.Enabled = false;
                    kasir_cardKontainerMinuman.Enabled = false;
                    kasir_cardBtTambahkan.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Belum ada pesanan yang ditambahkan!", "Peringatan");
                }

            }
            else
            {
                MessageBox.Show("Ada 1 pesanan belum ditambahkan!", "Peringatan");
            }


        }
        private void kaisr_cardBtBatal_Click(object sender, EventArgs e)
        {
            //set enable dan visible
            kasir_cardPilihMenu.Enabled = true;
            kasir_cardKontainerMakanan.Enabled = true;
            kasir_cardKontainerMinuman.Enabled = true;
            kasir_cardBtTambahkan.Enabled = true;
            kasir_cardCBPilihNominal.Visible = false;

            //setText
            TextBox[] grupTBTransaksi =
            {
                kasir_cardNoTransaksi, kasir_cardTanggalTransaksi, kassir_cardKasirTransaksi, kasir_cardSubMakanan,
                kasir_cardSubMinuman, kasir_cardTotalPesananans, kasir_cardTotalDiskon, kasir_cardTunai, kasir_cardTotalKembalian
            };
            foreach (TextBox tb in grupTBTransaksi)
            {
                tb.Clear();
            }
        }
        private void kasir_cardCBPilihNominal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Tunai = int.Parse(kasir_cardCBPilihNominal.Text);
            int Kembalian = 0;
            //txbox Tunai
            kasir_cardTunai.Text = Tunai.ToString("N0");
            if (Tunai >= totalHargaSeluruhPesanan)
            {
                Kembalian = Tunai - totalHargaSeluruhPesanan;
                kasir_cardTotalKembalian.Text = Kembalian.ToString("N0");
            }
            else
            {
                MessageBox.Show("Uang tunai Kurang!", "Peringatan");
                kasir_cardTunai.Clear();
                kasir_cardTotalKembalian.Clear();
            }
        }
        private void kasir_cardCBNominal_textChanged(object sender, EventArgs e)
        {
            int Tunai = 0;
            int Kembalian = 0;
            kasir_cardTunai.Text = Tunai.ToString("N0");
            if (Information.IsNumeric(kasir_cardCBPilihNominal.Text))
            {
                Tunai = int.Parse(kasir_cardCBPilihNominal.Text);
                kasir_cardTunai.Text = Tunai.ToString("N0");
                if (Tunai >= totalHargaSeluruhPesanan)
                {
                    Kembalian = Tunai - totalHargaSeluruhPesanan;
                    kasir_cardTotalKembalian.Text = Kembalian.ToString("N0");
                    kasir_cardTransaksiNotifikasi.Clear();
                    kasir_cardTransaksiNotifikasi.ForeColor = Color.Black;
                }
                else
                {
                    kasir_cardTransaksiNotifikasi.Text = "Nominal Kurang!";
                    kasir_cardTransaksiNotifikasi.ForeColor = Color.Red;
                    kasir_cardTunai.Clear();
                    kasir_cardTotalKembalian.Clear();
                }
            }
            else
            {
                kasir_cardTransaksiNotifikasi.Text = "Harus berupa angka!";
                kasir_cardTransaksiNotifikasi.ForeColor = Color.Red;
                kasir_cardTunai.Clear();
                kasir_cardTotalKembalian.Clear();
            }
        }
        private void kasir_card_BtCheckOut_Click(object sender, EventArgs e)
        {
            //Pelanggan
            string idPelanggan = kasir_cardIdPelanggan.Text;
            string tanggalDatang = DateTime.Now.ToString("D");
            string waktuDatang = DateTime.Now.ToString("t");
            string[] valuePelanggan = { idPelanggan, tanggalDatang, waktuDatang, kasir_cardCBPilihNominal.Text };
            //Transaksi
            string idTransaksi = kasir_cardNoTransaksi.Text;
            string tanggalTransaksi = kasir_cardTanggalTransaksi.Text;
            string idKasir = kassir_cardKasirTransaksi.Text;
            string subMakanan = sumPesananMakanan.ToString();
            string subMinuman = sumPesananMinuman.ToString();
            string diskon = "0";
            string totalBayar = totalHargaSeluruhPesanan.ToString();
            // grup textBox
            TextBox[] textBoxsTrans =
            {
                kasir_cardNoTransaksi, kasir_cardTanggalTransaksi, kassir_cardKasirTransaksi, kasir_cardSubMakanan, kasir_cardSubMinuman, kasir_cardTotalPesananans,
                kasir_cardTotalDiskon, kasir_cardTunai, kasir_cardTotalKembalian, kasir_sumTotalMakanan, kasir_sumTotalMinuman, kasir_cardTransaksiNotifikasi
            };
            //------parameters transaksi
            SqlParameter noTransaksi = new SqlParameter("@noTransaksi", SqlDbType.VarChar);
            SqlParameter tanggal = new SqlParameter("@tanggalTrans", SqlDbType.VarChar);
            SqlParameter pelanggan = new SqlParameter("@idPelanggan", SqlDbType.VarChar);
            SqlParameter kasir = new SqlParameter("@idKasir", SqlDbType.VarChar);
            SqlParameter tMakanan = new SqlParameter("@subMakanan", SqlDbType.VarChar);
            SqlParameter tMinuman = new SqlParameter("@subMinuman", SqlDbType.VarChar);
            SqlParameter tDiskon = new SqlParameter("@diskon", SqlDbType.VarChar);
            SqlParameter totBayar = new SqlParameter("@totalBayar", SqlDbType.Int);
            SqlParameter[] valueTransaksi = { noTransaksi, tanggal, pelanggan, kasir, tMakanan, tMinuman, tDiskon, totBayar };

            try
            {
                if (valuePelanggan.Length > 0 && valueTransaksi.Length > 0 && (PesananMakanan.Count > 0 || PesananMinuman.Count > 0) && kasir_cardTotalKembalian.Text != "-"
                    && kasir_cardNoTransaksi.Text != "-" && kasir_cardTanggalTransaksi.Text != "" && kasir_cardTotalKembalian.Text != "")
                {
                    if (odb.insertTable("Pelanggan", valuePelanggan) == true)
                    {
                        noTransaksi.Value = idTransaksi;
                        tanggal.Value = tanggalTransaksi;
                        pelanggan.Value = idPelanggan;
                        kasir.Value = idKasir;
                        tMakanan.Value = subMakanan;
                        tMinuman.Value = subMinuman;
                        tDiskon.Value = diskon;
                        totBayar.Value = totalBayar;
                        if (odb.commandLoginAdmin("Insert_Transaksi", valueTransaksi) == true)
                        {
                            if (odb.insertDataList(idTransaksi, PesananMakanan, PesananMinuman) == true)
                            {
                                //reset jumlah pesanan
                                sumPesananMakanan = 0;
                                sumPesananMinuman = 0;
                                totalHargaSeluruhPesanan = 0;
                                //
                                MessageBox.Show("berhasil!", "Succes");
                                // enable tiap komponen
                                kasir_cardPilihMenu.Enabled = true;
                                kasir_cardKontainerMakanan.Enabled = true;
                                kasir_cardKontainerMinuman.Enabled = true;
                                kasir_cardBtTambahkan.Enabled = true;
                                // auto idPelanggan
                                kasir_cardIdPelanggan.Text = fn.getAutoID("Pelanggan");
                                // clear lis peananan
                                PesananMakanan.Clear();
                                PesananMinuman.Clear();
                                // text total list pesanan
                                kasir_totalPesananMakanan.Text = PesananMakanan.Count().ToString();
                                kasir_totalPesananMinuman.Text = PesananMinuman.Count().ToString();
                                // clear komponen makanan dan minuman
                                foreach (Component[] komponen in listKomponenMakanan)
                                {
                                    foreach (Component komp in komponen)
                                    {
                                        kasir_wrapperPesananMakanan.Controls.Remove((Control)komp);
                                    }
                                }
                                foreach (Component[] komponen in listKomponenMinuman)
                                {
                                    foreach (Component komp in komponen)
                                    {
                                        kasir_wrapperPesananMinuman.Controls.Remove((Control)komp);
                                    }
                                }
                                //clear textBox
                                foreach (TextBox tbTrans in textBoxsTrans)
                                {
                                    tbTrans.Clear();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Record transaksi gagal", "Error");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gagal Bagian transaksi", "Error");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tidak bisa check out.\nProses tidak sesuai prosedur!", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal!\n" + ex.Message, "Error");
            }

            //Refresh card pada dashboard
            int penghasilanBruto = odb.getPenhasilanBruto();
            textBox1.Text = "Rp " + penghasilanBruto.ToString("N0");
            //
            odb.getMenuTerlasris(textBox8, textBox2, textBox4, pictureBox2, "Makanan");
            odb.getMenuTerlasris(textBox10, textBox6, textBox5, pictureBox1, "Minuman");

        }
        // ======================================================================= ADMIN =============================================================
        private void BT_ADMIN_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_ADMIN, wrapperAdmin);
            admin_cbJenisTabel.Text = "-Pilih Tabel-";
            toolStripLabel1.Text = "Admin";

        }
        private void admin_cardBtLogin_Click(object sender, EventArgs e)
        {
            string namaKasir = InfoAkun[2].ToString();
            string idKasir = InfoAkun[0].ToString();

            SqlParameter id = new SqlParameter("@id", SqlDbType.VarChar);
            SqlParameter Username = new SqlParameter("@Username", SqlDbType.VarChar);
            SqlParameter Password = new SqlParameter("@Password", SqlDbType.VarChar);

            SqlParameter[] parameters = { id, Username, Password };
            if (admin_cardTxUsername.Text != "" && admin_cardTxPassword.Text != "")
            {
                id.Value = idKasir;
                Username.Value = admin_cardTxUsername.Text;
                Password.Value = fn.Crypt2(admin_cardTxPassword.Text);
                if (odb.commandLoginAdmin("LoginAdmin", parameters) == true)
                {
                    admin_cardAfterLogin.Visible = true;
                    admin_cardLogin.Visible = false;
                    admin_cardCB.Visible = true;
                    admin_txNamaAfterLogin.Text = namaKasir;
                    admin_txIdAfterLogin.Text = idKasir;
                    MessageBox.Show("Berhasil Login sebagi admin");
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
        private string[] CB_JenisTabel = { "Kasir", "Makanan", "Minuman", "Transaksi" };
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
                admin_cardBtUbah.Visible = false;
                admin_cardBtTambahkan.Visible = false;
                admin_cardBtHapus.Visible = false;
                admin_cardPB.BackgroundImage = null;
                //button tambah dan reset
                kom.textBx_Reset(admin_GrupTextBox, null);
                admin_cardBtTambahData.Visible = false;
                //=====
                query = "SELECT idKasir, gambarKasir, namaKasir, noTelepon, statusAdmin, tipeProfil FROM kasir";
            }
            else if (admin_cbJenisTabel.SelectedItem == "Makanan")
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
                admin_cardBtUbah.Visible = false;
                admin_cardBtTambahkan.Visible = false;
                admin_cardBtHapus.Visible = false;
                admin_cardPB.BackgroundImage = null;
                //button tambah dan reset
                kom.textBx_Reset(admin_GrupTextBox, null);
                admin_cardBtTambahData.Visible = true;
                admin_cardPB.Visible = true;
                //===
                query = "SELECT idMakanan, gambarMakanan, namaMakanan, hargaMakanan FROM makanan WHERE ketersediaan='Yes'";
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
                admin_cardBtUbah.Visible = false;
                admin_cardBtTambahkan.Visible = false;
                admin_cardBtHapus.Visible = false;
                admin_cardPB.BackgroundImage = null;
                //button tambah dan reset
                kom.textBx_Reset(admin_GrupTextBox, null);
                admin_cardBtTambahData.Visible = true;
                admin_cardPB.Visible = true;

                //===
                query = "SELECT idMinuman, gambarMinuman, namaMinuman, hargaMinuman FROM minuman WHERE ketersediaan='Yes'";
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
                admin_cardLabelJenisTabel.Text = "Tipe";
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = false;
                admin_cardBtTambahkan.Visible = false;
                admin_cardBtHapus.Visible = false;
                admin_cardPB.BackgroundImage = null;
                //button tambah dan reset
                kom.textBx_Reset(admin_GrupTextBox, null);
                admin_cardBtTambahData.Visible = true;
                //===
                query = "SELECT * FROM transaksi";
            }
            else
            {
                MessageBox.Show("Jenis tabel belum dipilih!", "Peringatan");
                //button ubah dan simpan
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = true;
                //button tambah dan reset
                kom.textBx_Reset(admin_GrupTextBox, null);
                admin_cardBtTambahData.Visible = true;
                admin_cardPB.Visible = true;
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
            if (admin_cardLabelJenisTabel.Text == "Kasir" || admin_cardLabelJenisTabel.Text == "kasir")
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
            string id = admin_cardTxID.Text;
            string gambar = fn.fileNameUpload;// set deafault nama gambar
            string namaMenu = admin_cardTxNama.Text;
            string hargaMenu = admin_cardTxHarga_orTelp.Text;
            string jenisUpload = admin_cardLabelJenisTabel.Text;
            if (id != "" && namaMenu != "" && Information.IsNumeric(admin_cardTxHarga_orTelp.Text))
            {
                string query = "";
                string queryTampil = "";
                if (admin_cardLabelJenisTabel.Text == "Makanan")
                {
                    if (gambar == "")
                    {
                        gambar = label28.Text;
                    }
                    query = "UPDATE makanan set gambarMakanan='" + gambar + "', namaMakanan='" + namaMenu + "', hargaMakanan='" + hargaMenu + "' WHERE idMakanan='" + id + "'";
                    queryTampil = "SELECT idMakanan, gambarMakanan, namaMakanan, hargaMakanan FROM makanan WHERE idMakanan='" + id + "'";
                }
                else if (admin_cardLabelJenisTabel.Text == "Minuman")
                {
                    if (gambar == "")
                    {
                        gambar = label28.Text;
                    }
                    query = "UPDATE minuman set gambarMinuman='" + gambar + "', namaMinuman='" + namaMenu + "', hargaMinuman='" + hargaMenu + "' WHERE idMinuman='" + id + "'";
                    queryTampil = "SELECT idMinuman, gambarMinuman, namaMinuman, hargaMinuman FROM minuman WHERE idMinuman='" + id + "'";
                }
                else if (admin_cardLabelJenisTabel.Text == "Kasir")
                {
                    if (gambar == "")
                    {
                        gambar = label28.Text;
                    }
                    string statusAdmin = admin_cardTxStatusAdmin.Text;
                    query = "UPDATE kasir SET statusAdmin='" + statusAdmin + "' WHERE idKasir='" + id + "'";
                    queryTampil = "SELECT idKasir, gambarKasir, namaKasir, noTelepon, statusAdmin, tipeProfil FROM kasir WHERE idKasir='" + id + "'";
                }
                fn.saveUploaded(jenisUpload);
                odb.iudDataTunggal(query, "update");
                odb.showDataGV(queryTampil, admin_gvTabel);
                admin_cardPB.BackgroundImage = null;
                //After
                TextBox[] admin_GrupTextBox = { admin_cardTxID, admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin };
                kom.textBx_ReadOnly(admin_GrupTextBox, true, null);
                kom.textBx_Reset(admin_GrupTextBox, null);
                admin_cardTxID.BackColor = kom.primary;
                admin_cardBtUbahFoto.Visible = false;
                //button ubah dan simpan dan upload gambar
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = false;
                admin_cardBtUpload.Visible = false;
                admin_cardBtHapus.Visible = false;
            }
            else
            {
                MessageBox.Show("Berhasil!\nTidak ada perubahan data", "Succes");
            }
        }
        private void admin_cardBtUbahFoto_Click(object sender, EventArgs e)
        {
            admin_cardBtUpload.Visible = true;
        }
        private void admin_cardBtUpload_Click(object sender, EventArgs e) //ButtonUploadGambar
        {
            string jenisUpload = admin_cardLabelJenisTabel.Text;
            fn.uploadFoto(admin_cardPB, label28, jenisUpload);
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
            // visibilitas
            admin_cardBtTambahkan.Visible = true;
            admin_cardBtSimpan.Visible = false;
            admin_cardBtUbah.Visible = false;
            admin_cardBtHapus.Visible = false;
            admin_cardBtUbahFoto.Visible = true;
            admin_cardPB.BackgroundImage = null;
            // logika if
            if (admin_cardLabelJenisTabel.Text == "Makanan")
            {
                admin_cardTxID.Text = fn.getAutoID("Makanan");
            }
            else if (admin_cardLabelJenisTabel.Text == "Minuman")
            {
                admin_cardTxID.Text = fn.getAutoID("Minuman");
            }
        }
        private void admin_cardBtTambahkan_Click(object sender, EventArgs e)
        {
            string id = admin_cardTxID.Text;
            string gambar = fn.fileNameUpload;// setGambarNama
            string namaMenu = admin_cardTxNama.Text;
            string hargaMenu = admin_cardTxHarga_orTelp.Text;
            string jenisUpload = admin_cardLabelJenisTabel.Text;
            if (id != "" && gambar != "" && namaMenu != "" && Information.IsNumeric(admin_cardTxHarga_orTelp.Text))
            {
                string query = "";
                string queryTampil = "";
                if (admin_cardLabelJenisTabel.Text == "Makanan")
                {
                    query = "INSERT INTO makanan(idMakanan, gambarMakanan, namaMakanan, hargaMakanan)" +
                        "VALUES ('" + id + "','" + gambar + "','" + namaMenu + "','" + hargaMenu + "')";
                    queryTampil = "SELECT idMakanan, gambarMakanan, namaMakanan, hargaMakanan FROM makanan WHERE idMakanan='" + id + "'";
                }
                else if (admin_cardLabelJenisTabel.Text == "Minuman")
                {
                    query = "INSERT INTO minuman(idMinuman, gambarMinuman, namaMinuman, hargaMinuman)" +
                        "VALUES ('" + id + "','" + gambar + "','" + namaMenu + "','" + hargaMenu + "')";
                    queryTampil = "SELECT idMinuman, gambarMinuman, namaMinuman, hargaMinuman FROM minuman WHERE idMinuman='" + id + "'";
                }
                fn.saveUploaded(jenisUpload);
                odb.iudDataTunggal(query, "Tambahkan");
                odb.showDataGV(queryTampil, admin_gvTabel);
                admin_cardPB.BackgroundImage = null;
                //After
                TextBox[] admin_GrupTextBox = { admin_cardTxID, admin_cardTxNama, admin_cardTxHarga_orTelp, admin_cardTxStatusAdmin };
                kom.textBx_Reset(admin_GrupTextBox, null);
                kom.textBx_ReadOnly(admin_GrupTextBox, true, admin_cardTxID);
                admin_cardTxID.BackColor = kom.primary;
                admin_cardBtTambahkan.Visible = false;
                admin_cardBtSimpan.Visible = false;
                admin_cardBtUbah.Visible = false;
                admin_cardBtUbahFoto.Visible = false;
                admin_cardBtHapus.Visible = false;
            }
            else
            {
                MessageBox.Show("Kolom wajin diisi!", "Peringatan");
            }
        }
        private void admin_gvTabel_cellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = admin_gvTabel.CurrentRow.Index;
            string key_id = admin_gvTabel.Rows[indexRow].Cells[0].Value.ToString();

            string id = admin_gvTabel.Rows[indexRow].Cells[0].Value.ToString();
            string gambar = admin_gvTabel.Rows[indexRow].Cells[1].Value.ToString();
            string nama = admin_gvTabel.Rows[indexRow].Cells[2].Value.ToString();
            string harga, statusAdmin, jenisGambar, jenisFile;
            try
            {
                if (indexRow != null)
                {
                    if (admin_cbJenisTabel.Text == "Kasir")
                    {
                        harga = admin_gvTabel.Rows[indexRow].Cells[3].Value.ToString();
                        statusAdmin = admin_gvTabel.Rows[indexRow].Cells[4].Value.ToString();
                        jenisGambar = "Profil";
                        jenisFile = admin_gvTabel.Rows[indexRow].Cells[5].Value.ToString();
                        //
                        admin_cardBtSimpan.Visible = true;
                        admin_cardBtUbah.Visible = true;
                        admin_cardBtTambahkan.Visible = false;
                        admin_cardBtHapus.Visible = false;
                        //
                        admin_cardTxID.Text = id;
                        admin_cardTxNama.Text = nama;
                        admin_cardTxHarga_orTelp.Text = harga;
                        admin_cardTxStatusAdmin.Text = statusAdmin;
                        //odb.showDataGV(query, admin_gvTabel);
                        string[] NamajenisFile = { gambar, jenisFile };
                        fn.tampilGambar(admin_cardPB, NamajenisFile, jenisGambar);
                    }
                    else if (admin_cbJenisTabel.Text == "Makanan")
                    {
                        harga = admin_gvTabel.Rows[indexRow].Cells[3].Value.ToString();
                        statusAdmin = "";
                        jenisGambar = "Makanan";
                        jenisFile = "NULL";
                        //
                        admin_cardBtSimpan.Visible = true;
                        admin_cardBtUbah.Visible = true;
                        admin_cardBtTambahkan.Visible = false;
                        admin_cardBtHapus.Visible = true;
                        //
                        admin_cardTxID.Text = id;
                        admin_cardTxNama.Text = nama;
                        admin_cardTxHarga_orTelp.Text = harga;
                        admin_cardTxStatusAdmin.Text = statusAdmin;
                        //namaGambar
                        label28.Text = gambar;
                        //odb.showDataGV(query, admin_gvTabel);
                        string[] NamajenisFile = { gambar, jenisFile };
                        fn.tampilGambar(admin_cardPB, NamajenisFile, jenisGambar);
                    }
                    else if (admin_cbJenisTabel.Text == "Minuman")
                    {
                        harga = admin_gvTabel.Rows[indexRow].Cells[3].Value.ToString();
                        statusAdmin = "";
                        jenisGambar = "Minuman";
                        jenisFile = "NULL";
                        //
                        admin_cardBtSimpan.Visible = true;
                        admin_cardBtUbah.Visible = true;
                        admin_cardBtTambahkan.Visible = false;
                        admin_cardBtHapus.Visible = true;
                        //
                        admin_cardTxID.Text = id;
                        admin_cardTxNama.Text = nama;
                        admin_cardTxHarga_orTelp.Text = harga;
                        admin_cardTxStatusAdmin.Text = statusAdmin;
                        //namaGambar
                        label28.Text = gambar;
                        //odb.showDataGV(query, admin_gvTabel);
                        string[] NamajenisFile = { gambar, jenisFile };
                        fn.tampilGambar(admin_cardPB, NamajenisFile, jenisGambar);
                    }
                    else if (admin_cbJenisTabel.Text == "Transaksi")//ketika transaksi
                    {
                        admin_cardBtSimpan.Visible = false;
                        admin_cardBtUbah.Visible = false;
                        admin_cardBtTambahkan.Visible = false;
                        admin_cardBtHapus.Visible = false;

                        string query_dtMakanan = "SELECT * FROM dtMakanan WHERE noTransaksi = '" + key_id + "'";
                        string query_dtMinuman = "SELECT * FROM dtMinuman WHERE noTransaksi = '" + key_id + "'";
                        odb.showDataGV(query_dtMakanan, admin_gvDetailMakanan);
                        odb.showDataGV(query_dtMinuman, admin_gvDetailMinuman);
                    }
                }
                else
                {
                    MessageBox.Show("Tabel kosong!", "Peringatan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan !\n" + ex.Message, "Error");
            }
        }
        private void admin_cardBtHapus_Click(object sender, EventArgs e)
        {
            string query = "";
            string queryTampil = "";
            if (admin_cardLabelJenisTabel.Text == "Makanan")
            {
                query = "UPDATE makanan SET ketersediaan='No' WHERE idMakanan='" + admin_cardTxID.Text + "'";
                queryTampil = "SELECT idMakanan, gambarMakanan, namaMakanan, hargaMakanan FROM makanan WHERE ketersediaan='Yes'";
            }
            else if (admin_cardLabelJenisTabel.Text == "Minuman")
            {
                query = "UPDATE minuman SET ketersediaan='No' WHERE idMinuman='" + admin_cardTxID.Text + "'";
                queryTampil = "SELECT idMinuman, gambarMinuman, namaMinuman, hargaMinuman FROM minuman WHERE ketersediaan='Yes'";
            }

            DialogResult dr = MessageBox.Show("Yakin ingin Menghapus dari daftar menu?", "Konfirmasi Hapus", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                odb.iudDataTunggal(query, "Hapus");
            }
            admin_cardTxID.Clear();
            admin_cardTxNama.Clear();
            admin_cardTxHarga_orTelp.Clear();
            admin_cardTxStatusAdmin.Clear();
            admin_cardPB.BackgroundImage = null;
            admin_cardBtHapus.Visible = false; //visibilitas nonAktif
            odb.showDataGV(queryTampil, admin_gvTabel);
        }
        //======================================================================= DATABASE ===========================================================
        private void BT_DATABASE_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_DATABASE, wrapperDatabase);
            toolStripLabel1.Text = "Database";
            db_cardDetailTrans.Visible = false;
        }
        string[] tabelinDB =
        {
            "Kasir","Makanan","Minuman","Transaksi","Pelanggan"
        };
        private void db_CBTabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox[] tb_grupDB =
            {
                db_cardTxId ,db_cardTxNama, db_cardTxHarga
            };
            string pilihan = db_CBTabel.Text;
            if (pilihan != "")
            {
                string query = "";
                if (pilihan == "Kasir")
                {
                    query = "SELECT idKasir, gambarKasir, namaKasir, noTelepon, tipeProfil FROM kasir";
                    db_cardTampil.Enabled = true;
                    db_cardLabelJenis.Text = pilihan;
                    label31.Text = "Telp. :";
                    kom.textBx_Reset(tb_grupDB, null);
                    db_cardPBtampil.BackgroundImage = null;
                    db_cardDetailTrans.Visible = false;
                }
                else if (pilihan == "Makanan")
                {
                    query = "SELECT * FROM makanan WHERE ketersediaan='Yes'";
                    db_cardTampil.Enabled = true;
                    db_cardLabelJenis.Text = pilihan;
                    label31.Text = "Harga :";
                    kom.textBx_Reset(tb_grupDB, null);
                    db_cardPBtampil.BackgroundImage = null;
                    db_cardDetailTrans.Visible = false;
                }
                else if (pilihan == "Minuman")
                {
                    query = "SELECT * FROM minuman WHERE ketersediaan='Yes'";
                    db_cardTampil.Enabled = true;
                    db_cardLabelJenis.Text = pilihan;
                    label31.Text = "Harga :";
                    kom.textBx_Reset(tb_grupDB, null);
                    db_cardPBtampil.BackgroundImage = null;
                    db_cardDetailTrans.Visible = false;
                }
                else if (pilihan == "Transaksi")
                {
                    query = "SELECT * FROM transaksi";
                    kom.textBx_Reset(tb_grupDB, null);
                    db_cardPBtampil.BackgroundImage = null;
                    db_cardLabelJenis.Text = "";
                    db_cardTampil.Enabled = false;
                    db_cardDetailTrans.Visible = true;
                }
                else if (pilihan == "Pelanggan")
                {
                    query = "SELECT * FROM pelanggan";
                    kom.textBx_Reset(tb_grupDB, null);
                    db_cardPBtampil.BackgroundImage = null;
                    db_cardLabelJenis.Text = "";
                    db_cardTampil.Enabled = false;
                    db_cardDetailTrans.Visible = false;
                }
                odb.showDataGV(query, db_gVTabel);
            }
        }

        private void db_gVTabel_cellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = db_gVTabel.CurrentRow.Index;
            string key_id = db_gVTabel.Rows[indexRow].Cells[0].Value.ToString();
            string key_pg = db_gVTabel.Rows[indexRow].Cells[2].Value.ToString();
            if (key_id != null)
            {
                if (db_CBTabel.Text == "Transaksi" || db_CBTabel.Text == "Pelanggan")
                {
                    if (db_CBTabel.Text == "Transaksi")
                    {
                        string qdtMakanan = "SELECT * FROM dtMakanan WHERE noTransaksi='" + key_id + "' ";
                        string qdtMinuman = "SELECT * FROM dtMinuman WHERE noTransaksi='" + key_id + "' ";
                        string qdtBayar = "SELECT cash, kembalian FROM pelanggan WHERE idPelanggan='" + key_pg + "' ";

                        odb.showDataGV(qdtMakanan, db_gVdetMakanan);
                        odb.showDataGV(qdtMinuman, db_gVdetMinuman);
                        odb.showDataGV(qdtBayar, db_GvDetBayar);
                    }
                }
                else
                {
                    string id = db_gVTabel.Rows[indexRow].Cells[0].Value.ToString();
                    string gambar = db_gVTabel.Rows[indexRow].Cells[1].Value.ToString();
                    string nama = db_gVTabel.Rows[indexRow].Cells[2].Value.ToString();
                    if (db_CBTabel.Text == "Makanan" || db_CBTabel.Text == "Minuman")
                    {
                        string harga = db_gVTabel.Rows[indexRow].Cells[3].Value.ToString();
                        string[] namaJenisFile = { gambar, "Gambar" };
                        db_cardTxHarga.Text = harga;
                        if (db_CBTabel.Text == "Makanan")
                        {
                            fn.tampilGambar(db_cardPBtampil, namaJenisFile, "Makanan");
                        }
                        else
                        {
                            fn.tampilGambar(db_cardPBtampil, namaJenisFile, "Minuman");
                        }
                    }
                    else
                    {
                        string[] namaJenisFile = { gambar, db_gVTabel.Rows[indexRow].Cells[4].Value.ToString() };
                        string telp = db_gVTabel.Rows[indexRow].Cells[3].Value.ToString();

                        db_cardTxHarga.Text = telp;
                        fn.tampilGambar(db_cardPBtampil, namaJenisFile, "Profil");
                    }
                    db_cardTxId.Text = id;
                    db_cardTxNama.Text = nama;
                }
            }
            else
            {

            }
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

            fn.tampilGambar(profile_pictureBox, namaJenisFile, "Profil");
            string tampilTransaksiProfile = "SELECT * FROM transaksi WHERE idKasir='" + profile_txIdKasir.Text + "'";
            odb.showDataGV(tampilTransaksiProfile, profile_GvTransaksi);
            toolStripLabel1.Text = "Profile";
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
            kom.textBx_ReadOnly(profile_grupTextBox, false, null);
            profile_btSimpanProfile.Visible = true;
            profile_btGantiProfile.Visible = true;
        }

        private void profile_btSimpanProfile_Click(object sender, EventArgs e)
        {
            TextBox[] profile_grupTextBox =
            {
                profile_txNamaKasir,profile_txTeleponKasir,profile_txUsernameKasir,profile_txPasswordKasir
            };
            kom.textBx_ReadOnly(profile_grupTextBox, true, null);


            string namaProcedure = "UpdateKasir";
            SqlParameter id = new SqlParameter("@id", SqlDbType.VarChar);
            SqlParameter gambar = new SqlParameter("@gambar", SqlDbType.VarChar);
            SqlParameter nama = new SqlParameter("@nama", SqlDbType.VarChar);
            SqlParameter telepon = new SqlParameter("@telp", SqlDbType.VarChar);
            SqlParameter Username = new SqlParameter("@uname", SqlDbType.VarChar);
            SqlParameter Password = new SqlParameter("@pass", SqlDbType.VarChar);
            SqlParameter tipeProfile = new SqlParameter("@tipeGambar", SqlDbType.VarChar);

            SqlParameter[] parameters = { id, gambar, nama, telepon, Username, Password, tipeProfile };
            string uname = profile_txUsernameKasir.Text;
            string pass = fn.Crypt2(profile_txPasswordKasir.Text);

            gambar.Value = InfoAkun[1].ToString();
            if (profile_txNamaKasir.Text != "" && profile_txTeleponKasir.Text != "" && profile_txUsernameKasir.Text != "" && profile_txPasswordKasir.Text != "")
            {
                if (Information.IsNumeric(profile_txTeleponKasir.Text) == true)
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
                            odb.procedureIUD(namaProcedure, parameters, "Upadte");
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
        // cell_Click gridview
        private void profile_GvTransaksi_cellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = profile_GvTransaksi.CurrentRow.Index;
            string idTransaksi = profile_GvTransaksi.Rows[indexRow].Cells[0].Value.ToString();

            string tampilDetailPesanMakanan = "SELECT * FROM dtMakanan WHERE noTransaksi='" + idTransaksi + "'";
            string tampilDetailPesanMinuman = "SELECT * FROM dtMinuman WHERE noTransaksi='" + idTransaksi + "'";

            odb.showDataGV(tampilDetailPesanMakanan, profile_GvDtMakanan);
            odb.showDataGV(tampilDetailPesanMinuman, profile_GvDtMinuman);
        }
        // ============================================================= NAVIGASI LAPORAN ===========================================================
        private void BT_LAPORAN_Click(object sender, EventArgs e)
        {
            Navigasi_diCLICK(BT_LAPORAN, wrapperLaporan);
            toolStripLabel1.Text = "Laporan";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string dateSekarang = DateTime.Now.ToString("D");
            string[] parameters =
            {
                InfoAkun[0], InfoAkun[2], dateSekarang, laporan_txBulan.Text, laporan_txTahun.Text, textBox1.Text
            };
            if (laporan_txBulan.Text != "" && laporan_txTahun.Text != "" && laporan_txBulan.TextLength == 2 && laporan_txTahun.TextLength == 4)
            {
                if (Information.IsNumeric(laporan_txBulan.Text) && Information.IsNumeric(laporan_txTahun.Text))
                {
                    if (int.Parse(laporan_txBulan.Text) <= 12 && laporan_txBulan.Text != "00")
                    {
                        odb.reportPDF(reportViewer1, parameters);
                    }
                    else
                    {
                        MessageBox.Show("Kode bulan atau kode tahun salah!", "Peringatan");
                    }
                }
                else
                {
                    MessageBox.Show("Kode bulan dan tahun harus berupa kode angka 2 digit!", "Peringatan");
                }
            }
            else
            {
                MessageBox.Show("Bulan dan Tahun harus di isi!", "Peringatan");
            }
        }
        // =============================================================== TOMBOL LOG OUT ==============================================================
        private void BT_LOGOUT_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin Log Out?", "Log Out", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }
        // ============================================================================================================================================

    }
}

