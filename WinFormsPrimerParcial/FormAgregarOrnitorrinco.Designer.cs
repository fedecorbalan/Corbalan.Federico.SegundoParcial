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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
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
            // textBox1
            // 
            textBox1.Location = new Point(23, 284);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(293, 31);
            textBox1.TabIndex = 13;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(23, 369);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(293, 31);
            textBox2.TabIndex = 14;
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
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Name = "FormAgregarOrnitorrinco";
            Text = "FormAgregarOrnitorrinco";
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(textBox2, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
    }
}