using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
namespace KantjaStuck
{
    class FungsiMethod : KoneksiDB
    {
        //===== UNTUK LOGIN ========================================================
        public bool Connect_DB()
        {
            return this.cek_Koneksi();
        }
        public void LOGIN(string namaProcedure, SqlParameter[] parameters, Form namaForm)
        {
            this.commandParameter(namaProcedure, parameters, namaForm);
        }
        // =============================== GET AUTO ID untuk KASIR ============================
        public string getAutoID(string jenisId)
        { ///^SW\d{4}$/
            Regex patternKodeTahun = new Regex(@"(\d{4})");
            Regex patternKodeUrut = new Regex(@"(\d{2})$");
            string bulanIni = DateTime.Now.ToString("yyMM");
            string next_ID = "";
            string query = "";
            if (jenisId == "Kasir")
            {
                query = "SELECT MAX(right(idKasir,6)) FROM kasir";
            }else if (jenisId == "Makanan")
            {
                query = "SELECT MAX(right(idMakanan,4)) FROM makanan";
            }else if (jenisId == "Minuman")
            {
                query = "SELECT MAX(right(idMinuman,4)) FROM minuman";
            }else if (jenisId == "Transaksi")
            {
                query = "SELECT MAX(right(noTransaksi,6)) FROM transaksi";
            }
            else
            {
                query = "";
                MessageBox.Show("Anda salah memasukan tipe", "Error");
            }
            SqlConnection con = get_Koneksi();
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                dr.Read();
                string fullstring = dr[0].ToString();
                string kodeUrut = patternKodeUrut.Match(fullstring).Value.ToString();
                if (jenisId == "Kasir")
                {
                    string kodeTahun = patternKodeTahun.Match(fullstring).Value.ToString();
                    if (fullstring != "" && kodeTahun == bulanIni)
                    {
                        next_ID = "KS" + bulanIni + "0" + (int.Parse(kodeUrut) + 1).ToString();
                    }
                    else
                    {
                        next_ID = "KS" + bulanIni + "01";

                    }
                }
                else if (jenisId == "Makanan")
                {
                    if (fullstring != "")
                    {
                        next_ID = "MK-" + "00" + (int.Parse(kodeUrut) + 1).ToString();
                    }
                    else
                    {
                        next_ID = "MK-" + "00" + "01";
                    }
                }
                else if (jenisId == "Minuman")
                {
                    if (fullstring != "")
                    {
                        next_ID = "MN-" + "00" + (int.Parse(kodeUrut) + 1).ToString();
                    }
                    else
                    {
                        next_ID = "MN-" + "00" + "01";
                    }
                }
                else if (jenisId == "Transaksi")
                {
                    string kodeTahun = patternKodeTahun.Match(fullstring).Value.ToString();
                    if (fullstring != "" && kodeTahun == bulanIni)
                    {
                        next_ID = "TR" + bulanIni + "-0" + (int.Parse(kodeUrut) + 1).ToString();
                    }
                    else
                    {
                        next_ID = "TR" + bulanIni +"-"+ "01";
                    }
                }
                else
                {
                    MessageBox.Show("Gagal Generate autoNext-ID!", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal memuat Auto-ID");
            }
            dr.Close();
            return next_ID;
        }
        //==================== File Upload =====================
        public string fileNameUpload = "hahaha";
        private string fullPath = "";
        private string extensiGambar = "";
        public void uploadFoto(PictureBox namaPictureBox, Label labelJenis,string jenisUpload)
        {
            string namaGambar = "";
            string kodeUnik = DateTime.Now.ToString("ddMMyyHHmmss");
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg;*.jpeg;)|*.jpg;*.jpeg;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                namaPictureBox.BackgroundImage = new Bitmap(open.FileName);
                labelJenis.Text = "Gambar";
                this.fullPath = open.FileName;
                this.extensiGambar = Path.GetExtension(open.FileName);
                if (jenisUpload == "Profil")
                {
                    namaGambar = "IMG_" + kodeUnik + extensiGambar;
                }
                else if (jenisUpload == "Makanan")
                {
                    namaGambar = "MK_" + kodeUnik + extensiGambar;
                }
                else if (jenisUpload == "Minuman")
                {
                    namaGambar = "MK_" + kodeUnik + extensiGambar;
                }
                this.fileNameUpload = namaGambar;
            }
        }
        public bool saveUploaded(string jenisUpload)
        {
            try
            {
                if(fileNameUpload != "")
                {
                    if (jenisUpload == "Profil")
                    {
                        File.Copy(fullPath, Path.Combine(@"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\images\", fileNameUpload), true);
                    }else if (jenisUpload == "Makanan")
                    {
                        File.Copy(fullPath, Path.Combine(@"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\makanan\", fileNameUpload), true);
                    }
                    else if (jenisUpload == "Minuman")
                    {
                        File.Copy(fullPath, Path.Combine(@"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\minuman\", fileNameUpload), true);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
            return true;

        }
        //====== method untuk menampilkan gambar atau avatar
        public void tampilGambar(PictureBox namaPictureBox, string[] namaJenisFile, string jenisTampil)
        {
            string namaFile = namaJenisFile[0].ToString();
            string jenisFile = namaJenisFile[1].ToString();
            string path = "";
            //Regex reg = new Regex(@"([a-z][a-z][a-z])$");
            //string extensi = reg.Match(namaGambar).Value.ToLower().ToString();
            if(jenisTampil== "Profil")
            {
                if (jenisFile == "Avatar" || jenisFile == "avatar")
                {
                    path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\avatar\";
                }
                else if (jenisFile == "Gambar")
                {
                    path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\images\";
                }
                else
                {
                    namaFile = "clown.png";
                    MessageBox.Show("Foto Profil tidak ditemukan", "Error!");
                    path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\avatar\";
                }
            }else if(jenisTampil == "Makanan")
            {
                path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\makanan\";
            }
            else if(jenisTampil == "Minuman")
            {
                path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\minuman\";
            }
            try
            {
                string fullPath = path + namaFile;
                Bitmap b = new Bitmap(fullPath);
                namaPictureBox.BackgroundImage = b;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        // ======================================= Pilih avatar ================
        private string[] avatar = { "beard", "child", "clown", "hacker", "hero", "malika", "old_Lady", "pirate", "teacher", "woman" };
        public void pickAvatar(ComboBox namaCB, PictureBox namaPB, Label namaLabel)
        {
            string path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\src\avatar\";
            string namaFile = "";
            if (namaCB.SelectedItem == avatar[0])
            {
                namaFile = avatar[0].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[1])
            {
                namaFile = avatar[1].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[2])
            {
                namaFile = avatar[2].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[3])
            {
                namaFile = avatar[3].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[4])
            {
                namaFile = avatar[4].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[5])
            {
                namaFile = avatar[5].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[6])
            {
                namaFile = avatar[6].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[7])
            {
                namaFile = avatar[7].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[8])
            {
                namaFile = avatar[8].ToString() + ".png";
            }
            else if (namaCB.SelectedItem == avatar[9])
            {
                namaFile = avatar[9].ToString() + ".png";
            }
            else
            {
                namaFile = avatar[0].ToString() + ".png";
            }
            string fullPath = path + namaFile;
            Bitmap b = new Bitmap(fullPath);
            namaPB.BackgroundImage = b;
            namaLabel.Text = "Avatar";
        }





        public void loadingWrapper(Panel namaPanel, Timer timerLoad)
        {
            timerLoad.Start();
            int maxLod = 609;
            namaPanel.Height += 40;
            if (namaPanel.Height == maxLod)
            {
                timerLoad.Stop();
            }
        }
        //======================= ENCRYPTION AND DECRYPTION ===============================
        public static string Crypt(string text)
        {
            return Convert.ToBase64String(
                ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(text), null, DataProtectionScope.CurrentUser));
        }

        public static string Decrypt(string text)
        {
            return Encoding.Unicode.GetString(
                ProtectedData.Unprotect(
                     Convert.FromBase64String(text), null, DataProtectionScope.CurrentUser));
        }
        //gunakan encrypsi dan decripsi ke-2
        private byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public string Crypt2(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public string Decrypt2(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}
/* UNTUK GANTI ICON
string path = @"E:\KULIAH\SEMESTER 5\FRAMEWORK-IFB\praktikum\KantjaStuck\KantjaStuck\Resources\";
string namaFile = "";
if (cbAvatar.SelectedItem == avatar[0])
{
    namaFile = avatar[0].ToString() + ".png";
}
else if (cbAvatar.SelectedItem == avatar[1])
{
    namaFile = avatar[1].ToString() + ".png";
}
string fullPath = path + namaFile;
Bitmap b = new Bitmap(fullPath);
pb_avatar.BackgroundImage = b;
*/