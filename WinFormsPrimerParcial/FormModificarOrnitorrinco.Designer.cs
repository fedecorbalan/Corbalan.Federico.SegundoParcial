namespace WinFormsSegundoParcial
{
    partial class FormModificarOrnitorrinco
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
            txtTieneCola = new TextBox();
            label2 = new Label();
            txtOviparo = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 250);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 19;
            label1.Text = "Tiene Cola?";
            // 
            // txtTieneCola
            // 
            txtTieneCola.Location = new Point(27, 278);
            txtTieneCola.Name = "txtTieneCola";
            txtTieneCola.Size = new Size(293, 31);
            txtTieneCola.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 333);
            label2.Name = "label2";
            label2.Size = new Size(106, 25);
            label2.TabIndex = 21;
            label2.Text = "Es Oviparo?";
            // 
            // txtOviparo
            // 
            txtOviparo.Location = new Point(27, 361);
            txtOviparo.Name = "txtOviparo";
            txtOviparo.Size = new Size(293, 31);
            txtOviparo.TabIndex = 22;
            // 
            // FormModificarOrnitorrinco
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(345, 488);
            Controls.Add(txtOviparo);
            Controls.Add(label2);
            Controls.Add(txtTieneCola);
            Controls.Add(label1);
            Name = "FormModificarOrnitorrinco";
            Text = "FormModificarOrnitorrinco";
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(txtTieneCola, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(txtOviparo, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTieneCola;
        private Label label2;
        private TextBox txtOviparo;
    }
}