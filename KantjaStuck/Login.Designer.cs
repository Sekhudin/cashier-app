
namespace KantjaStuck
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btMinimize = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.progresStatus = new System.Windows.Forms.Label();
            this.txUsername = new System.Windows.Forms.TextBox();
            this.panelUsername = new System.Windows.Forms.Panel();
            this.txPassword = new System.Windows.Forms.TextBox();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.iconPassword = new System.Windows.Forms.PictureBox();
            this.iconUsername = new System.Windows.Forms.PictureBox();
            this.txPesanError = new System.Windows.Forms.TextBox();
            this.ckLihatPassword = new System.Windows.Forms.CheckBox();
            this.timer_fadeIn = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUsername)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel1.Controls.Add(this.btMinimize);
            this.panel1.Controls.Add(this.btClose);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panel1.Size = new System.Drawing.Size(350, 50);
            this.panel1.TabIndex = 0;
            // 
            // btMinimize
            // 
            this.btMinimize.FlatAppearance.BorderSize = 0;
            this.btMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMinimize.Font = new System.Drawing.Font("Poppins Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMinimize.ForeColor = System.Drawing.Color.White;
            this.btMinimize.Location = new System.Drawing.Point(284, 12);
            this.btMinimize.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btMinimize.Name = "btMinimize";
            this.btMinimize.Size = new System.Drawing.Size(26, 26);
            this.btMinimize.TabIndex = 2;
            this.btMinimize.Text = "_";
            this.btMinimize.UseVisualStyleBackColor = true;
            this.btMinimize.Click += new System.EventHandler(this.btMinimize_Click);
            // 
            // btClose
            // 
            this.btClose.FlatAppearance.BorderSize = 0;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold);
            this.btClose.ForeColor = System.Drawing.Color.White;
            this.btClose.Location = new System.Drawing.Point(315, 12);
            this.btClose.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(26, 26);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "X";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::KantjaStuck.Properties.Resources.kantjaTrans;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(117, 8);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(117, 35);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // progresStatus
            // 
            this.progresStatus.AutoSize = true;
            this.progresStatus.Font = new System.Drawing.Font("Poppins Medium", 10F, System.Drawing.FontStyle.Bold);
            this.progresStatus.ForeColor = System.Drawing.Color.Black;
            this.progresStatus.Location = new System.Drawing.Point(148, 91);
            this.progresStatus.Name = "progresStatus";
            this.progresStatus.Size = new System.Drawing.Size(54, 25);
            this.progresStatus.TabIndex = 3;
            this.progresStatus.Text = "Login";
            // 
            // txUsername
            // 
            this.txUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txUsername.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txUsername.Location = new System.Drawing.Point(99, 171);
            this.txUsername.Margin = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.txUsername.Name = "txUsername";
            this.txUsername.Size = new System.Drawing.Size(192, 18);
            this.txUsername.TabIndex = 1;
            this.txUsername.Click += new System.EventHandler(this.username_Clicked);
            this.txUsername.TextChanged += new System.EventHandler(this.txUsername_TextChanged);
            this.txUsername.Leave += new System.EventHandler(this.username_Leave);
            // 
            // panelUsername
            // 
            this.panelUsername.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelUsername.Location = new System.Drawing.Point(99, 194);
            this.panelUsername.Name = "panelUsername";
            this.panelUsername.Size = new System.Drawing.Size(192, 2);
            this.panelUsername.TabIndex = 7;
            // 
            // txPassword
            // 
            this.txPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txPassword.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txPassword.Location = new System.Drawing.Point(99, 229);
            this.txPassword.Margin = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.txPassword.Name = "txPassword";
            this.txPassword.Size = new System.Drawing.Size(192, 18);
            this.txPassword.TabIndex = 1;
            this.txPassword.Click += new System.EventHandler(this.password_Clicked);
            this.txPassword.TextChanged += new System.EventHandler(this.txPassword_TextChanged);
            this.txPassword.Leave += new System.EventHandler(this.password_Leave);
            // 
            // panelPassword
            // 
            this.panelPassword.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelPassword.Location = new System.Drawing.Point(99, 252);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(192, 2);
            this.panelPassword.TabIndex = 7;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.LightSeaGreen;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(59, 307);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(232, 30);
            this.buttonLogin.TabIndex = 8;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.BackColor = System.Drawing.Color.White;
            this.buttonRegister.FlatAppearance.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.buttonRegister.FlatAppearance.BorderSize = 2;
            this.buttonRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegister.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegister.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.buttonRegister.Location = new System.Drawing.Point(59, 348);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(232, 30);
            this.buttonRegister.TabIndex = 8;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = false;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // iconPassword
            // 
            this.iconPassword.BackgroundImage = global::KantjaStuck.Properties.Resources.padlock;
            this.iconPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconPassword.Location = new System.Drawing.Point(59, 224);
            this.iconPassword.Margin = new System.Windows.Forms.Padding(50, 0, 10, 0);
            this.iconPassword.Name = "iconPassword";
            this.iconPassword.Size = new System.Drawing.Size(30, 30);
            this.iconPassword.TabIndex = 3;
            this.iconPassword.TabStop = false;
            // 
            // iconUsername
            // 
            this.iconUsername.BackgroundImage = global::KantjaStuck.Properties.Resources.profile_1_;
            this.iconUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconUsername.Location = new System.Drawing.Point(59, 166);
            this.iconUsername.Margin = new System.Windows.Forms.Padding(50, 0, 10, 0);
            this.iconUsername.Name = "iconUsername";
            this.iconUsername.Size = new System.Drawing.Size(30, 30);
            this.iconUsername.TabIndex = 3;
            this.iconUsername.TabStop = false;
            // 
            // txPesanError
            // 
            this.txPesanError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txPesanError.Font = new System.Drawing.Font("Poppins", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txPesanError.Location = new System.Drawing.Point(99, 141);
            this.txPesanError.Margin = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.txPesanError.Name = "txPesanError";
            this.txPesanError.Size = new System.Drawing.Size(192, 14);
            this.txPesanError.TabIndex = 9;
            this.txPesanError.Text = "test";
            this.txPesanError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ckLihatPassword
            // 
            this.ckLihatPassword.AutoSize = true;
            this.ckLihatPassword.Font = new System.Drawing.Font("Poppins", 6.75F, System.Drawing.FontStyle.Italic);
            this.ckLihatPassword.Location = new System.Drawing.Point(197, 260);
            this.ckLihatPassword.Name = "ckLihatPassword";
            this.ckLihatPassword.Size = new System.Drawing.Size(94, 20);
            this.ckLihatPassword.TabIndex = 10;
            this.ckLihatPassword.Text = "Lihat Password";
            this.ckLihatPassword.UseVisualStyleBackColor = true;
            this.ckLihatPassword.CheckedChanged += new System.EventHandler(this.ckLihatPassword_CheckedChanged);
            // 
            // timer_fadeIn
            // 
            this.timer_fadeIn.Enabled = true;
            this.timer_fadeIn.Interval = 50;
            this.timer_fadeIn.Tick += new System.EventHandler(this.timer_fadeIn_Tick);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 500);
            this.Controls.Add(this.ckLihatPassword);
            this.Controls.Add(this.txPesanError);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.progresStatus);
            this.Controls.Add(this.panelPassword);
            this.Controls.Add(this.panelUsername);
            this.Controls.Add(this.iconPassword);
            this.Controls.Add(this.iconUsername);
            this.Controls.Add(this.txPassword);
            this.Controls.Add(this.txUsername);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUsername)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btMinimize;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label progresStatus;
        private System.Windows.Forms.TextBox txUsername;
        private System.Windows.Forms.PictureBox iconUsername;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panelUsername;
        private System.Windows.Forms.TextBox txPassword;
        private System.Windows.Forms.PictureBox iconPassword;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.TextBox txPesanError;
        private System.Windows.Forms.CheckBox ckLihatPassword;
        private System.Windows.Forms.Timer timer_fadeIn;
    }
}