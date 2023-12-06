namespace WinFormsSegundoParcial
{
    partial class FormModificarRana
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
            label1 = new Label();
            label2 = new Label();
            txtVenenosa = new TextBox();
            txtArboricola = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 250);
            label1.Name = "label1";
            label1.Size = new Size(118, 25);
            label1.TabIndex = 24;
            label1.Text = "Es Venenosa?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 332);
            label2.Name = "label2";
            label2.Size = new Size(120, 25);
            label2.TabIndex = 25;
            label2.Text = "Es Arboricola;";
            // 
            // txtVenenosa
            // 
            txtVenenosa.Location = new Point(27, 288);
            txtVenenosa.Name = "txtVenenosa";
            txtVenenosa.Size = new Size(293, 31);
            txtVenenosa.TabIndex = 26;
            // 
            // txtArboricola
            // 
            txtArboricola.Location = new Point(27, 370);
            txtArboricola.Name = "txtArboricola";
            txtArboricola.Size = new Size(293, 31);
            txtArboricola.TabIndex = 27;
            // 
            // FormModificarRana
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(341, 492);
            Controls.Add(txtArboricola);
            Controls.Add(txtVenenosa);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormModificarRana";
            Text = "FormModificarRana";
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(txtVenenosa, 0);
            Controls.SetChildIndex(txtArboricola, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtVenenosa;
        private TextBox txtArboricola;
    }
}