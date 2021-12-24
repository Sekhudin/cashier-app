
namespace KantjaStuck
{
    partial class SplashScreen
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
            this.boxLogo = new System.Windows.Forms.PictureBox();
            this.progresBar = new System.Windows.Forms.Panel();
            this.progresStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.boxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // boxLogo
            // 
            this.boxLogo.BackColor = System.Drawing.Color.Gainsboro;
            this.boxLogo.BackgroundImage = global::KantjaStuck.Properties.Resources.kantjaTrans;
            this.boxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.boxLogo.Location = new System.Drawing.Point(77, 107);
            this.boxLogo.Name = "boxLogo";
            this.boxLogo.Size = new System.Drawing.Size(297, 51);
            this.boxLogo.TabIndex = 0;
            this.boxLogo.TabStop = false;
            // 
            // progresBar
            // 
            this.progresBar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.progresBar.Location = new System.Drawing.Point(0, 340);
            this.progresBar.Name = "progresBar";
            this.progresBar.Size = new System.Drawing.Size(450, 10);
            this.progresBar.TabIndex = 1;
            // 
            // progresStatus
            // 
            this.progresStatus.AutoSize = true;
            this.progresStatus.Font = new System.Drawing.Font("Poppins Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progresStatus.Location = new System.Drawing.Point(216, 237);
            this.progresStatus.Name = "progresStatus";
            this.progresStatus.Size = new System.Drawing.Size(19, 23);
            this.progresStatus.TabIndex = 2;
            this.progresStatus.Text = "-";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 50;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.progresStatus);
            this.Controls.Add(this.progresBar);
            this.Controls.Add(this.boxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.boxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boxLogo;
        private System.Windows.Forms.Panel progresBar;
        private System.Windows.Forms.Label progresStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

