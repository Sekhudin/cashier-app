using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace KantjaStuck
{
    class Komponen
    {
        //================= COLOR
        public Color primary = StringToColor("#1d3557");
        public Color secondary = StringToColor("#457b9d");
        public Color bar = StringToColor("#17c3b2");
        public Color wrapper = StringToColor("#f1faee");
        private static Color StringToColor(string colorStr) //methode untuk mengubah string menjadi color ---using componentModel
        {
            TypeConverter cc = TypeDescriptor.GetConverter(typeof(Color));
            var result = (Color)cc.ConvertFromString(colorStr);
            return result;
        }
        //============================ TOMBOL CLOSE dan MINIMIZE
        public void button_Close()
        {
            DialogResult dr = MessageBox.Show("Yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButtons.YesNo);
            if(dr== DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void button_Minimize(Form namaForm)
        {
            namaForm.WindowState = FormWindowState.Minimized;
        }
        // ==================================== WARNA AWAL BUTTON dan PANEL NAVIGASI
        public void colorBase_ButtonPanel(Button[] grupButton, Panel[] grupWrapper)
        {
            foreach(Button button in grupButton)
            {
                button.BackColor = secondary;
                button.ForeColor = Color.White;
            }
            foreach(Panel wrap in grupWrapper)
            {
                wrap.BackColor = Color.White;
            }
        }
        // ================================================= Button background Primary
        public void button_colorPrimary(Button[] grupButton, Button buttonKecuali)
        {
            if(buttonKecuali == null)
            {
                foreach( Button button in grupButton)
                {
                    button.BackColor = primary;
                    button.ForeColor = Color.White;
                }
            }
            else
            {
                foreach (Button button in grupButton)
                {
                    button.BackColor = primary;
                    button.ForeColor = Color.White;
                }
                buttonKecuali.BackColor = Color.Transparent;
                buttonKecuali.ForeColor = primary;
            }
        }
        // ================================================ TextBox Readonly True/false
        public void textBx_ReadOnly(TextBox [] grupTextBox, bool status, TextBox tbKecuali)
        {
            if (tbKecuali == null)
            {
                foreach (TextBox tb in grupTextBox)
                {
                    if (status == true)
                    {
                        tb.ReadOnly = true;
                        tb.BackColor = SystemColors.ControlLight;
                    }
                    else
                    {
                        tb.ReadOnly = false;
                        tb.BackColor = Color.White;
                    }
                }
            }
            else
            {
                foreach (TextBox tb in grupTextBox)
                {
                    if (status == true)
                    {
                        tb.ReadOnly = true;
                        tb.BackColor = SystemColors.ControlLight;
                        tbKecuali.ReadOnly = false;
                        tbKecuali.BackColor = primary;
                        tbKecuali.ForeColor = Color.White;
                    }
                    else
                    {
                        tb.ReadOnly = false;
                        tb.BackColor = Color.White;
                        tbKecuali.ReadOnly = true;
                        tbKecuali.BackColor = primary;
                        tbKecuali.ForeColor = Color.White;
                    }
                }
            }
        }
        // ==================================================== TextBox Header
        public void textBoxHeader(TextBox[] grupTextBox)
        {
            foreach(TextBox tx in grupTextBox)
            {
                tx.BackColor = primary;
                tx.ForeColor = Color.White;
            }
        }
        //====================================================== TextBox Reset Text
        public void textBx_Reset(TextBox[] grupTextBox, TextBox tbKecuali)
        {
            if (tbKecuali == null)
            {
                foreach(TextBox tb in grupTextBox)
                {
                    tb.Clear();
                }
            }
            else
            {
                foreach (TextBox tb in grupTextBox)
                {
                    if(tb != tbKecuali)
                    {
                        tb.Clear();
                    }
                }
            }
        }
       
    }
}


