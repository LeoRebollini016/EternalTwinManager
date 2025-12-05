
namespace EternalTwinManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private TextBox txtUser;
        private TextBox txtPassword;
        private Button btnRun;
        private TextBox txtLog;
        private void InitializeComponent()
        {
            SuspendLayout();
            this.txtUser = new TextBox();
            this.txtPassword = new TextBox();
            this.btnRun = new Button();
            this.txtLog = new TextBox();

            // User
            txtUser.Location = new Point(20, 20);
            txtUser.Width = 200;
            txtUser.PlaceholderText = "Usuario";

            // Password
            txtPassword.Location = new Point(20, 60);
            txtPassword.Width = 200;
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Contraseña";

            // Button
            btnRun.Location = new Point(20, 100);
            btnRun.Text = "Ejecutar tareas diarias";
            btnRun.Click += btnRun_Click;

            // Log
            txtLog.Location = new Point(20, 150);
            txtLog.Width = 400;
            txtLog.Height = 300;
            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;

            this.Controls.Add(txtUser);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnRun);
            this.Controls.Add(txtLog);

            this.Text = "Brute Automation";
            this.Width = 480;
            this.Height = 520;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
