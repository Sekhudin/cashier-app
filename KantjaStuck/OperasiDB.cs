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
        public SqlCommand command(string query)
        {
            SqlConnection conn = this.get_Koneksi();
            conn.Open();
            SqlCommand com = new SqlCommand(query, conn);
            return com;
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
                int usercount = (Int32)cmd.ExecuteScalar();
                if (usercount == 1)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Login sebagi admin");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
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
