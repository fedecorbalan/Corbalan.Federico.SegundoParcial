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
            groupBox3 = new GroupBox();
            rbtnColaNo = new RadioButton();
            rbtnColaSi = new RadioButton();
            groupBox2 = new GroupBox();
            rbtnOviparoNo = new RadioButton();
            rbtnOviparoSi = new RadioButton();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rbtnColaNo);
            groupBox3.Controls.Add(rbtnColaSi);
            groupBox3.Location = new Point(27, 367);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(293, 66);
            groupBox3.TabIndex = 19;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tiene Cola?";
            // 
            // rbtnColaNo
            // 
            rbtnColaNo.AutoSize = true;
            rbtnColaNo.Location = new Point(168, 30);
            rbtnColaNo.Name = "rbtnColaNo";
            rbtnColaNo.Size = new Size(61, 29);
            rbtnColaNo.TabIndex = 1;
            rbtnColaNo.TabStop = true;
            rbtnColaNo.Text = "No";
            rbtnColaNo.UseVisualStyleBackColor = true;
            // 
            // rbtnColaSi
            // 
            rbtnColaSi.AutoSize = true;
            rbtnColaSi.Location = new Point(68, 30);
            rbtnColaSi.Name = "rbtnColaSi";
            rbtnColaSi.Size = new Size(51, 29);
            rbtnColaSi.TabIndex = 0;
            rbtnColaSi.TabStop = true;
            rbtnColaSi.Text = "Si";
            rbtnColaSi.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rbtnOviparoNo);
            groupBox2.Controls.Add(rbtnOviparoSi);
            groupBox2.Location = new Point(27, 269);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(293, 81);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Es Oviparo?";
            // 
            // rbtnOviparoNo
            // 
            rbtnOviparoNo.AutoSize = true;
            rbtnOviparoNo.Location = new Point(168, 30);
            rbtnOviparoNo.Name = "rbtnOviparoNo";
            rbtnOviparoNo.Size = new Size(61, 29);
            rbtnOviparoNo.TabIndex = 1;
            rbtnOviparoNo.TabStop = true;
            rbtnOviparoNo.Text = "No";
            rbtnOviparoNo.UseVisualStyleBackColor = true;
            // 
            // rbtnOviparoSi
            // 
            rbtnOviparoSi.AutoSize = true;
            rbtnOviparoSi.Location = new Point(68, 30);
            rbtnOviparoSi.Name = "rbtnOviparoSi";
            rbtnOviparoSi.Size = new Size(51, 29);
            rbtnOviparoSi.TabIndex = 0;
            rbtnOviparoSi.TabStop = true;
            rbtnOviparoSi.Text = "Si";
            rbtnOviparoSi.UseVisualStyleBackColor = true;
            // 
            // FormModificarOrnitorrinco
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(345, 488);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Name = "FormModificarOrnitorrinco";
            Text = "FormModificarOrnitorrinco";
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
        private RadioButton rbtnColaNo;
        private RadioButton rbtnColaSi;
        private GroupBox groupBox2;
        private RadioButton rbtnOviparoNo;
        private RadioButton rbtnOviparoSi;
    }
}