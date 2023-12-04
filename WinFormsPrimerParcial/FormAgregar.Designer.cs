namespace WinFormsPrimerParcial
{
    partial class FormAgregar
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
            lblTitulo = new Label();
            btnCancelar = new Button();
            btnAceptar = new Button();
            lblNombre = new Label();
            txtNombre = new TextBox();
            txtEsPeludo = new TextBox();
            lblEsPeludo = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(127, 38);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "AÑADIR";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.PaleTurquoise;
            btnCancelar.Location = new Point(23, 458);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.PaleTurquoise;
            btnAceptar.Location = new Point(204, 458);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(112, 34);
            btnAceptar.TabIndex = 5;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(23, 66);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(160, 25);
            lblNombre.TabIndex = 6;
            lblNombre.Text = "Ingrese el nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(23, 103);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(293, 31);
            txtNombre.TabIndex = 7;
            // 
            // txtEsPeludo
            // 
            txtEsPeludo.Location = new Point(23, 197);
            txtEsPeludo.Name = "txtEsPeludo";
            txtEsPeludo.Size = new Size(293, 31);
            txtEsPeludo.TabIndex = 8;
            // 
            // lblEsPeludo
            // 
            lblEsPeludo.AutoSize = true;
            lblEsPeludo.Location = new Point(23, 157);
            lblEsPeludo.Name = "lblEsPeludo";
            lblEsPeludo.Size = new Size(106, 25);
            lblEsPeludo.TabIndex = 11;
            lblEsPeludo.Text = "¿Es peludo?";
            // 
            // FormAgregar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(351, 519);
            Controls.Add(lblEsPeludo);
            Controls.Add(txtEsPeludo);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormAgregar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitulo;
        private Button btnCancelar;
        private Button btnAceptar;
        private Label lblNombre;
        private TextBox txtNombre;
        private TextBox txtEsPeludo;
        private Label lblEsPeludo;
    }
}