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
            groupBox2 = new GroupBox();
            rbtnVenenosaNo = new RadioButton();
            rbtnVenenosaSi = new RadioButton();
            groupBox3 = new GroupBox();
            rbtnArboricolaNo = new RadioButton();
            rbtnArboricolaSi = new RadioButton();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rbtnVenenosaNo);
            groupBox2.Controls.Add(rbtnVenenosaSi);
            groupBox2.Location = new Point(23, 259);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(293, 81);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Es Venenosa";
            // 
            // rbtnVenenosaNo
            // 
            rbtnVenenosaNo.AutoSize = true;
            rbtnVenenosaNo.Location = new Point(168, 30);
            rbtnVenenosaNo.Name = "rbtnVenenosaNo";
            rbtnVenenosaNo.Size = new Size(61, 29);
            rbtnVenenosaNo.TabIndex = 1;
            rbtnVenenosaNo.TabStop = true;
            rbtnVenenosaNo.Text = "No";
            rbtnVenenosaNo.UseVisualStyleBackColor = true;
            // 
            // rbtnVenenosaSi
            // 
            rbtnVenenosaSi.AutoSize = true;
            rbtnVenenosaSi.Location = new Point(68, 30);
            rbtnVenenosaSi.Name = "rbtnVenenosaSi";
            rbtnVenenosaSi.Size = new Size(51, 29);
            rbtnVenenosaSi.TabIndex = 0;
            rbtnVenenosaSi.TabStop = true;
            rbtnVenenosaSi.Text = "Si";
            rbtnVenenosaSi.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rbtnArboricolaNo);
            groupBox3.Controls.Add(rbtnArboricolaSi);
            groupBox3.Location = new Point(23, 353);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(293, 88);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Es Arboricola?";
            // 
            // rbtnArboricolaNo
            // 
            rbtnArboricolaNo.AutoSize = true;
            rbtnArboricolaNo.Location = new Point(168, 42);
            rbtnArboricolaNo.Name = "rbtnArboricolaNo";
            rbtnArboricolaNo.Size = new Size(61, 29);
            rbtnArboricolaNo.TabIndex = 1;
            rbtnArboricolaNo.TabStop = true;
            rbtnArboricolaNo.Text = "No";
            rbtnArboricolaNo.UseVisualStyleBackColor = true;
            // 
            // rbtnArboricolaSi
            // 
            rbtnArboricolaSi.AutoSize = true;
            rbtnArboricolaSi.Location = new Point(68, 42);
            rbtnArboricolaSi.Name = "rbtnArboricolaSi";
            rbtnArboricolaSi.Size = new Size(51, 29);
            rbtnArboricolaSi.TabIndex = 0;
            rbtnArboricolaSi.TabStop = true;
            rbtnArboricolaSi.Text = "Si";
            rbtnArboricolaSi.UseVisualStyleBackColor = true;
            // 
            // FormAgregarRana
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 516);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Name = "FormAgregarRana";
            Text = "FormAgregarRana";
            Controls.SetChildIndex(groupBox2, 0);
            Controls.SetChildIndex(groupBox3, 0);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private RadioButton rbtnVenenosaNo;
        private RadioButton rbtnVenenosaSi;
        private GroupBox groupBox3;
        private RadioButton rbtnArboricolaNo;
        private RadioButton rbtnArboricolaSi;
    }
}