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
            groupBox1 = new GroupBox();
            rbtnPeludoNo = new RadioButton();
            rbtnPeludoSi = new RadioButton();
            groupBox1.SuspendLayout();
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
            // groupBox1
            // 
            groupBox1.Controls.Add(rbtnPeludoNo);
            groupBox1.Controls.Add(rbtnPeludoSi);
            groupBox1.Location = new Point(23, 151);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(293, 88);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Es Peludo?";
            // 
            // rbtnPeludoNo
            // 
            rbtnPeludoNo.AutoSize = true;
            rbtnPeludoNo.Location = new Point(168, 42);
            rbtnPeludoNo.Name = "rbtnPeludoNo";
            rbtnPeludoNo.Size = new Size(61, 29);
            rbtnPeludoNo.TabIndex = 1;
            rbtnPeludoNo.TabStop = true;
            rbtnPeludoNo.Text = "No";
            rbtnPeludoNo.UseVisualStyleBackColor = true;
            // 
            // rbtnPeludoSi
            // 
            rbtnPeludoSi.AutoSize = true;
            rbtnPeludoSi.Location = new Point(68, 42);
            rbtnPeludoSi.Name = "rbtnPeludoSi";
            rbtnPeludoSi.Size = new Size(51, 29);
            rbtnPeludoSi.TabIndex = 0;
            rbtnPeludoSi.TabStop = true;
            rbtnPeludoSi.Text = "Si";
            rbtnPeludoSi.UseVisualStyleBackColor = true;
            // 
            // FormAgregar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(351, 519);
            Controls.Add(groupBox1);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormAgregar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitulo;
        private Button btnCancelar;
        private Button btnAceptar;
        private Label lblNombre;
        private TextBox txtNombre;
        private GroupBox groupBox1;
        private RadioButton rbtnPeludoNo;
        private RadioButton rbtnPeludoSi;
    }
}