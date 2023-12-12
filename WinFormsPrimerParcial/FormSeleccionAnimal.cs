using Entidades;
using PrimerParcial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WinFormsSegundoParcial
{
    /// <summary>
    /// Formulario para la selección de un tipo de animal.
    /// </summary>
    public partial class FormSeleccionAnimal : Form
    {
        
        /// <summary>
        /// Constructor de la clase FormSeleccionAnimal.
        /// </summary>
        public FormSeleccionAnimal()
        {
            InitializeComponent();
            SeleccionRadioButtons();
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Aceptar.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            SeleccionAnimal();
            this.Close();
        }
        /// <summary>
        /// Abre el formulario correspondiente al tipo de animal seleccionado.
        /// </summary>
        public void SeleccionAnimal()
        {

            if (rbtnHornero.Checked)
            {
                FormAgregarHornero frmHornero = new FormAgregarHornero();
                frmHornero.ShowDialog();
                this.Close();
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                FormAgregarOrnitorrinco frmOrnitorrinco = new FormAgregarOrnitorrinco();
                frmOrnitorrinco.ShowDialog();
                this.Close();
            }
            else if (rbtnRana.Checked)
            {
                FormAgregarRana frmRana = new FormAgregarRana();
                frmRana.ShowDialog();
                this.Close();
            }   
            else
            {
                MessageBox.Show("Seleccione un Animal.");
            }
        }
        /// <summary>
        /// Obtiene el tipo de animal seleccionado.
        /// </summary>
        /// <returns>Tipo de animal.</returns>
        public Type GetSelectedAnimalType()
        {
            if (rbtnHornero.Checked)
            {
                return typeof(Hornero);
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                return typeof(Ornitorrinco);
            }
            else if (rbtnRana.Checked)
            {
                return typeof(Rana);
            }
            else
            {
                throw new ExcepcionAnimalNoSeleccionado();
            }
        }
        /// <summary>
        /// Establece las selecciones de los radio buttons.
        /// </summary>
        public void SeleccionRadioButtons()
        {
            if (rbtnHornero.Checked)
            {
                rbtnOrnitorrinco.Checked = false;
                rbtnRana.Checked = false;
            }
            else if (rbtnOrnitorrinco.Checked)
            {
                rbtnRana.Checked = false;
                rbtnHornero.Checked = false;
            }
            else if (rbtnRana.Checked)
            {
                rbtnOrnitorrinco.Checked = false;
                rbtnHornero.Checked = false;
            }
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
