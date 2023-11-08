namespace WinFormsPrimerParcial
{
    partial class FormLogin
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
            lblLogin = new Label();
            btnAceptar = new Button();
            txtMailUser = new TextBox();
            txtPassword = new TextBox();
            btnSalir = new Button();
            lblMailUser = new Label();
            lblPassword = new Label();
            SuspendLayout();
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblLogin.Location = new Point(12, 9);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(106, 38);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "LOGIN";
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.White;
            btnAceptar.Location = new Point(386, 381);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(261, 57);
            btnAceptar.TabIndex = 1;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtMailUser
            // 
            txtMailUser.Location = new Point(146, 129);
            txtMailUser.Name = "txtMailUser";
            txtMailUser.Size = new Size(511, 31);
            txtMailUser.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(146, 212);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(511, 31);
            txtPassword.TabIndex = 3;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.White;
            btnSalir.Location = new Point(136, 381);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(244, 57);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblMailUser
            // 
            lblMailUser.AutoSize = true;
            lblMailUser.Location = new Point(146, 84);
            lblMailUser.Name = "lblMailUser";
            lblMailUser.Size = new Size(132, 25);
            lblMailUser.TabIndex = 5;
            lblMailUser.Text = "E-Mail/Usuario:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(146, 175);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(105, 25);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña:";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Pink;
            ClientSize = new Size(800, 450);
            Controls.Add(lblPassword);
            Controls.Add(lblMailUser);
            Controls.Add(btnSalir);
            Controls.Add(txtPassword);
            Controls.Add(txtMailUser);
            Controls.Add(btnAceptar);
            Controls.Add(lblLogin);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LOGIN";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLogin;
        private Button btnAceptar;
        private TextBox txtMailUser;
        private TextBox txtPassword;
        private Button btnSalir;
        private Label lblMailUser;
        private Label lblPassword;
    }
}