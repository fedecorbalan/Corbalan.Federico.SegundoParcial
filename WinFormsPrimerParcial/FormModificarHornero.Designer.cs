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
            txtVelocidad = new TextBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            rbtnSi = new RadioButton();
            rbtnNo = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtVelocidad
            // 
            txtVelocidad.Location = new Point(27, 389);
            txtVelocidad.Name = "txtVelocidad";
            txtVelocidad.Size = new Size(293, 31);
            txtVelocidad.TabIndex = 22;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 361);
            label2.Name = "label2";
            label2.Size = new Size(164, 25);
            label2.TabIndex = 24;
            label2.Text = "Velocidad en KM/H";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbtnSi);
            groupBox1.Controls.Add(rbtnNo);
            groupBox1.Location = new Point(27, 254);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(293, 95);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tiene Alas?";
            // 
            // rbtnSi
            // 
            rbtnSi.AutoSize = true;
            rbtnSi.Location = new Point(55, 47);
            rbtnSi.Name = "rbtnSi";
            rbtnSi.Size = new Size(51, 29);
            rbtnSi.TabIndex = 16;
            rbtnSi.TabStop = true;
            rbtnSi.Text = "Si";
            rbtnSi.UseVisualStyleBackColor = true;
            // 
            // rbtnNo
            // 
            rbtnNo.AutoSize = true;
            rbtnNo.Location = new Point(171, 47);
            rbtnNo.Name = "rbtnNo";
            rbtnNo.Size = new Size(61, 29);
            rbtnNo.TabIndex = 17;
            rbtnNo.TabStop = true;
            rbtnNo.Text = "No";
            rbtnNo.UseVisualStyleBackColor = true;
            // 
            // FormModificarHornero
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 498);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(txtVelocidad);
            Name = "FormModificarHornero";
            Text = "FormModificarHornero";
            Controls.SetChildIndex(txtVelocidad, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(groupBox1, 0);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtVelocidad;
        private Label label2;
        private GroupBox groupBox1;
        private RadioButton rbtnSi;
        private RadioButton rbtnNo;
    }
}