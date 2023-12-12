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
            groupBox3 = new GroupBox();
            rbtnArboricolaNo = new RadioButton();
            rbtnArboricolaSi = new RadioButton();
            groupBox2 = new GroupBox();
            rbtnVenenosaNo = new RadioButton();
            rbtnVenenosaSi = new RadioButton();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rbtnArboricolaNo);
            groupBox3.Controls.Add(rbtnArboricolaSi);
            groupBox3.Location = new Point(27, 348);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(293, 83);
            groupBox3.TabIndex = 19;
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
            // groupBox2
            // 
            groupBox2.Controls.Add(rbtnVenenosaNo);
            groupBox2.Controls.Add(rbtnVenenosaSi);
            groupBox2.Location = new Point(27, 254);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(293, 76);
            groupBox2.TabIndex = 20;
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
            // FormModificarRana
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(341, 492);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Name = "FormModificarRana";
            Text = "FormModificarRana";
            Controls.SetChildIndex(groupBox2, 0);
            Controls.SetChildIndex(groupBox3, 0);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox3;
        private RadioButton rbtnArboricolaNo;
        private RadioButton rbtnArboricolaSi;
        private GroupBox groupBox2;
        private RadioButton rbtnVenenosaNo;
        private RadioButton rbtnVenenosaSi;
    }
}