namespace WinFormsSegundoParcial
{
    partial class FormModificar
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
            lblEsPeludo = new Label();
            txtEsPeludo = new TextBox();
            txtNombre = new TextBox();
            lblNombre = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            lblTitulo = new Label();
            SuspendLayout();
            // 
            // lblEsPeludo
            // 
            lblEsPeludo.AutoSize = true;
            lblEsPeludo.Location = new Point(27, 158);
            lblEsPeludo.Name = "lblEsPeludo";
            lblEsPeludo.Size = new Size(106, 25);
            lblEsPeludo.TabIndex = 18;
            lblEsPeludo.Text = "¿Es peludo?";
            // 
            // txtEsPeludo
            // 
            txtEsPeludo.Location = new Point(27, 198);
            txtEsPeludo.Name = "txtEsPeludo";
            txtEsPeludo.Size = new Size(293, 31);
            txtEsPeludo.TabIndex = 17;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(27, 108);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(293, 31);
            txtNombre.TabIndex = 16;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(27, 66);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(160, 25);
            lblNombre.TabIndex = 15;
            lblNombre.Text = "Ingrese el nombre:";
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.PaleTurquoise;
            btnAceptar.Location = new Point(208, 439);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(112, 34);
            btnAceptar.TabIndex = 14;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.PaleTurquoise;
            btnCancelar.Location = new Point(27, 439);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(174, 38);
            lblTitulo.TabIndex = 12;
            lblTitulo.Text = "MODIFICAR";
            // 
            // FormModificar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(356, 502);
            Controls.Add(lblEsPeludo);
            Controls.Add(txtEsPeludo);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormModificar";
            Text = "FormModificar";
            Load += FormModificar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEsPeludo;
        private TextBox txtEsPeludo;
        private TextBox txtNombre;
        private Label lblNombre;
        private Button btnAceptar;
        private Button btnCancelar;
        private Label lblTitulo;
    }
}