namespace WinFormsSegundoParcial
{
    partial class FormAgregarHornero
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
            label3 = new Label();
            label4 = new Label();
            txtVelocidad = new TextBox();
            txtTieneAlas = new TextBox();
            lblId = new Label();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 254);
            label3.Name = "label3";
            label3.Size = new Size(96, 25);
            label3.TabIndex = 13;
            label3.Text = "Tiene alas?";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 337);
            label4.Name = "label4";
            label4.Size = new Size(174, 25);
            label4.TabIndex = 14;
            label4.Text = "Velocidad (En KM/H)";
            // 
            // txtVelocidad
            // 
            txtVelocidad.Location = new Point(23, 365);
            txtVelocidad.Name = "txtVelocidad";
            txtVelocidad.Size = new Size(293, 31);
            txtVelocidad.TabIndex = 15;
            // 
            // txtTieneAlas
            // 
            txtTieneAlas.Location = new Point(23, 282);
            txtTieneAlas.Name = "txtTieneAlas";
            txtTieneAlas.Size = new Size(293, 31);
            txtTieneAlas.TabIndex = 16;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(155, 422);
            lblId.Name = "lblId";
            lblId.Size = new Size(28, 25);
            lblId.TabIndex = 17;
            lblId.Text = "Id";
            // 
            // FormAgregarHornero
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 512);
            Controls.Add(lblId);
            Controls.Add(txtTieneAlas);
            Controls.Add(txtVelocidad);
            Controls.Add(label4);
            Controls.Add(label3);
            Name = "FormAgregarHornero";
            Text = "FormAgregarHornero";
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(txtVelocidad, 0);
            Controls.SetChildIndex(txtTieneAlas, 0);
            Controls.SetChildIndex(lblId, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private Label label4;
        private TextBox txtVelocidad;
        private TextBox txtTieneAlas;
        private Label lblId;
    }
}