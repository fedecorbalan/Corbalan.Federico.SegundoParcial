using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPrimerParcial
{
    /// <summary>
    /// Formulario para visualizar un contenido de registro.
    /// </summary>
    public partial class FormVisualizador : Form
    {
        /// <summary>
        /// Constructor de la clase FormVisualizador.
        /// </summary>
        /// <param name="logContent">Contenido del registro a visualizar.</param>
        public FormVisualizador(string logContent)
        {
            InitializeComponent();
            rtxtUsuarios.Text = logContent;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Salir.
        /// </summary>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
