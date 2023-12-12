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

namespace WinFormsPrimerParcial
{
    /// <summary>
    /// Formulario para agregar animales.
    /// </summary>
    public partial class FormAgregar : Form
    {

        /// <summary>
        /// Propiedades que obtienen o establecen el nuevo animal creado y una referencia al form principal.
        /// </summary>
        public Animal NuevoAnimal { get; private set; }

        public FormPrincipal FormPrincipalRef { get; set; }

        /// <summary>
        /// Constructor por defecto del form
        /// </summary>
        public FormAgregar()
        {
            InitializeComponent();
            setRadioButtons();
        }
        /// <summary>
        /// Propiedades que obtiene o establecen el título de un Label, Un boton aceptar, un boton cancelar y el texto de la Textbox Nombre.
        /// </summary>
        public string LblTitulo { get { return lblTitulo.Text; } set { lblTitulo.Text = value; } }

        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        public Button BtnCancelar
        {
            get { return btnCancelar; }
            set { btnCancelar = value; }
        }
        public string TxtNombre { get { return txtNombre.Text; } set { txtNombre.Text = value; } }

        /// <summary>
        /// Maneja el evento aceptar que en este caso no se usa, ya que este seria un form padre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Obtiene el valor de la opción seleccionada en el grupo "Es peludo".
        /// </summary>
        /// <returns>True si es peludo, false si no lo es.</returns>
        public bool VerificarEsPeludo()
        {
            bool esPeludo;

            if (rbtnPeludoSi.Checked)
            {
                esPeludo = true;
            }
            else if (rbtnPeludoNo.Checked)
            {
                esPeludo = false;
            }
            else
            {
                throw new ExcepcionPeludoVacio();
            }
            return esPeludo;
        }
        /// <summary>
        /// Maneja el evento de hacer clic en el botón Cancelar.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Muestra mensajes de error en caso de excepciones.
        /// </summary>
        /// <param name="errores">Lista de mensajes de error.</param>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void AvisoDeErrores(List<string> errores, List<Exception> excepciones)
        {
            foreach (Exception excepcion in excepciones)
            {
                errores.Add(excepcion.Message);
            }
            if (errores.Count > 0)
            {
                string mensajeError = string.Join("\n", errores);
                MessageBox.Show(mensajeError, "Errores al validar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Valida los datos del animal y agrega excepciones a la lista.
        /// </summary>
        /// <param name="excepciones">Lista de excepciones.</param>
        public void ValidarDatosAnimal(List<Exception> excepciones)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                excepciones.Add(new ExcepcionNombreVacio());
            }
            if (!(rbtnPeludoSi.Checked) && !(rbtnPeludoNo.Checked))
            {
                excepciones.Add(new ExcepcionPeludoVacio());
            }
        }
        /// <summary>
        /// Establece el estado de los botones de opción "Es peludo".
        /// </summary>
        public void setRadioButtons()
        {
            if (rbtnPeludoSi.Checked)
            {
                rbtnPeludoNo.Checked = false;
            }
            else if (rbtnPeludoNo.Checked)
            {
                rbtnPeludoSi.Checked = false;
            }
        }

    }
}
