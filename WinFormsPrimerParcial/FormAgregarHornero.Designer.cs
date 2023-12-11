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
            label4 = new Label();
            txtVelocidad = new TextBox();
            rbtnSi = new RadioButton();
            rbtnNo = new RadioButton();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 366);
            label4.Name = "label4";
            label4.Size = new Size(174, 25);
            label4.TabIndex = 14;
            label4.Text = "Velocidad (En KM/H)";
            // 
            // txtVelocidad
            // 
            txtVelocidad.Location = new Point(23, 406);
            txtVelocidad.Name = "txtVelocidad";
            txtVelocidad.Size = new Size(293, 31);
            txtVelocidad.TabIndex = 15;
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
            // groupBox1
            // 
            groupBox1.Controls.Add(rbtnSi);
            groupBox1.Controls.Add(rbtnNo);
            groupBox1.Location = new Point(23, 248);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(293, 103);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tiene Alas?";
            // 
            // FormAgregarHornero
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 512);
            Controls.Add(groupBox1);
            Controls.Add(txtVelocidad);
            Controls.Add(label4);
            Name = "FormAgregarHornero";
            Text = "FormAgregarHornero";
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(txtVelocidad, 0);
            Controls.SetChildIndex(groupBox1, 0);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private TextBox txtVelocidad;
        private RadioButton rbtnSi;
        private RadioButton rbtnNo;
        private GroupBox groupBox1;
    }
}