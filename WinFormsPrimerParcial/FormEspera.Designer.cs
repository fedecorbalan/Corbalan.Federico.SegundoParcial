namespace WinFormsSegundoParcial
{
    partial class FormEspera
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
            lblProceso = new Label();
            SuspendLayout();
            // 
            // lblProceso
            // 
            lblProceso.AutoSize = true;
            lblProceso.Font = new Font("Arial Rounded MT Bold", 14F, FontStyle.Regular, GraphicsUnit.Point);
            lblProceso.Location = new Point(36, 75);
            lblProceso.Name = "lblProceso";
            lblProceso.Size = new Size(764, 32);
            lblProceso.TabIndex = 0;
            lblProceso.Text = "Aguarde un momento mientras se completa el proceso";
            // 
            // FormEspera
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(846, 189);
            Controls.Add(lblProceso);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormEspera";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aguarde un momento...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProceso;
    }
}