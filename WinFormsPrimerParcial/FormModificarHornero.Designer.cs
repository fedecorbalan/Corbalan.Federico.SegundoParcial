namespace WinFormsSegundoParcial
{
    partial class FormModificarHornero
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
            txtAlas = new TextBox();
            txtVelocidad = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtAlas
            // 
            txtAlas.Location = new Point(27, 279);
            txtAlas.Name = "txtAlas";
            txtAlas.Size = new Size(293, 31);
            txtAlas.TabIndex = 21;
            // 
            // txtVelocidad
            // 
            txtVelocidad.Location = new Point(27, 361);
            txtVelocidad.Name = "txtVelocidad";
            txtVelocidad.Size = new Size(293, 31);
            txtVelocidad.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 242);
            label1.Name = "label1";
            label1.Size = new Size(99, 25);
            label1.TabIndex = 23;
            label1.Text = "Tiene Alas?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 327);
            label2.Name = "label2";
            label2.Size = new Size(164, 25);
            label2.TabIndex = 24;
            label2.Text = "Velocidad en KM/H";
            // 
            // FormModificarHornero
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 498);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtVelocidad);
            Controls.Add(txtAlas);
            Name = "FormModificarHornero";
            Text = "FormModificarHornero";
            Controls.SetChildIndex(txtAlas, 0);
            Controls.SetChildIndex(txtVelocidad, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAlas;
        private TextBox txtVelocidad;
        private Label label1;
        private Label label2;
    }
}