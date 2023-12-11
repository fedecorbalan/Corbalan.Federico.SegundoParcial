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
            txtNombre = new TextBox();
            lblNombre = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            lblTitulo = new Label();
            groupBox1 = new GroupBox();
            rbtnPeludoNo = new RadioButton();
            rbtnPeludoSi = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
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
            // groupBox1
            // 
            groupBox1.Controls.Add(rbtnPeludoNo);
            groupBox1.Controls.Add(rbtnPeludoSi);
            groupBox1.Location = new Point(27, 159);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(293, 88);
            groupBox1.TabIndex = 17;
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
            // FormModificar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(356, 502);
            Controls.Add(groupBox1);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormModificar";
            Text = "FormModificar";
            Load += FormModificar_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtNombre;
        private Label lblNombre;
        private Button btnAceptar;
        private Button btnCancelar;
        private Label lblTitulo;
        private GroupBox groupBox1;
        private RadioButton rbtnPeludoNo;
        private RadioButton rbtnPeludoSi;
    }
}