namespace WinFormsPrimerParcial
{
    partial class FormVisualizador
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
            btnSalir = new Button();
            rtxtUsuarios = new RichTextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.PaleTurquoise;
            btnSalir.Location = new Point(300, 590);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(218, 52);
            btnSalir.TabIndex = 6;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // rtxtUsuarios
            // 
            rtxtUsuarios.Location = new Point(36, 75);
            rtxtUsuarios.Name = "rtxtUsuarios";
            rtxtUsuarios.Size = new Size(723, 498);
            rtxtUsuarios.TabIndex = 7;
            rtxtUsuarios.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label1.Location = new Point(331, 9);
            label1.Name = "label1";
            label1.Size = new Size(149, 45);
            label1.TabIndex = 8;
            label1.Text = "Usuarios";
            // 
            // FormVisualizador
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(800, 654);
            Controls.Add(label1);
            Controls.Add(rtxtUsuarios);
            Controls.Add(btnSalir);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormVisualizador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormVisualizador";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private RichTextBox rtxtUsuarios;
        private Label label1;
    }
}