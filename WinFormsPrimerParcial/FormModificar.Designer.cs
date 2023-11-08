namespace WinFormsPrimerParcial
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
            txtAtributo1 = new TextBox();
            txtPeludo = new TextBox();
            label1 = new Label();
            label3 = new Label();
            lblAtributo1 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnCancelar = new Button();
            btnAceptar = new Button();
            txtAtributo2 = new TextBox();
            lblAtributo2 = new Label();
            lblClase = new Label();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(24, 126);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(496, 31);
            txtNombre.TabIndex = 0;
            // 
            // txtAtributo1
            // 
            txtAtributo1.Location = new Point(24, 296);
            txtAtributo1.Name = "txtAtributo1";
            txtAtributo1.Size = new Size(496, 31);
            txtAtributo1.TabIndex = 1;
            // 
            // txtPeludo
            // 
            txtPeludo.Location = new Point(24, 207);
            txtPeludo.Name = "txtPeludo";
            txtPeludo.Size = new Size(496, 31);
            txtPeludo.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(111, 9);
            label1.Name = "label1";
            label1.Size = new Size(173, 45);
            label1.TabIndex = 5;
            label1.Text = "Modificar:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 82);
            label3.Name = "label3";
            label3.Size = new Size(78, 25);
            label3.TabIndex = 7;
            label3.Text = "Nombre";
            // 
            // lblAtributo1
            // 
            lblAtributo1.AutoSize = true;
            lblAtributo1.Location = new Point(24, 268);
            lblAtributo1.Name = "lblAtributo1";
            lblAtributo1.Size = new Size(156, 25);
            lblAtributo1.TabIndex = 8;
            lblAtributo1.Text = "Atributo de clase1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 179);
            label5.Name = "label5";
            label5.Size = new Size(98, 25);
            label5.TabIndex = 9;
            label5.Text = "Es peludo?";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 456);
            label6.Name = "label6";
            label6.Size = new Size(0, 25);
            label6.TabIndex = 10;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.PaleTurquoise;
            btnCancelar.Location = new Point(24, 447);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.PaleTurquoise;
            btnAceptar.Location = new Point(408, 447);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(112, 34);
            btnAceptar.TabIndex = 12;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtAtributo2
            // 
            txtAtributo2.Location = new Point(24, 384);
            txtAtributo2.Name = "txtAtributo2";
            txtAtributo2.Size = new Size(496, 31);
            txtAtributo2.TabIndex = 13;
            // 
            // lblAtributo2
            // 
            lblAtributo2.AutoSize = true;
            lblAtributo2.Location = new Point(24, 356);
            lblAtributo2.Name = "lblAtributo2";
            lblAtributo2.Size = new Size(156, 25);
            lblAtributo2.TabIndex = 14;
            lblAtributo2.Text = "Atributo de clase2";
            // 
            // lblClase
            // 
            lblClase.AutoSize = true;
            lblClase.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            lblClase.Location = new Point(290, 9);
            lblClase.Name = "lblClase";
            lblClase.Size = new Size(100, 45);
            lblClase.TabIndex = 15;
            lblClase.Text = "Clase";
            // 
            // FormModificar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(548, 503);
            Controls.Add(lblClase);
            Controls.Add(lblAtributo2);
            Controls.Add(txtAtributo2);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(lblAtributo1);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(txtPeludo);
            Controls.Add(txtAtributo1);
            Controls.Add(txtNombre);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormModificar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormModificar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtAtributo1;
        private TextBox txtPeludo;
        private Label label1;
        private Label label3;
        private Label lblAtributo1;
        private Label label5;
        private Label label6;
        private Button btnCancelar;
        private Button btnAceptar;
        private TextBox txtAtributo2;
        private Label lblAtributo2;
        private Label lblClase;
    }
}