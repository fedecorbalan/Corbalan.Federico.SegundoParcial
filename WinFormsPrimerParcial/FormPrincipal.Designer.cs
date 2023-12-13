namespace WinFormsPrimerParcial
{
    partial class FormPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstVisor = new ListBox();
            btnModificar = new Button();
            btnAgregar = new Button();
            btnEliminar = new Button();
            lblInfo = new Label();
            btnSalir = new Button();
            btnOrdenar1 = new Button();
            btnOrdenar2 = new Button();
            lblDateTime = new Label();
            label1 = new Label();
            label2 = new Label();
            btnVisualizador = new Button();
            btnArchivoEntrada = new Button();
            btnArchivoSalida = new Button();
            openFileDialogDeserializar = new OpenFileDialog();
            saveFileDialogSerializar = new SaveFileDialog();
            SuspendLayout();
            // 
            // lstVisor
            // 
            lstVisor.FormattingEnabled = true;
            lstVisor.ItemHeight = 25;
            lstVisor.Location = new Point(22, 37);
            lstVisor.Name = "lstVisor";
            lstVisor.Size = new Size(1063, 404);
            lstVisor.TabIndex = 0;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.PaleTurquoise;
            btnModificar.Location = new Point(217, 460);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(161, 119);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.PaleTurquoise;
            btnAgregar.Location = new Point(22, 460);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(161, 119);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.PaleTurquoise;
            btnEliminar.Location = new Point(441, 460);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(161, 119);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(106, 587);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(59, 25);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "label1";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.PaleTurquoise;
            btnSalir.Location = new Point(925, 585);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(160, 119);
            btnSalir.TabIndex = 5;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnOrdenar1
            // 
            btnOrdenar1.BackColor = Color.PaleTurquoise;
            btnOrdenar1.Location = new Point(669, 460);
            btnOrdenar1.Name = "btnOrdenar1";
            btnOrdenar1.Size = new Size(160, 119);
            btnOrdenar1.TabIndex = 6;
            btnOrdenar1.Text = "Ordenar por Extremidades";
            btnOrdenar1.UseVisualStyleBackColor = false;
            btnOrdenar1.Click += btnOrdenar1_Click;
            // 
            // btnOrdenar2
            // 
            btnOrdenar2.BackColor = Color.PaleTurquoise;
            btnOrdenar2.Location = new Point(925, 460);
            btnOrdenar2.Name = "btnOrdenar2";
            btnOrdenar2.Size = new Size(160, 119);
            btnOrdenar2.TabIndex = 7;
            btnOrdenar2.Text = "Ordenar por Nombre";
            btnOrdenar2.UseVisualStyleBackColor = false;
            btnOrdenar2.Click += btnOrdenar2_Click;
            // 
            // lblDateTime
            // 
            lblDateTime.AutoSize = true;
            lblDateTime.Location = new Point(106, 664);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(59, 25);
            lblDateTime.TabIndex = 8;
            lblDateTime.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 664);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 9;
            label1.Text = "Fecha:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 587);
            label2.Name = "label2";
            label2.Size = new Size(76, 25);
            label2.TabIndex = 10;
            label2.Text = "Usuario:";
            // 
            // btnVisualizador
            // 
            btnVisualizador.BackColor = Color.PaleTurquoise;
            btnVisualizador.Location = new Point(669, 585);
            btnVisualizador.Name = "btnVisualizador";
            btnVisualizador.Size = new Size(160, 119);
            btnVisualizador.TabIndex = 11;
            btnVisualizador.Text = "Visualizador Usuarios";
            btnVisualizador.UseVisualStyleBackColor = false;
            btnVisualizador.Click += btnVisualizador_Click;
            // 
            // btnArchivoEntrada
            // 
            btnArchivoEntrada.BackColor = Color.PaleTurquoise;
            btnArchivoEntrada.Location = new Point(217, 585);
            btnArchivoEntrada.Name = "btnArchivoEntrada";
            btnArchivoEntrada.Size = new Size(160, 119);
            btnArchivoEntrada.TabIndex = 12;
            btnArchivoEntrada.Text = "Elegir archivo a Deserializar";
            btnArchivoEntrada.UseVisualStyleBackColor = false;
            btnArchivoEntrada.Click += btnArchivoEntrada_Click;
            // 
            // btnArchivoSalida
            // 
            btnArchivoSalida.BackColor = Color.PaleTurquoise;
            btnArchivoSalida.Location = new Point(442, 587);
            btnArchivoSalida.Name = "btnArchivoSalida";
            btnArchivoSalida.Size = new Size(160, 119);
            btnArchivoSalida.TabIndex = 13;
            btnArchivoSalida.Text = "Elegir archivo a Serializar";
            btnArchivoSalida.UseVisualStyleBackColor = false;
            btnArchivoSalida.Click += btnArchivoSalida_Click;
            // 
            // openFileDialogDeserializar
            // 
            openFileDialogDeserializar.FileName = "openFileDialog2";
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Pink;
            ClientSize = new Size(1113, 718);
            Controls.Add(btnArchivoSalida);
            Controls.Add(btnArchivoEntrada);
            Controls.Add(btnVisualizador);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblDateTime);
            Controls.Add(btnOrdenar2);
            Controls.Add(btnOrdenar1);
            Controls.Add(btnSalir);
            Controls.Add(lblInfo);
            Controls.Add(btnEliminar);
            Controls.Add(btnAgregar);
            Controls.Add(btnModificar);
            Controls.Add(lstVisor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Listado Users";
            Load += FormPrincipal_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstVisor;
        private Button btnModificar;
        private Button btnAgregar;
        private Button btnEliminar;
        private Label lblInfo;
        private Button btnSalir;
        private Button btnOrdenar1;
        private Button btnOrdenar2;
        private Label lblDateTime;
        private Label label1;
        private Label label2;
        private Button btnVisualizador;
        private Button btnArchivoEntrada;
        private Button btnArchivoSalida;
        private OpenFileDialog openFileDialogDeserializar;
        private SaveFileDialog saveFileDialogSerializar;
    }
}