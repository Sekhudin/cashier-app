using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace KantjaStuck
{
    class OperasiDB : FungsiMethod 
    {
        public bool insertDataList(string noTransaksi, List<string[]> pesananMakanan, List<string[]> pesananMinuman)
        {
            SqlConnection conn = this.get_Koneksi();
            conn.Open();
            try
            {
                //untuk detailMakanan
                foreach (string[] makanan in pesananMakanan)
                {
                    string idMakanan = makanan[0];
                    string qtyMakanan = makanan[2];
                    string totalCostMakanan = makanan[3].ToString();
                    string query = "INSERT INTO dtMakanan(noTransaksi, idMinuman, jumlahPesan, totalHarga)" +
                        "VALUES('" + noTransaksi + "','" + idMakanan + "','" + qtyMakanan + "','" + totalCostMakanan + "')";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.CommandType = CommandType.Text;
                    com.ExecuteNonQuery();
                }
                //untuk detailMinuman
                foreach (string[] minuman in pesananMinuman)
                {
                    string idMakanan = minuman[0];
                    string qtyMakanan = minuman[2];
                    string totalCostMakanan = minuman[3].ToString();
                    string query = "INSERT INTO dtMinuman(noTransaksi, idMinuman, jumlahPesan, totalHarga)" +
                        "VALUES('" + noTransaksi + "','" + idMakanan + "','" + qtyMakanan + "','" + totalCostMakanan + "')";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.CommandType = CommandType.Text;
                    com.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        //========================================================= REGISTRASI KASIR ===================================================
        public void registerKasir(string namaProcedure, SqlParameter[] parameters, Form namaForm,TextBox passwotd, TextBox cekPassword)
        {
            SqlConnection conn = get_Koneksi();
            conn.Open();
            SqlCommand cmd = new SqlCommand(namaProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            //coba jalankaan
            try
            {
                int usercount = (Int32)cmd.ExecuteScalar();
                if (usercount == 0)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Mendaftar!");
                    Login l = new Login();
                    l.Show();
                    namaForm.Hide();
                }
                else
                {
                    MessageBox.Show("Username atau Password sudah digunakan!","Error!");
                    passwotd.Clear();
                    cekPassword.Clear();
                    passwotd.Focus();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username atau Password sudah digunakan\n"+ex.Message, "ERROR!");
            }
        }
        // =========================================== Tampil data Into gridview
        public void showDataGV(string query, DataGridView namaGridview)
        {
            SqlConnection conn = get_Koneksi();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query,conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                namaGridview.DataSource = dt;
                namaGridview.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        // =========================================== Procedure Login sebagai Admin
        public bool commandLoginAdmin(string namaProcedure, SqlParameter[] parameters)
        {
            SqlConnection conn = get_Koneksi();
            conn.Open();
            SqlCommand cmd = new SqlCommand(namaProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            //coba jalankaan
            try
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                int akun = (Int32)cmd.ExecuteScalar();
                if (akun == 1)
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
            return true;
        }
        // ============================================ PROCEDURE INSERT UPDATE DELETE
        public void procedureIUD(string namaProcedure, SqlParameter[] parameters,string jenisProcedure)
        {
            SqlConnection conn = get_Koneksi();
            conn.Open();
            SqlCommand cmd = new SqlCommand(namaProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            //coba jalankaan
            try
            {
                cmd.ExecuteNonQuery();
                if(jenisProcedure== "Update")
                {
                    MessageBox.Show("Berhasil perbarui data!","Succes!");
                }else if(jenisProcedure== "Insert")
                {
                    MessageBox.Show("Berhasil menambahkan data!", "Succes!");
                }else if(jenisProcedure== "Delete")
                {
                    MessageBox.Show("Berhasil dihapus!", "Succes!");
                }

            }
            catch (Exception ex)
            {
                if (jenisProcedure == "Update")
                {
                    MessageBox.Show("Data gagal diperbarui!\n"+ex.Message,"Error!");
                }
                else if (jenisProcedure == "Insert")
                {
                    MessageBox.Show("Data gagal ditambahkan!\n" + ex.Message, "Error!");
                }
                else if (jenisProcedure == "Delete")
                {
                    MessageBox.Show("Data gagal dihapus!\n" + ex.Message, "Error!");
                }
            }
        }
        // insert data pada tabel
        public bool insertTable(string namaTabel, string[] dataInsert)
        {
            SqlConnection conn = get_Koneksi();
            conn.Open();
            string query = "";
            try
            {
                if (namaTabel == "Pelanggan")
                {
                    string id = dataInsert[0];
                    string tanggalDatang = dataInsert[1];
                    string waktuDatang = dataInsert[2];
                    query = "INSERT INTO pelanggan(idPelanggan, tanggalDatang, waktuDatang) VALUES('"+ id + "','"+ tanggalDatang + "','"+ waktuDatang + "')";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                return true;
            }catch //(Exception ex)
            {
                return false;
            }
            return true;
        }
        // Insert, update, delete data tunggal
        public bool iudDataTunggal(string query, string jenisOperasiDB)
        {
            SqlConnection conn = get_Koneksi();
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil di"+jenisOperasiDB,"Succes");
            }catch(Exception ex)
            {
                MessageBox.Show("Gagal " + jenisOperasiDB+"!\n"+ex.Message, "Error");
                return false;
            }
            return true;
        }
        // Memperoleh Array dafatr makanan atau minuman
        public string[] getDaftarMenu(string jenisMenu)
        {
            string query = "";
            string[] daftarMenu = { };
            SqlConnection conn = get_Koneksi();
            DataTable ds = new DataTable();
            conn.Open();
            try
            {
                if (jenisMenu == "Makanan")
                {
                    query = "SELECT namaMakanan FROM makanan";
                }
                else if (jenisMenu == "Minuman")
                {
                    query = "SELECT namaMinuman FROM minuman";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                daftarMenu = new string[ds.Rows.Count];
                for(int i=0; i < daftarMenu.Length; i++)
                {
                    daftarMenu[i] = ds.Rows[i].ItemArray[0].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Gagal memuat menu\n"+ex.Message,"Error");
            }
            return daftarMenu;
        }
        //Memperoleh detail menu dan tampung ke dalam array
        public string[] getDetailMenu(string query)
        {
            string[] detail = { };
            SqlConnection conn = get_Koneksi();
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        detail = new string[dr.FieldCount];
                        for (int i = 0; i < detail.Length; i++)
                        {

                            detail[i] = dr[i].ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Menu tidak ditemukan!", "Error");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data menu\n" + ex.Message, "Error");
            }

            return detail;
        }
        //Memperoleh data kasir dan menyimpanya pada sebuah array =============================
        public string[] getInfoKasir(string usernameKasir, string passwordKasir)
        {
            string[] akunKasir = { "id Kasir", "gambar", "nama", "telepon", "username", "password", "statusAdmin", "tipeProfil" };
            string query = "SELECT * FROM kasir WHERE usernameKasir='"+usernameKasir+"' AND passwordKasir='"+passwordKasir+"'";
            SqlConnection conn = this.get_Koneksi();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                try
                {
                    while (dr.Read())
                    {
                        for(int i=0; i< akunKasir.Length; i++)
                        {
                            akunKasir[i] = dr[i].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error!");
                }
            }
            else
            {
                MessageBox.Show("Akun Anda tidak ditemukan", "Error");
            }
            return akunKasir;
        }
        
    }
}
