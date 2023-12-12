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
using WinFormsPrimerParcial;

namespace WinFormsSegundoParcial
{
    /// <summary>
    /// Formulario para la modificación de datos de un animal.
    /// </summary>
    public partial class FormModificar : Form
    {
        /// <summary>
        /// Animal que se va a modificar.
        /// </summary>
        public Animal animalAModificar { get; private set; }
        /// <summary>
        /// Referencia al formulario principal.
        /// </summary>
        public FormPrincipal FormPrincipalRef { get; set; }
        /// <summary>
        /// Constructor de la clase FormModificar.
        /// </summary>
        public FormModificar()
        {
            InitializeComponent();
            setRadioButtons();
        }

        /// <summary>
        /// Constructor sobrecargado de la clase FormModificar.
        /// </summary>
        /// <param name="a">Animal que se va a modificar.</param>
        public FormModificar(Animal a) : this()
        {
            txtNombre.Text = a.nombre;
            if (a.esPeludo)
            {
                rbtnPeludoSi.Checked = true;
            }
            else
            {
                rbtnPeludoNo.Checked = true;
            }
        }
        /// <summary>
        /// Título del formulario.
        /// </summary>
        public string LblTitulo { get { return lblTitulo.Text; } set { lblTitulo.Text = value; } }
        /// <summary>
        /// RadioButton para indicar si el animal es peludo.
        /// </summary>
        public RadioButton RbtnPeludoSi { get {return rbtnPeludoSi; } set { rbtnPeludoSi = value; } }
        /// <summary>
        /// RadioButton para indicar si el animal no es peludo.
        /// </summary>
        public RadioButton RbtnPeludoNo { get { return rbtnPeludoNo; } set { rbtnPeludoNo = value; } }
        /// <summary>
        /// Botón de aceptar.
        /// </summary>
        public Button BtnAceptar
        {
            get { return btnAceptar; }
            set { btnAceptar = value; }
        }
        /// <summary>
        /// Botón de cancelar.
        /// </summary>
        public Button BtnCancelar
        {
            get { return btnCancelar; }
            set { btnCancelar = value; }
        }
        /// <summary>
        /// Nombre del animal.
        /// </summary>
        public string TxtNombre { get { return txtNombre.Text; } set { txtNombre.Text = value; } }

        /// <summary>
        /// Maneja el evento aceptar, que no se usa ya que este es un formulario base.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Verifica si el animal es peludo.
        /// </summary>
        /// <returns>True si el animal es peludo, False si no.</returns>
        public bool VerificarEsPeludo()
        {
            bool esPeludo;

            if (rbtnPeludoSi.Checked)
            {
                esPeludo = true;
            }
            else if(rbtnPeludoNo.Checked)
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
        /// Valida los datos del animal.
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
        /// Maneja el evento load, que no es utilizado
        /// </summary>
        private void FormModificar_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Configura los RadioButtons del formulario.
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
