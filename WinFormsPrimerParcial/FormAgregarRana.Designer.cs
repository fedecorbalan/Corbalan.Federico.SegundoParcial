namespace WinFormsSegundoParcial
{
    partial class FormAgregarRana
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
            txtVenenosa = new TextBox();
            label2 = new Label();
            txtArboricola = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtVenenosa
            // 
            txtVenenosa.Location = new Point(23, 288);
            txtVenenosa.Name = "txtVenenosa";
            txtVenenosa.Size = new Size(293, 31);
            txtVenenosa.TabIndex = 15;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 260);
            label2.Name = "label2";
            label2.Size = new Size(118, 25);
            label2.TabIndex = 14;
            label2.Text = "Es Venenosa?";
            // 
            // txtArboricola
            // 
            txtArboricola.Location = new Point(23, 377);
            txtArboricola.Name = "txtArboricola";
            txtArboricola.Size = new Size(293, 31);
            txtArboricola.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 349);
            label3.Name = "label3";
            label3.Size = new Size(124, 25);
            label3.TabIndex = 16;
            label3.Text = "Es Arboricola?";
            // 
            // FormAgregarRana
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 516);
            Controls.Add(txtArboricola);
            Controls.Add(label3);
            Controls.Add(txtVenenosa);
            Controls.Add(label2);
            Name = "FormAgregarRana";
            Text = "FormAgregarRana";
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(txtVenenosa, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(txtArboricola, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtVenenosa;
        private Label label2;
        private TextBox txtArboricola;
        private Label label3;
    }
}