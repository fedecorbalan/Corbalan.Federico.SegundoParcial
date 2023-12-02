namespace WinFormsSegundoParcial
{
    partial class FormSeleccionAnimal
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
            groupBox1 = new GroupBox();
            rbtnHornero = new RadioButton();
            rbtnOrnitorrinco = new RadioButton();
            rbtnRana = new RadioButton();
            btnCancelar = new Button();
            btnAceptar = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbtnRana);
            groupBox1.Controls.Add(rbtnOrnitorrinco);
            groupBox1.Controls.Add(rbtnHornero);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(12, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(371, 181);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Seleccione un Animal";
            // 
            // rbtnHornero
            // 
            rbtnHornero.AutoSize = true;
            rbtnHornero.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rbtnHornero.Location = new Point(9, 108);
            rbtnHornero.Name = "rbtnHornero";
            rbtnHornero.Size = new Size(108, 29);
            rbtnHornero.TabIndex = 0;
            rbtnHornero.TabStop = true;
            rbtnHornero.Text = "Hornero";
            rbtnHornero.UseVisualStyleBackColor = true;
            // 
            // rbtnOrnitorrinco
            // 
            rbtnOrnitorrinco.AutoSize = true;
            rbtnOrnitorrinco.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rbtnOrnitorrinco.Location = new Point(132, 108);
            rbtnOrnitorrinco.Name = "rbtnOrnitorrinco";
            rbtnOrnitorrinco.Size = new Size(142, 29);
            rbtnOrnitorrinco.TabIndex = 1;
            rbtnOrnitorrinco.TabStop = true;
            rbtnOrnitorrinco.Text = "Ornitorrinco";
            rbtnOrnitorrinco.UseVisualStyleBackColor = true;
            // 
            // rbtnRana
            // 
            rbtnRana.AutoSize = true;
            rbtnRana.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rbtnRana.Location = new Point(281, 108);
            rbtnRana.Name = "rbtnRana";
            rbtnRana.Size = new Size(80, 29);
            rbtnRana.TabIndex = 2;
            rbtnRana.TabStop = true;
            rbtnRana.Text = "Rana";
            rbtnRana.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.PaleTurquoise;
            btnCancelar.Location = new Point(12, 229);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.PaleTurquoise;
            btnAceptar.Location = new Point(271, 229);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(112, 34);
            btnAceptar.TabIndex = 2;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // FormSeleccionAnimal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(400, 280);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormSeleccionAnimal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormSeleccionAnimal";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton rbtnRana;
        private RadioButton rbtnOrnitorrinco;
        private RadioButton rbtnHornero;
        private Button btnCancelar;
        private Button btnAceptar;
    }
}