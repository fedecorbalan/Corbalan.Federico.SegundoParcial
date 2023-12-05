namespace WinFormsSegundoParcial
{
    partial class FormAgregarOrnitorrinco
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
            label2 = new Label();
            txtTieneCola = new TextBox();
            txtOviparo = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 256);
            label2.Name = "label2";
            label2.Size = new Size(101, 25);
            label2.TabIndex = 12;
            label2.Text = "Tiene Cola?";
            // 
            // txtTieneCola
            // 
            txtTieneCola.Location = new Point(23, 284);
            txtTieneCola.Name = "txtTieneCola";
            txtTieneCola.Size = new Size(293, 31);
            txtTieneCola.TabIndex = 13;
            // 
            // txtOviparo
            // 
            txtOviparo.Location = new Point(23, 369);
            txtOviparo.Name = "txtOviparo";
            txtOviparo.Size = new Size(293, 31);
            txtOviparo.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 341);
            label3.Name = "label3";
            label3.Size = new Size(106, 25);
            label3.TabIndex = 15;
            label3.Text = "Es Oviparo?";
            // 
            // FormAgregarOrnitorrinco
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 524);
            Controls.Add(label3);
            Controls.Add(txtOviparo);
            Controls.Add(txtTieneCola);
            Controls.Add(label2);
            Name = "FormAgregarOrnitorrinco";
            Text = "FormAgregarOrnitorrinco";
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(txtTieneCola, 0);
            Controls.SetChildIndex(txtOviparo, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox txtTieneCola;
        private TextBox txtOviparo;
        private Label label3;
    }
}